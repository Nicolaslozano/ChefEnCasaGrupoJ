# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: receta.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import symbol_database as _symbol_database
from google.protobuf.internal import builder as _builder
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x0creceta.proto\"\xcc\x01\n\x06Receta\x12\x10\n\x08idreceta\x18\x01 \x01(\x05\x12\x0e\n\x06titulo\x18\x02 \x01(\t\x12\x13\n\x0b\x64\x65scripcion\x18\x03 \x01(\t\x12\x19\n\x11tiempoPreparacion\x18\x04 \x01(\x05\x12\x14\n\x0cingredientes\x18\x05 \x01(\t\x12\r\n\x05pasos\x18\x06 \x01(\t\x12\x11\n\turl_fotos\x18\x07 \x03(\t\x12\x19\n\x11usuario_idusuario\x18\x08 \x01(\x05\x12\x1d\n\x15\x63\x61tegoria_idcategoria\x18\t \x01(\x05\"\xb7\x01\n\x0cRecetaEditar\x12\x10\n\x08idreceta\x18\x01 \x01(\x05\x12\x0e\n\x06titulo\x18\x02 \x01(\t\x12\x13\n\x0b\x64\x65scripcion\x18\x03 \x01(\t\x12\x19\n\x11tiempoPreparacion\x18\x04 \x01(\x05\x12\x14\n\x0cingredientes\x18\x05 \x01(\t\x12\r\n\x05pasos\x18\x06 \x01(\t\x12\x11\n\turl_fotos\x18\x07 \x03(\t\x12\x1d\n\x15\x63\x61tegoria_idcategoria\x18\t \x01(\x05\"\x0c\n\nNuloReceta\".\n\tResponsea\x12\x0f\n\x07message\x18\x01 \x01(\t\x12\x10\n\x08idreceta\x18\x02 \x01(\x05\"&\n\nRecetaList\x12\x18\n\x07recetas\x18\x01 \x03(\x0b\x32\x07.Receta2\x7f\n\x07Recetas\x12!\n\nAltaReceta\x12\x07.Receta\x1a\n.Responsea\x12)\n\x0c\x45\x64itarReceta\x12\r.RecetaEditar\x1a\n.Responsea\x12&\n\x0cTraerRecetas\x12\x0b.NuloReceta\x1a\x07.Receta0\x01\x62\x06proto3')

_globals = globals()
_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, _globals)
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'receta_pb2', _globals)
if _descriptor._USE_C_DESCRIPTORS == False:

  DESCRIPTOR._options = None
  _globals['_RECETA']._serialized_start=17
  _globals['_RECETA']._serialized_end=221
  _globals['_RECETAEDITAR']._serialized_start=224
  _globals['_RECETAEDITAR']._serialized_end=407
  _globals['_NULORECETA']._serialized_start=409
  _globals['_NULORECETA']._serialized_end=421
  _globals['_RESPONSEA']._serialized_start=423
  _globals['_RESPONSEA']._serialized_end=469
  _globals['_RECETALIST']._serialized_start=471
  _globals['_RECETALIST']._serialized_end=509
  _globals['_RECETAS']._serialized_start=511
  _globals['_RECETAS']._serialized_end=638
# @@protoc_insertion_point(module_scope)
