import requests

def update_condition_threshold(id, data):
    url = f'http://localhost:5045/api/ConditionThreshold/{id}'
    response = requests.put(url, json=data)
    if response.status_code == 200:
        print(f"ConditionThreshold {id} updated successfully.")
    else:
        print(f"Failed to update ConditionThreshold {id}. Status code: {response.status_code}, Message: {response.text}")

def main():
    thresholds = [
        {
            "id": 1,
            "name": "Coffee Flowering Adverse Conditions",
            "minTemperature": 10,
            "maxTemperature": 30,
            "mildMinTemp": 25,
            "mildMaxTemp": 29,
            "mildResilienceDuration": 6,
            "moderateMinTemp": 30,
            "moderateMaxTemp": 34,
            "moderateResilienceDuration": 4,
            "severeMinTemp": 35,
            "severeMaxTemp": 38,
            "severeResilienceDuration": 2,
            "extremeMinTemp": 39,
            "extremeMaxTemp": 50,
            "extremeResilienceDuration": 1,
            "optimalHumidity": 70,
            "minHumidity": 50,
            "maxHumidity": 90,
            "minRainfall": 10,
            "maxRainfall": 50,
            "maxWindSpeed": 40
        },
        {
            "id": 2,
            "name": "Coffee Fruit Development Adverse Conditions",
            "minTemperature": 12,
            "maxTemperature": 30,
            "mildMinTemp": 25,
            "mildMaxTemp": 29,
            "mildResilienceDuration": 7,
            "moderateMinTemp": 30,
            "moderateMaxTemp": 34,
            "moderateResilienceDuration": 5,
            "severeMinTemp": 35,
            "severeMaxTemp": 38,
            "severeResilienceDuration": 3,
            "extremeMinTemp": 39,
            "extremeMaxTemp": 50,
            "extremeResilienceDuration": 1,
            "optimalHumidity": 60,
            "minHumidity": 40,
            "maxHumidity": 85,
            "minRainfall": 15,
            "maxRainfall": 45,
            "maxWindSpeed": 35
        },
        {
            "id": 3,
            "name": "Coffee Flowering Optimal Conditions",
            "minTemperature": 18,
            "maxTemperature": 24,
            "optimalHumidity": 75,
            "minHumidity": 60,
            "maxHumidity": 85,
            "minRainfall": 20,
            "maxRainfall": 30,
            "maxWindSpeed": 20
        },
        {
            "id": 4,
            "name": "Coffee Fruit Development Optimal Conditions",
            "minTemperature": 19,
            "maxTemperature": 25,
            "optimalHumidity": 70,
            "minHumidity": 50,
            "maxHumidity": 80,
            "minRainfall": 20,
            "maxRainfall": 35,
            "maxWindSpeed": 25
        }
    ]

    for threshold in thresholds:
        update_condition_threshold(threshold['id'], threshold)

if __name__ == "__main__":
    main()
