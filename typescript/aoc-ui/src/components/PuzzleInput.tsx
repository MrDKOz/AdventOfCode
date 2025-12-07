import { TextField } from "@mui/material";

interface PuzzleInputProps {
  value: string;
  onChange: (newValue: string) => void;
}

function PuzzleInput({ value, onChange }: PuzzleInputProps) {
  return (
    <TextField
      value={value}
      onChange={(e) => onChange(e.target.value)}
      multiline
      rows={10}
      margin="normal"
      placeholder="Paste your puzzle input here..."
    />
  );
}

export { PuzzleInput };