import { OutputBox } from "./OutputBox";
import type { SolverFunction } from "../types/SolverFunction";
import { RunControls } from "./RunControls";
import { PuzzleInput } from "./PuzzleInput";
import { usePuzzleState } from "../hooks/usePuzzleState";

interface DayTemplateProps {
    title: string;
    description?: string;
    part1Label?: string;
    part2Label?: string;
    solverPart1: SolverFunction;
    solverPart2: SolverFunction;
}

function DayTemplate({
    title,
    description,
    solverPart1,
    solverPart2,
}: DayTemplateProps) {
    const {
        input,
        setInput,
        outputPart1,
        outputPart2,
        errorPart1,
        errorPart2,
        runPart1,
        runPart2,
        clear,
    } = usePuzzleState({ solverPart1, solverPart2 });

    return (
        <div style={{
            width: "100%",
            boxSizing: "border-box",
            flexDirection: "column",
        }}>
            <h1 style={{ marginBottom: "0.5rem" }}>{title}</h1>
            {description && (
                <p style={{ marginBottom: "1rem", color: "#4b5563" }}>{description}</p>
            )}

            <PuzzleInput value={input} onChange={setInput} />

            <RunControls
                onRunPart1={runPart1}
                onRunPart2={runPart2}
                onClear={clear}
            />

            <div style={{ display: "grid", gap: "0.75rem" }}>
                <OutputBox title="Part 1 Output" output={outputPart1} error={errorPart1} />
                <OutputBox title="Part 2 Output" output={outputPart2} error={errorPart2} />
            </div>
        </div>
    );
}

export { DayTemplate };