import Axios from "axios";
import { getApiVersion } from "../../hooks/useApiVersion";

export const api = Axios.create({
  baseURL: `/api/${getApiVersion()}`,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("authToken");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});
