import type { ICreatePersonDto } from "./create-person.dto";

export interface IUpdatePersonDto extends ICreatePersonDto {
  id: number;
}
