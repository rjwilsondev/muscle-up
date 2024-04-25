import { useQuery } from "@tanstack/react-query";
import { getWorkoutsQueryOptions } from "./queries";

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
      <ul>
        {getWorkoutsQuery.isSuccess &&
          getWorkoutsQuery.data.map((workout) => (
            <li>{JSON.stringify(workout)}</li>
          ))}
      </ul>
    </main>
  );
}
