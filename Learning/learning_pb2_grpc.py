# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
"""Client and server classes corresponding to protobuf-defined services."""
import grpc
import warnings

import learning_pb2 as learning__pb2

GRPC_GENERATED_VERSION = '1.67.1'
GRPC_VERSION = grpc.__version__
_version_not_supported = False

try:
    from grpc._utilities import first_version_is_lower
    _version_not_supported = first_version_is_lower(GRPC_VERSION, GRPC_GENERATED_VERSION)
except ImportError:
    _version_not_supported = True

if _version_not_supported:
    raise RuntimeError(
        f'The grpc package installed is at version {GRPC_VERSION},'
        + f' but the generated code in learning_pb2_grpc.py depends on'
        + f' grpcio>={GRPC_GENERATED_VERSION}.'
        + f' Please upgrade your grpc module to grpcio>={GRPC_GENERATED_VERSION}'
        + f' or downgrade your generated code using grpcio-tools<={GRPC_VERSION}.'
    )


class GrpcLearningStub(object):
    """Missing associated documentation comment in .proto file."""

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.GetRul = channel.unary_unary(
                '/greet.GrpcLearning/GetRul',
                request_serializer=learning__pb2.GetRulRequest.SerializeToString,
                response_deserializer=learning__pb2.GetRulReply.FromString,
                _registered_method=True)
        self.StartLearning = channel.unary_unary(
                '/greet.GrpcLearning/StartLearning',
                request_serializer=learning__pb2.StartLearningRequest.SerializeToString,
                response_deserializer=learning__pb2.StartLearningReply.FromString,
                _registered_method=True)


class GrpcLearningServicer(object):
    """Missing associated documentation comment in .proto file."""

    def GetRul(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def StartLearning(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_GrpcLearningServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'GetRul': grpc.unary_unary_rpc_method_handler(
                    servicer.GetRul,
                    request_deserializer=learning__pb2.GetRulRequest.FromString,
                    response_serializer=learning__pb2.GetRulReply.SerializeToString,
            ),
            'StartLearning': grpc.unary_unary_rpc_method_handler(
                    servicer.StartLearning,
                    request_deserializer=learning__pb2.StartLearningRequest.FromString,
                    response_serializer=learning__pb2.StartLearningReply.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'greet.GrpcLearning', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))
    server.add_registered_method_handlers('greet.GrpcLearning', rpc_method_handlers)


 # This class is part of an EXPERIMENTAL API.
class GrpcLearning(object):
    """Missing associated documentation comment in .proto file."""

    @staticmethod
    def GetRul(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(
            request,
            target,
            '/greet.GrpcLearning/GetRul',
            learning__pb2.GetRulRequest.SerializeToString,
            learning__pb2.GetRulReply.FromString,
            options,
            channel_credentials,
            insecure,
            call_credentials,
            compression,
            wait_for_ready,
            timeout,
            metadata,
            _registered_method=True)

    @staticmethod
    def StartLearning(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(
            request,
            target,
            '/greet.GrpcLearning/StartLearning',
            learning__pb2.StartLearningRequest.SerializeToString,
            learning__pb2.StartLearningReply.FromString,
            options,
            channel_credentials,
            insecure,
            call_credentials,
            compression,
            wait_for_ready,
            timeout,
            metadata,
            _registered_method=True)
