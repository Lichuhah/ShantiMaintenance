# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# NO CHECKED-IN PROTOBUF GENCODE
# source: learning.proto
# Protobuf Python Version: 5.27.2
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import runtime_version as _runtime_version
from google.protobuf import symbol_database as _symbol_database
from google.protobuf.internal import builder as _builder
_runtime_version.ValidateProtobufRuntimeVersion(
    _runtime_version.Domain.PUBLIC,
    5,
    27,
    2,
    '',
    'learning.proto'
)
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x0elearning.proto\x12\x05greet\"b\n\rGetRulRequest\x12\x0f\n\x07\x61ssetId\x18\x01 \x01(\x05\x12\x0e\n\x06typeId\x18\x02 \x01(\x05\x12\r\n\x05works\x18\x03 \x01(\t\x12\x0f\n\x07\x64\x65\x66\x65\x63ts\x18\x04 \x01(\t\x12\x10\n\x08\x66\x61ilures\x18\x05 \x01(\t\"\x1a\n\x0bGetRulReply\x12\x0b\n\x03rul\x18\x01 \x01(\x05\"g\n\x12SetLearningRequest\x12\x0f\n\x07\x61ssetId\x18\x01 \x01(\x05\x12\x0e\n\x06typeId\x18\x02 \x01(\x05\x12\r\n\x05works\x18\x03 \x01(\t\x12\x0f\n\x07\x64\x65\x66\x65\x63ts\x18\x04 \x01(\t\x12\x10\n\x08\x66\x61ilures\x18\x05 \x01(\t\"2\n\x14StartLearningRequest\x12\n\n\x02id\x18\x01 \x01(\x05\x12\x0e\n\x06typeId\x18\x02 \x01(\x05\"$\n\x12StartLearningReply\x12\x0e\n\x06result\x18\x01 \x01(\x08\"5\n\x13LearningRequestItem\x12\x10\n\x08\x64\x61tetime\x18\x01 \x01(\t\x12\x0c\n\x04type\x18\x02 \x01(\x05\x32\xd4\x01\n\x0cGrpcLearning\x12\x32\n\x06GetRul\x12\x14.greet.GetRulRequest\x1a\x12.greet.GetRulReply\x12G\n\rStartLearning\x12\x1b.greet.StartLearningRequest\x1a\x19.greet.StartLearningReply\x12G\n\x0fSetLearningData\x12\x19.greet.SetLearningRequest\x1a\x19.greet.StartLearningReplyB\x0b\xaa\x02\x08Learningb\x06proto3')

_globals = globals()
_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, _globals)
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'learning_pb2', _globals)
if not _descriptor._USE_C_DESCRIPTORS:
  _globals['DESCRIPTOR']._loaded_options = None
  _globals['DESCRIPTOR']._serialized_options = b'\252\002\010Learning'
  _globals['_GETRULREQUEST']._serialized_start=25
  _globals['_GETRULREQUEST']._serialized_end=123
  _globals['_GETRULREPLY']._serialized_start=125
  _globals['_GETRULREPLY']._serialized_end=151
  _globals['_SETLEARNINGREQUEST']._serialized_start=153
  _globals['_SETLEARNINGREQUEST']._serialized_end=256
  _globals['_STARTLEARNINGREQUEST']._serialized_start=258
  _globals['_STARTLEARNINGREQUEST']._serialized_end=308
  _globals['_STARTLEARNINGREPLY']._serialized_start=310
  _globals['_STARTLEARNINGREPLY']._serialized_end=346
  _globals['_LEARNINGREQUESTITEM']._serialized_start=348
  _globals['_LEARNINGREQUESTITEM']._serialized_end=401
  _globals['_GRPCLEARNING']._serialized_start=404
  _globals['_GRPCLEARNING']._serialized_end=616
# @@protoc_insertion_point(module_scope)
