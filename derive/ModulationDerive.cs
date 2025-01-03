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
        var configNoChange = namedArguments.Any(arg => arg.Key == "ConfigNoChange" && (bool)(arg.Value.Value ?? false));

        var ns = typeSymbol.ContainingNamespace.IsGlobalNamespace ? "" : $"namespace {typeSymbol.ContainingNamespace}";

        var cacheCode = $"        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Cache<{typeName}> WithCache() => new AUTD3Sharp.Driver.Datagram.Modulation.Cache<{typeName}>(this);";
        var radiationPressureCode = $"        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<{typeName}> WithRadiationPressure() => new AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<{typeName}>(this);";

        var firCode = $"        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Fir<{typeName}> WithFir(IEnumerable<float> iter) => new AUTD3Sharp.Driver.Datagram.Modulation.Fir<{typeName}>(this, iter);";

        var configCode = configNoChange ? "" :
            $$"""
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public {{typeName}} WithSamplingConfig(AUTD3Sharp.SamplingConfig config)
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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AUTD3Sharp;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Datagram.Modulation;
using AUTD3Sharp.NativeMethods;

{{nsBegin}}
    partial class {{typeName}} : AUTD3Sharp.Driver.Datagram.Modulation.IModulation, IDatagramS<ModulationPtr>, IDatagram
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr());
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage] 
        DatagramPtr IDatagramS<ModulationPtr>.IntoSegmentTransition(ModulationPtr p, Segment segment, TransitionModeWrap? transitionMode) 
        => NativeMethodsBase.AUTDModulationIntoDatagramWithSegment(p, segment, transitionMode ?? TransitionMode.None);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        ModulationPtr IDatagramS<ModulationPtr>.RawPtr(Geometry geometry) => ModulationPtr();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        ModulationPtr AUTD3Sharp.Driver.Datagram.Modulation.IModulation.ModulationPtr() => ModulationPtr();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public DatagramWithSegment<{{typeName}}, ModulationPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode) => new DatagramWithSegment<{{typeName}}, ModulationPtr>(this, segment, transitionMode);
        
        [ExcludeFromCodeCoverage]
        public AUTD3Sharp.SamplingConfig SamplingConfig => new(NativeMethodsBase.AUTDModulationSamplingConfig(ModulationPtr()));

        private AUTD3Sharp.SamplingConfig _config = new(10);

        public AUTD3Sharp.LoopBehavior LoopBehavior => _loopBehavior;

        private AUTD3Sharp.LoopBehavior _loopBehavior = AUTD3Sharp.LoopBehavior.Infinite;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public {{typeName}} WithLoopBehavior(AUTD3Sharp.LoopBehavior loopBehavior)
        {
            _loopBehavior = loopBehavior;
            return this;
        }

{{cacheCode}}
{{configCode}}
{{radiationPressureCode}}
{{firCode}}
    }
{{nsEnd}}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif

""";
        context.AddSource($"{fullType}.ModulationDerive.g.cs", code);
    }
}
