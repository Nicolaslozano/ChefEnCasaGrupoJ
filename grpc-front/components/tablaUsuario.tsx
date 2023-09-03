import React, { useState } from "react";
import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  getKeyValue,
  Pagination,
  Button,
  Dropdown,
  DropdownTrigger,
  DropdownMenu,
  DropdownItem,
} from "@nextui-org/react";
import { ActionsIcon} from "./icons";


const rows = [
  {
    key: "1",
    name: "test",
    role: "test",
    status: "test",
  },
  {
    key: "2",
    name: "test",
    role: "test",
    status: "test",
  },
  {
    key: "3",
    name: "test",
    role: "test",
    status: "test",
  },
  {
    key: "4",
    name: "test",
    role: "test",
    status: "test",
  },
];

const columns = [
  {
    key: "name",
    label: "Receta",
  },
  {
    key: "role",
    label: "Categoria",
  },
  {
    key: "status",
    label: "Autor",
  },
  {
    key: "actions",
    label: "Acciones",
  },
];

export default function tablaUsuario() {
  const itemsPerPage = 2;
  const [currentPage, setCurrentPage] = useState(1);

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const paginatedRows = rows.slice(startIndex, endIndex);

  const handleView = (item:any) => {
    // Falta implementar el Ver Receta
    alert(`Viewing: ${item.name}`);
  };

  const handleEdit = (item:any) => {
    //Falta implementar el UPDATE
    alert(`Editing: ${item.name}`);
  };

  const handleDelete = (item:any) => {
    //Falta implementar el borrado
    alert(`Deleting: ${item.name}`);
  };

  return (
    <div style={{ width: "100%" }}>
      <Table aria-label="Tabla de recetas">
        <TableHeader columns={columns}>
          {(column) => <TableColumn key={column.key}>{column.label}</TableColumn>}
        </TableHeader>
        <TableBody items={paginatedRows}>
          {(item) => (
            <TableRow key={item.key}>
              {(columnKey) => (
                <TableCell>
                  {columnKey === "actions" ? (
                    <Dropdown>
                      <DropdownTrigger>
                      <Button isIconOnly radius="full" size="sm" variant="light">
                    <ActionsIcon className="text-default-400" />
                        </Button>
                      </DropdownTrigger>
                      <DropdownMenu>
                        <DropdownItem onClick={() => handleView(item)}>Ver</DropdownItem>
                        <DropdownItem onClick={() => handleEdit(item)}>Editar</DropdownItem>
                        <DropdownItem onClick={() => handleDelete(item)}>Eliminar</DropdownItem>
                      </DropdownMenu>
                    </Dropdown>
                  ) : (
                    getKeyValue(item, columnKey)
                  )}
                </TableCell>
              )}
            </TableRow>
          )}
        </TableBody>
      </Table>

      <Pagination
        className="my-2 flex justify-center"
        total={rows.length}
        showControls
        showShadow
        page={currentPage}
        onChange={(newPage) => setCurrentPage(newPage)}
      />
    </div>
  );
}
