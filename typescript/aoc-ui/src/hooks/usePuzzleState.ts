import { useState } from "react";
import type { SolverFunction } from "../types/SolverFunction";

interface UsePuzzleStateArgs {
  solverPart1: SolverFunction;
  solverPart2: SolverFunction;
}

function usePuzzleState({ solverPart1, solverPart2 }: UsePuzzleStateArgs) {
  const [input, setInput] = useState("");
  const [outputPart1, setOutputPart1] = useState<string | null>(null);
  const [outputPart2, setOutputPart2] = useState<string | null>(null);
  const [errorPart1, setErrorPart1] = useState<string | null>(null);
  const [errorPart2, setErrorPart2] = useState<string | null>(null);

  const runPart1 = () => {
    try {
      setErrorPart1(null);
      const result = solverPart1(input);
      setOutputPart1(result);
    } catch (e) {
      console.error(e);
      setOutputPart1(null);
      setErrorPart1(e instanceof Error ? e.message : "Unknown error");
    }
  };

  const runPart2 = () => {
    try {
      setErrorPart2(null);
      const result = solverPart2(input);
      setOutputPart2(result);
    } catch (e) {
      console.error(e);
      setOutputPart2(null);
      setErrorPart2(e instanceof Error ? e.message : "Unknown error");
    }
  };

  const clear = () => {
    setInput("");
    setOutputPart1(null);
    setOutputPart2(null);
    setErrorPart1(null);
    setErrorPart2(null);
  };

  return {
    input,
    setInput,
    outputPart1,
    outputPart2,
    errorPart1,
    errorPart2,
    runPart1,
    runPart2,
    clear,
  };
}

export { usePuzzleState };