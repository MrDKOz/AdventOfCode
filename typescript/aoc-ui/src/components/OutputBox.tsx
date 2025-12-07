import { Box, Typography, Paper, Alert } from "@mui/material";

interface OutputBoxProps {
  title: string;
  output: string | null;
  error: string | null;
}

export function OutputBox({ title, error, output }: OutputBoxProps) {
  return (
    <Box>
      <Typography variant="subtitle1" sx={{ mb: 1 }}>
        {title}
      </Typography>

      <Paper elevation={3} sx={{ p: 1, whiteSpace: 'pre-wrap' }}>
        {error && (
          <Alert severity="error" sx={{ p: 0, mt: 0, mb: 0 }}>
            Error: {error}
          </Alert>
        )}

        {!error && output !== null && <Typography component="span">{output}</Typography>}

        {!error && output === null && (
          <Typography component="span" color="text.secondary">
            No output yet.
          </Typography>
        )}
      </Paper>
    </Box>
  );
}