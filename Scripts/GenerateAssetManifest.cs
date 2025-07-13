#:property TargetFramework net10.0

using System.Text.Json;

string staticRoute = $".{Path.DirectorySeparatorChar}";
string[] ignoredPaths =
[
    "/service-worker.js"
];

List<string> fileNames = Directory.EnumerateFiles(staticRoute, "*", SearchOption.AllDirectories)
    .Select(fileName => fileName
        // .\static\index.html -> \index.html.
        .Replace(staticRoute, "\\")
        // \index.html -> /index.html
        // Change Windows file system slashes to web slashes.
        .Replace("\\", "/")
        // /index.html -> /
        // Change index.html routes to directory routing.
        .Replace("index.html", "")
	)
	.Where(path => !ignoredPaths.Contains(path))
    .ToList();

// TODO: figure out if I need to add these
// fileNames.Add("images/logo-square.svg");

Manifest manifest = new()
{
    Version = Environment.GetEnvironmentVariable("TAG_NAME") ?? "0.0.0",
    Assets = fileNames,
};

string json = JsonSerializer.Serialize(manifest, new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true 
});

File.WriteAllText($"{staticRoute}asset-manifest.json", json);

public class Manifest
{
    public required string Version { get; set; }

    public required IEnumerable<string> Assets { get; set; }
}
