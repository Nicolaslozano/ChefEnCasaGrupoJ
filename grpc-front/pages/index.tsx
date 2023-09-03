import { title} from "@/components/primitives";
import TablaGeneral from "@/components/tablaGeneral";
import DefaultLayout from "@/layouts/default";
import { Spacer } from "@nextui-org/react";

export default function IndexPage() {
  return (
    <DefaultLayout>
      <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
        <div className="inline-block max-w-lg text-center justify-center">
          <h1 className={title({color: "blue"})}>Recetas creadas por la  &nbsp;</h1>
          <h1 className={title({ color: "green" })}>Comunidad&nbsp;</h1>
       </div>
      <Spacer/>
      <TablaGeneral/>
      </section>
    </DefaultLayout>
  );
}
