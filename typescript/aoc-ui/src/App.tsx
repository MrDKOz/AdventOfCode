import { useState } from "react";
import { DAYS } from "./config/days";
import type { DayConfig, DayId } from "./config/days";
import { Container, Stack } from "@mui/material";
import { Navbar } from "./components/Navbar";

function App() {
  const [activeDay, setActiveDay] = useState<DayId>("day1");
  const activeConfig = DAYS.find((d: DayConfig) => d.id === activeDay);

  return (
    <Stack direction="row" style={{ height: "100vh" }}>
      <Navbar activeDay={activeDay} onChangeDay={setActiveDay} />

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
