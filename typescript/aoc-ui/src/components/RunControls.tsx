interface RunControlsProps {
  onRunPart1: () => void;
  onRunPart2: () => void;
  onClear: () => void;
  part1Label?: string;
  part2Label?: string;
}

function RunControls({
  onRunPart1,
  onRunPart2,
  onClear,
  part1Label = "Run Part 1",
  part2Label = "Run Part 2",
}: RunControlsProps) {
  return (
    <div style={{ display: "flex", gap: "0.5rem", marginBottom: "1rem" }}>
      <button
        onClick={onRunPart1}
        style={{
          padding: "0.5rem 1rem",
          borderRadius: "4px",
          border: "none",
          background: "#7c3aed",
          color: "white",
          cursor: "pointer",
          fontWeight: 600,
        }}
      >
        {part1Label}
      </button>

      <button
        onClick={onRunPart2}
        style={{
          padding: "0.5rem 1rem",
          borderRadius: "4px",
          border: "none",
          background: "#2563eb",
          color: "white",
          cursor: "pointer",
          fontWeight: 600,
        }}
      >
        {part2Label}
      </button>

      <button
        onClick={onClear}
        style={{
          padding: "0.5rem 1rem",
          borderRadius: "4px",
          border: "none",
          background: "red",
          cursor: "pointer",
        }}
      >
        Clear
      </button>
    </div>
  );
}

export { RunControls };