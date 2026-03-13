
namespace BrokerageFactory.Common.Models;

public record BRDSection(string Title, string Content);

public record BRDDocument(
    string DocId,
    string Version,
    Dictionary<string, string> Metadata,
    List<BRDSection> Sections
);

public record Requirement(
    string BrdId,
    string Text,
    string Category
);

public record SRSItem(
    string SrsId,
    string Text,
    string Category,
    List<string> Links
);
