import type { ICreatePersonDto } from "./create-person.dto";

export interface IUpdatePersonDto extends Partial<ICreatePersonDto> {}
