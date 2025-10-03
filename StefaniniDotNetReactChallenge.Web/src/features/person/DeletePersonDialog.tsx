import { Button, Dialog, Flex, Spinner } from "@radix-ui/themes";
import { useRef } from "react";
import { toast } from "react-toastify";
import { useMutation, useQueryClient } from "@tanstack/react-query";

import type { IPerson } from "../../types/interfaces/person";
import type { ReactNode } from "react";
import { deletePerson } from "../../services/api/person/delete-person";

interface DeletePersonDialogProps {
  children: ReactNode;
  person: IPerson;
}

export function DeletePersonDialog({
  children,
  person,
}: DeletePersonDialogProps) {
  const closeButtonRef = useRef<HTMLButtonElement>(null);
  const client = useQueryClient();
  const mutation = useMutation({
    mutationFn: deletePerson,
    onSuccess: () => {
      client.invalidateQueries({ queryKey: ["person"] });
      toast("Cadastro removido com sucesso.");
      closeButtonRef.current?.click();
    },
  });

  return (
    <Dialog.Root>
      <Dialog.Trigger>{children}</Dialog.Trigger>

      <Dialog.Content maxWidth="450px">
        <Dialog.Title mb="0">Remover {person.name}?</Dialog.Title>
        <Dialog.Description size="2" mb="4">
          Tem certeza que deseja remover {person.name}?
        </Dialog.Description>

        <Flex gap="2" justify="end">
          <Dialog.Close>
            <Button variant="soft" color="gray" ref={closeButtonRef}>
              Fechar
            </Button>
          </Dialog.Close>

          <Button
            variant="solid"
            color="red"
            onClick={() => mutation.mutate(person.id)}
            disabled={mutation.isPending}
          >
            {mutation.isPending ? <Spinner></Spinner> : "Deletar"}
          </Button>
        </Flex>
      </Dialog.Content>
    </Dialog.Root>
  );
}
