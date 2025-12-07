import { DayTemplate } from "../components/DayTemplate";
import type { SolverFunction } from "../types/SolverFunction";
import { getLines } from "../utils/input";

const solverPart1: SolverFunction = (input) => {
    const lines = getLines(input);

    const calculatePosition = (current: number, delta: number) => ((current + delta) % 100 + 100) % 100;

    const movement = lines.reduce((previousState, instruction) => {
        const raw = Number(instruction.substring(1));
        const delta = instruction.startsWith("L") ? -raw : raw;
        const newPosition = calculatePosition(previousState.position, delta);

        return {
            position: newPosition,
            numberOfTimesAtZero: previousState.numberOfTimesAtZero + (newPosition === 0 ? 1 : 0)
        }

    }, { position: 50, numberOfTimesAtZero: 0 });

    return `Final Position: ${movement.position}\nNumber of times at 0: ${movement.numberOfTimesAtZero}`;
};

const solverPart2: SolverFunction = (input) => {
    const lines = getLines(input);

    type StepResult = {
        position: number;
        zerosHit: number;
    };

    const stepThroughMovement = (current: number, delta: number): StepResult => {
        if (delta === 0) {
            return { position: current, zerosHit: 0 };
        }

        const step = delta > 0 ? 1 : -1;
        const steps = Math.abs(delta);

        let position = current;
        let zerosHit = 0;

        for (let i = 0; i < steps; i++) {
            position = ((position + step) % 100 + 100) % 100;
            if (position === 0) {
                zerosHit++;
            }
        }

        return { position, zerosHit };
    };

    const movement = lines.reduce((previousState, instruction) => {
        const raw = Number(instruction.substring(1));
        const delta = instruction.startsWith("L") ? -raw : raw;
        const { position, zerosHit } = stepThroughMovement(previousState.position, delta);

        return {
            position,
            numberOfTimesAtZero: previousState.numberOfTimesAtZero + zerosHit,
        };

    }, { position: 50, numberOfTimesAtZero: 0 });

    return `Final Position: ${movement.position}\nNumber of times at 0: ${movement.numberOfTimesAtZero}`;
};

function Day1() {
    return (
        <DayTemplate
            title="Day 1: Secret Entrance"
            description="Dial rotation puzzle."
            solverPart1={solverPart1}
            solverPart2={solverPart2}
        />
    );
}

export { Day1 };