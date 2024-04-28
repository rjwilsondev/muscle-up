import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Workout, getWorkoutsQueryOptions, restClient } from "./queries";
import { format } from "date-fns";
import { AddWorkout } from "./AddWorkout";
import { Trash2Icon } from "lucide-react";

const WorkoutListItem = ({ workout }: { workout: Workout }) => {
  const startDateFormatted = format(workout.startDate, "EEEE MMMM d yyyy ");
  const queryClient = useQueryClient();

  const deleteWorkoutMutation = useMutation({
    mutationFn: () => restClient.delete(`/workouts/${workout.id}`),
    onSuccess: async () =>
      await queryClient.invalidateQueries(getWorkoutsQueryOptions()),
  });

  return (
    <div className={"h-20 border py-2 px-3 relative group data-[state=done]:bg-white data-[state=pending]:bg-gray-200 " } data-state={!!workout.endDate ? "done" : "pending"} >
      <button className="flex items-start justify-start text-left rounded-md w-full h-full ">
        <div>{!!workout.endDate ? "" : "In Progress"}</div>
        <div className="flex-1">{startDateFormatted}</div>
        <span className="absolute bottom-2 right-3 text-gray-500 text-sm ">
          #{workout.id}
        </span>
      </button>
      <button className="h-8 w-8 text-center grid place-content-center  group-hover:visible invisible absolute top-1 right-1 hover:bg-sky-100 ">
        <Trash2Icon
          size={18}
          className="stroke-gray-800"
          onClick={() => deleteWorkoutMutation.mutate()}
        />
      </button>
    </div>
  );
};

export function WorkoutCrudPage() {
  const getWorkoutsQuery = useQuery(getWorkoutsQueryOptions());

  const message = getWorkoutsQuery.isPending
    ? "Loading"
    : getWorkoutsQuery.isError
    ? "An error has occurred."
    : "Workouts loaded.";

  return (
    <main>
      <h1 className="text-2xl font-bold ">Hello World!! With Tailwind!</h1>
      <p>Workouts</p>
      <div>{message}</div>
      <AddWorkout />
      <ul className="max-w-xl mx-auto space-y-2 ">
        {getWorkoutsQuery.isSuccess &&
          getWorkoutsQuery.data.map((workout) => (
            <li key={workout.id}>
              <WorkoutListItem workout={workout} />
            </li>
          ))}
      </ul>
    </main>
  );
}
