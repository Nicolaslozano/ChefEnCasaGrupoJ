import React, { useState, useEffect } from "react";
import {
  Input,
  Spinner,
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Pagination,
  Card,
} from "@nextui-org/react";

const columns = [
  {
    key: "User",
    label: "Usuario",
  },
  {
    key: "Popular",
    label: "Popularidad",
  },
];

export default function TablaGeneral() {
  const itemsPerPage = 10;
  const [currentPage, setCurrentPage] = useState(1);
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      try {
        const response = await fetch(
          `https://localhost:44323/api/Usuarios/GetUsuarioPopular`
        );
        const userData = await response.json();
        setData(userData);
        setIsLoading(false);
      } catch (error) {
        console.error("Error al cargar los datos:", error);
        setIsLoading(false);
      }
    };

    fetchData();
  }, []);

  const filterData = (data, searchTerm) => {
    return data.filter((item) =>
      Object.keys(item).some((key) =>
        String(item[key]).toLowerCase().includes(searchTerm.toLowerCase())
      )
    );
  };

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const filteredData = filterData(data, searchTerm);
  const paginatedRows = filteredData.slice(startIndex, endIndex);

  return (
    <Card className="p-4">
      <div style={{ width: "100%" }}>
        <Input
          className="my-2"
          placeholder="Buscar usuario"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        {isLoading ? (
          <Spinner
            className="flex justify-center "
            label="Cargando Datos..."
            color="primary"
            size="lg"
          />
        ) : (
          <Table aria-label="Tabla de usuarios">
            <TableHeader columns={columns}>
              {(column) => (
                <TableColumn key={column.key}>{column.label}</TableColumn>
              )}
            </TableHeader>
            <TableBody items={paginatedRows}>
              {(item) => (
                <TableRow key={item.Idusuario}>
                  {(columnKey) => (
                    <TableCell key={`${item.Idusuario}-${columnKey}`}>
                      {item[columnKey]}
                    </TableCell>
                  )}
                </TableRow>
              )}
            </TableBody>
          </Table>
        )}

        <Pagination
          className="my-2 flex justify-center"
          total={filteredData.length}
          showShadow
          page={currentPage}
          onChange={(newPage) => setCurrentPage(newPage)}
        />
      </div>
    </Card>
  );
}
