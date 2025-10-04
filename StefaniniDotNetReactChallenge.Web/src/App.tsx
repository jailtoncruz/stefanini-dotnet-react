import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { HomeLayout } from "./layouts/home-layout";
import { Flip, ToastContainer } from "react-toastify";
import { HomeRoutes } from "./pages/Home.Routes";

const queryClient = new QueryClient();

export function App() {
  return (
    <div className="flex h-dvh flex-col max-h-dvh">
      <QueryClientProvider client={queryClient}>
        <HomeLayout>
          <HomeRoutes />
        </HomeLayout>
        <ToastContainer theme="dark" transition={Flip} />
      </QueryClientProvider>
    </div>
  );
}
