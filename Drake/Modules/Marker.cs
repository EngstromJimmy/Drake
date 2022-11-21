using System.Text.Json.Serialization;

namespace Drake.Modules;
public class Marker
{
	[JsonPropertyName("startLineNumber")]
	public int StartLineNumber { get; set; }
	[JsonPropertyName("message")]
	public string Message { get; set; }
}