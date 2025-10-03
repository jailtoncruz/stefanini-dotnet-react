import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { HomeLayout } from "./layouts/home-layout";
import { Flip, ToastContainer } from "react-toastify";
import { Home } from "./pages/Home";

const queryClient = new QueryClient();

export function App() {
  return (
    <div className="flex h-dvh flex-col max-h-dvh">
      <QueryClientProvider client={queryClient}>
        <HomeLayout>
          <Home />
        </HomeLayout>
        <ToastContainer theme="dark" transition={Flip} />
      </QueryClientProvider>
    </div>
  );
}
