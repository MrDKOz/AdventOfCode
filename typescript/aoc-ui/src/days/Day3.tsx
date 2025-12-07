import { DayTemplate } from "../components/DayTemplate";
import type { SolverFunction } from "../types/SolverFunction";
import { getLines } from "../utils/input";

const solverPart1: SolverFunction = (input) => {
  const lines = getLines(input);

  const maxPairsForLine = (line: string): number => {
    if (line.split("9").length > 2) {
      return 99;
    }

    let max = 0;

    for (let i = 0; i < line.length - 1; i++) {
      const number = line[i];

      for (let j = i + 1; j < line.length; j++) {
        const candidate = Number(number + line[j]);

        if (candidate > max) {
          max = candidate;
        }
      }
    }

    return max;
  }

  const processLines = lines.reduce((totalJoltage, line) => {
    return totalJoltage + maxPairsForLine(line);
  }, 0);

  return `Total Joltage: ${processLines}`;
};

const solverPart2: SolverFunction = (input) => {
  const lines = getLines(input);

  const maxValueForLine = (line: string, requiredLength: number): number => {
    const currentValue: string[] = [];
    let dropsLeft = line.length - requiredLength;

    for (let i = 0; i < line.length; i++) {
      const currentDigit = line[i];

      while (dropsLeft > 0 &&
        currentValue.length > 0 &&
        currentValue[currentValue.length - 1] < currentDigit) {
        currentValue.pop();
        dropsLeft--;
      }

      currentValue.push(currentDigit);
    }

    return Number(currentValue.slice(0, requiredLength).join(''));
  }

  const processLines = lines.reduce((totalJoltage, line) => {
    return totalJoltage + maxValueForLine(line, 12);
  }, 0);

  return `Total Joltage: ${processLines}`;
};


function Day3() {
  return (
    <DayTemplate
      title="Day 3: Lobby"
      description="Find the maximum joltage for each bank."
      solverPart1={solverPart1}
      solverPart2={solverPart2}
    />
  );
}

export { Day3 };