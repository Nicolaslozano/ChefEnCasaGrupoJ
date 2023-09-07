import React, { useState, useEffect } from "react";
import { Spinner, Input } from "@nextui-org/react";
import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Pagination,
} from "@nextui-org/react";

const columns = [
  {
    key: "Titulo",
    label: "Receta",
  },
  {
    key: "TiempoPreparacion",
    label: "Tiempo de Preparación",
  },
  {
    key: "Ingredientes",
    label: "Ingredientes",
  },
  {
    key: "Pasos",
    label: "Pasos",
  },
  {
    key: "UsuarioIdusuario",
    label: "Autor",
  },
  {
    key: "NombreCategoria",
    label: "Categoría",
  },
];

export default function TablaGeneral() {
  const itemsPerPage = 2;
  const [currentPage, setCurrentPage] = useState(1);
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState(""); // Estado para el término de búsqueda

  useEffect(() => {
    // Realiza la solicitud GET a la API
    fetch("https://localhost:44323/api/Receta/GetRecetas")
      .then((response) => response.json())
      .then((responseData) => {
        setData(responseData);
        setIsLoading(false);
      })
      .catch((error) => {
        console.error("Error al cargar los datos:", error);
      });
  }, []);

  // Función para filtrar los datos en función del término de búsqueda
  const filterData = (data:any, searchTerm:any) => {
    return data.filter((item:any) =>
      Object.keys(item)
        .filter((key) => key !== "UrlFotos") // Excluye el campo "UrlFotos"
        .some((key) =>
          String(item[key]).toLowerCase().includes(searchTerm.toLowerCase())
        )
    );
  };  

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const filteredData = filterData(data, searchTerm);
  const paginatedRows = filteredData.slice(startIndex, endIndex);

  const RecetaColumn = ({ item }:any) => {
    // Verifica si el array de fotos tiene al menos una foto
    if (Array.isArray(item.UrlFotos) && item.UrlFotos.length > 0) {
      return (
        <div style={{ display: "flex", alignItems: "center" }}>
          <img
            src={item.UrlFotos[0]} // La URL de la primera imagen en el array
            alt={item.Titulo} // Un texto alternativo para la imagen
            style={{ width: "50px", height: "50px", marginRight: "10px" }} // Estilo para el tamaño de la imagen
          />
          <div>
            <p>{item.Titulo}</p>
            <p>{item.Descripcion}</p>
          </div>
        </div>
      );
    } else {
      // Si no hay fotos, muestra solo el título y la descripción
      return (
        <div>
          <p>{item.Titulo}</p>
          <p>{item.Descripcion}</p>
        </div>
      );
    }
  };

  return (
    <div style={{ width: "100%" }}>
      <Input
        className="my-2"
        placeholder="Buscar receta"
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
        <Table aria-label="Tabla de recetas">
          <TableHeader columns={columns}>
            {(column) => (
              <TableColumn key={column.key}>{column.label}</TableColumn>
            )}
          </TableHeader>
          <TableBody items={paginatedRows}>
            {(item:any) => (
              <TableRow key={item.Idreceta}>
                {(columnKey) => (
                  <TableCell key={columnKey}>
                    {columnKey === "Titulo" ? (
                      <RecetaColumn item={item} />
                    ) : (
                      item[columnKey]
                    )}
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
        showControls
        showShadow
        page={currentPage}
        onChange={(newPage) => setCurrentPage(newPage)}
      />
    </div>
  );
}
