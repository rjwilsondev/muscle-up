import { useMutation } from "@tanstack/react-query";
import { restClient } from "./queries";

export function AddWorkout() {
  const startWorkoutMutation = useMutation({
    mutationKey: ["Start Workout"],
    mutationFn: () =>
      restClient.post("/workouts", {
        id: 0,
        startDate: (new Date()).toISOString(),
        endDate: null,
      }),
  });

  return (
    <div>
      <button
        className="h-11 bg-gray-200 hover:bg-gray-300/90 py-1 px-3 rounded-md"
        onClick={() => startWorkoutMutation.mutate()}
      >
        Start Workout
      </button>
    </div>
  );
}
