import {
  Button,
  IconButton,
  ScrollArea,
  Spinner,
  Table,
  TextField,
} from "@radix-ui/themes";
import { DeletePersonDialog } from "../features/person/DeletePersonDialog";
import { PlusIcon, Search, Trash2 } from "lucide-react";
import { usePersonQuery } from "../hooks/usePerson";
import { useState } from "react";
import { cpfMask } from "../utils/cpfMask";
import { format } from "date-fns";
import { CreatePersonDialog } from "../features/person/CreatePersonDialog";

export function Home() {
  const { data, isLoading } = usePersonQuery();
  const [searchName, setSearchName] = useState<string>("");

  if (isLoading)
    return (
      <div className="flex flex-col items-center justify-center">
        <Spinner></Spinner>
      </div>
    );

  return (
    <div className="my-4">
      <div className="flex flex-row justify-between mb-4">
        <TextField.Root
          placeholder="Pesquisa por nome"
          value={searchName}
          onChange={(e) => setSearchName(e.target.value)}
        >
          <TextField.Slot>
            <Search size={16} />
          </TextField.Slot>
        </TextField.Root>

        <CreatePersonDialog>
          <Button variant="soft">
            <PlusIcon /> Adicionar
          </Button>
        </CreatePersonDialog>
      </div>

      <div className="flex-1 overflow-hidden">
        <ScrollArea type="hover" scrollbars="vertical" className="h-full">
          <div className="flex flex-col gap-2 flex-1">
            <Table.Root size="3">
              <Table.Header>
                <Table.Row>
                  <Table.ColumnHeaderCell>ID</Table.ColumnHeaderCell>
                  <Table.ColumnHeaderCell>Nome</Table.ColumnHeaderCell>
                  <Table.ColumnHeaderCell>Email</Table.ColumnHeaderCell>
                  <Table.ColumnHeaderCell>CPF</Table.ColumnHeaderCell>
                  <Table.ColumnHeaderCell>Nacionalidade</Table.ColumnHeaderCell>
                  <Table.ColumnHeaderCell>Naturalidade</Table.ColumnHeaderCell>
                  <Table.ColumnHeaderCell>
                    Data de nascimento
                  </Table.ColumnHeaderCell>
                  <Table.ColumnHeaderCell></Table.ColumnHeaderCell>
                </Table.Row>
              </Table.Header>

              <Table.Body>
                {data?.map((person) => (
                  <Table.Row key={person.id}>
                    <Table.RowHeaderCell>{person.id}</Table.RowHeaderCell>
                    <Table.Cell>{person.name}</Table.Cell>
                    <Table.Cell>{person.email}</Table.Cell>
                    <Table.Cell>{cpfMask(person.cpf)}</Table.Cell>
                    <Table.Cell>{person.nationality}</Table.Cell>
                    <Table.Cell>{person.placeOfBirth}</Table.Cell>
                    <Table.Cell>
                      {format(person.birthDay, "dd/MM/yyyy")}
                    </Table.Cell>
                    <Table.Cell className="w-16">
                      <DeletePersonDialog
                        person={{
                          name: "string",
                          birthDay: new Date(),
                          cpf: "string",
                          gender: "string",
                          nationality: "string",
                          placeOfBirth: "string",
                          email: "user@example.com",
                          id: 12,
                        }}
                      >
                        <IconButton
                          className="cursor-pointer"
                          variant="soft"
                          color="red"
                        >
                          <Trash2 size={16} />
                        </IconButton>
                      </DeletePersonDialog>
                    </Table.Cell>
                  </Table.Row>
                ))}
              </Table.Body>
            </Table.Root>
          </div>
        </ScrollArea>
      </div>
    </div>
  );
}
