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
        query = (f"INSERT INTO usuario (`nombre`, `email`, `user`, `password`) VALUES "
                 f"('{request.nombre}', '{request.email}', '{request.user}', '{request.password}')")
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
                           email=row['email'], user=row['user'], password=row['password'])
        else:
            return Usuario()

class ServicioRecetas(RecetasServicer):

    def Listo(self, request, context):
        return Nulo()

    def AltaReceta(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root', 
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()
        stmt = f"INSERT INTO receta (`titulo`, `descripcion`, `tiempoPreparacion`,  `ingredientes`, `pasos`, `usuario_idusuario`,`nombreCategoria1`"
        values =  f" VALUES ('{request.titulo}', '{request.descripcion}', '{request.tiempoPreparacion}', '{request.ingredientes}', '{request.pasos}', '{request.usuario_idusuario}','{request.nombreCategoria}'" 
        for idx,url_foto in enumerate(request.url_fotos,start=1):
            stmt += f", `url_foto{idx}`"
            values += f", '{url_foto}'"
        stmt += ")"
        query = stmt+values+")"

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
        "INNER JOIN usuario AS u INNER JOIN categoria AS c WHERE receta.usuario_idusuario = u.idusuario " +
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
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_idusuario = row.usuario_idusuario)


    def TraerRecetasPorUsuario(self, request, context):
        print("usuario recibido:", request.usu)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta AS r WHERE r.usuario_idusuario = (SELECT idusuario FROM usuario AS u WHERE u.user = '{request.usu}' )")
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
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_idusuario = row.usuario_idusuario)





class ServicioRecetasFav(RecetaFavServicer):

    def AgregarRecetaFav(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root',
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()
        query = (f"INSERT INTO recetaFavoritas (`idrecetaFavoritas`, `recetasFavoritascol`, `usuario_idusuario1`) VALUES "
                 f"('{request.idrecetaFavoritas}', '{request.recetasFavoritascol}', '{request.usuario_idusuario}')")
        cursor.execute(query)
        cnx.commit()
        resp = Responsea(message="204", idreceta=cursor.lastrowid)
        cursor.close()
        cnx.close()
        return resp
    
    def TraerRecetasFav(self, request, context):
        print("ID de usuario recibido:", request.idusuario)
        cnx = mysql.connector.connect(user='root', password='root', 
                              host='localhost', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM recetaFavoritas AS rf  INNER JOIN receta AS r WHERE rf.usuario_idusuario1 = {request.idusuario} AND r.idreceta = rf.recetasFavoritascol")
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
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, nombreCategoria = row.nombreCategoria1, usuario_idusuario = row.usuario_idusuario)


  

def start():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    add_UsuariosServicer_to_server(ServicioUsuarios(), server)
    add_RecetasServicer_to_server(ServicioRecetas(), server)
    add_RecetaFavServicer_to_server(ServicioRecetasFav(), server)
    server.add_insecure_port('[::]:50051')
    print("The server is running!")
    server.start()
    server.wait_for_termination()

    
if __name__ == "__main__":
    start()