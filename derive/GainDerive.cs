using System.Text;
using Microsoft.CodeAnalysis;

namespace AUTD3Sharp.Derive;

[Generator(LanguageNames.CSharp)]
public partial class GainDeriveGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            "AUTD3Sharp.Derive.GainAttribute",
            static (node, token) => true,
            static (context, token) => context);

        context.RegisterSourceOutput(source, Emit);
    }
    static void Emit(SourceProductionContext context, GeneratorAttributeSyntaxContext source)
    {
        var typeSymbol = (INamedTypeSymbol)source.TargetSymbol;
        var typeName = typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

        var attribute = source.Attributes.First(attr => attr!.AttributeClass!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ==
                                                        "global::AUTD3Sharp.Derive.GainAttribute");
        var namedArguments = attribute.NamedArguments;
        var noCache = namedArguments.Any(arg => arg.Key == "NoCache" && (bool)(arg.Value.Value ?? false));
        var noTransform = namedArguments.Any(arg => arg.Key == "NoTransform" && (bool)(arg.Value.Value ?? false));

        var isCustom = typeSymbol.GetMembers("Calc").Length != 0;

        var ns = typeSymbol.ContainingNamespace.IsGlobalNamespace ? "" : $"namespace {typeSymbol.ContainingNamespace}";

        var customCode = isCustom ?
            $$"""
              
        private GainPtr GainPtr(Geometry geometry)
        {
            return Calc(geometry).Aggregate(NativeMethodsBase.AUTDGainCustom(), (acc, d) =>
            {
                unsafe
                {
                    fixed (Drive* p = &d.Value[0])
                        return NativeMethodsBase.AUTDGainCustomSet(acc, (uint)d.Key, (DriveRaw*)p, (uint)d.Value.Length);
                }
            });
        }
        
        [ExcludeFromCodeCoverage] private static Dictionary<int, Drive[]> Transform(Geometry geometry, Func<Device, Transducer, Drive> f)
        {
            return geometry.Devices().Select(dev => (dev.Idx, dev.Select(tr => f(dev, tr)).ToArray())).ToDictionary(x => x.Idx, x => x.Item2);
        }

""" : "";

        var cacheCode = noCache ? "" :
            $$"""
              
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Gain.Cache<{{typeName}}> WithCache()
        {
            return new AUTD3Sharp.Driver.Datagram.Gain.Cache<{{typeName}}>(this);
        }

""";
        var transformCode = noTransform ? "" :
            $$"""
              
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Gain.Transform<{{typeName}}> WithTransform(Func<AUTD3Sharp.Device, AUTD3Sharp.Transducer, AUTD3Sharp.Drive, AUTD3Sharp.Drive> f)
        {
            return new AUTD3Sharp.Driver.Datagram.Gain.Transform<{{typeName}}>(this, f);
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
using AUTD3Sharp;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

{{ns}} {
    public partial class {{typeName}} : AUTD3Sharp.Driver.Datagram.Gain.IGain, IDatagramS<GainPtr>, IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDGainIntoDatagram(((AUTD3Sharp.Driver.Datagram.Gain.IGain)this).GainPtr(geometry));
        [ExcludeFromCodeCoverage] DatagramPtr IDatagramS<GainPtr>.IntoSegment(GainPtr p, Segment segment, bool updateSegment) => NativeMethodsBase.AUTDGainIntoDatagramWithSegment(p, segment, updateSegment);
        [ExcludeFromCodeCoverage] GainPtr IDatagramS<GainPtr>.RawPtr(Geometry geometry) => GainPtr(geometry);
        GainPtr AUTD3Sharp.Driver.Datagram.Gain.IGain.GainPtr(Geometry geometry) => GainPtr(geometry);
        [ExcludeFromCodeCoverage] public DatagramWithSegment<{{typeName}}, GainPtr> WithSegment(Segment segment, bool updateSegment)
        {
            return new DatagramWithSegment<{{typeName}}, GainPtr>(this, segment, updateSegment);
        }
{{customCode}}
{{cacheCode}}
{{transformCode}}
    }
}
""";
        context.AddSource($"{fullType}.GainDerive.g.cs", code);
    }
}