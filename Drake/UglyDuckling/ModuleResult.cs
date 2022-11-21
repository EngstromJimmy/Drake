namespace Drake.UglyDuckling
{
	public class ModuleResult
	{
		public List<MatchResult> ShouldMatch { get; set; } = new();
		public List<MatchResult> ShouldNotMatch { get; set; } = new();
		public bool? VulnerabilityFound { get; set; }
    }
}
