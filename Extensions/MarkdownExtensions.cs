using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Renderers;
using Markdig.Syntax;
using YamlDotNet.Serialization;

namespace Decksplain.Extensions;

public static class MarkdownExtensions
{
    private static IDeserializer GetYamlDeserializer() =>  new DeserializerBuilder()
        // .IgnoreUnmatchedProperties()
        .Build();
    
    private static MarkdownPipeline GetPipeline() => new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UseYamlFrontMatter()
        .Build();

    public static T? ReadMarkdownWithFrontMatter<T>(this string markdown) where T : IContent
    {
        var pipeline = GetPipeline();
        
        StringWriter writer = new StringWriter();
        var renderer = new HtmlRenderer(writer);
        pipeline.Setup(renderer);
        
        MarkdownDocument document = Markdown.Parse(markdown, pipeline);
        YamlFrontMatterBlock? yamlBlock = document
            .Descendants<YamlFrontMatterBlock>()
            .FirstOrDefault();

        if (yamlBlock == null) 
            return default;
        
        var yaml = yamlBlock
            .Lines // StringLineGroup[]
            .Lines // StringLine[]
            .Where(x => !string.IsNullOrWhiteSpace(x.ToString()))
            .Select(x => $"{x}\n")
            .ToList()
            .Aggregate((s, agg) => agg + s);

        T item = GetYamlDeserializer().Deserialize<T>(yaml);
        
        renderer.Render(document);
        writer.Flush();
        string html = writer.ToString();
        
        item.Content = html;

        return item;
    }
}
