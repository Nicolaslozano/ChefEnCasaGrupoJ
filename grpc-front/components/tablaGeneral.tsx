import React, { useState, useEffect } from "react";
import Cookies from "js-cookie";

import {
  Button,
  Input,
  Spinner,
  Dropdown,
  DropdownTrigger,
  DropdownMenu,
  DropdownItem,
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Pagination,
  Link,
  Card
} from "@nextui-org/react";

const columns = [
  {
    key: "Titulo",
    label: "Receta",
  },
  {
    key: "TiempoPreparacion",
    label: "Tiempo de Preparación (Minutos)",
  },
];

export default function TablaGeneral() {
  const itemsPerPage = 3;
  const [currentPage, setCurrentPage] = useState(1);
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState(""); 
  const [selectedKeys, setSelectedKeys] = React.useState(
    new Set(["Filtrar Por:"])
  );
  const [showFavorites, setShowFavorites] = useState(false);
  const [showPopular, setShowPopular] = useState(false); // Nuevo filtro

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      try {
        const selectedCategoriesArray = Array.from(selectedKeys);
        const categoryPromises = selectedCategoriesArray.map((category) => {
          switch (category) {
            case "Postres":
              return fetch(
                `https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Postres&nombreUsuario=${Cookies.get(
                  "usuario"
                )}`
              );
            case "Veganas":
              return fetch(
                `https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Veganas&nombreUsuario=${Cookies.get(
                  "usuario"
                )}`
              );
            case "Reposteria":
              return fetch(
                `https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Reposteria&nombreUsuario=${Cookies.get(
                  "usuario"
                )}`
              );
            case "Bebidas":
              return fetch(
                `https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Bebidas&nombreUsuario=${Cookies.get(
                  "usuario"
                )}`
              );
            case "Regionales":
              return fetch(
                `https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Regionales&nombreUsuario=${Cookies.get(
                  "usuario"
                )}`
              );
            case "Mas Populares":
              return fetch(
                `https://localhost:44323/api/Receta/GetRecetasPopulares?nombreUsuario=${Cookies.get(
                  "usuario"
                )}`
              );
            default:
              return null;
          }
          
        });

        let recetasFavoritasPromise = null;

        if (showFavorites) {
          // Consulta para recetas favoritas
          recetasFavoritasPromise = fetch(
            `https://localhost:44323/api/RecetaFavoritas/GetRecetasFav?nombreUsuario=${Cookies.get(
              "usuario"
            )}`
          );
        }

        const responses = await Promise.all(
          [...categoryPromises.filter(Boolean), recetasFavoritasPromise].filter(
            Boolean
          )
        );
        const responseData = await Promise.all(
          responses.map((response) => response.json())
        );
        const combinedData = responseData.flat();

        // Filtra las recetas duplicadas por Idreceta
        const uniqueRecipeIds = new Set();
        const filteredData = combinedData.filter((newRecipe) => {
          if (!uniqueRecipeIds.has(newRecipe.Idreceta)) {
            uniqueRecipeIds.add(newRecipe.Idreceta);
            return true;
          }
          return false;
        });

        if (selectedCategoriesArray.length === 1 && !showFavorites && !showPopular) {
          const response = await fetch(
            `https://localhost:44323/api/Receta/GetRecetas?nombreUsuario=${Cookies.get(
              "usuario"
            )}`
          );
          const defaultData = await response.json();
          setData(defaultData);
        } else {
          setData(filteredData);
        }

        setIsLoading(false);
      } catch (error) {
        console.error("Error al cargar los datos:", error);
        setIsLoading(false);
      }
    };

    fetchData();
  }, [selectedKeys, showFavorites, showPopular]); // Actualizado para incluir showPopular

  // Función para filtrar los datos en función del término de búsqueda
  const filterData = (data, searchTerm) => {
    return data.filter((item) =>
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
  const selectedValue = React.useMemo(
    () => Array.from(selectedKeys).join(", ").replaceAll("_", " "),
    [selectedKeys]
  );

  const RecetaColumn = ({ item }) => {
    // Verifica si el array de fotos tiene al menos una foto
    if (Array.isArray(item.UrlFotos) && item.UrlFotos.length > 0) {
      return (
        <div style={{ display: "flex", alignItems: "center" }}>
          <img
            src={item.UrlFotos[0]} 
            alt={item.Titulo} 
            style={{ width: "100px", height: "100px", marginRight: "10px" }} 
          />
          <div>
            <p>{item.Titulo}</p>
          </div>
        </div>
      );
    } else {
      // Si no hay fotos, muestra solo el título
      return (
        <div>
          <p>{item.Titulo}</p>
        </div>
      );
    }
  };

  return (
    <Card className="p-4">
    <div style={{ width: "100%" }}>
      <Dropdown>
        <DropdownTrigger>
          <Button variant="bordered" className="capitalize ml-4">
            {selectedValue}
          </Button>
        </DropdownTrigger>
        <DropdownMenu
          variant="flat"
          closeOnSelect={false}
          disallowEmptySelection
          selectionMode="multiple"
          selectedKeys={selectedKeys}
          onSelectionChange={setSelectedKeys}
        >
          <DropdownItem key="Postres">Postres</DropdownItem>
          <DropdownItem key="Veganas">Veganas</DropdownItem>
          <DropdownItem key="Reposteria">Reposteria</DropdownItem>
          <DropdownItem key="Bebidas">Bebidas</DropdownItem>
          <DropdownItem key="Regionales">Regionales</DropdownItem>
          <DropdownItem key="Mas Populares" onClick={() => setShowPopular(!showPopular)}>Mas Populares</DropdownItem> {/* Nuevo filtro */}
          <DropdownItem
            key="RecetasFavoritas"
            onClick={() => {
              // Toggle the "Recetas Favoritas" filter
              setShowFavorites(!showFavorites);
            }}
          >
            Recetas Favoritas
          </DropdownItem>
        </DropdownMenu>
      </Dropdown>

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
            {(item) => (
              <TableRow key={item.Idreceta}>
                {(columnKey) => (
                  <TableCell key={columnKey}>
                    {columnKey === "Titulo" ? (
                      <Link href={`/receta/${item.Idreceta}`}>
                        <RecetaColumn item={item} />
                      </Link>
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
        showShadow
        page={currentPage}
        onChange={(newPage) => setCurrentPage(newPage)}
      />
    </div>
    </Card>
  );
}
