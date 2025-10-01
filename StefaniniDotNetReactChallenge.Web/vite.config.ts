import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      "/api": {
        target: "https://localhost:7009", // .NET API URL
        changeOrigin: true,
        secure: false,
      },
    },
  },

  base: "./",
  build: {
    outDir: "../StefaniniDotNetReactChallenge.API/wwwroot",
    emptyOutDir: true,
  },
});
