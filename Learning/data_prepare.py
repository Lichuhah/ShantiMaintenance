import pandas as pd
import matplotlib.pyplot as plt
import sys
from io import StringIO

id = sys.argv[1]
path = "./asset-"+sys.argv[1]


client = MongoClient("mongodb://root:example@localhost:27017/")  # Подключаемся к MongoDB
db = client['shanti']  # Имя базы данных
data_object = db.data_objects.find_one({"assetId": int(id)})
telemetry = pd.read_json(StringIO(data_object["telemetry"]))
telemetry=telemetry.rename(columns={0: "key", 1: "assetId", 2: "datetime", 3: "pressure", 4:"rotate", 5:"vibration", 6:"volt"})
telemetry=telemetry.drop(["key", "assetId"], axis=1)
telemetry['datetime']=pd.to_datetime(telemetry['datetime'])


defects = pd.read_json(StringIO(data_object["defects"]))
defects=defects.rename(columns={"Datetime": "datetime", "Type":"type"})
defects['datetime']=pd.to_datetime(defects['datetime'])

works = pd.read_json(StringIO(data_object["works"]))
works=works.rename(columns={"Datetime": "datetime_work", "Type": "type"})
works['datetime_work']=pd.to_datetime(works['datetime_work'])
works=works.drop(["type"], axis=1)

fails = pd.read_json(StringIO(data_object["failures"]))
fails = fails.rename(columns={"Datetime": "datetime_fail", "Type": "type_fail"})
fails['datetime_fail']=pd.to_datetime(fails['datetime_fail'])

telemetry = telemetry.sort_values(by='datetime')
works = works.sort_values(by='datetime_work')
fails = fails.sort_values(by='datetime_fail')

telemetry = pd.merge_asof(telemetry, works, left_on='datetime', right_on='datetime_work', direction='backward')
telemetry['last_work'] = (telemetry['datetime'] - telemetry['datetime_work']).dt.total_seconds() / 3600
telemetry = pd.merge_asof(telemetry, fails, left_on='datetime', right_on='datetime_fail', direction='forward')
telemetry['rul'] = (telemetry['datetime_fail'] - telemetry['datetime']).dt.total_seconds() / 3600
telemetry = telemetry.sort_values(by='datetime')

for error_type in range(1, 6):
    telemetry[f'error_{error_type}'] = 0

for idx, row in telemetry.iterrows():
    start_time = row['datetime_work']
    end_time = row['datetime']

    defects_in_range = defects[(defects['datetime'] > start_time) & (defects['datetime'] <= end_time)]

    error_counts = defects_in_range['type'].value_counts()
    for error_type, count in error_counts.items():
        telemetry.at[idx, f'error_{error_type}'] = count

telemetry['volt_last_hour'] = telemetry['volt'].shift(1)
telemetry['pressure_last_hour'] = telemetry['pressure'].shift(1)
telemetry['rotate_last_hour'] = telemetry['rotate'].shift(1)
telemetry['vibration_last_hour'] = telemetry['vibration'].shift(1)

telemetry['volt_change'] = telemetry['volt'] - telemetry['volt_last_hour']
telemetry['pressure_change'] = telemetry['pressure'] - telemetry['pressure_last_hour']
telemetry['rotate_change'] = telemetry['rotate'] - telemetry['rotate_last_hour']
telemetry['vibration_change'] = telemetry['vibration'] - telemetry['vibration_last_hour']

telemetry = telemetry.drop(["datetime_fail", "datetime_work"], axis=1)
telemetry = telemetry.drop(["volt", "pressure", "rotate", "vibration"], axis=1)
telemetry = telemetry.drop(["volt_last_hour", "pressure_last_hour", "rotate_last_hour", "vibration_last_hour"], axis=1)
telemetry = telemetry.dropna()

data = {
    "assetId": int(id),
    "defects": telemetry.to_csv(),
}
db.prepared_data.insert_one(data)