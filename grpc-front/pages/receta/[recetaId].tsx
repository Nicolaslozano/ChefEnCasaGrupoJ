import { useRouter } from 'next/router'
import { useEffect, useState } from 'react'
import { title} from "@/components/primitives";
import {Accordion, AccordionItem,Tabs,Tab,Image, Divider} from "@nextui-org/react";
import DefaultLayout from "@/layouts/default";

interface Receta {
  Idreceta?: string
  Titulo: string
  Ingredientes: string[]
  Pasos: string
}

export default function Page() {
  const router = useRouter()
  const { recetaId } = router.query
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)
  const [receta, setReceta] = useState<Receta | null>()

  useEffect(() => {
    if (!recetaId) return

    const getReceta = async () => {
      setLoading(true)
      setError(false)

      fetch(`/api/recetas/${recetaId}`)
        .then((res) => res.json())
        .then((receta) => setReceta(receta))
        .catch((err) => {
          console.error('Error fetching recipe', err)
          setError(true)
        })
        .finally(() => {
          setLoading(false)
        })
    }

    getReceta()
  }, [recetaId])

  if (loading) return <p>Loading...</p>

  if (error) return <p>ERROR AL CONSEGUIR RECETA!</p>

  return (
    <DefaultLayout>
    <div className="inline-block  text-center w-full justify-center mt-10">
    <h1 className={title({color: "blue"})}>Nombre de receta  </h1>
    <Divider className="mt-4"/>
    <h2 className= {title({color: "blue"})}>Descripcion  </h2>
    <div className="grid grid-cols-4 mt-4">
    <Image className=" "
      width={300}
      alt="RecetaImg "
      src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
    />
    <Image className="  "
      width={300}
      alt="RecetaImg "
      src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
    />
    <Image className="  "
      width={300}
      alt="RecetaImg "
      src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
    />
    <Image className="  "
      width={300}
      alt="RecetaImg "
      src="https://img.bekiacocina.com/cocina/0000/179-h.jpg"
    />
    <Image className="mt-4 "
    width={300}
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