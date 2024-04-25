import { queryOptions } from "@tanstack/react-query";
import axios from "axios";

const restClient = axios.create({
  baseURL: "https://localhost:5151",
});

export const getWorkoutsQueryOptions = () =>
  queryOptions({
    queryKey: ["Get All Workouts"],
    queryFn: async () => {
      const response = await restClient.get<Array<Workout>>("/workouts");
      return response.data;
    },
  });

type Workout = {
  startDate: string;
  endDate: string | null;
  exercises: Array<unknown>;
};
