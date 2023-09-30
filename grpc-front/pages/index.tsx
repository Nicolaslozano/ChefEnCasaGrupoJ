import { useEffect, useState } from "react";
import { title } from "@/components/primitives";
import DefaultLayout from "@/layouts/default";
import { Divider, Spacer } from "@nextui-org/react";
import { Card, CardHeader, CardBody, Image } from "@nextui-org/react";
import TablaGeneral from "@/components/tablaGeneral"; 
import TablaUsuariosPop from "@/components/tablaUsuariosPopulares"; 

export default function IndexPage() {
  const [cardsData, setCardsData] = useState([]);
  const [selectedSection, setSelectedSection] = useState("Listado Recetas"); 
  const [hasData, setHasData] = useState(false); 

  useEffect(() => {
    const fetchData = () => {
      fetch("https://localhost:44323/api/Receta/GetNovedades")
        .then((response) => response.json())
        .then((data) => {
          const mappedData = data.map((receta) => ({
            title: receta.Titulo,
            description: receta.Descripcion,
            image: receta.UrlFotos,
            id: receta.Idreceta,
          }));
          setCardsData(mappedData);
          setHasData(true); 
          console.log(data);
        })
        .catch((error) => {
          console.error("Error al obtener los datos de la API", error);
        });
    };

    fetchData();

    const intervalId = setInterval(fetchData, 5000);

    return () => clearInterval(intervalId);
  }, []);



 
  const handleSectionClick = (section) => {
    setSelectedSection(section);
  };

  return (
    <DefaultLayout>
       <div className="text-center mt-8">
        {hasData && <h1 className={title({ color: "violet" })}>¡Bienvenidos a ChefEnCasa !</h1>}
      </div>

      <div className="text-center mt-8">
        {hasData && <h1 className={title({ color: "violet" })}>¡Últimas Novedades!</h1>}
      </div>
      <div className="flex justify-center gap-4 mt-10">
        {cardsData.map((card, index) => (
          <Card key={index} className="w-72">
            <CardHeader className="p-4 flex justify-center">
              {card.title}
            </CardHeader>
            <CardBody>
              <Image
                src={card.image[0]}
                style={{
                  width: "300px",
                  height: "20vh",
                }}
              />
            </CardBody>
          </Card>
        ))}
      </div>
      <Spacer y={8}/>
      <div className="flex">
        <div className="flex-1 p-4 ">
          <div className="flex flex-col h-full">
            <Card className="h-full">
              <CardHeader className="p-4 flex justify-center items-center">
                {selectedSection ? (
                  <h3 className={title({ color: "cyan" })}>
                    {selectedSection}
                  </h3>
                ) : (
                  <h3 className={title({ color: "pink" })}>Secciones</h3>
                )}
              </CardHeader>
              <Divider className="mt-4"/>
              <CardBody className="flex-grow">
                <div
                  onClick={() => handleSectionClick("Usuarios Famosos")} 
                  className="cursor-pointer  text-center"
                >
                   <h3 className={title({ color: "violet" })}>Usuarios Populares</h3>
                </div>
                <Divider className="mt-12 mb-8"/>
                <div
                  onClick={() => handleSectionClick("Listado Recetas")} 
                  className="cursor-pointer text-center"
                >
                   <h3 className={title({ color: "violet" })}>Todas las Recetas </h3>
                </div>
                
              </CardBody>
            </Card>
          </div>
        </div>
        <div className="flex-1 p-4">
          {selectedSection === "Usuarios Famosos" && (
            <TablaUsuariosPop/>
          )}
          {selectedSection === "Listado Recetas" && <TablaGeneral />}
        </div>
      </div>
    </DefaultLayout>
  );
}