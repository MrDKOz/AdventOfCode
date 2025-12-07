interface OutputBoxProps {
  title: string;
  output: string | null;
  error: string | null;
}

export function OutputBox({ title, error, output }: OutputBoxProps) {
  return (
    <div>
      <h2 style={{ fontSize: "1rem", marginBottom: "0.25rem" }}>{title}</h2>

      <div
        style={{
          minHeight: "2rem",
          padding: "0.5rem",
          borderRadius: "4px",
          border: "1px solid #e5e7eb",
          background: "#f9fafb",
          fontFamily: "monospace",
          whiteSpace: "pre-wrap",
          color: "#000000ff",
        }}
      >
        {error && <span style={{ color: "#b91c1c" }}>Error: {error}</span>}

        {!error && output !== null && <span>{output}</span>}

        {!error && output === null && (
          <span style={{ color: "#000000ff" }}>No output yet.</span>
        )}
      </div>
    </div>
  );
}