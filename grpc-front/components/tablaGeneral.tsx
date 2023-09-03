import React, { useState } from "react";
import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Pagination,
} from "@nextui-org/react";

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
];

export default function TablaGeneral() {
  const itemsPerPage = 2;
  const [currentPage, setCurrentPage] = useState(1);

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const paginatedRows = rows.slice(startIndex, endIndex);

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
        <TableCell key={item.key}>
          {(item as any)[columnKey]}
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
