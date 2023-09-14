import { title, subtitle } from "@/components/primitives";
import DefaultLayout from "@/layouts/default";
import { Spacer } from "@nextui-org/react";
import TableComponent from "@/components/tablaUsuario";
import TablaUsuario from "@/components/tablaUsuario";
import TablaSeguidores from "@/components/seguidores";


export default function user() {
    return (
      <DefaultLayout>
        <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
          <div className="inline-block max-w-lg text-center justify-center">
            <h1 className={title({color: "blue"})}>Tus Recetas  &nbsp;</h1>
         </div>
        <Spacer/>
        </section>
        <TableComponent/>
        <Spacer/>
        <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
          <div className="inline-block max-w-lg text-center justify-center">
            <h1 className={title({color: "blue"})}>Usuarios Seguidos  &nbsp;</h1>
         </div>
        <Spacer/>
        <TablaSeguidores/>
        </section>
      </DefaultLayout>
    );
  }
  