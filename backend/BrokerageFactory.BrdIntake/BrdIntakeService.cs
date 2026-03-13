
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using BrokerageFactory.Common.Models;

namespace BrokerageFactory.BrdIntake;

public static partial class BrdIntakeService
{
    [GeneratedRegex(@"^##\s+(.+)$", RegexOptions.Multiline)]
    private static partial Regex SecRegex();

    public static BRDDocument Normalize(string markdown)
    {
        var secRe = SecRegex();
        var parts = secRe.Split(markdown);
        var sections = new List<BRDSection>();
        for (int i = 1; i + 1 < parts.Length; i += 2)
        {
            var title = parts[i].Trim();
            var content = parts[i + 1].Trim();
            sections.Add(new BRDSection(title, content));
        }
        var sha = SHA1.HashData(Encoding.UTF8.GetBytes(markdown));
        var digest = Convert.ToHexString(sha).ToLowerInvariant()[..8];
        var meta = new Dictionary<string, string> { ["hash"] = digest };
        return new BRDDocument($"BRD-{digest}", "v1.0", meta, sections);
    }
}
