from concurrent import futures
import grpc
import learning_pb2
import learning_pb2_grpc
import json
import os
import sys
from cassandra.cluster import Cluster
from cassandra.query import SimpleStatement
from datetime import datetime
from pymongo import MongoClient
from gridfs import GridFS
import pickle

def exec_data_prepare_script(id):
    sys.argv = ['data_prepare.py',str(id)]
    scr = db.data_prepare.find_one({"assetId": 0})["script"]
    exec(scr)

        
def read_data_from_cassandra(table_name, start_date, end_date):
    cluster = Cluster(['localhost'], port=9042)
    session = cluster.connect('master')
    start_date = datetime.strptime(start_date, "%Y-%m-%d %H:%M:%S")
    end_date = datetime.strptime(end_date, "%Y-%m-%d %H:%M:%S")
    
    query = f"""
    SELECT * FROM {table_name} 
    WHERE datetime >= %s AND datetime <= %s
    ALLOW FILTERING
    """
    statement = SimpleStatement(query)
    rows = session.execute(statement, (start_date, end_date))
 
    cluster.shutdown()
    result = [row for row in rows]
    return result

class GrpcLearningServicer(learning_pb2_grpc.GrpcLearningServicer):
    def SendData(self, request, context):
        print(f"Received message: {request.message}")
        return learning_pb2.DataResponse(reply="Message received!")
    
    def StartLearning(self, request, context):
        id = request.assetId
        os.makedirs("asset-"+str(id), exist_ok=True)    
        
        start_date = "2024-01-01 00:00:00"
        end_date = "2024-05-30 23:59:59"
        telemetry=read_data_from_cassandra("sensor_data_"+str(id), start_date, end_date)
        telemetry2=[]
        
        db.data_objects.delete_many({"assetId": id})
        db.prepared_data.delete_many({"assetId": id})
         
        data = {
          "assetId": id,
          "defects": request.defects,
          "works": request.works,
          "failures": request.failures,
          "telemetry": json.dumps(telemetry+telemetry2, default=str)
        }
        db.data_objects.insert_one(data)
        exec_data_prepare_script(id);
        return learning_pb2.StartLearningReply(result=True)

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    learning_pb2_grpc.add_GrpcLearningServicer_to_server(GrpcLearningServicer(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    print("Server is running on port 50051...")
    server.wait_for_termination()
 
client = MongoClient("mongodb://root:example@localhost:27017/")
db = client['shanti']

if __name__ == '__main__':
    serve()