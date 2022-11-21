// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;

public class Match
{
	[JsonPropertyName("type")]
	public string Type { get; set; }

	[JsonPropertyName("pattern")]
	public string Pattern { get; set; }

	[JsonPropertyName("required")]
	public bool? Required { get; set; }

	[JsonPropertyName("code")]
	public int? Code { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }
}


public class Request
{
	[JsonPropertyName("method")]
	public string Method { get; set; }
	[JsonPropertyName("path")]
	public string Path { get; set; }

	[JsonPropertyName("paths")]
	public List<string> Paths { get; set; } = new();

	[JsonPropertyName("body")]
	public string Body { get; set; }

	[JsonPropertyName("headers")]
	public List<string> Headers { get; set; } = new();
}

public class Response
{
	[JsonPropertyName("matchesRequired")]
	public int MatchesRequired { get; set; } = 1;

	[JsonPropertyName("matches")]
	public List<Match> Matches { get; set; } = new();

	[JsonPropertyName("mustNotMatch")]
	public List<Match> MustNotMatch { get; set; } = new();
}

public class Module
{
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("request")]
	public Request Request { get; set; }

	[JsonPropertyName("response")]
	public Response Response { get; set; }
}

///https://cveawg.mitre.org/api/cve/CVE-2017-9140
///https://nvd.nist.gov/vuln/detail/CVE-2017-9140