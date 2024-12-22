import requests

def post_weather_data():
    url = 'http://localhost:5045/api/Weather'
    data = {
        "id": 0,
        "date": "2024-05-08T17:40:13.518Z",
        "temperature": 25.0,  # Example temperature in Celsius
        "humidity": 80,       # Example humidity percentage
        "rainfall": 5,        # Example rainfall in mm
        "windSpeed": 15,      # Example wind speed in km/h
        "locationId": 62      # Using location ID 62 as specified
    }
    
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print("Weather data added successfully.")
    else:
        print(f"Failed to add weather data. Status code: {response.status_code}, Message: {response.text}")

if __name__ == "__main__":
    post_weather_data()
