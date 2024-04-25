import "./App.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { WorkoutCrudPage } from "./WorkoutCrudPage";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 0,
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <WorkoutCrudPage />
    </QueryClientProvider>
  );
}

export default App;
