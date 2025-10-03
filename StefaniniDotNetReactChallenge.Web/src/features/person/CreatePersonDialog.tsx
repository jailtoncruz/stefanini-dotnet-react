import { Button, Dialog, Flex, Select } from "@radix-ui/themes";
import { useRef, useState } from "react";
import { toast } from "react-toastify";
import { useQueryClient } from "@tanstack/react-query";

import type { ReactNode } from "react";
import type { ICreatePersonDto } from "../../types/dto/create-person.dto";
import { Form } from "radix-ui";

interface CreatePersonDialogProps {
  children: ReactNode;
}

export function CreatePersonDialog({ children }: CreatePersonDialogProps) {
  const closeButtonRef = useRef<HTMLButtonElement>(null);
  const client = useQueryClient();

  const [person, setPerson] = useState<ICreatePersonDto>({
    birthDay: new Date(),
    cpf: "",
    email: "",
    gender: "no-selected",
    name: "",
    nationality: "",
    placeOfBirth: "",
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPerson({
      ...person,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async () => {
    toast("Cadastro realizado com sucesso");
    client.invalidateQueries({ queryKey: ["person"] });
    closeButtonRef.current?.click();
    console.log(person);
  };

  return (
    <Dialog.Root>
      <Dialog.Trigger>{children}</Dialog.Trigger>

      <Dialog.Content maxWidth="650px">
        <Dialog.Title mb="0">Novo registro</Dialog.Title>
        <Dialog.Description size="2" mb="4">
          Criando um novo registro de pessoa
        </Dialog.Description>

        <Form.Root
          className="mt-4 flex flex-col gap-2"
          onSubmit={(e) => {
            e.stopPropagation();
            e.preventDefault();
            handleSubmit();
          }}
        >
          <div className="flex flex-row gap-4 items-center flex-1">
            <Form.Field name="name" className="flex flex-col flex-1">
              <Form.Label className="text-sm font-medium mb-1">Nome</Form.Label>
              <Form.Control asChild>
                <input
                  type="text"
                  name="name"
                  value={person.name}
                  onChange={handleInputChange}
                  required
                  placeholder="Nome"
                  className="p-2 border border-gray-500 rounded focus:outline-none"
                />
              </Form.Control>
            </Form.Field>

            <Form.Field name="gender" className="flex flex-col">
              <Form.Label className="text-sm font-medium mb-1">
                Sexo Biológico
              </Form.Label>
              <Form.Control asChild>
                <Select.Root
                  size="3"
                  defaultValue="no-selected"
                  onValueChange={(v) => setPerson((p) => ({ ...p, gender: v }))}
                  value={person.gender}
                >
                  <Select.Trigger />
                  <Select.Content>
                    <Select.Item value="no-selected" disabled>
                      Selecione
                    </Select.Item>
                    <Select.Item value="male">Masculino</Select.Item>
                    <Select.Item value="female">Feminino</Select.Item>
                    <Select.Item value="no-informed">Não Informar</Select.Item>
                    <Select.Item value="other">Outro</Select.Item>
                  </Select.Content>
                </Select.Root>
              </Form.Control>
            </Form.Field>
          </div>

          <Form.Field name="email" className="flex flex-col flex-1">
            <Form.Label className="text-sm font-medium mb-1">E-mail</Form.Label>
            <Form.Control asChild>
              <input
                type="email"
                name="email"
                value={person.email}
                onChange={handleInputChange}
                required
                placeholder="E-mail"
                className="p-2 border border-gray-500 rounded focus:outline-none"
              />
            </Form.Control>
          </Form.Field>

          <div className="flex flex-row gap-4 items-center">
            <Form.Field name="nationality" className="flex flex-col flex-1">
              <Form.Label className="text-sm font-medium mb-1">
                Nacionalidade
              </Form.Label>
              <Form.Control asChild>
                <input
                  type="text"
                  name="nationality"
                  value={person.nationality}
                  onChange={handleInputChange}
                  required
                  placeholder="Nacionalidade"
                  className="p-2 border border-gray-500 rounded focus:outline-none"
                />
              </Form.Control>
            </Form.Field>
            <Form.Field name="placeOfBirth" className="flex flex-col flex-1">
              <Form.Label className="text-sm font-medium mb-1">
                Naturalidade
              </Form.Label>
              <Form.Control asChild>
                <input
                  type="text"
                  name="placeOfBirth"
                  value={person.placeOfBirth}
                  onChange={handleInputChange}
                  required
                  placeholder="Naturalidade"
                  className="p-2 border border-gray-500 rounded focus:outline-none"
                />
              </Form.Control>
            </Form.Field>
          </div>
          <Flex gap="2" justify="end" mt="4">
            <Dialog.Close>
              <Button variant="soft" color="gray" ref={closeButtonRef}>
                Fechar
              </Button>
            </Dialog.Close>

            <Button type="submit">Save</Button>
          </Flex>
        </Form.Root>
      </Dialog.Content>
    </Dialog.Root>
  );
}
