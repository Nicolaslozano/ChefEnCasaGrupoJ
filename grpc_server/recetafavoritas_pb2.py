# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: recetafavoritas.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import symbol_database as _symbol_database
from google.protobuf.internal import builder as _builder
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x15recetafavoritas.proto\"b\n\x0fRecetaFavoritas\x12\x19\n\x11idrecetaFavoritas\x18\x01 \x01(\x05\x12\x1b\n\x13recetasFavoritascol\x18\x02 \x01(\x05\x12\x17\n\x0fusuario_userfav\x18\x03 \x01(\t\"\xcc\x01\n\x11RecetaFavCompleta\x12\x10\n\x08idreceta\x18\x01 \x01(\x05\x12\x0e\n\x06titulo\x18\x02 \x01(\t\x12\x13\n\x0b\x64\x65scripcion\x18\x03 \x01(\t\x12\x19\n\x11tiempoPreparacion\x18\x04 \x01(\x05\x12\x14\n\x0cingredientes\x18\x05 \x01(\t\x12\r\n\x05pasos\x18\x06 \x01(\t\x12\x11\n\turl_fotos\x18\x07 \x03(\t\x12\x14\n\x0cusuario_user\x18\x08 \x01(\t\x12\x17\n\x0fnombreCategoria\x18\t \x01(\t\"\x07\n\x05Nulos\"(\n\x0fUsuariologueado\x12\x15\n\rnombreUsuario\x18\x01 \x01(\t2t\n\tRecetaFav\x12,\n\x10\x41gregarRecetaFav\x12\x10.RecetaFavoritas\x1a\x06.Nulos\x12\x39\n\x0fTraerRecetasFav\x12\x10.Usuariologueado\x1a\x12.RecetaFavCompleta0\x01\x62\x06proto3')

_globals = globals()
_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, _globals)
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'recetafavoritas_pb2', _globals)
if _descriptor._USE_C_DESCRIPTORS == False:

  DESCRIPTOR._options = None
  _globals['_RECETAFAVORITAS']._serialized_start=25
  _globals['_RECETAFAVORITAS']._serialized_end=123
  _globals['_RECETAFAVCOMPLETA']._serialized_start=126
  _globals['_RECETAFAVCOMPLETA']._serialized_end=330
  _globals['_NULOS']._serialized_start=332
  _globals['_NULOS']._serialized_end=339
  _globals['_USUARIOLOGUEADO']._serialized_start=341
  _globals['_USUARIOLOGUEADO']._serialized_end=381
  _globals['_RECETAFAV']._serialized_start=383
  _globals['_RECETAFAV']._serialized_end=499
# @@protoc_insertion_point(module_scope)
