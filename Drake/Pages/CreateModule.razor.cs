namespace Drake.Pages;
public partial class CreateModule
{
	public string tempjson = """
          {
          	"request": {
          		"path": "test1.txt"
          	},
          	"response": {
          		"matches": [
          			{"type": "static", "pattern": "data"}
          		]
          	}
          }
          """;
}