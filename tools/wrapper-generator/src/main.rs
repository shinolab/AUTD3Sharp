use std::env;
use std::path::Path;

use anyhow::Result;
use glob::glob;

use convert_case::{Case, Casing};

fn generate<P1: AsRef<Path>, P2: AsRef<Path>>(crate_path: P1, path: P2) -> Result<()> {
    let sub_abbr = |str: String| -> String { str.replace("Twincat", "TwinCAT") };

    let to_pascal = |name: &str| -> String {
        let res = name.to_case(Case::Pascal);
        sub_abbr(res)
    };

    let to_class_name = |name: &str| {
        if name == "autd3capi" {
            return "Base".to_string();
        }
        to_pascal(&name.replace("autd3capi-", ""))
    };

    let crate_name = crate_path.as_ref().file_name().unwrap().to_str().unwrap();
    let out_file = Path::new(path.as_ref()).join(format!("{}.cs", to_class_name(crate_name)));
    let dll_name = crate_name.replace('-', "_");
    let class_name = to_class_name(crate_name);

    let temp_dir = tempfile::tempdir()?;
    glob::glob(&format!(
        "{}/**/*.rs",
        crate_path.as_ref().join("src").display()
    ))?
    .try_fold(csbindgen::Builder::default(), |acc, path| -> Result<_> {
        use std::io::Write;

        let orig_path = path?.canonicalize()?;
        let orig_file = std::fs::read_to_string(&orig_path)?;
        let orig_file = orig_file.replace("#[unsafe(no_mangle)]", "#[no_mangle]");
        let tmp_path = temp_dir.path().join(
            orig_path
                .display()
                .to_string()
                .replace(
                    &crate_path
                        .as_ref()
                        .join("src")
                        .canonicalize()?
                        .display()
                        .to_string(),
                    "",
                )
                .replace("\\", "_"),
        );
        let mut file = std::fs::File::create(&tmp_path)?;
        file.write_all(orig_file.as_bytes())?;
        Ok(acc.input_extern_file(tmp_path))
    })?
    .always_included_types([
        "ParallelMode",
        "SleeperWrap",
        "Drive",
        "Segment",
        "LoopBehavior",
        "SamplingConfig",
        "SyncMode",
        "ProcessPriority",
        "DatagramPtr",
        "GainPtr",
        "ResultModulation",
        "ResultGain",
        "ResultSamplingConfig",
        "SpinStrategyTag",
        "ResultStatus",
        "ResultLink",
        "ControllerPtr",
        "TransitionModeWrap",
        "TransducerPtr",
        "DevicePtr",
        "GeometryPtr",
        "LinkPtr",
        "GPIOOutputTypeWrap",
        "FociSTMPtr",
        "GainSTMPtr",
        "GPIOIn",
        "GPIOOut",
        "GainSTMMode",
        "FocusOption",
        "BesselOption",
        "PlaneOption",
        "Angle",
        "SenderPtr",
        "FixedCompletionSteps",
        "ResultU16",
        "ResultF32",
        "ResultDuration",
    ])
    .csharp_dll_name(dll_name)
    .csharp_class_name(format!("NativeMethods{}", class_name))
    .csharp_namespace("AUTD3Sharp.NativeMethods")
    .csharp_import_namespace("AUTD3Sharp.Utils")
    .csharp_import_namespace("AUTD3Sharp.Link")
    .csharp_generate_const_filter(|x| x != "METER")
    .csharp_class_accessibility("internal")
    .method_filter(|x| x.starts_with("AUTD"))
    .generate_csharp_file(&out_file)
    .map_err(|_| anyhow::anyhow!("failed to generate cs wrapper"))?;

    let content = std::fs::read_to_string(&out_file)?;
    let content = content.replace("void*", "IntPtr");
    let content = content.replace("void @null", "byte @null");
    let content = content.replace(
        "internal unsafe partial struct ConstPtr",
        "public unsafe partial struct ConstPtr",
    );
    let content = content.replace(
        "internal unsafe partial struct ResultLink",
        "public unsafe partial struct ResultLink",
    );
    let content = content.replace(
        "internal unsafe partial struct LinkPtr",
        "public unsafe partial struct LinkPtr",
    );
    let content = content.replace(
        "internal unsafe partial struct ResultStatus",
        "public unsafe partial struct ResultStatus",
    );
    let content = content.replace(
        "internal unsafe partial struct Duration",
        "internal unsafe partial struct Duration_",
    );

    let content = content.replace("internal enum AUTDStatus", "public enum AUTDStatus");

    // Following substitutions are required to avoid alignment issues
    let content = content.replace("public DcSysTime sys_time;", "public ulong sys_time;");
    let content = content.replace("public GPIOIn gpio_in;", "public byte gpio_in;");
    let content = content.replace("public GPIOOutputTypeValue value;", "public ulong value;");

    std::fs::write(&out_file, content)?;

    Ok(())
}

fn main() -> Result<()> {
    let home = env::var("CARGO_MANIFEST_DIR")?;
    {
        let crate_path = format!("{}/autd3/autd3-core", home);
        generate(&crate_path, "../../src/NativeMethods")?;
        generate(&crate_path, "../../unity/Assets/Scripts/NativeMethods")?;
    }
    {
        let crate_path = format!("{}/autd3/autd3", home);
        generate(&crate_path, "../../src/NativeMethods")?;
        generate(&crate_path, "../../unity/Assets/Scripts/NativeMethods")?;
    }
    for entry in glob(&format!("{}/capi/*/Cargo.toml", home))? {
        let entry = entry?;
        let crate_path = Path::new(&entry).parent().unwrap();
        generate(&crate_path, "../../src/NativeMethods")?;
        generate(&crate_path, "../../unity/Assets/Scripts/NativeMethods")?;
    }
    Ok(())
}
