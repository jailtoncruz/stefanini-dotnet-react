import { api } from "..";
import type { IPerson } from "../../../types/interfaces/person";

export async function listPeople() {
  const { data } = await api.get<IPerson[]>("/person");
  return data;
}
