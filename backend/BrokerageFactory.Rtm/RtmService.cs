
using BrokerageFactory.Common.Models;
using System.Text;

namespace BrokerageFactory.Rtm;

public static class RtmService
{
    public static string BuildCsv(List<Requirement> reqs, List<SRSItem> srs)
    {
        var map = new Dictionary<string, List<string>>();
        foreach (var s in srs)
            foreach (var b in s.Links)
                (map.TryGetValue(b, out var list) ? list : map[b] = new List<string>()).Add(s.SrsId);

        var sb = new StringBuilder();
        sb.AppendLine("BRD_ID,Requirement_Text,Category,SRS_IDs,Test_Case_ID,Coverage_Status,Comments");
        foreach (var r in reqs)
        {
            map.TryGetValue(r.BrdId, out var idsList);
            var ids = idsList is null ? string.Empty : string.Join(';', idsList);
            var cov = string.IsNullOrEmpty(ids) ? "Uncovered" : "Covered";
            sb.AppendLine(string.Join(',', new [] {
                Escape(r.BrdId), Escape(r.Text), Escape(r.Category), Escape(ids), "", cov, ""
            }));
        }
        return sb.ToString();
    }

    private static string Escape(string s) => (s.Contains(',') || s.Contains('"')) ? """ + s.Replace(""", """") + """ : s;
}
