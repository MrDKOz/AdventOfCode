import { useState } from "react";
import { DAYS } from "./config/days";
import type { DayConfig, DayId } from "./config/days";

function App() {
  const [activeDay, setActiveDay] = useState<DayId>("day1");
  const activeConfig = DAYS.find((d: DayConfig) => d.id === activeDay);

  return (
    <div style={{ width: "100%", display: "flex", height: "100vh", fontFamily: "sans-serif" }}>
      <nav
        style={{
          width: "200px",
          borderRight: "1px solid #ddd",
          padding: "1rem",
          boxSizing: "border-box",
        }}
      >
        <h2 style={{ fontSize: "1.1rem", marginBottom: "1rem" }}>Advent of Code</h2>
        <ul style={{ listStyle: "none", padding: 0, margin: 0 }}>
          {DAYS.map((day) => (
            <li key={day.id} style={{ marginBottom: "0.5rem" }}>
              <button
                onClick={() => setActiveDay(day.id)}
                style={{
                  width: "100%",
                  padding: "0.5rem 0.75rem",
                  borderRadius: "4px",
                  border: day.id === activeDay ? "2px solid #4f1699ff" : "1px solid #444444ff",
                  background: day.id === activeDay ? "#444444ff" : "#969696ff",
                  cursor: "pointer",
                  textAlign: "left",
                }}
              >
                {day.label}
              </button>
            </li>
          ))}
        </ul>
      </nav>

      <main
        style={{
          flex: 1,
          boxSizing: "border-box",
          display: "flex",
          justifyContent: "center",
          alignItems: "flex-start",
          padding: "1rem",
        }}
      >
        <div
          style={{
            width: "100%",
            maxWidth: "900px",   // fixed content width
            boxSizing: "border-box",
          }}
        >
          {activeConfig ? (
            <activeConfig.component />
          ) : (
            <div>Select a day</div>
          )}
        </div>
      </main>
    </div>
  );
}

export default App;
