import { Stack, Typography, Tabs, Tab } from "@mui/material";
import { DAYS } from "../config/days";
import type { DayId } from "../config/days";

type NavbarProps = {
    activeDay: DayId;
    onChangeDay: (day: DayId) => void;
};

const Navbar: React.FC<NavbarProps> = ({ activeDay, onChangeDay }) => {
    return <Stack direction="column" maxWidth="200px">
        <Typography variant="h6" sx={{ p: 2 }}>Advent of Code [2025]</Typography>
        <Tabs
            orientation="vertical"
            variant="scrollable"
            value={activeDay}
            onChange={(_, newValue) => onChangeDay(newValue)}
            sx={{ borderRight: 1, borderColor: 'divider' }}
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
};

export { Navbar };