import { api } from "..";

interface AuthToken {
  token: string;
}

export async function authenticate(username: string) {
  const { data } = await api.post<AuthToken>("/auth/login", { username });
  return data;
}
