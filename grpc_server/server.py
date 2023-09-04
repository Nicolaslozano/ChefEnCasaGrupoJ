from time import sleep
from usuarios_pb2_grpc import UsuariosServicer, add_UsuariosServicer_to_server
from usuarios_pb2 import Usuario, Response
from datetime import datetime
from google.protobuf.timestamp_pb2 import Timestamp
import grpc
from concurrent import futures
from receta_pb2_grpc import RecetasServicer, add_RecetasServicer_to_server
from receta_pb2 import Receta, Responsea
import mysql.connector
from recetafavoritas_pb2_grpc import RecetaFavServicer, add_RecetaFavServicer_to_server
from recetafavoritas_pb2 import RecetaFavoritas # aca va el nombre del messege proto

class ServicioUsuarios(UsuariosServicer):

    def Listo(self, request, context):
        return Nulo()

    def AltaUsuario(self, request, context):
        cnx = mysql.connector.connect(user='root', password='password',
                                      host='192.168.99.100', port='3306',
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
        cnx = mysql.connector.connect(user='root', password='password',
                                      host='192.168.99.100', port='3306',
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
        cnx = mysql.connector.connect(user='root', password='password',
                                      host='192.168.99.100', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()
        stmt = f"INSERT INTO receta (`titulo`, `descripcion`, `tiempoPreparacion`,  `ingredientes`, `pasos`, `usuario_idusuario`,`categoria_idcategoria`"
        values =  f" VALUES ('{request.titulo}', '{request.descripcion}', '{request.tiempoPreparacion}', '{request.ingredientes}', '{request.pasos}', '{request.usuario_idusuario}','{request.categoria_idcategoria}'"
          
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

    def TraerRecetas(self, request, context):
        cnx = mysql.connector.connect(user='root', password='password', 
                              host='192.168.99.100', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM receta "+
        "INNER JOIN usuario AS u INNER JOIN categoria AS c WHERE receta.usuario_idusuario = u.idusuario " +
        " AND receta.categoria_idcategoria = c.idcategoria")
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
            tiempoPreparacion = row.tiempoPreparacion, ingredientes = row.ingredientes,  pasos = row.pasos, url_fotos = fotos, categoria_idcategoria = row.categoria_idcategoria, usuario_idusuario = row.usuario_idusuario)

class ServicioRecetasFav(RecetaFavServicer):

    def AgregarRecetaFav(self, request, context):
        cnx = mysql.connector.connect(user='root', password='password',
                                      host='192.168.99.100', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor()
        query = (f"INSERT INTO recetaFavoritas (`idrecetaFavoritas`, `recetasFavoritascol`, `usuario_idusuario`) VALUES "
                 f"('{request.idrecetaFavoritas}', '{request.recetasFavoritascol}', '{request.usuario_idusuario}')")
        cursor.execute(query)
        cnx.commit()
        resp = Responsea(message="204", idreceta=cursor.lastrowid)
        cursor.close()
        cnx.close()
        return resp
    
    def TraerRecetasFav(self, request, context):
        cnx = mysql.connector.connect(user='root', password='password', 
                              host='192.168.99.100', port='3306',
                              database='chefencasagrupoj')
        cursor = cnx.cursor(named_tuple=True)
        query = (f"SELECT * FROM recetaFavoritas "+
                 "INNER JOIN usuario AS u"+ "INNER JOIN receta AS r"+
                 "WHERE recetafavorita.usuario_idusuario = u.idusuario "+
                 "AND r.idReceta = recetaFavoritas.recetasFavoritascol")
        cursor.execute(query)
        records = cursor.fetchall()


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