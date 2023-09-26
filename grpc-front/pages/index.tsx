import { title } from "@/components/primitives";
import TablaGeneral from "@/components/tablaGeneral";
import DefaultLayout from "@/layouts/default";
import { Spacer } from "@nextui-org/react";
import { Card, CardHeader, CardBody, Image } from "@nextui-org/react";

const cardsData = [
  {
    title: "Título 1",
    description: "Descripción de la carta 1",
    image: "URL_de_la_imagen_1",
  },
  {
    title: "Título 2",
    description: "Descripción de la carta 2",
    image: "URL_de_la_imagen_2",
  },
  {
    title: "Título 3",
    description: "Descripción de la carta 2",
    image: "URL_de_la_imagen_2",
  },
  {
    title: "Título 4",
    description: "Descripción de la carta 2",
    image: "URL_de_la_imagen_2",
  },
  {
    title: "Título 5",
    description: "Descripción de la carta 2",
    image: "URL_de_la_imagen_2",
  },

];

export default function IndexPage() {
  return (
    <DefaultLayout>
      <div className="text-center">
        <h1 className={title({ color: "violet" })}>Ultimas Novedades!</h1>
      </div>
      <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
        <div className="flex justify-center gap-4">
          {cardsData.map((card, index) => (
            <Card key={index}>
              <CardHeader>{card.title}</CardHeader>
              <CardBody>
                <img src={card.image} alt={card.title} />
                <p>{card.description}</p>
              </CardBody>
            </Card>
          ))}
        </div>
        <div className="text-center">
          <h1 className={title({ color: "blue" })}>Recetas creadas por la &nbsp;</h1>
          <h1 className={title({ color: "green" })}>Comunidad&nbsp;</h1>
        </div>
        <Spacer />
        <TablaGeneral />
      </section>
    </DefaultLayout>
  );
}
