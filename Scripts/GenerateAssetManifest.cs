#:property TargetFramework net10.0

using System.Text.Json;

const string staticRoute = ".\\static\\";
string[] ignoredPaths =
[
    "/asset-manifest.json",
    "/service-worker.js"
];

string[] fileNames = Directory.EnumerateFiles(staticRoute, "*", SearchOption.AllDirectories)
    .Select(fileName => fileName
        // .\static\index.html -> \index.html.
        .Replace(staticRoute, "\\")
        // \index.html -> /index.html
        // Change file system slashes to web slashes.
        .Replace("\\", "/")
        // /index.html -> /
        // Change index.html routes to directory routing.
        .Replace("index.html", "")
	)
	.Where(path => !ignoredPaths.Contains(path))
    .ToArray();

// TODO: figure out if I need to add these
// fileNames.Add("service-worker.js");
// fileNames.Add("images/logo-square.svg");

Manifest manifest = new()
{
    Version = "1.0.0",
    Assets = fileNames,
};

string json = JsonSerializer.Serialize(manifest, new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true 
});

File.WriteAllText(".\\static\\asset-manifest.json", json);

public class Manifest
{
    public required string Version { get; set; }

    public required IEnumerable<string> Assets { get; set; }
}