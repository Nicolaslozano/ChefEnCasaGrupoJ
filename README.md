# ChefEnCasaGrupoJ

----Agustin

    def AgregarAFavoritos(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root',
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor(dictionary=True)  
        query = ( "INSERT INTO suscripcion (`idsuscripcion`, `usuario_idusuario`) VALUES "
                f"('{request.idsuscripcion}', '{request.usuario_idusuario}'")
        cursor.execute(query)
        cnx.commit()
        cursor.close()
        agregado = Response(message="Usuario Agregado Exitosamente.")
        cnx.close()
        return agregado()
        
    def TraerListaFavoritos(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root',
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor(dictionary=True) 
        query = (f"SELECT su.idSeguidor, u.user, u.nombre, u.email FROM suscriptor_usuarios su INNER JOIN usuario uON su.idUsuarioSeguido = u.idusuario")
        cursor.execute(query)
        lista = cursor.fetchall()
        cursor.close()
        cnx.close()
        return lista()

    def EliminarDeFavoritos(self, request, context):
        cnx = mysql.connector.connect(user='root', password='root',
                                      host='localhost', port='3306',
                                      database='chefencasagrupoj')
        cursor = cnx.cursor(dictionary=True) 
        query = (f"DELETE FROM suscripcion WHERE idsuscripcion = '{request.idsuscripcion}' AND usuario_idusuario = '{request.usuario_idusuario}'")
        cursor.execute(query)
        cnx.commit()
        cursor.close()
        cnx.close()
        eliminado = Response(message="Usuario Eliminado Exitosamente.")
        return eliminado()

        
