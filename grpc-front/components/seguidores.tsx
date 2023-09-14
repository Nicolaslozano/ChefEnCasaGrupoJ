import React, { useState, useEffect } from "react";
import {
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  useDisclosure,
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
} from "@nextui-org/react";
import Auth from "./Auth";
import { ActionsIcon, RedCross, UserIcon } from "./icons";
import Cookies from "js-cookie";

const columns = [
  { key: "Usuario", label: "Usuario Seguido" },
  { key: "actions", label: "Acciones" },
];





export default function TablaSeguidores() {
  const itemsPerPage = 10;
  const [currentPage, setCurrentPage] = useState(1);
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    // Cambia la URL de la API para obtener los usuarios seguidos
    fetch(
      `https://localhost:44323/api/Suscripciones/GetSeg?seg=${Cookies.get(
        "usuario"
      )}`
    )
      .then((response) => response.json())
      .then((responseData) => {
        setData(responseData);
        setIsLoading(false);
      })
      .catch((error) => {
        console.error("Error al cargar los datos:", error);
      });
  }, []);

  const handleUnfollow = (item) => {
    const currentUser = Cookies.get("usuario");
    const followedUser = item.FollowedUser;
  
    // Realiza la solicitud para dejar de seguir al usuario
    fetch(
      `https://localhost:44323/api/Usuarios/DeleteSeguidor?user=${currentUser}&segui=${followedUser}`,
      {
        method: "DELETE",
      }
    )
      .then((response) => {
        if (response.ok) {
          // Elimina el usuario de la lista de datos (data)
          setData((prevData) =>
            prevData.filter((dataItem) => dataItem.FollowedUser !== followedUser)
          );
          console.log(`Dejaste de seguir a: ${followedUser}`);
        } else {
          console.error(`Error al dejar de seguir a: ${followedUser}`);
        }
      })
      .catch((error) => {
        console.error("Error en la solicitud:", error);
      });
  };
  

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
    <Auth>
      <div style={{ width: "100%" }}>
        <Input
          className="my-2"
          placeholder="Buscar Usuario Seguido"
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
          <Table aria-label="Tabla de usuarios seguidos">
            <TableHeader columns={columns}>
              {(column) => (
                <TableColumn key={column.key}>{column.label}</TableColumn>
              )}
            </TableHeader>
            <TableBody items={paginatedRows}>
              {(item) => {
                return (
                  <TableRow key={item.Idsuscripcion}>
                    {(columnKey) => (
                      <TableCell key={columnKey}>
                        {columnKey === "Usuario" ? (
                          <>
                            <div className="flex align-middle">          
                              <UserIcon  size={30}/>
                              <h1 className="text-lg">{item.FollowedUser}</h1>
                            </div>
                          </>
                        ) : columnKey === "actions" ? (
                          <Button
                            onClick={() => handleUnfollow(item)}
                            color="error"
                            size="small"
                          >
                            <RedCross />
                          </Button>
                        ) : null}
                      </TableCell>
                    )}
                  </TableRow>
                );
              }}
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
    </Auth>
  );
}
