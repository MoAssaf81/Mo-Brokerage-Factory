
using BrokerageFactory.Common.Models;

namespace BrokerageFactory.RequirementExtraction;

public static class RequirementExtractor
{
    private static readonly Dictionary<string, string[]> CATEGORIES = new()
    {
        ["Functional"]     = new[] { "shall", "must", "enable", "process", "calculate" },
        ["Non-functional"] = new[] { "latency", "performance", "availability", "security", "scalability", "throughput" },
        ["Interface"]      = new[] { "interface", "api", "integration", "protocol", "webhook" },
        ["Reporting"]      = new[] { "report", "dashboard", "analytics" },
        ["Data"]           = new[] { "data", "field", "table", "quality", "retention", "schema" },
        ["Control"]        = new[] { "control", "authorization limit", "approval", "sod", "segregation" },
        ["Audit"]          = new[] { "audit", "log", "trail" },
        ["Security"]       = new[] { "encryption", "authentication", "authorization", "roles", "permissions" },
    };

    private static readonly string[] TRIGGERS = new[] { "shall", "must", "the system shall", "the system must" };

    private static string Classify(string sentence)
    {
        var low = sentence.ToLowerInvariant();
        foreach (var kv in CATEGORIES)
            if (kv.Value.Any(k => low.Contains(k))) return kv.Key;
        return "Business";
    }

    public static List<Requirement> Extract(BRDDocument doc)
    {
        var list = new List<Requirement>();
        foreach (var sec in doc.Sections)
        {
            foreach (var raw in sec.Content.Split('
'))
            {
                var line = raw.Trim().TrimStart('-').Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var low = line.ToLowerInvariant();
                if (TRIGGERS.Any(t => low.Contains(t)))
                    list.Add(new Requirement(doc.DocId, line, Classify(low)));
            }
        }
        return list;
    }
}
