import { Button, Stack } from "@mui/material";

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
    <Stack direction="row" spacing={1} mb={2}>
      <Button variant="contained" color="primary" onClick={onRunPart1}>
        {part1Label}
      </Button>
      <Button variant="contained" color="secondary" onClick={onRunPart2}>
        {part2Label}
      </Button>
      <Button variant="outlined" color="error" onClick={onClear}>
        Clear
      </Button>
    </Stack>
  );
}

export { RunControls };