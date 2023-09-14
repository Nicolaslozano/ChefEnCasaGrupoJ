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
import { ActionsIcon } from "./icons";
import Cookies from "js-cookie";

const columns = [
  { key: "Titulo", label: "Receta" },
  { key: "TiempoPreparacion", label: "Tiempo de Preparación(Minutos)" },
  { key: "NombreCategoria", label: "Categoría" },
  { key: "actions", label: "Acciones" },
];

const handleView = (item) => {
  window.location.href = `/receta/${item}`;
};



const handleDelete = (item) => {
  console.log("Deleting item:", item);
  alert(`Deleting: ${item.Titulo}`);
};

export default function TablaUsuario() {
  const itemsPerPage = 10;
  const [currentPage, setCurrentPage] = useState(1);
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState("");
  const { isOpen, onOpen, onOpenChange, onClose } = useDisclosure();
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editingRecipe, setEditingRecipe] = useState(null);
  const [selectedCategory, setSelectedCategory] = useState("Categoria");
  const [formData, setFormData] = useState({
    titulo: "",
    descripcion: "",
    tiempoPreparacion: 0,
    ingredientes: "",
    pasos: "",
    url_fotos: [],
    usuario_user: Cookies.get("usuario"),
    categoria_idcategoria: 0,
  });

  useEffect(() => {
    fetch(`https://localhost:44323/api/Receta/GetRecetasToUser?usu=${Cookies.get("usuario")}`)
      .then((response) => response.json())
      .then((responseData) => {
        setData(responseData);
        setIsLoading(false);
      })
      .catch((error) => {
        console.error("Error al cargar los datos:", error);
      });
  }, []);

  const filterData = (data, searchTerm) => {
    return data.filter((item) =>
      Object.keys(item)
        .filter((key) => key !== "UrlFotos")
        .some((key) =>
          String(item[key]).toLowerCase().includes(searchTerm.toLowerCase())
        )
    );
  };

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const filteredData = filterData(data, searchTerm);
  const paginatedRows = filteredData.slice(startIndex, endIndex);

  const handleSubmit = () => {

    fetch("https://localhost:44323/api/Receta", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(formData),
    })
      .then((response) => response.json())
      .then((data) => {
        onClose();
      })
      .catch((error) => {
        console.error("Error :", error);
      });
  };

  const handleEditSubmit = () => {
    const { ...editedRecipe } = editingRecipe;

    fetch(`https://localhost:44323/api/Receta/EditarReceta`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(editedRecipe),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Respuesta del servidor:", data);
        setIsEditModalOpen(false);
      })
      .catch((error) => {
        console.error("Error al enviar la solicitud:", error);
      });
  };

  const RecetaColumn = ({ item }) => {
    if (Array.isArray(item.UrlFotos) && item.UrlFotos.length > 0) {
      return (
        <div style={{ display: "flex", alignItems: "center" }}>
          <img
            src={item.UrlFotos[0]}
            alt={item.Titulo}
            style={{ width: "50px", height: "50px", marginRight: "10px" }}
          />
          <div>
            <p>{item.Titulo}</p>
            <p>{item.Descripcion}</p>
          </div>
        </div>
      );
    } else {
      return (
        <div>
          <p>{item.Titulo}</p>
          <p>{item.Descripcion}</p>
        </div>
      );
    }
  };

  const handleEdit = (idreceta) => {
    const recipeToEdit = data.find((item) => item.Idreceta === idreceta);

    if (recipeToEdit) {
      setIsEditModalOpen(true);
      setSelectedCategory(recipeToEdit.NombreCategoria)
      setEditingRecipe({
        idreceta: recipeToEdit.Idreceta,
        titulo: recipeToEdit.Titulo,
        descripcion: recipeToEdit.Descripcion,
        tiempoPreparacion: recipeToEdit.TiempoPreparacion,
        ingredientes: recipeToEdit.Ingredientes,
        pasos: recipeToEdit.Pasos,
        url_fotos: recipeToEdit.UrlFotos,
        nombreCategoria1: recipeToEdit.NombreCategoria,
        usuario_user: Cookies.get("usuario"),
      });
    }
  };

  return (
    <Auth>
      <div style={{ width: "100%" }}>
        <Button onPress={onOpen}>Agregar Receta</Button>
        <Modal isOpen={isOpen} onOpenChange={onOpenChange}>
          <ModalContent>
            {(onClose) => (
              <>
                <ModalHeader className="flex flex-col gap-1">
                  Crear una Nueva Receta
                </ModalHeader>
                <ModalBody>
                  <Input
                    label="Título"
                    value={formData.titulo}
                    onChange={(e) =>
                      setFormData({ ...formData, titulo: e.target.value })
                    }
                  />
                  <Input
                    label="Descripción"
                    value={formData.descripcion}
                    onChange={(e) =>
                      setFormData({ ...formData, descripcion: e.target.value })
                    }
                  />
                  <Input
                    label="Tiempo de Preparación"
                    value={formData.tiempoPreparacion}
                    onChange={(e) =>
                      setFormData({
                        ...formData,
                        tiempoPreparacion: e.target.value,
                      })
                    }
                  />
                  <Input
                    label="Ingredientes"
                    value={formData.ingredientes}
                    onChange={(e) =>
                      setFormData({
                        ...formData,
                        ingredientes: e.target.value,
                      })
                    }
                  />
                  <Input
                    label="Pasos"
                    value={formData.pasos}
                    onChange={(e) =>
                      setFormData({
                        ...formData,
                        pasos: e.target.value,
                      })
                    }
                  />
                  <Input
                    label="URL Fotos (Separadas por comas)"
                    value={formData.url_fotos.join(", ")}
                    onChange={(e) =>
                      setFormData({
                        ...formData,
                        url_fotos: e.target.value.split(", "),
                      })
                    }
                  />
                  <Dropdown>
                    <DropdownTrigger>
                      <Button variant="bordered" className="capitalize">
                        {selectedCategory}
                      </Button>
                    </DropdownTrigger>
                    <DropdownMenu
                      aria-label="Single selection example"
                      variant="flat"
                      disallowEmptySelection
                      selectionMode="single"
                      selectedKeys={selectedCategory}
                      onSelectionChange={(category) => {
                        setSelectedCategory(category.currentKey);
                        setFormData({
                          ...formData,
                          nombreCategoria1: category.currentKey,
                        });
                      }}
                    >
                      <DropdownItem key="Postres">Postres</DropdownItem>
                      <DropdownItem key="Bebidas">Bebidas</DropdownItem>
                      <DropdownItem key="Regionales">Regionales</DropdownItem>
                      <DropdownItem key="Reposteria">Reposteria</DropdownItem>
                      <DropdownItem key="Veganas">Veganas</DropdownItem>
                    </DropdownMenu>
                  </Dropdown>
                </ModalBody>
                <ModalFooter>
                  <Button color="danger" variant="light" onPress={onClose}>
                    Close
                  </Button>
                  <Button color="primary" onPress={handleSubmit}>
                    Guardar
                  </Button>
                </ModalFooter>
              </>
            )}
          </ModalContent>
        </Modal>
        <Modal
          isOpen={isEditModalOpen}
          onOpenChange={() => setIsEditModalOpen(false)}
        >
          <ModalContent>
            {(onClose) => (
              <>
                <ModalHeader className="flex flex-col gap-1">
                  Editar Receta
                </ModalHeader>
                <ModalBody>
                  <Input
                    label="Título"
                    value={editingRecipe?.titulo || ""}
                    onChange={(e) =>
                      setEditingRecipe({
                        ...editingRecipe,
                        titulo: e.target.value,
                      })
                    }
                  />
                  <Input
                    label="Descripción"
                    value={editingRecipe?.descripcion || ""}
                    onChange={(e) =>
                      setEditingRecipe({
                        ...editingRecipe,
                        descripcion: e.target.value,
                      })
                    }
                  />
                  <Input
                    label="Tiempo de Preparación"
                    value={editingRecipe?.tiempoPreparacion || 0}
                    onChange={(e) =>
                      setEditingRecipe({
                        ...editingRecipe,
                        tiempoPreparacion: parseInt(e.target.value),
                      })
                    }
                  />
                  <Input
                    label="Ingredientes"
                    value={editingRecipe?.ingredientes || ""}
                    onChange={(e) =>
                      setEditingRecipe({
                        ...editingRecipe,
                        ingredientes: e.target.value,
                      })
                    }
                  />
                  <Input
                    label="Pasos"
                    value={editingRecipe?.pasos || ""}
                    onChange={(e) =>
                      setEditingRecipe({
                        ...editingRecipe,
                        pasos: e.target.value,
                      })
                    }
                  />
                  <Input
                    label="URL Fotos (Separadas por comas)"
                    value={(editingRecipe?.url_fotos || []).join(", ")}
                    onChange={(e) =>
                      setEditingRecipe({
                        ...editingRecipe,
                        url_fotos: e.target.value.split(", "),
                      })
                    }
                  />
                  <Dropdown>
                    <DropdownTrigger>
                      <Button variant="bordered" className="capitalize">
                        {selectedCategory}
                      </Button>
                    </DropdownTrigger>
                    <DropdownMenu
                      aria-label="Single selection example"
                      variant="flat"
                      disallowEmptySelection
                      selectionMode="single"
                      selectedKeys={selectedCategory}
                      onSelectionChange={(category) => {
                        setSelectedCategory(category.currentKey);
                        setEditingRecipe({
                          ...editingRecipe,
                          nombreCategoria1: category.currentKey,
                        });
                      }}
                    >
                      <DropdownItem key="Postres">Postres</DropdownItem>
                      <DropdownItem key="Bebidas">Bebidas</DropdownItem>
                      <DropdownItem key="Regionales">Regionales</DropdownItem>
                      <DropdownItem key="Reposteria">Reposteria</DropdownItem>
                      <DropdownItem key="Veganas">Veganas</DropdownItem>
                    </DropdownMenu>
                  </Dropdown>
                </ModalBody>
                <ModalFooter>
                  <Button
                    color="danger"
                    variant="light"
                    onPress={() => setIsEditModalOpen(false)}
                  >
                    Cancelar
                  </Button>
                  <Button color="primary" onPress={handleEditSubmit}>
                    Guardar
                  </Button>
                </ModalFooter>
              </>
            )}
          </ModalContent>
        </Modal>
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
              {(item) => {
                return (
                  <TableRow key={item.Idreceta}>
                    {(columnKey) => (
                      <TableCell key={columnKey}>
                        {columnKey === "Titulo" ? (
                          <RecetaColumn item={item} />
                        ) : columnKey === "actions" ? (
                          <Dropdown>
                            <DropdownTrigger>
                              <Button
                                isIconOnly
                                radius="full"
                                size="sm"
                                variant="light"
                              >
                                <ActionsIcon className="text-default-400" />
                              </Button>
                            </DropdownTrigger>
                            <DropdownMenu>
                              <DropdownItem onClick={() => handleView(item.Idreceta)}>Ver</DropdownItem>
                              <DropdownItem onClick={() => handleEdit(item.Idreceta)}>Editar</DropdownItem>
                              <DropdownItem onClick={() => handleDelete(item.Idreceta)}>Eliminar</DropdownItem>
                            </DropdownMenu>
                          </Dropdown>
                        ) : (
                          item[columnKey]
                        )}
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
  