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
        query = (f"INSERT INTO receta (`titulo`, `descripcion`, `tiempoPreparacion`,  `ingredientes`, `pasos`, `usuario_idusuario`,`categoria_idcategoria`) VALUES "
                 f"('{request.titulo}', '{request.descripcion}', '{request.tiempoPreparacion}', '{request.ingredientes}', '{request.pasos}', '{request.usuario_idusuario}','{request.categoria_idcategoria}')")
        cursor.execute(query)
        cnx.commit()
        resp = Responsea(message="204", idreceta=cursor.lastrowid)
        cursor.close()
        cnx.close()
        return resp

    




def start():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    add_UsuariosServicer_to_server(ServicioUsuarios(), server)
    add_RecetasServicer_to_server(ServicioRecetas(), server)
    server.add_insecure_port('[::]:50051')
    print("The server is running!")
    server.start()
    server.wait_for_termination()

    


if __name__ == "__main__":
    start()