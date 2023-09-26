  import { useRouter } from "next/router";
  import { useEffect, useState } from "react";
  import { title } from "@/components/primitives";
  import {
    Accordion,
    AccordionItem,
    Tabs,
    Tab,
    Image,
    Divider,
    Spinner,
    Button,
    Card,
    CardHeader,
    CardBody,
    CardFooter,
  } from "@nextui-org/react";
  import DefaultLayout from "@/layouts/default";
  import Cookies from "js-cookie";
  import { FavIcon, FavIconFilled, UserIcon } from "@/components/icons";

  interface Receta {
    Idreceta?: string;
    Titulo: string;
    TiempoPreparacion: string;
    UrlFotos: string[];
    UsuarioUser: string;
    Descripcion: string;
    NombreCategoria: string;
    Ingredientes: string;
    Pasos: string;
  }

  export default function Page() {
    const router = useRouter();
    const { recetaId } = router.query;
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(false);
    const [receta, setReceta] = useState<Receta | null>();
    const [isFollowing, setIsFollowing] = useState(false);
    const [isFollowed, setIsFollowed] = useState(false);
    const [rating, setRating] = useState([false, false, false, false, false]);
    const [selectedRating, setSelectedRating] = useState(0);

    useEffect(() => {
      if (!recetaId) return;

      const getReceta = async () => {
        setLoading(true);
        setError(false);

        fetch(`https://localhost:44323/api/Receta/receta?idreceta=${recetaId}`)
          .then((res) => res.json())
          .then((receta) => setReceta(receta))
          .catch((err) => {
            console.error("Error", err);
            setError(true);
          })
          .finally(() => {
            setLoading(false);
          });
      };

      getReceta();
    }, [recetaId]);

    if (loading)
      return (
        <Spinner
          className="flex justify-center mt-64 "
          label="Cargando Datos..."
          color="primary"
          size="lg"
        />
      );

    if (error) return <p>ERROR AL CONSEGUIR RECETA!</p>;

    const handleFollowClick = () => {
      const checkFollowUrl = `https://localhost:44323/api/Suscripciones/GetSeg?seg=${Cookies.get(
        "usuario"
      )}`;
      fetch(checkFollowUrl)
        .then((response) => {
          if (response.ok) {
            const followUrl = `https://localhost:44323/api/Usuarios/PostSeguidor?user=${Cookies.get(
              "usuario"
            )}&segui=${receta.UsuarioUser}`;
            fetch(followUrl, {
              method: "POST",
            })
              .then((followResponse) => {
                if (followResponse.ok) {
                  setIsFollowing(true);
                  console.log(
                    `Siguiendo correctamente al usuario ${receta.UsuarioUser}`
                  );
                } else {
                  console.error(`error al seguir usuario ${receta.UsuarioUser}`);
                }
              })
              .catch((followError) => {
                console.error("error al seguir usuario:", followError);
              });
          } else {
          }
        })
        .catch((error) => {
          console.error("Error al chequear si el usuario existe:", error);
        });
    };

    const handleFollowRecipe = async () => {
      try {
        const recetaId = receta?.Idreceta;

        const data = {
          recetasFavoritascol: recetaId,
          usuario_userfav: Cookies.get("usuario"),
        };

        const followRecipeUrl = "https://localhost:44323/api/RecetaFavoritas";

        const response = await fetch(followRecipeUrl, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        });

        if (response.ok) {
          setIsFollowed(true);
          console.log(`Siguiendo Correctamente a la receta ${recetaId}`);
        } else {
          console.error(`Fallo al seguir receta ${recetaId}`);
        }
      } catch (error) {
        console.error("Error al seguir receta:", error);
      }
    };

    const handleRating = (value) => {
      const newRating = new Array(5).fill(false);
      for (let i = 0; i < value; i++) {
        newRating[i] = true;
      }
      setRating(newRating);
      setSelectedRating(value);
      console.log(`Calificación: ${value}`);
    };

    return (
      <DefaultLayout>
        <div className="flex flex-col items-end mt-8 ">
          <div className="flex items-center">
            <UserIcon />
            <h1 className="">{receta?.UsuarioUser}</h1>
          </div>
          <Button className="flex ml-2" size="sm" onClick={handleFollowClick}>
            {isFollowing ? "Following" : "Follow"}
          </Button>
        </div>
        <div className="inline-block  text-center w-full justify-center ">
          <h1 className={title({ color: "blue" })}>
            {receta?.Titulo}
            <div className="flex justify-end items-end ">
              <div className="flex justify-end items-end ">
                {isFollowed ? (
                  <FavIconFilled />
                ) : (
                  <FavIcon onClick={handleFollowRecipe} />
                )}
              </div>
            </div>
          </h1>
          <Divider className="mt-4" />
          <h2 className="text-base mt-4">{receta?.Descripcion}</h2>
          <div className="flex mt-4">
            {receta?.UrlFotos &&
              receta.UrlFotos.map((url, index) => (
                <Image
                  key={index}
                  width={300}
                  alt={`RecetaImg-${index}`}
                  src={url}
                  style={{ maxHeight: "200px" }}
                  className={`${index > 0 ? "ml-2" : ""}`}
                />
              ))}
          </div>

          <Divider className="mt-10" />
          <Accordion variant="splitted" className="mt-4 ">
            <AccordionItem key="1" aria-label="Ingredientes" title="Ingredientes">
              {receta?.Ingredientes}
            </AccordionItem>
            <AccordionItem key="2" aria-label="Pasos" title="Pasos">
              {receta?.Pasos}
            </AccordionItem>
            <AccordionItem key="3" aria-label="Categorias" title="Categorias">
              <Tabs aria-label="Options" className="mb-8">
                {Array.isArray(receta?.NombreCategoria) ? (
                  receta?.NombreCategoria.map((categoria, index) => (
                    <Tab key={index} title={categoria}></Tab>
                  ))
                ) : (
                  <Tab key="singleCategory" title={receta?.NombreCategoria}></Tab>
                )}
              </Tabs>
            </AccordionItem>
          </Accordion>
          <h1 className={title({ color: "pink", size:"sm"})}>¿Que opinas de la receta?</h1>
          <Card className="mt-4">
            <CardBody>
              {receta?.comentarios &&
                receta.comentarios.map((comentario, index) => (
                  <div key={index} className="mb-4">
                    <strong>{comentario.usuario}</strong>: {comentario.texto}
                  </div>
                ))}
            </CardBody>
            <CardFooter className="flex justify-center">
              <form className="w-full">
                <textarea
                  rows="4"
                  placeholder="Escribe tu comentario aquí"
                  className="w-full border rounded p-2 mb-2"
                />
                <Button type="submit" color="primary">
                  Enviar Comentario
                </Button>
              </form>
            </CardFooter>
          </Card>
          <h1 className="mt-4">
            Comentanos que te parecio la receta calificandola
          </h1>
          <div className="mt-4">
            <div className="flex items-center justify-center">
              {rating.map((isFilled, index) => (
                <div
                  key={index}
                  onClick={() => handleRating(index + 1)}
                  className={`cursor-pointer ${
                    isFilled ? "text-yellow-400 fill-current" : "text-gray-300"
                  }`}
                  style={{ width: "40px", height: "40px" }}
                >
                  <FavIconFilled className={isFilled ? "" : "hidden"} />
                  <FavIcon className={isFilled ? "hidden" : ""} />
                </div>
              ))}
            </div>
          </div>
        </div>
      </DefaultLayout>
    );
  }
