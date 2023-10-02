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
  Spacer,
  Avatar
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
  const [selectedRating,setSelectedRating] = useState(0);
  const [averageRating, setAverageRating] = useState<number | null>(null);
  const [userComment, setUserComment] = useState("");
  const [comments, setComments] = useState([]);

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

  useEffect(() => {
    if (recetaId) {
      fetch(
        `https://localhost:44323/api/Receta/GetPromedioCalificacion?idRec=${recetaId}`
      )
        .then((res) => res.json())
        .then((promedio) => setAverageRating(promedio))
        .catch((err) => {
          console.error("Error al obtener el puntaje promedio", err);
        });
    }
  }, [recetaId]);

  useEffect(() => {
    if (!recetaId) return;

    // Llamada a la API para obtener los comentarios
    fetch(`https://localhost:44323/api/Comentarios/GetComentariosToReceta?reid=${recetaId}`)
      .then((res) => res.json())
      .then((data) => {
        setComments(data);
        setLoading(false);
        console.log(data);
      })
      .catch((error) => {
        console.error("Error al obtener comentarios:", error);
        setLoading(false);
      });
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
    const user = Cookies.get("usuario");
    const userToFollow = receta.UsuarioUser;

    if (isFollowing) {
      // Si ya estás siguiendo al usuario, realiza unfollow
      const unfollowUrl = `https://localhost:44323/api/Usuarios/DeleteSeguidor?user=${user}&segui=${userToFollow}`;
      fetch(unfollowUrl, {
        method: "DELETE",
      })
        .then((unfollowResponse) => {
          if (unfollowResponse.ok) {
            setIsFollowing(false);
            console.log(`Dejaste de seguir al usuario ${userToFollow}`);
          } else {
            console.error(`Error al dejar de seguir al usuario ${userToFollow}`);
          }
        })
        .catch((unfollowError) => {
          console.error("Error al dejar de seguir al usuario:", unfollowError);
        });
    } else {
      // Si no estás siguiendo al usuario, realiza follow
      const followUrl = `https://localhost:44323/api/Usuarios/PostSeguidor?user=${user}&segui=${userToFollow}`;
      fetch(followUrl, {
        method: "POST",
      })
        .then((followResponse) => {
          if (followResponse.ok) {
            setIsFollowing(true);
            console.log(`Siguiendo correctamente al usuario ${userToFollow}`);
          } else {
            console.error(`Error al seguir al usuario ${userToFollow}`);
          }
        })
        .catch((followError) => {
          console.error("Error al seguir al usuario:", followError);
        });
    }
  };


  const handleFollowRecipe = async () => {
    try {
      const recetaId = receta?.Idreceta;
      const usuario = Cookies.get("usuario");

      if (isFollowed) {
        // Si ya estás siguiendo la receta, realiza unfollow
        const unfollowRecipeUrl = `https://localhost:44323/api/RecetaFavoritas/DeleteRecetaFav?rec=${recetaId}&us=${usuario}`;
        fetch(unfollowRecipeUrl, {
          method: "DELETE",
        })
          .then((unfollowResponse) => {
            if (unfollowResponse.ok) {
              setIsFollowed(false);
              console.log(`Dejaste de seguir la receta ${recetaId}`);
            } else {
              console.error(`Error al dejar de seguir la receta ${recetaId}`);
            }
          })
          .catch((unfollowError) => {
            console.error("Error al dejar de seguir la receta:", unfollowError);
          });
      } else {
        // Si no estás siguiendo la receta, realiza follow
        const data = {
          recetasFavoritascol: recetaId,
          usuario_userfav: usuario,
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
          console.error(`Fallo al seguir la receta ${recetaId}`);
        }
      }
    } catch (error) {
      console.error("Error al seguir/recuperar la receta:", error);
    }
  };



  const handleSubmitComment = async (e) => {
    e.preventDefault();

    try {
      const recetaId = receta?.Idreceta;
      const usuarioComen = Cookies.get("usuario");

      const data = {
        recet: recetaId,
        usuario_comen: usuarioComen,
        comentario: userComment,
      };

      const commentUrl = "https://localhost:44323/api/Comentarios";

      const response = await fetch(commentUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });

      if (response.ok) {

        console.log("Comentario enviado correctamente");
      } else {
        console.error("Fallo al enviar el comentario");
      }
    } catch (error) {
      console.error("Error al enviar el comentario:", error);
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

    const idReceta = receta?.Idreceta;
    const nombreUsuario = Cookies.get("usuario");
    const calificacion = value;

    const postCalificacionUrl = `https://localhost:44323/api/Receta/PostCalificacion?idRec=${idReceta}&nomusu=${nombreUsuario}&califi=${calificacion}`;

    fetch(postCalificacionUrl, {
      method: "POST",
    })
      .then((response) => {
        if (response.ok) {
          console.log(
            `Calificación enviada correctamente para la receta ${idReceta}`
          );
        } else {
          console.error(
            `Error al enviar la calificación para la receta ${idReceta}`
          );
        }
      })
      .catch((error) => {
        console.error("Error al enviar la calificación:", error);
      });
  };

  return (
    <DefaultLayout>
      <div className="flex flex-col items-end mt-8 ">
        <div className="flex items-center">
          <UserIcon />
          <h1 className="">{receta?.UsuarioUser}</h1>
        </div>
        <Button className="flex ml-2" size="sm" onClick={handleFollowClick}>
          {isFollowing ? "Dejar de Seguir" : "Seguir"}
        </Button>
      </div>
      <div className="inline-block  text-center w-full justify-center ">
        <h1 className={title({ color: "blue" })}>
          {receta?.Titulo}
          <div className="flex justify-end items-end ">
            <div className="flex justify-end items-end ">
              {isFollowed ? (
                <FavIconFilled onClick={handleFollowRecipe} />
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
        <Card className="mt-8">
          <CardHeader>Comentarios de usuario</CardHeader>
          <CardBody>
            {comments.length === 0 ? (
              <p>No hay comentarios disponibles.</p>
            ) : (
              <ul>
                {comments.map((comment, index) => (
                  <li key={index} className="mb-4">
                    <div className="flex items-center mb-2">
                      <Avatar size="small" className="mr-2" />
                      <div>
                        <strong className="text-lg">{comment.UsuarioComen}</strong>
                      </div>
                    </div>
                    <div className="ml-10">{comment.Comentario}</div>
                  </li>
                ))}
              </ul>
            )}
          </CardBody>
        </Card>
        <Spacer />
        <h1 className={title({ color: "pink", size: "sm" })}>
          ¿Que opinas de la receta?
        </h1>
        <Card className="mt-4">
          <CardBody>
            {receta?.comentarios &&
              receta.comentarios.map((comentario, index) => (
                <div key={index} className="mb-4">
                  <strong>{comentario.usuario}</strong>: {comentario.texto}
                </div>
              ))}
          </CardBody>
          <CardFooter className="flex justify-c enter">
            <form className="w-full" onSubmit={handleSubmitComment}>
              <textarea
                rows="4"
                placeholder="Escribe tu comentario aquí"
                className="w-full border rounded p-2 mb-2"
                value={userComment}
                onChange={(e) => setUserComment(e.target.value)}
              />
              <Button type="submit" color="primary">
                Enviar Comentario
              </Button>
            </form>
          </CardFooter>
        </Card>
        <h1 className="mt-4">Tambien puedes calificar la receta</h1>
        <div className="mt-4">
          <div className="flex items-center justify-center">
            {rating.map((isFilled, index) => (
              <div
                key={index}
                onClick={() => handleRating(index + 1)}
                className={`cursor-pointer ${isFilled ? "text-yellow-400 fill-current" : "text-gray-300"
                  }`}
                style={{ width: "40px", height: "40px" }}
              >
                <FavIconFilled className={isFilled ? "" : "hidden"} />
                <FavIcon className={isFilled ? "hidden" : ""} />
              </div>
            ))}
          </div>
          <div className="mt-4">
            {averageRating !== null ? (
              <p>Puntaje promedio: {averageRating.promedio.toFixed(2)}</p>
            ) : (
              <p>Cargando puntaje promedio...</p>
            )}
          </div>
        </div>
      </div>
    </DefaultLayout>
  );
}
