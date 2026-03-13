📘 Mo Brokerage Factory
Automated BRD → SRS → RTM Engine with Country‑Aware Compliance (Default: KSA / CMA)
Mo Brokerage Factory is an end‑to‑end automation system that transforms Business Requirement Documents (BRDs) into a complete Software Requirements Specification (SRS), RTM (Traceability Matrix), Gap Analysis, and QA Validation.
It supports two implementations:

Python CLI Engine (lightweight, scriptable)
.NET 8 + React Web Application (backend API + web UI)

The system includes country‑aware regulation rules, with default = KSA (CMA Brokerage Rules) and optional UAE / US / UK frameworks.

🚀 Features
✔ Auto‑extract requirements from BRD

Functional
Non‑functional
Interface
Data
Reporting
Controls / Security / Audit

✔ Auto‑generate SRS with structured numbering
Example: SRS-F-001, SRS-NF-001, SRS-IF-001
✔ Gap Detection Engine

Missing standard sections
Ambiguous wording
Non‑testable requirements

✔ Country Compliance Engine
Default: KSA (CMA Rules)
Optional: UAE (SCA), US (SEC/FINRA), UK (FCA)
✔ RTM (Traceability Matrix) Generator
Links BRD → SRS and marks coverage.
✔ QA Gate
Checks duplicates, numbering, and model consistency.
✔ Web UI
Paste BRD → Select country → Get results instantly.
