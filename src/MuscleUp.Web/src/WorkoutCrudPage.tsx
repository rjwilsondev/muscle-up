import { useQuery } from "@tanstack/react-query";
import { Workout, getWorkoutsQueryOptions } from "./queries";
import { format } from "date-fns";
import { AddWorkout } from "./AddWorkout";
import { Trash2Icon } from "lucide-react";

const WorkoutListItem = ({ workout }: { workout: Workout }) => {
  const startDateFormatted = format(workout.startDate, "EEEE MMMM d yyyy ");
  return (
    <button className=" text-left h-20 border py-2 px-3 flex relative rounded-md w-full group">
      <div>{!!workout.endDate ? "" : "In Progress"}</div>
      <div>{startDateFormatted}</div>
      <span className="absolute bottom-2 right-3 text-gray-500 text-sm ">
        #{workout.id}
      </span>
      <button className="h-8 w-8 text-center grid place-content-center  group-hover:visible invisible absolute top-1 right-1 hover:bg-sky-100 ">
        <Trash2Icon size={18} className="stroke-gray-800" />
      </button>
    </button>
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
