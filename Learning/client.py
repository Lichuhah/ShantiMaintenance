import grpc
import learning_pb2
import learning_pb2_grpc

def run():
    with grpc.insecure_channel('localhost:50051') as channel:
        stub = learning_pb2_grpc.MyServiceStub(channel)
        response = stub.SendData(learning_pb2.DataRequest(message="Hello, Server!"))
        print(f"Response from server: {response.reply}")

if __name__ == '__main__':
    run()