
using System.Text.Json;
using BrokerageFactory.BrdIntake;
using BrokerageFactory.RequirementExtraction;
using BrokerageFactory.GapDetection;
using BrokerageFactory.SrsGeneration;
using BrokerageFactory.Rtm;
using BrokerageFactory.Qa;
using BrokerageFactory.Common.Config;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Urls.Add("http://localhost:5080");

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapPost("/pipeline/run", async (HttpRequest req, HttpContext http) =>
{
    using var reader = new StreamReader(req.Body);
    var brdText = await reader.ReadToEndAsync();
    var brd = BrdIntakeService.Normalize(brdText);
    var reqs = RequirementExtractor.Extract(brd);
    var gaps = GapDetector.Detect(brd);
    var srs = SrsGenerator.Generate(reqs);
    var rtmCsv = RtmService.BuildCsv(reqs, srs);
    var qa = QaService.Review(srs);
    var country = http.Request.Query["country"].FirstOrDefault();
    var rules = CountryRules.Resolve(country);

    var payload = new
    {
        brd = new { brd.DocId, brd.Version, brd.Metadata, sections = brd.Sections },
        requirements = reqs,
        gaps,
        srs,
        rtmCsv,
        qa,
        rules
    };
    return Results.Ok(payload);
});

app.Run();
