using Drake.UglyDuckling;
using System.Net.Http.Headers;

namespace UglyDuckling;
public class UglyDucklingEngine
{
	public async Task<ModuleResult> ValidateRequestAsync(Module module, string responseBody, int responseStatusCode, HttpResponseHeaders headers)
	{
		var result=new ModuleResult();

		//Verify module
		if (module.Response.Matches != null)
		{
			foreach (var match in module.Response.Matches)
			{
				var mresult=await HandleMatchAsync(match,responseStatusCode, responseBody,headers);
				//If match is required but not matched
				if (!mresult.IsMatched && (match.Required??false))
				{
					result.VulnerabilityFound = false;
				}
				result.ShouldMatch.Add(mresult);
			}
		}
		if (module.Response.MustNotMatch != null)
		{
			foreach (var match in module.Response.MustNotMatch)
			{
				var mresult = await HandleMatchAsync(match, responseStatusCode, responseBody,headers);
				
				if (mresult.IsMatched)
				{
					result.VulnerabilityFound = false;
				}
				result.ShouldNotMatch.Add(mresult);
			}
		}
		if (result.VulnerabilityFound == null)
		{
			result.VulnerabilityFound = result.ShouldMatch.Count(m => m.IsMatched) >= module.Response.MatchesRequired;
		}
		return result;
	}

	async Task<MatchResult> HandleMatchAsync(Match match, int statusCode, string responseBody, HttpResponseHeaders headers)
	{
		var isMatch = false;
		if (match.Type == "static")
		{
			if (responseBody.Contains(match.Pattern))
			{
				isMatch = true;
			}
		}
		if (match.Type == "regex")
		{
			System.Text.RegularExpressions.Regex regex = new(match.Pattern);
			if (regex.IsMatch(responseBody))
			{
				isMatch = true;
			}
		}

		if (match.Type == "status" && match.Code != null)
		{
			if (statusCode == match.Code)
			{
				isMatch = true;
			}
		}

		if (match.Type == "header")
		{
			if (headers.TryGetValues(match.Name, out var values))
			{
				foreach (var v in values)
				{
					System.Text.RegularExpressions.Regex regex = new(match.Pattern);
					if (regex.IsMatch(v))
					{
						isMatch = true;
					}
				}
			}
		}

		return new() { Match = match, IsMatched = isMatch};
	}

}