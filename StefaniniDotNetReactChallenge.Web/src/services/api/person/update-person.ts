import { format } from "date-fns";
import { api } from "..";
import type { IUpdatePersonDto } from "../../../types/dto/update-person.dto";
import type { IPerson } from "../../../types/interfaces/person";

export async function updatePerson(dto: IUpdatePersonDto) {
  const { data } = await api.put<IPerson>(`/person/${dto.id}`, {
    ...dto,
    birthDay: format(dto.birthDay, "yyyy-MM-dd"),
  });
  return data;
}
