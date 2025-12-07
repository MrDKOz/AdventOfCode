import { DayTemplate } from "../components/DayTemplate";
import type { SolverFunction } from "../types/SolverFunction";
import { getLines } from "../utils/input";

const solverPart1: SolverFunction = (input) => {
  const lines = getLines(input);
  const productIds = lines[0].split(",").map(id => id.trim());

  const isRepeatedTwice = (id: number): boolean => {
    const idStr = id.toString();
    if (idStr.length % 2 !== 0) return false;
    const half = idStr.length / 2;
    return idStr.slice(0, half) === idStr.slice(half);
  }

  const sumInvalidIdsInRange = (range: string): number => {
    const [start, end] = range.split("-").map(Number);
    let sum = 0;

    for (let i = start; i <= end; i++) {
      if (isRepeatedTwice(i)) {
        sum += i;
      }
    }

    return sum;
  };

  const result = productIds.reduce((total, rangeToCheck) => {
    return total + sumInvalidIdsInRange(rangeToCheck);
  }, 0);

  return `Sum of invalid product IDs: ${result}`;
};

const solverPart2: SolverFunction = (input) => {
  const lines = getLines(input);
  const productIds = lines[0].split(",").map(id => id.trim());

  const containsRepeatedPatterns = (id: number): boolean => {
    const idStr = id.toString();
    
    for (let i = 0; i < idStr.length - 1; i++) {
      const tmp = idStr.slice(0, i + 1);
      let allMatch = true;

      if (idStr.length % tmp.length !== 0) continue; // Ensure chunk fits evenly
      if (idStr.length / tmp.length < 2) continue;   // Ensure at least two repetitions

      for (let j = tmp.length; j < idStr.length; j += tmp.length) {
        const compareSlice = idStr.slice(j, j + tmp.length);
        if (compareSlice !== tmp) {
          allMatch = false;
          break;
        }
      }
      if (allMatch) return true;
    }

    return false;
  }

  const sumInvalidIdsInRange = (range: string): number => {
    const [start, end] = range.split("-").map(Number);
    let sum = 0;

    for (let i = start; i <= end; i++) {
      if (containsRepeatedPatterns(i)) {
        sum += i;
      }
    }

    return sum;
  };

  const result = productIds.reduce((total, rangeToCheck) => {
    return total + sumInvalidIdsInRange(rangeToCheck);
  }, 0);

  return `Sum of invalid product IDs: ${result}`;
};


function Day2() {
  return (
    <DayTemplate
      title="Day 2: Gift Shop"
      description="Finding invalid product IDs."
      solverPart1={solverPart1}
      solverPart2={solverPart2}
    />
  );
}

export { Day2 };