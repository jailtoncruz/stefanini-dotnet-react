import { Button, Dialog, Flex } from "@radix-ui/themes";
import { useRef } from "react";
import { useQueryClient } from "@tanstack/react-query";

import type { ReactNode } from "react";
import { useUpdatePersonMutation } from "../../hooks/usePerson";
import { toast } from "react-toastify";
import { PersonForm } from "../../components/PersonForm";
import type { IPerson } from "../../types/interfaces/person";
import type { ICreatePersonDto } from "../../types/dto/create-person.dto";

interface UpdatePersonDialogProps {
  children: ReactNode;
  person: IPerson;
}

export function UpdatePersonDialog({
  person,
  children,
}: UpdatePersonDialogProps) {
  console.log(person);
  const closeButtonRef = useRef<HTMLButtonElement>(null);
  const client = useQueryClient();

  const mutation = useUpdatePersonMutation();

  const handleSubmit = (dto: ICreatePersonDto) => {
    mutation.mutate(
      {
        ...dto,
        id: person.id,
      },
      {
        onSuccess: () => {
          toast("Cadastro atualizado com sucesso");
          client.invalidateQueries({ queryKey: ["person"] });
          closeButtonRef.current?.click();
        },
      }
    );
  };

  return (
    <Dialog.Root>
      <Dialog.Trigger>{children}</Dialog.Trigger>

      <Dialog.Content maxWidth="650px">
        <Dialog.Title mb="0">Atualização cadastral</Dialog.Title>
        <Dialog.Description size="2" mb="4">
          Atualização do cadastro de {person.name}
        </Dialog.Description>

        <PersonForm defaultValue={person} handleSubmit={handleSubmit}>
          <Flex gap="2" justify="end" mt="4">
            <Dialog.Close>
              <Button variant="soft" color="gray" ref={closeButtonRef}>
                Fechar
              </Button>
            </Dialog.Close>
            <Button type="submit">Save</Button>
          </Flex>
        </PersonForm>
      </Dialog.Content>
    </Dialog.Root>
  );
}
