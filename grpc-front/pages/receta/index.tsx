import React from "react";
import { title} from "@/components/primitives";
import {Accordion, AccordionItem,Tabs,Tab,Image, Divider} from "@nextui-org/react";
import DefaultLayout from "@/layouts/default";

export default function App() {
 
  const defaultContent =
    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

  return (
    <DefaultLayout>
    <div className="inline-block  text-center w-full justify-center mt-10">
    <h1 className={title({color: "blue"})}>Nombre de receta  </h1>
    <Divider className="mt-4"/>
    <h2 className= {title({color: "blue"})}>Descripcion  </h2>
    <div className="grid grid-cols-4 mt-4">
    <Image className=" "
      width={400}
      alt="RecetaImg "
      src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
    />
    <Image className="  "
      width={400}
      alt="RecetaImg "
      src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
    />
    <Image className="  "
      width={400}
      alt="RecetaImg "
      src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
    />
    <Image className="  "
      width={400}
      alt="RecetaImg "
      src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
    />
    <Image className="mt-4 "
    width={400}
    alt="RecetaImg "
    src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
  />
    
    </div>
  
    <Divider className="mt-10"/>
    <Accordion variant="splitted" className="mt-4 ">
      <AccordionItem key="1" aria-label="Ingredientes" title="Ingredientes">
        {defaultContent}
      </AccordionItem>
      <AccordionItem key="2" aria-label="Pasos" title="Pasos">
        {defaultContent}
      </AccordionItem>
      <AccordionItem key="3" aria-label="Categorias" title="Categorias">
      <Tabs aria-label="Options">
        <Tab key="photos" title="Vegana">
        </Tab>
        <Tab key="music" title="Regional">
        </Tab>
        <Tab key="videos" title="Bebidas">          
        </Tab>
      </Tabs>
      </AccordionItem>
    </Accordion>
    </div>
    </DefaultLayout>
  );
}