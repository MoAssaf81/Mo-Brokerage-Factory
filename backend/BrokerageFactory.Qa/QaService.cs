
using BrokerageFactory.Common.Models;

namespace BrokerageFactory.Qa;

public static class QaService
{
    public static Dictionary<string, int> Review(List<SRSItem> srs)
    {
        var seen = new HashSet<string>();
        var duplicates = 0;
        foreach (var i in srs)
            if (!seen.Add(i.SrsId)) duplicates++;
        return new Dictionary<string, int> { ["total_srs"] = srs.Count, ["duplicate_ids"] = duplicates, ["unique_ids"] = seen.Count };
    }
}
