import { Day1 } from "../days/Day1";
import { Day2 } from "../days/Day2";
import { Day3 } from "../days/Day3";

const DAYS = [
    { id: "day1", label: "Day 1", component: Day1 },
    { id: "day2", label: "Day 2", component: Day2 },
    { id: "day3", label: "Day 3", component: Day3 },
] as const; // Make DAYS a constant tuple with literal types

// Extract the union type of the array elements
type DayConfig = (typeof DAYS)[number];
// DayConfig is now { id: "day1"; label: string; component: React.ComponentType } | { id: "day2"; ... } | { id: "day3"; ... }

// Extract the union type of the 'id' properties
type DayId = DayConfig["id"];
// DayId is now "day1" | "day2" | "day3"

export { DAYS };
export type { DayId, DayConfig };