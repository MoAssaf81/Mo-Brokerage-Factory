
# BA Brokerage Factory – .NET 8 + React (Country-Aware)

Default country = **KSA** (CMA rules). Override via `?country=UAE|US|UK` in API or UI selector.

## Backend
```bash
cd backend
dotnet restore && dotnet build
dotnet run --project BrokerageFactory.Api/BrokerageFactory.Api.csproj
# http://localhost:5080
```

## Frontend
```bash
cd frontend
npm install
npm run dev
# http://localhost:5173
```

## Test the pipeline
```bash
curl -X POST 'http://localhost:5080/pipeline/run?country=KSA'   -H 'Content-Type: text/plain'   --data-binary @../examples/sample-brd.md
```
