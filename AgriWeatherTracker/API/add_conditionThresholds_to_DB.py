import requests

def post_condition_threshold(data):
    url = 'http://localhost:5045/api/ConditionThreshold'
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Successfully added: {data['Name']}")
    else:
        print(f"Failed to add {data['Name']}. Status code: {response.status_code}, Message: {response.text}")

def main():
    thresholds = [
        {
            "Id": 3,
            "Name": "Coffee Flowering Optimal Conditions",
            "MinTemperature": 18,
            "MaxTemperature": 24,
            "OptimalHumidity": 75,
            "MinHumidity": 60,
            "MaxHumidity": 85,
            "MinRainfall": 20,
            "MaxRainfall": 30,
            "MinWindSpeed": 0,
            "MaxWindSpeed": 20,
            "ResilienceDuration": 5
        },
        {
            "Id": 4,
            "Name": "Coffee Fruit Development Optimal Conditions",
            "MinTemperature": 19,
            "MaxTemperature": 25,
            "OptimalHumidity": 70,
            "MinHumidity": 50,
            "MaxHumidity": 80,
            "MinRainfall": 20,
            "MaxRainfall": 35,
            "MinWindSpeed": 0,
            "MaxWindSpeed": 25,
            "ResilienceDuration": 5
        }
    ]

    for threshold in thresholds:
        post_condition_threshold(threshold)

if __name__ == "__main__":
    main()
