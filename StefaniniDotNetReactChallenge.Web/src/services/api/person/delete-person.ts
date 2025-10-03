import { api } from "..";

export async function deletePerson(id: number) {
  await api.delete<void>(`/person/${id}`);
}
