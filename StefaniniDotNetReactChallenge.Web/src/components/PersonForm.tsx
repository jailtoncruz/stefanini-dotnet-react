import { Form } from "radix-ui";
import { InputField } from "./UI/InputField";
import type { IPerson } from "../types/interfaces/person";
import { useState, type ReactNode } from "react";
import { Callout } from "@radix-ui/themes";
import { AlertTriangle } from "lucide-react";
import { useApiVersionQuery } from "../hooks/useApiVersion";
import { cpfMask } from "../utils/cpfMask";
import { dateMask } from "../utils/dateMask";
import z, { ZodError } from "zod";
import { cpfSchema } from "../utils/CPFSchema";
import type { ICreatePersonDto } from "../types/dto/create-person.dto";
import { parse } from "date-fns";
import { EMPTY_PERSON } from "../services/constants/person";

interface PersonFormProps {
  children: ReactNode;
  defaultValue?: IPerson;
  handleSubmit: (person: ICreatePersonDto) => void;
}

export function PersonForm({
  children,
  defaultValue,
  handleSubmit,
}: PersonFormProps) {
  const [person, setPerson] = useState<IPerson>(
    defaultValue
      ? {
          ...defaultValue,
          birthDay: defaultValue.birthDay.split("-").reverse().join("/"),
        }
      : EMPTY_PERSON
  );
  const [errorMessage, setErrorMessage] = useState<string>("");
  const { data: apiVersion } = useApiVersionQuery();

  const emailSchema =
    apiVersion === "v1"
      ? z.email("Email Inválido").optional()
      : z.email("Email Inválido").nonoptional();

  const personSchema = z.object({
    email: emailSchema,

    name: z.string("Nome é obrigatório"),
    birthDay: z.coerce
      .date("Data Inválida")
      .nonoptional("Data de nascimento é obrigatória"),

    cpf: cpfSchema.nonoptional("CPF é obrigatório"),
    nationality: z.string().optional(),
    placeOfBirth: z.string().optional(),
    gender: z.string().optional(),
  });

  const handleInputChange = ({
    name,
    value,
  }: {
    name: string;
    value: string | number | Date | undefined;
  }) => {
    setErrorMessage("");
    setPerson({
      ...person,
      [name]: value,
    });
  };

  const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.stopPropagation();
    e.preventDefault();

    try {
      const dto: ICreatePersonDto = personSchema.parse({
        ...person,
        birthDay:
          person.birthDay !== ""
            ? parse(dateMask(person.birthDay), "dd/MM/yyyy", new Date())
            : undefined,
        email: person.email !== "" ? person.email : undefined,
      });

      handleSubmit(dto);
      setPerson(EMPTY_PERSON);
    } catch (_err) {
      const err = _err as ZodError;
      const messages = JSON.parse(err.message) as {
        message: string;
      }[];

      setErrorMessage(messages.map((m) => m.message).join(", "));
    }
  };

  return (
    <Form.Root className="mt-4 flex flex-col gap-2" onSubmit={onSubmit}>
      {errorMessage && (
        <Callout.Root variant="surface" color="red">
          <Callout.Icon>
            <AlertTriangle size={20} />
          </Callout.Icon>
          <Callout.Text>{errorMessage}</Callout.Text>
        </Callout.Root>
      )}

      <div className="flex flex-row gap-2 items-start flex-1">
        <InputField
          placeholder="Nome"
          label="Nome"
          type="text"
          name="name"
          value={person.name}
          onChange={handleInputChange}
          required
          className="flex-1"
        />
        <InputField
          placeholder="Sexo Biológico"
          label="Sexo Biológico"
          type="text"
          name="gender"
          value={person.gender}
          onChange={handleInputChange}
        />
      </div>

      <div className="flex flex-row gap-2 items-start flex-1">
        <InputField
          type="email"
          name="email"
          label="E-Mail"
          placeholder="E-mail"
          value={person.email}
          onChange={handleInputChange}
          required={apiVersion === "v2"}
          className="flex-1"
        />
        <InputField
          type="text"
          name="cpf"
          label="CPF"
          placeholder="CPF"
          value={cpfMask(person.cpf)}
          onChange={handleInputChange}
          required
        />
      </div>

      <div className="flex flex-row gap-2 items-start">
        <InputField
          type="text"
          name="nationality"
          placeholder="Nacionalidade"
          label="Nacionalidade"
          value={person.nationality}
          onChange={handleInputChange}
          className="flex-1"
        />

        <InputField
          type="text"
          name="placeOfBirth"
          value={person.placeOfBirth}
          onChange={handleInputChange}
          placeholder="Naturalidade"
          label="Naturalidade"
          className="flex-1"
        />

        <InputField
          type="text"
          name="birthDay"
          label="Data de nascimento"
          placeholder="dd/mm/yyyy"
          value={dateMask(person.birthDay)}
          required
          onChange={(e) =>
            setPerson({
              ...person,
              birthDay: e.value as string,
            })
          }
        />
      </div>

      {children}
    </Form.Root>
  );
}
