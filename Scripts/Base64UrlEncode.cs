#:property TargetFramework net10.0

var input = args[0];

if (string.IsNullOrEmpty(input))
{
    Console.WriteLine("Error: No input provided.");
    return;
}

byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
string encoded = System.Buffers.Text.Base64Url.EncodeToString(bytes);

Console.WriteLine(encoded);
