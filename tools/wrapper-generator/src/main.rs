use std::env;
use std::path::Path;

use anyhow::Result;
use glob::glob;

use convert_case::{Case, Casing};

fn generate<P1: AsRef<Path>, P2: AsRef<Path>>(
    crate_path: P1,
    path: P2,
    excludes: Option<Vec<String>>,
) -> Result<()> {
    let sub_abbr = |str: String| -> String { str.replace("Twincat", "TwinCAT") };

    let to_pascal = |name: &str| -> String {
        let res = name.to_case(Case::Pascal);
        sub_abbr(res)
    };

    let to_class_name = |name: &str| {
        if name.split('-').count() == 1 {
            return "Base".to_string();
        }
        to_pascal(&name.replace("autd3capi-", ""))
    };

    let crate_name = crate_path.as_ref().file_name().unwrap().to_str().unwrap();
    let out_file = Path::new(path.as_ref()).join(format!("{}.cs", to_class_name(crate_name)));
    let dll_name = crate_name.replace('-', "_");
    let class_name = to_class_name(crate_name);

    let builder = glob::glob(&format!(
        "{}/**/*.rs",
        crate_path.as_ref().join("src").display()
    ))?
    .try_fold(csbindgen::Builder::default(), |acc, path| -> Result<_> {
        let path = path?;
        Ok(acc.input_extern_file(path))
    })?
    .always_included_types([
        "Drive",
        "Segment",
        "LoopBehavior",
        "SamplingConfig",
        "SyncMode",
        "ProcessPriority",
        "TimerStrategy",
        "DatagramPtr",
        "GainPtr",
        "DynWindow",
        "DynSincInterpolator",
        "ResultModulation",
        "ResultGain",
        "ResultSamplingConfig",
        "TimerStrategyTag",
        "SpinStrategyTag",
        "TimerStrategyWrap",
        "ResultStatus",
        "ResultSyncLinkBuilder",
        "ResultLinkBuilder",
        "ControllerPtr",
        "RuntimePtr",
        "HandlePtr",
        "TransitionModeWrap",
        "TransducerPtr",
        "DevicePtr",
        "GeometryPtr",
        "LinkPtr",
        "DebugTypeWrap",
        "FociSTMPtr",
        "GainSTMPtr",
        "GPIOIn",
        "GPIOOut",
        "SilencerTarget",
        "GainSTMMode",
        "ResultFociSTM",
        "ResultGainSTM",
        "ControllerBuilderPtr",
    ])
    .csharp_dll_name(dll_name)
    .csharp_class_name(format!("NativeMethods{}", class_name))
    .csharp_namespace("AUTD3Sharp.NativeMethods")
    .csharp_import_namespace("AUTD3Sharp.Utils")
    .csharp_import_namespace("AUTD3Sharp.Link")
    .csharp_generate_const_filter(|_| true)
    .csharp_class_accessibility("public");

    if excludes.is_some() {
        // currently, clousure is not supported by `method_filter`
        builder.method_filter(|x| {
            const EXCLUDES: &[&str] = &[
                "AUTDDatagramSilencerFromCompletionTime",
                "AUTDDatagramSilencerFixedCompletionTimeIsValid",
                "AUTDSTMConfigFromPeriod",
                "AUTDSTMConfigFromPeriodNearest",
                "AUTDSTMPeriod",
                "AUTDSamplingConfigFromPeriod",
                "AUTDSamplingConfigFromPeriodNearest",
                "AUTDSamplingConfigPeriod",
            ];
            if EXCLUDES.contains(&x.as_str()) {
                return false;
            }
            x.starts_with("AUTD")
        })
    } else {
        builder.method_filter(|x| x.starts_with("AUTD"))
    }
    .generate_csharp_file(&out_file)
    .map_err(|_| anyhow::anyhow!("failed to generate cs wrapper"))?;

    let content = std::fs::read_to_string(&out_file)?;
    let content = content.replace("void*", "IntPtr");
    let content = content.replace("void @null", "byte @null");
    let content = content.replace("public DcSysTime sys_time;", "public ulong sys_time;");
    let content = content.replace("public GPIOIn gpio_in;", "public byte gpio_in;");
    let content = content.replace("public DebugTypeValue value;", "public ulong value;");

    std::fs::write(&out_file, content)?;

    Ok(())
}

fn extract_not_dyn_freq_fn(crate_path: &Path) -> Result<Vec<String>> {
    let mut result = vec![];
    let re = regex::Regex::new(r#"pub unsafe extern "C" fn AUTD(.*)\("#)?;
    for rs_entry in glob(&format!("{}/**/*.rs", crate_path.to_str().unwrap()))? {
        let rs_entry = rs_entry?;
        let content = std::fs::read_to_string(&rs_entry)?;
        let mut dyn_freq_fn = false;
        for line in content.lines() {
            if line.is_empty() {
                dyn_freq_fn = false;
                continue;
            }
            if let Some(m) = re.captures(&line) {
                if dyn_freq_fn {
                    result.push(format!("AUTD{}", m[1].to_string()));
                }
                dyn_freq_fn = false;
            }
            if line.contains("#[cfg(not(feature = \"dynamic_freq\"))]") {
                dyn_freq_fn = true;
            }
        }
    }
    Ok(result)
}

fn main() -> Result<()> {
    let home = env::var("CARGO_MANIFEST_DIR")?;
    for entry in glob(&format!("{}/capi/*/Cargo.toml", home))? {
        let entry = entry?;
        let crate_path = Path::new(&entry).parent().unwrap();
        let not_dyn_freq_fn = extract_not_dyn_freq_fn(&crate_path)?;
        generate(&crate_path, "../../src/NativeMethods", None)?;
        generate(
            &crate_path,
            "../../unity/Assets/Scripts/NativeMethods",
            None,
        )?;
        generate(
            &crate_path,
            "../../unity-dynamic_freq/Assets/Scripts/NativeMethods",
            Some(not_dyn_freq_fn),
        )?;
    }
    Ok(())
}
