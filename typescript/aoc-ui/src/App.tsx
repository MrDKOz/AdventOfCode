import { useState } from "react";
import { DAYS } from "./config/days";
import type { DayConfig, DayId } from "./config/days";
import { Container, Stack, Tab, Tabs, Typography } from "@mui/material";

function App() {
  const [activeDay, setActiveDay] = useState<DayId>("day1");
  const activeConfig = DAYS.find((d: DayConfig) => d.id === activeDay);

  return (
    <Stack direction="row" style={{ height: "100vh" }}>
      <Stack direction="column" sx={{ flexGrow: 1, background: 'background.paper', display: 'flex', height: '100vh'}}>
        <Typography variant="h6" sx={{ p: 2 }}>Advent of Code [2025]</Typography>
        <Tabs
          orientation="vertical"
          variant="scrollable"
          value={activeDay}
          onChange={(_, newValue) => setActiveDay(newValue)}
          sx={{ borderRight: 1, borderColor: 'divider', width: '200px' }}
        >
          {DAYS.map((day) => (
            <Tab
              key={day.id}
              label={day.label}
              value={day.id}
              sx={{
                alignItems: "flex-start",
                textAlign: "left",
              }}
            />
          ))}
        </Tabs>
      </Stack>

      <Container>
        {activeConfig ? (
          <activeConfig.component />
        ) : (
          <div>Select a day</div>
        )}
      </Container>
    </Stack>
  );
}

export default App;
