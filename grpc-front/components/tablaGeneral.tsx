import React, { useState, useEffect } from "react";
import Cookies from "js-cookie";

  import {
    Modal,
    ModalContent,
    ModalHeader,
    ModalBody,
    ModalFooter,
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
    Link
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
];

export default function TablaGeneral() {
  const itemsPerPage = 10;
  const [currentPage, setCurrentPage] = useState(1);
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState(""); // Estado para el término de búsqueda
  const [selectedKeys, setSelectedKeys] = React.useState(
    new Set(["Filtrar Por:"])
  );
 
    useEffect(() => {
      const fetchData = async () => {
        setIsLoading(true);
        try {
          const selectedCategoriesArray = Array.from(selectedKeys);
          const categoryPromises = selectedCategoriesArray.map((category) => {
            switch (category) {
              case "Postres":
                return fetch("https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Postres");
              case "Veganas":
                return fetch("https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Veganas");
              case "Reposteria":
                return fetch("https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Reposteria");
              case "Bebidas":
                return fetch("https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Bebidas");
              case "Regionales":
                return fetch("https://localhost:44323/api/Receta/GetRecetasToCategoria?usu=Regionales");
              default:
                return null;
            }
          });
    
          const responses = await Promise.all(categoryPromises.filter(Boolean));
          const responseData = await Promise.all(responses.map((response) => response.json()));
          const combinedData = responseData.flat();
    
          
          if (selectedCategoriesArray.length === 1) {
            const response = await fetch("https://localhost:44323/api/Receta/GetRecetas");
            const defaultData = await response.json();
            setData(defaultData);
          } else {
            setData(combinedData);
          }
    
          setIsLoading(false);
        } catch (error) {
          console.error("Error al cargar los datos:", error);
          setIsLoading(false);
        }
      };
    
      fetchData();
    }, [selectedKeys]);
  

  // Función para filtrar los datos en función del término de búsqueda
  const filterData = (data: any, searchTerm: any) => {
    return data.filter((item: any) =>
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

  const RecetaColumn = ({ item }: any) => {
    // Verifica si el array de fotos tiene al menos una foto
    if (Array.isArray(item.UrlFotos) && item.UrlFotos.length > 0) {
      return (
        <div style={{ display: "flex", alignItems: "center" }}>
          <img
            src={item.UrlFotos[0]} // La URL de la primera imagen en el array
            alt={item.Titulo} // Un texto alternativo para la imagen
            style={{ width: "100px", height: "100px", marginRight: "10px" }} // Estilo para el tamaño de la imagen
          />
          <div>
            <p>{item.Titulo}</p>
          </div>
        </div>
      );
    } else {
      // Si no hay fotos, muestra solo el título y la descripción
      return (
        <div>
          <p>{item.Titulo}</p>
        </div>
      );
    }
  };

  return (
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
            {(item: any) => (
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
          showControls
          showShadow
          page={currentPage}
          onChange={(newPage) => setCurrentPage(newPage)}
        />
      </div>
  );
}
