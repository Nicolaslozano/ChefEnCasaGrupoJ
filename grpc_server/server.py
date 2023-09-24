from time import sleep
from usuarios_pb2_grpc import UsuariosServicer, add_UsuariosServicer_to_server
from usuarios_pb2 import Usuario, Response
from datetime import datetime
from google.protobuf.timestamp_pb2 import Timestamp
import grpc
from concurrent import futures
from receta_pb2_grpc import RecetasServicer, add_RecetasServicer_to_server
from receta_pb2 import Receta, RecetaEditar, Responsea
import mysql.connector
from recetafavoritas_pb2_grpc import RecetaFavServicer, add_RecetaFavServicer_to_server
from recetafavoritas_pb2 import RecetaFavoritas # aca va el nombre del messege proto
from suscripcion_pb2_grpc import SuscripcionesServicer, add_SuscripcionesServicer_to_server
from suscripcion_pb2 import Suscripcion


class ServicioUsuarios(UsuariosServicer):

    def Listo(self, request, context):
        return Nulo()

    def AltaUsuario(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root', 
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()
        query = f"SELECT * FROM usuario WHERE user = '{request.user}'"
        cursor.execute(query)
        row = cursor.fetchone()
        if row is not None:
            return Response(message="400 - Ya existe usuario")
        query = (f"INSERT INTO usuario (`nombre`, `email`, `user`, `password`,`popular`) VALUES "
                 f"('{request.nombre}', '{request.email}', '{request.user}', '{request.password}', 0)")
        cursor.execute(query)
        cnx.commit()
        resp = Response(message="204", idusuario=cursor.lastrowid)
        cursor.close()
        cnx.close()
        return resp

    def TraerUsuario(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root', 
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor(dictionary=True)  # Use dictionary cursor
        query = (f"SELECT * FROM usuario WHERE user = '{request.user}' AND password = '{request.password}'")
        cursor.execute(query)
        row = cursor.fetchone()
        cursor.close()
        cnx.close()

        if row is not None:
            return Usuario(idusuario=row['idusuario'], nombre=row['nombre'],
                           email=row['email'], user=row['user'], password=row['password'], popular=row['popular'])
        else:
            return Usuario()


    def SeguirUsuario(self, request, context):
        
        if request.segui == request.user:
            return Response(message="No puedes seguirte a ti mismo")
        
        cnx = mysql.connector.connect(user='root', password='root',
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()

        try:

            query = (f"INSERT INTO suscripcion (`followed_user`, `my_user`) VALUES "
                    f"('{request.segui}', '{request.user}')")
            cursor.execute(query)
            cnx.commit()
            if cursor.rowcount > 0:
                resp = Response(message="Se pudo seguir el usuario")
            else:
                resp = Response(message="No se pudo seguir el usuario")
        except mysql.connector.Error as err:
            resp = Response(message=f"Error en la base de datos: {err}")        
        finally:        
            cursor.close()
            cnx.close()
        return resp

    def EliminarSeguidor(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root',
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()

        try:
            query = (f"DELETE FROM suscripcion WHERE followed_user = '{request.segui}' AND my_user = '{request.user}'") 
            cursor.execute(query)
            cnx.commit()
            if cursor.rowcount > 0:
                resp = Response(message="Se pudo eliminar el seguidor")
            else:
                resp = Response(message="No se encontró el seguidor a eliminar")
        except mysql.connector.Error as err:
            resp = Response(message=f"Error en la base de datos: {err}")
        finally:
            cursor.close()
            cnx.close()
        return resp   


class ServicioSuscripciones(SuscripcionesServicer):

    def TraerSeguidores(self, request, context):
        print("usuario recibido:", request.s)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM suscripcion AS s WHERE my_user = '{request.s}' ")
        cursor.execute(query)
        records = cursor.fetchall()
        for row in records:
            yield Suscripcion(idsuscripcion = row.idsuscripcion, followed_user = row.followed_user, my_user = row.my_user)




class ServicioRecetas(RecetasServicer):

    def Listo(self, request, context):
        return Nulo()

    def AltaReceta(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root', 
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()
            # Split the comma-separated string of URLs into a list
        url_fotos_list = request.url_fotos[0].split(',')

        # Initialize the SQL statement
        stmt = f"INSERT INTO receta (`titulo`, `descripcion`, `tiempoPreparacion`,  `ingredientes`, `pasos`, `usuario_user`,`nombreCategoria1`"

        # Initialize the VALUES part of the SQL statement
        values = f" VALUES ('{request.titulo}', '{request.descripcion}', '{request.tiempoPreparacion}', '{request.ingredientes}', '{request.pasos}', '{request.usuario_user}','{request.nombreCategoria}'"

        # Iterate through the split URLs and insert them into separate columns
        for idx, url in enumerate(url_fotos_list, start=1):
            stmt += f", `url_foto{idx}`"
            values += f", '{url.strip()}'"

        stmt += ")"
        query = stmt + values + ")"

        cursor.execute(query)
        cnx.commit()
        resp = Responsea(message="204", idreceta=cursor.lastrowid)
        cursor.close()
        cnx.close()
        return resp
    
    def EditarReceta(self, request, context):
        cnx =mysql.connector.connect(user='root', password='root',
                             host='localhost', port='3306',
                             database='chefencasagrupoj')
        cursor = cnx.cursor()
        query = (f"SELECT * FROM receta WHERE idreceta = '{request.idreceta}'")
        cursor.execute(query)
        row = cursor.fetchone()

        if row is None:
            return Response(message = "404 Not-Found. La receta con ese id no existe")
        else:
            query = (f"UPDATE receta SET `titulo` ='{request.titulo}', `descripcion` ='{request.descripcion}', `tiempoPreparacion`='{request.tiempoPreparacion}', "+
            f"`ingredientes`='{request.ingredientes}', `pasos`= '{request.pasos}', `nombreCategoria1`= '{request.nombreCategoria}'")
            for idx, url_foto in enumerate(request.url_fotos, start=1):
                query += (f", `url_foto{idx}` = '{url_foto}'")
            query += (f"where idreceta= '{request.idreceta}' ")
            cursor.execute(query)
            cnx.commit()

            resp = Responsea(message="204", idreceta=cursor.lastrowid)
            cursor.close()
            cnx.close()
            return resp
        

    def TraerRecetas(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root', 
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta "+
        "INNER JOIN usuario AS u INNER JOIN categoria AS c WHERE receta.usuario_user = u.user " +
        " AND receta.nombreCategoria1 = c.nombreCategoria")
        cursor.execute(query)
        records = cursor.fetchall()
        for row in records:
            fotos = []
            if row.url_foto1 is not None:
                fotos.append(row.url_foto1)
            if row.url_foto2 is not None:
                fotos.append(row.url_foto2)
            if row.url_foto3 is not None:
                fotos.append(row.url_foto3)
            if row.url_foto4 is not None:
                fotos.append(row.url_foto4)
            if row.url_foto5 is not None:
                fotos.append(row.url_foto5)
            yield Receta(idreceta = row.idreceta, titulo = row.titulo, descripcion = row.descripcion,
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_user = row.usuario_user)

    def TraerRecetaPorId(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root', 
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta INNER JOIN usuario AS u INNER JOIN categoria AS c WHERE receta.usuario_user = u.user AND receta.nombreCategoria1 = c.nombreCategoria AND receta.idreceta = '{request.idreceta}'")
        cursor.execute(query)
        row = cursor.fetchone()
        if row is not None:
            fotos = []
            if row.url_foto1 is not None:
                fotos.append(row.url_foto1)
            if row.url_foto2 is not None:
                fotos.append(row.url_foto2)
            if row.url_foto3 is not None:
                fotos.append(row.url_foto3)
            if row.url_foto4 is not None:
                fotos.append(row.url_foto4)
            if row.url_foto5 is not None:
                fotos.append(row.url_foto5)
            result = Receta(idreceta = row.idreceta, titulo = row.titulo, descripcion = row.descripcion,
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_user = row.usuario_user)
        return result

    def TraerRecetasPorUsuario(self, request, context):
        print("usuario recibido:", request.usu)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta AS r WHERE r.usuario_user  = '{request.usu}' ")
        cursor.execute(query)
        records = cursor.fetchall()
        for row in records:
            fotos = []
            if row.url_foto1 is not None:
                fotos.append(row.url_foto1)
            if row.url_foto2 is not None:
                fotos.append(row.url_foto2)
            if row.url_foto3 is not None:
                fotos.append(row.url_foto3)
            if row.url_foto4 is not None:
                fotos.append(row.url_foto4)
            if row.url_foto5 is not None:
                fotos.append(row.url_foto5)
            yield Receta(idreceta = row.idreceta, titulo = row.titulo, descripcion = row.descripcion,
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_user = row.usuario_user)

    def TraerRecetasPorCategoria(self, request, context):
        print("Categoria recibida:", request.usu)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta AS r WHERE r.nombreCategoria1 = '{request.usu}' ")
        cursor.execute(query)
        records = cursor.fetchall()
        for row in records:
            fotos = []
            if row.url_foto1 is not None:
                fotos.append(row.url_foto1)
            if row.url_foto2 is not None:
                fotos.append(row.url_foto2)
            if row.url_foto3 is not None:
                fotos.append(row.url_foto3)
            if row.url_foto4 is not None:
                fotos.append(row.url_foto4)
            if row.url_foto5 is not None:
                fotos.append(row.url_foto5)
            yield Receta(idreceta = row.idreceta, titulo = row.titulo, descripcion = row.descripcion,
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_user = row.usuario_user)


    def TraerRecetasPorTitulo(self, request, context):
        print("titulo recibido:", request.usu)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta AS r WHERE r.titulo LIKE CONCAT('%', '{request.usu}', '%') ")
        cursor.execute(query)
        records = cursor.fetchall()
        for row in records:
            fotos = []
            if row.url_foto1 is not None:
                fotos.append(row.url_foto1)
            if row.url_foto2 is not None:
                fotos.append(row.url_foto2)
            if row.url_foto3 is not None:
                fotos.append(row.url_foto3)
            if row.url_foto4 is not None:
                fotos.append(row.url_foto4)
            if row.url_foto5 is not None:
                fotos.append(row.url_foto5)
            yield Receta(idreceta = row.idreceta, titulo = row.titulo, descripcion = row.descripcion,
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_user = row.usuario_user)

    def TraerRecetasPorTiempo(self, request, context):
        print("tiempo desde recibido:", request.desde)
        print("tiempo hasta recibido:", request.hasta)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta AS r WHERE r.tiempoPreparacion >= {request.desde} AND r.tiempoPreparacion <= {request.hasta} ")
        cursor.execute(query)
        records = cursor.fetchall()
        for row in records:
            fotos = []
            if row.url_foto1 is not None:
                fotos.append(row.url_foto1)
            if row.url_foto2 is not None:
                fotos.append(row.url_foto2)
            if row.url_foto3 is not None:
                fotos.append(row.url_foto3)
            if row.url_foto4 is not None:
                fotos.append(row.url_foto4)
            if row.url_foto5 is not None:
                fotos.append(row.url_foto5)
            yield Receta(idreceta = row.idreceta, titulo = row.titulo, descripcion = row.descripcion,
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_user = row.usuario_user)

    def TraerRecetasPorIngredientes(self, request, context):
        print("Ingrediente recibido:", request.usu)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta AS r WHERE r.ingredientes LIKE CONCAT('%', '{request.usu}', '%') ")
        cursor.execute(query)
        records = cursor.fetchall()
        for row in records:
            fotos = []
            if row.url_foto1 is not None:
                fotos.append(row.url_foto1)
            if row.url_foto2 is not None:
                fotos.append(row.url_foto2)
            if row.url_foto3 is not None:
                fotos.append(row.url_foto3)
            if row.url_foto4 is not None:
                fotos.append(row.url_foto4)
            if row.url_foto5 is not None:
                fotos.append(row.url_foto5)
            yield Receta(idreceta = row.idreceta, titulo = row.titulo, descripcion = row.descripcion,
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_user = row.usuario_user)




class ServicioRecetasFav(RecetaFavServicer):

    def AgregarRecetaFav(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root',
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()
        query = (f"INSERT INTO recetaFavoritas (`idrecetaFavoritas`, `recetasFavoritascol`, `usuario_userfav`) VALUES "
                 f"('{request.idrecetaFavoritas}', '{request.recetasFavoritascol}', '{request.usuario_userfav}')")
        cursor.execute(query)
        cnx.commit()
        resp = Responsea(message="204", idreceta=cursor.lastrowid)
        cursor.close()
        cnx.close()
        return resp
    
    def TraerRecetasFav(self, request, context):
        print("ID de usuario recibido:", request.nombreUsuario)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM recetaFavoritas AS rf  INNER JOIN receta AS r WHERE rf.usuario_userfav = '{request.nombreUsuario}' AND r.idreceta = rf.recetasFavoritascol")
        cursor.execute(query)
        records = cursor.fetchall()
        for row in records:
            fotos = []
            if row.url_foto1 is not None:
                fotos.append(row.url_foto1)
            if row.url_foto2 is not None:
                fotos.append(row.url_foto2)
            if row.url_foto3 is not None:
                fotos.append(row.url_foto3)
            if row.url_foto4 is not None:
                fotos.append(row.url_foto4)
            if row.url_foto5 is not None:
                fotos.append(row.url_foto5)
            yield Receta(idreceta = row.idreceta, titulo = row.titulo, descripcion = row.descripcion,
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_user = row.usuario_user)

  

def start():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    add_UsuariosServicer_to_server(ServicioUsuarios(), server)
    add_RecetasServicer_to_server(ServicioRecetas(), server)
    add_RecetaFavServicer_to_server(ServicioRecetasFav(), server)
    add_SuscripcionesServicer_to_server(ServicioSuscripciones(), server)
    server.add_insecure_port('[::]:50051')
    print("The server is running!")
    server.start()
    server.wait_for_termination()

    
if __name__ == "__main__":
    start()