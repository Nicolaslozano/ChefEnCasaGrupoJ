import { title, subtitle } from "@/components/primitives";
import DefaultLayout from "@/layouts/default";
import { Spacer } from "@nextui-org/react";
import TableComponent from "@/components/tablaUsuario";


export default function user() {
    return (
      <DefaultLayout>
        <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
          <div className="inline-block max-w-lg text-center justify-center">
            <h1 className={title({color: "blue"})}>Tus contribuciones  &nbsp;</h1>
         </div>
        <Spacer/>
        </section>
        <TableComponent/>
      </DefaultLayout>
    );
  }
  