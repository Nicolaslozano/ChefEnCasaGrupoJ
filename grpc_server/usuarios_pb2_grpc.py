# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
"""Client and server classes corresponding to protobuf-defined services."""
import grpc

import usuarios_pb2 as usuarios__pb2


class UsuariosStub(object):
    """Missing associated documentation comment in .proto file."""

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.Listo = channel.unary_unary(
                '/Usuarios/Listo',
                request_serializer=usuarios__pb2.Nulo.SerializeToString,
                response_deserializer=usuarios__pb2.Nulo.FromString,
                )
        self.TraerUsuario = channel.unary_unary(
                '/Usuarios/TraerUsuario',
                request_serializer=usuarios__pb2.Username.SerializeToString,
                response_deserializer=usuarios__pb2.Usuario.FromString,
                )
        self.AltaUsuario = channel.unary_unary(
                '/Usuarios/AltaUsuario',
                request_serializer=usuarios__pb2.Usuario.SerializeToString,
                response_deserializer=usuarios__pb2.Response.FromString,
                )


class UsuariosServicer(object):
    """Missing associated documentation comment in .proto file."""

    def Listo(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def TraerUsuario(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def AltaUsuario(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_UsuariosServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'Listo': grpc.unary_unary_rpc_method_handler(
                    servicer.Listo,
                    request_deserializer=usuarios__pb2.Nulo.FromString,
                    response_serializer=usuarios__pb2.Nulo.SerializeToString,
            ),
            'TraerUsuario': grpc.unary_unary_rpc_method_handler(
                    servicer.TraerUsuario,
                    request_deserializer=usuarios__pb2.Username.FromString,
                    response_serializer=usuarios__pb2.Usuario.SerializeToString,
            ),
            'AltaUsuario': grpc.unary_unary_rpc_method_handler(
                    servicer.AltaUsuario,
                    request_deserializer=usuarios__pb2.Usuario.FromString,
                    response_serializer=usuarios__pb2.Response.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'Usuarios', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))


 # This class is part of an EXPERIMENTAL API.
class Usuarios(object):
    """Missing associated documentation comment in .proto file."""

    @staticmethod
    def Listo(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/Usuarios/Listo',
            usuarios__pb2.Nulo.SerializeToString,
            usuarios__pb2.Nulo.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def TraerUsuario(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/Usuarios/TraerUsuario',
            usuarios__pb2.Username.SerializeToString,
            usuarios__pb2.Usuario.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def AltaUsuario(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/Usuarios/AltaUsuario',
            usuarios__pb2.Usuario.SerializeToString,
            usuarios__pb2.Response.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)
