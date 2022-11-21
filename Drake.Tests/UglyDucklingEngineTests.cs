using System.Net.Http.Headers;
using UglyDuckling;

namespace Drake.Tests;

public class UglyDucklingEngineTests
{
    [Fact]
    public async Task StaticShouldMatch()
    {
        UglyDucklingEngine engine = new();
        Module m = new();
        m.Response = new();
        m.Response.Matches.Add(new Match() { Type = "static", Pattern = "Test" });
        var message = new HttpResponseMessage();
        var headers = message.Headers;

        var result= await engine.ValidateRequestAsync(m, "Test", 200, headers);
        Assert.NotNull(result);
        Assert.True(result.VulnerabilityFound);
    }

    [Fact]
    public async Task StaticShouldNotMatch()
    {
        UglyDucklingEngine engine = new();
        Module m = new();
        m.Response = new();
        m.Response.Matches.Add(new Match() { Type = "static", Pattern = "NotMatching" });
        var message = new HttpResponseMessage();
        var headers = message.Headers;

        var result = await engine.ValidateRequestAsync(m, "Test", 200, headers);
        Assert.NotNull(result);
        Assert.False(result.VulnerabilityFound);
    }
    [Fact]
    public async Task StatusCodeShouldMatch()
    {
        UglyDucklingEngine engine = new();
        Module m = new();
        m.Response = new();
        m.Response.Matches.Add(new Match() { Type = "status", Code=200 });
        var message = new HttpResponseMessage();
        var headers = message.Headers;

        var result = await engine.ValidateRequestAsync(m, "Test", 200, headers);
        Assert.NotNull(result);
        Assert.True(result.VulnerabilityFound);
    }
    [Fact]
    public async Task StatusCodeShouldNotMatch()
    {
        UglyDucklingEngine engine = new();
        Module m = new();
        m.Response = new();
        m.Response.Matches.Add(new Match() { Type = "status", Code = 1337 });
        var message = new HttpResponseMessage();
        var headers = message.Headers;

        var result = await engine.ValidateRequestAsync(m, "Test", 200, headers);
        Assert.NotNull(result);
        Assert.False(result.VulnerabilityFound);
    }


    [Fact]
    public async Task RegexShouldMatch()
    {
        UglyDucklingEngine engine = new();
        Module m = new();
        m.Response = new();
        m.Response.Matches.Add(new Match() { Type = "regex", Pattern= "[1][1-3][1-3][7]" });
        var message = new HttpResponseMessage();
        var headers = message.Headers;

        var result = await engine.ValidateRequestAsync(m, "1337", 200, headers);
        Assert.NotNull(result);
        Assert.True(result.VulnerabilityFound);
    }

    [Fact]
    public async Task RegexShouldNotMatch()
    {
        UglyDucklingEngine engine = new();
        Module m = new();
        m.Response = new();
        m.Response.Matches.Add(new Match() { Type = "regex", Pattern = "[1][1-3][1-3][7]" });
        var message = new HttpResponseMessage();
        var headers = message.Headers;

        var result = await engine.ValidateRequestAsync(m, "1338", 200, headers);
        Assert.NotNull(result);
        Assert.False(result.VulnerabilityFound);
    }

    [Fact]
    public async Task HeaderShouldNotMatch()
    {
        UglyDucklingEngine engine = new();
        Module m = new();
        m.Response = new();
        m.Response.Matches.Add(new Match() { Type = "header", Name= "HeaderKey", Pattern = "[1][1-3][1-3][7]" });
        var message = new HttpResponseMessage();
        var headers = message.Headers;
        headers.Add("HeaderKey", "1338");
        var result = await engine.ValidateRequestAsync(m, "1338", 200, headers);
        Assert.NotNull(result);
        Assert.False(result.VulnerabilityFound);
    }

    [Fact]
    public async Task HeaderShouldMatch()
    {
        UglyDucklingEngine engine = new();
        Module m = new();
        m.Response = new();
        m.Response.Matches.Add(new Match() { Type = "header", Name = "HeaderKey", Pattern = "[1][1-3][1-3][7]" });
        var message = new HttpResponseMessage();
        var headers = message.Headers;
        headers.Add("HeaderKey", "Other1337Text");
        var result = await engine.ValidateRequestAsync(m, "1338", 200, headers);
        Assert.NotNull(result);
        Assert.True(result.VulnerabilityFound);
    }

}