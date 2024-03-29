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

        private ModulationPtr ModulationPtr()
        {
            var data = Calc();
            unsafe
            {
                fixed (EmitIntensity* ptr = &data[0])
                    return NativeMethodsBase.AUTDModulationCustom(_config.Internal, (byte*)ptr, (ulong)data.Length, LoopBehavior.Internal);
            }
        }

""" : "";

        var cacheCode = noCache ? "" :
            $$"""
              
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Cache<{{typeName}}> WithCache()
        {
            return new AUTD3Sharp.Driver.Datagram.Modulation.Cache<{{typeName}}>(this);
        }

""";
        var radiationPressureCode = noRadiationPressure ? "" :
            $$"""
              
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<{{typeName}}> WithRadiationPressure()
        {
            return new AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<{{typeName}}>(this);
        }

""";

        var transformCode = noTransform ? "" :
            $$"""
              
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Transform<{{typeName}}> WithTransform(Func<int, EmitIntensity, EmitIntensity> f)
        {
            return new AUTD3Sharp.Driver.Datagram.Modulation.Transform<{{typeName}}>(this, f);
        }

""";

        var configCode = configNoChange ? "" :
            $$"""
              
        public {{typeName}} WithSamplingConfig(SamplingConfiguration config)
        {
            _config = config;
            return this;
        }

""";

        var fullType = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            .Replace("global::", "")
            .Replace("<", "_")
            .Replace(">", "_");
        var code = $$"""
// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

{{ns}} {
    partial class {{typeName}} : AUTD3Sharp.Driver.Datagram.Modulation.IModulation, IDatagramS<ModulationPtr>, IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr());
        [ExcludeFromCodeCoverage] DatagramPtr IDatagramS<ModulationPtr>.IntoSegment(ModulationPtr p, Segment segment, bool updateSegment) => NativeMethodsBase.AUTDModulationIntoDatagramWithSegment(p, segment, updateSegment);
        [ExcludeFromCodeCoverage] ModulationPtr IDatagramS<ModulationPtr>.RawPtr(Geometry _) => ModulationPtr();
        [ExcludeFromCodeCoverage] ModulationPtr AUTD3Sharp.Driver.Datagram.Modulation.IModulation.ModulationPtr() => ModulationPtr();
        [ExcludeFromCodeCoverage] public DatagramWithSegment<{{typeName}}, ModulationPtr> WithSegment(Segment segment, bool updateSegment)
        {
            return new DatagramWithSegment<{{typeName}}, ModulationPtr>(this, segment, updateSegment);
        }

        private SamplingConfiguration _config = SamplingConfiguration.FromFrequency(4000);

        public SamplingConfiguration SamplingConfiguration => new SamplingConfiguration(NativeMethodsBase.AUTDModulationSamplingConfig(ModulationPtr()));

        public LoopBehavior LoopBehavior { get; private set; } = LoopBehavior.Infinite;

        public int Length => NativeMethodsBase.AUTDModulationSize(ModulationPtr()).Validate();

        [ExcludeFromCodeCoverage] SamplingConfiguration AUTD3Sharp.Driver.Datagram.Modulation.IModulation.InternalSamplingConfiguration() => _config;
        [ExcludeFromCodeCoverage] LoopBehavior AUTD3Sharp.Driver.Datagram.Modulation.IModulation.InternalLoopBehavior() => LoopBehavior; 

        /// <summary>
        /// Set loop behavior
        /// </summary>
        /// <param name="loopBehavior">loop behavior</param>
        /// <returns></returns>
        public {{typeName}} WithLoopBehavior(LoopBehavior loopBehavior)
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
}
""";
        context.AddSource($"{fullType}.ModulationDerive.g.cs", code);
    }
}