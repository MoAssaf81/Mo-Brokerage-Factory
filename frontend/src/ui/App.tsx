
import React, { useState } from 'react'

export default function App() {
  const [brd, setBrd] = useState(`# Sample BRD (Brokerage)

## Objectives
- Enable digital account opening across channels.

## Functional Requirements
- The system must allow clients to submit an account opening request with KYC.
- The system must integrate with an AML provider for automated screening.

## Non-functional Requirements
- Account opening API p95 latency <= 2 seconds.

## Reporting
- Daily report of approvals/denials.`)
  const [result, setResult] = useState<any>(null)
  const [loading, setLoading] = useState(false)
  const [country, setCountry] = useState('KSA')

  const run = async () => {
    setLoading(true); setResult(null)
    try {
      const res = await fetch(`http://localhost:5080/pipeline/run?country=${country}`, { method: 'POST', headers: { 'Content-Type': 'text/plain' }, body: brd })
      const json = await res.json(); setResult(json)
    } catch (e) {
      console.error(e); alert('Failed to call API. Is the backend running on http://localhost:5080?')
    } finally { setLoading(false) }
  }

  return (
    <div style={{ maxWidth: 1000, margin: '30px auto', fontFamily: 'Inter, system-ui, sans-serif' }}>
      <h1>BA Brokerage Factory</h1>
      <p>Paste your BRD markdown below, choose a country, and click <b>Run pipeline</b>.</p>
      <textarea value={brd} onChange={e => setBrd(e.target.value)} rows={18} style={{ width: '100%', fontFamily: 'monospace' }} />
      <div style={{ display: 'flex', gap: 12, alignItems: 'center', marginTop: 10 }}>
        <label>Country:</label>
        <select value={country} onChange={e=>setCountry(e.target.value)}>
          <option value='KSA'>KSA</option>
          <option value='UAE'>UAE</option>
          <option value='US'>US</option>
          <option value='UK'>UK</option>
        </select>
        <button onClick={run} disabled={loading}>{loading ? 'Running…' : 'Run pipeline'}</button>
      </div>

      {result && (
        <div style={{ marginTop: 30 }}>
          <h2>Outputs</h2>
          <section>
            <h3>Applied Rules</h3>
            <pre>{JSON.stringify(result.rules, null, 2)}</pre>
          </section>
          <section>
            <h3>SRS</h3>
            <ul>
              {result.srs?.map((i: any) => (
                <li key={i.srsId}><b>{i.srsId}</b> [{i.category}] — {i.text}</li>
              ))}
            </ul>
          </section>
          <section>
            <h3>Gaps</h3>
            <pre>{JSON.stringify(result.gaps, null, 2)}</pre>
          </section>
          <section>
            <h3>QA</h3>
            <pre>{JSON.stringify(result.qa, null, 2)}</pre>
          </section>
          <section>
            <h3>RTM (CSV)</h3>
            <textarea readOnly rows={10} style={{ width: '100%', fontFamily: 'monospace' }} value={result.rtmCsv} />
          </section>
        </div>
      )}
    </div>
  )
}
