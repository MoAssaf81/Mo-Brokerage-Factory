
using BrokerageFactory.Common.Models;

namespace BrokerageFactory.GapDetection;

public static class GapDetector
{
    private static readonly string[] STANDARD_SECTIONS = new[]
    {
        "background", "scope", "actors", "lifecycle", "capabilities", "integrations", "controls",
        "non-functional", "reporting", "data", "security", "audit", "edge cases"
    };
    private static readonly string[] AMBIGUOUS = new[] { "fast", "flexible", "user-friendly", "supports all", "zero downtime" };

    public static Dictionary<string, List<string>> Detect(BRDDocument doc)
    {
        var result = new Dictionary<string, List<string>>
        {
            ["missing_sections"] = new(),
            ["ambiguous"] = new(),
            ["non_testable"] = new(),
        };

        var titles = string.Join(' ', doc.Sections.Select(s => s.Title.ToLowerInvariant()));
        foreach (var kw in STANDARD_SECTIONS)
            if (!titles.Contains(kw)) result["missing_sections"].Add(kw);

        foreach (var s in doc.Sections)
        {
            foreach (var raw in s.Content.Split('
'))
            {
                var line = raw.Trim();
                var low = line.ToLowerInvariant();
                if (AMBIGUOUS.Any(a => low.Contains(a))) result["ambiguous"].Add(line);
                if ((low.Contains("must") || low.Contains("shall")) && !low.Any(char.IsDigit))
                    result["non_testable"].Add(line);
            }
        }
        return result;
    }
}
