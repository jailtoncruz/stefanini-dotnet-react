import { format } from "date-fns";
import { api } from "..";
import type { ICreatePersonDto } from "../../../types/dto/create-person.dto";
import type { IPerson } from "../../../types/interfaces/person";

export async function createPerson(dto: ICreatePersonDto) {
  const { data } = await api.post<IPerson>("/person", {
    ...dto,
    birthDay: format(dto.birthDay, "yyyy-MM-dd"),
  });
  return data;
}
