using System.Text;
using Microsoft.CodeAnalysis;

namespace AUTD3Sharp.Derive;

[Generator(LanguageNames.CSharp)]
public partial class ModulationDeriveGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            "AUTD3Sharp.Derive.ModulationAttribute",
            static (node, token) => true,
            static (context, token) => context);

        context.RegisterSourceOutput(source, Emit);
    }
    static void Emit(SourceProductionContext context, GeneratorAttributeSyntaxContext source)
    {
        var typeSymbol = (INamedTypeSymbol)source.TargetSymbol;
        var typeName = typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

        var attribute = source.Attributes.First(attr => attr!.AttributeClass!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ==
                                                        "global::AUTD3Sharp.Derive.ModulationAttribute");
        var namedArguments = attribute.NamedArguments;
        var noCache = namedArguments.Any(arg => arg.Key == "NoCache" && (bool)(arg.Value.Value ?? false));
        var noRadiationPressure = namedArguments.Any(arg => arg.Key == "NoRadiationPressure" && (bool)(arg.Value.Value ?? false));
        var noTransform = namedArguments.Any(arg => arg.Key == "NoTransform" && (bool)(arg.Value.Value ?? false));
        var configNoChange = namedArguments.Any(arg => arg.Key == "ConfigNoChange" && (bool)(arg.Value.Value ?? false));

        var isCustom = typeSymbol.GetMembers("Calc").Length != 0;

        var ns = typeSymbol.ContainingNamespace.IsGlobalNamespace ? "" : $"namespace {typeSymbol.ContainingNamespace}";

        var customCode = isCustom ?
            $$"""

        private ModulationPtr ModulationPtr(Geometry geometry)
        {
            var data = Calc();
            unsafe
            {
                fixed (EmitIntensity* ptr = &data[0])
                    return NativeMethodsBase.AUTDModulationRaw(_config, LoopBehavior, (byte*)ptr, (uint)data.Length);
            }
        }

""" : "";

        var cacheCode = noCache ? "" :
            $"        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Cache<{typeName}> WithCache() => new AUTD3Sharp.Driver.Datagram.Modulation.Cache<{typeName}>(this);";
        var radiationPressureCode = noRadiationPressure ? "" :
            $"        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<{typeName}> WithRadiationPressure() => new AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<{typeName}>(this);";

        var transformCode = noTransform ? "" :
            $"        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Transform<{typeName}> WithTransform(Func<int, EmitIntensity, EmitIntensity> f) => new AUTD3Sharp.Driver.Datagram.Modulation.Transform<{typeName}>(this, f);";

        var configCode = configNoChange ? "" :
            $$"""
              
        public {{typeName}} WithSamplingConfig(SamplingConfigWrap config)
        {
            _config = config;
            return this;
        }
""";

        var fullType = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            .Replace("global::", "")
            .Replace("<", "_")
            .Replace(">", "_");
        var nsBegin = ns == "" ? "" : $"{ns} {{";
        var nsEnd = ns == "" ? "" : "}";
        var code = $$"""
// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Datagram.Modulation;
using AUTD3Sharp.NativeMethods;

{{nsBegin}}
    partial class {{typeName}} : AUTD3Sharp.Driver.Datagram.Modulation.IModulation, IDatagramST<ModulationPtr>, IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr(geometry));
        [ExcludeFromCodeCoverage] DatagramPtr IDatagramST<ModulationPtr>.IntoSegmentTransition(ModulationPtr p, Segment segment, TransitionModeWrap? transitionMode) 
        => transitionMode.HasValue ? NativeMethodsBase.AUTDModulationIntoDatagramWithSegmentTransition(p, segment, transitionMode.Value) : NativeMethodsBase.AUTDModulationIntoDatagramWithSegment(p, segment);
        [ExcludeFromCodeCoverage] ModulationPtr IDatagramST<ModulationPtr>.RawPtr(Geometry geometry) => ModulationPtr(geometry);
        [ExcludeFromCodeCoverage] ModulationPtr AUTD3Sharp.Driver.Datagram.Modulation.IModulation.ModulationPtr(Geometry geometry) => ModulationPtr(geometry);
        [ExcludeFromCodeCoverage] public DatagramWithSegmentTransition<{{typeName}}, ModulationPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode) => new DatagramWithSegmentTransition<{{typeName}}, ModulationPtr>(this, segment, transitionMode);
        
        [ExcludeFromCodeCoverage] AUTD3Sharp.NativeMethods.LoopBehavior IModulation.LoopBehavior() => LoopBehavior;
        [ExcludeFromCodeCoverage] SamplingConfigWrap IModulation.SamplingConfig() => _config;

        private SamplingConfigWrap _config = SamplingConfig.Division(5120);

        public AUTD3Sharp.NativeMethods.LoopBehavior LoopBehavior { get; private set; } = AUTD3Sharp.LoopBehavior.Infinite;

        [ExcludeFromCodeCoverage] public {{typeName}} WithLoopBehavior(AUTD3Sharp.NativeMethods.LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }

{{customCode}}
{{cacheCode}}
{{configCode}}
{{radiationPressureCode}}
{{transformCode}}
    }
{{nsEnd}}
""";
        context.AddSource($"{fullType}.ModulationDerive.g.cs", code);
    }
}