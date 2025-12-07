interface PuzzleInputProps {
  value: string;
  onChange: (newValue: string) => void;
}

function PuzzleInput({ value, onChange }: PuzzleInputProps) {
  return (
    <div style={{ marginBottom: "1rem" }}>
      <label
        style={{
          display: "block",
          marginBottom: "0.25rem",
          fontWeight: 600,
        }}
      >
        Puzzle input
      </label>

      <textarea
        value={value}
        onChange={(e) => onChange(e.target.value)}
        rows={10}
        style={{
          width: "100%",
          boxSizing: "border-box",
          fontFamily: "monospace",
          fontSize: "0.9rem",
          padding: "0.5rem",
          borderRadius: "4px",
          border: "1px solid #d1d5db",
          marginBottom: "0.75rem",
        }}
        placeholder="Paste your puzzle input here..."
      />
    </div>
  );
}

export { PuzzleInput };