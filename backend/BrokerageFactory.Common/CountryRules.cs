
namespace BrokerageFactory.Common.Config;

public static class CountryRules
{
    public const string DefaultCountry = "KSA";

    private static readonly Dictionary<string, Dictionary<string, string>> Rules = new()
    {
        ["KSA"] = new() { ["regulator"] = "CMA",      ["framework"] = "CMA Brokerage Rules",    ["notes"] = "Apply Saudi CMA rules." },
        ["UAE"] = new() { ["regulator"] = "SCA",      ["framework"] = "SCA Brokerage Regulations", ["notes"] = "Apply UAE SCA rules." },
        ["US"]  = new() { ["regulator"] = "SEC/FINRA",["framework"] = "US Broker-Dealer Rules",  ["notes"] = "Apply SEC and FINRA rules." },
        ["UK"]  = new() { ["regulator"] = "FCA",      ["framework"] = "FCA Handbook",            ["notes"] = "Apply FCA COB rules." },
    };

    public static Dictionary<string, string> Resolve(string? country)
    {
        var c = (country ?? DefaultCountry).ToUpperInvariant();
        var baseRules = Rules.TryGetValue(c, out var val) ? val : Rules[DefaultCountry];
        return new Dictionary<string, string>(baseRules) { ["country"] = c };
    }
}
