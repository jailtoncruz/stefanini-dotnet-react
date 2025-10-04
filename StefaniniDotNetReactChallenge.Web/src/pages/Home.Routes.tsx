import { Routes, Route, BrowserRouter } from "react-router-dom";
import { Welcome } from "./Welcome";
import { Home } from "./Home";

export function HomeRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/welcome" element={<Welcome />} />
      </Routes>
    </BrowserRouter>
  );
}
