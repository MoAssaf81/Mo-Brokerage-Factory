
using BrokerageFactory.Common.Models;

namespace BrokerageFactory.SrsGeneration;

public static class SrsGenerator
{
    private static readonly Dictionary<string, string> PREFIX = new()
    {
        ["Functional"] = "SRS-F",
        ["Non-functional"] = "SRS-NF",
        ["Interface"] = "SRS-IF",
        ["Reporting"] = "SRS-RP",
        ["Data"] = "SRS-D",
        ["Control"] = "SRS-C",
        ["Audit"] = "SRS-AU",
        ["Security"] = "SRS-S",
        ["Business"] = "SRS-BU",
    };

    public static List<SRSItem> Generate(List<Requirement> reqs)
    {
        var counters = PREFIX.Keys.ToDictionary(k => k, _ => 0);
        var items = new List<SRSItem>();
        foreach (var r in reqs)
        {
            var p = PREFIX.TryGetValue(r.Category, out var prefix) ? prefix : "SRS-BU";
            counters[r.Category] = counters.GetValueOrDefault(r.Category) + 1;
            var id = $"{p}-{counters[r.Category]:000}";
            items.Add(new SRSItem(id, r.Text, r.Category, new List<string> { r.BrdId }));
        }
        return items;
    }
}
