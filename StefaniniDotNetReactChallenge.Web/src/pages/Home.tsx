import {
  Button,
  IconButton,
  ScrollArea,
  Spinner,
  Table,
} from "@radix-ui/themes";
import { DeletePersonDialog } from "../features/person/DeletePersonDialog";
import { Edit, PlusIcon, Trash2 } from "lucide-react";
import { usePersonQuery } from "../hooks/usePerson";
import { cpfMask } from "../utils/cpfMask";
import { CreatePersonDialog } from "../features/person/CreatePersonDialog";
import { UpdatePersonDialog } from "../features/person/UpdatePersonDialog";

export function Home() {
  const { data, isLoading } = usePersonQuery();

  if (isLoading)
    return (
      <div className="flex flex-col items-center justify-center flex-1">
        <Spinner></Spinner>
      </div>
    );

  return (
    <div className="my-4 flex flex-col flex-1">
      <div className="flex flex-row justify-end mb-4">
        <CreatePersonDialog>
          <Button variant="soft">
            <PlusIcon /> Adicionar
          </Button>
        </CreatePersonDialog>
      </div>

      <ScrollArea
        type="hover"
        scrollbars="both"
        className="flex-1 max-h-[60vh] md:max-h-[80vh]"
      >
        <Table.Root size="2">
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
                <Table.Cell className="min-w-36">
                  {cpfMask(person.cpf)}
                </Table.Cell>
                <Table.Cell>{person.nationality}</Table.Cell>
                <Table.Cell>{person.placeOfBirth}</Table.Cell>
                <Table.Cell>
                  {person.birthDay.split("-").reverse().join("/")}
                </Table.Cell>
                <Table.Cell className="w-16" style={{ paddingRight: 0 }}>
                  <div className="flex flex-row gap-1">
                    <UpdatePersonDialog person={person}>
                      <IconButton
                        className="cursor-pointer"
                        variant="soft"
                        color="blue"
                      >
                        <Edit size={16} />
                      </IconButton>
                    </UpdatePersonDialog>

                    <DeletePersonDialog person={person}>
                      <IconButton
                        className="cursor-pointer"
                        variant="soft"
                        color="red"
                      >
                        <Trash2 size={16} />
                      </IconButton>
                    </DeletePersonDialog>
                  </div>
                </Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>
        </Table.Root>
      </ScrollArea>
    </div>
  );
}
