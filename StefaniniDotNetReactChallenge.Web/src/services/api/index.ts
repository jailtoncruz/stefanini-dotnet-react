import Axios, { AxiosError } from "axios";
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

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("authToken");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error: AxiosError) => {
    if (error.response) {
      const { status } = error.response;
      if (status === 401 || status === 403) {
        localStorage.removeItem("authToken");

        if (typeof window !== "undefined") {
          window.location.assign("/welcome");
        }
      }
    }
    return Promise.reject(error);
  }
);
