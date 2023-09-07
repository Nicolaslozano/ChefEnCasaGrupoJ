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
} from "@nextui-org/react";
import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Pagination,
} from "@nextui-org/react";
import Auth from "./Auth";
import { ActionsIcon} from "./icons";

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
    key: "NombreCategoria",
    label: "Categoría",
  },
  {
    key: "actions",
    label: "Acciones",
  },
];

const handleView = (item) => {
  console.log("Viewing item:", item);
  window.location.href = `/receta/${item}`;
};

const handleEdit = (item) => {
  console.log("Editing item:", item);
  alert(`Editing: ${item.Titulo}`);
};

const handleDelete = (item) => {
  console.log("Deleting item:", item);
  alert(`Deleting: ${item.Titulo}`);
};

export default function TablaUsuario() {
  const itemsPerPage = 2;
  const [currentPage, setCurrentPage] = useState(1);
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState(""); // Estado para el término de búsqueda
  const { isOpen, onOpen, onOpenChange, onClose } = useDisclosure();
  const [selectedKeys, setSelectedKeys] = React.useState(new Set(["Filtrar Por:"]));
  const [formData, setFormData] = useState({
    titulo: "",
    descripcion: "",
    tiempoPreparacion: 0,
    ingredientes: "",
    pasos: "",
    url_fotos: [],
    usuario_idusuario: 1,
    categoria_idcategoria: 0,
  });

  const selectedValue = React.useMemo(
    () => Array.from(selectedKeys).join(", ").replaceAll("_", " "),
    [selectedKeys]
  );
  
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
  const filterData = (data: any, searchTerm: any) => {
    return data.filter((item: any) =>
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
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData), // Use formData as the entire "receta" object
    })
      .then((response) => response.json())
      .then((data) => {
        // Handle the response from the API
        console.log("Response from the API:", data);
        onClose(); // Close the modal
      })
      .catch((error) => {
        console.error("Error sending the request:", error);
      });
  };
  


  const RecetaColumn = ({ item }: any) => {
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
    <Auth>
    <div style={{ width: "100%" }}>
      <Button onPress={onOpen}>Agregar Receta</Button>
      <Dropdown>
      <DropdownTrigger>
        <Button  
          variant="bordered" 
          className="capitalize ml-4"
        >
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
                  label="ingredientes"
                  value={formData.ingredientes}
                  onChange={(e) =>
                    setFormData({
                      ...formData,
                      ingredientes: e.target.value,
                    })
                  }
                />
                <Input
                  label="pasos"
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
                <Input
                  label="categoria"
                  value={formData.categoria_idcategoria}
                  onChange={(e) =>
                    setFormData({
                      ...formData,
                      categoria_idcategoria: e.target.value,
                    })
                  }
                />
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
  {(item: any) => {
    console.log(item); // Add this line to inspect the structure of the item
    return (
      <TableRow key={item.Idreceta}>
        {(columnKey) => (
          <TableCell key={columnKey}>
            {columnKey === "Titulo" ? (
              <RecetaColumn item={item} />
            ) : columnKey === "actions" ? (
              <Dropdown>
                <DropdownTrigger>
                  <Button isIconOnly radius="full" size="sm" variant="light">
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
