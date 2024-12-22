import requests
from datetime import datetime, timedelta
import statistics
import time

def fetch_weather_data(api_key, lat, lon, date):
    formatted_date = date.strftime('%Y-%m-%d')  # Format date as YYYY-MM-DD
    url = f"https://api.openweathermap.org/data/3.0/onecall/day_summary?lat={lat}&lon={lon}&date={formatted_date}&appid={api_key}&units=metric"
    attempts = 0
    max_attempts = 5  # Max attempts per day

    while attempts < max_attempts:
        try:
            response = requests.get(url, timeout=10)  # Timeout for the request
            if response.status_code == 200:
                return response.json()
            else:
                print(f"Failed to fetch weather data. Status code: {response.status_code}, Message: {response.text}")
                attempts += 1
                time.sleep(5)  # Wait for 5 seconds before retrying
        except requests.ConnectionError as e:
            print(f"Connection error occurred: {e}")
            attempts += 1
            time.sleep(5)
        except requests.Timeout as e:
            print(f"Request timed out: {e}")
            attempts += 1
            time.sleep(5)
    return None

def post_weather_data(weather_data, location_id):
    url = 'http://localhost:5045/api/Weather'
    if weather_data:
        temp_values = [weather_data['temperature']['morning'], weather_data['temperature']['afternoon'],
                       weather_data['temperature']['evening'], weather_data['temperature']['night']]
        median_temperature = statistics.median(temp_values)

        data = {
            "date": weather_data['date'],
            "temperature": median_temperature,
            "humidity": weather_data['humidity']['afternoon'],
            "rainfall": weather_data['precipitation']['total'],
            "windSpeed": weather_data['wind']['max']['speed'],
            "locationId": location_id
        }
        try:
            response = requests.post(url, json=data)
            if response.status_code == 201:
                print("Weather data added successfully.")
                return True
            else:
                print(f"Failed to add weather data. Status code: {response.status_code}, Message: {response.text}")
        except requests.RequestException as e:
            print(f"Request failed: {e}")
    return False

if __name__ == "__main__":
    API_KEY = '29400f483282572ff46891992cc1d3aa'
    LATITUDE = 1.4686  # Latitude for Minas Gerais, Brazil
    LONGITUDE = 102.1411  # Longitude for Minas Gerais, Brazil
    LOCATION_ID = 72  # Adjust this to match the location ID in your database
    START_DATE = datetime.now() - timedelta(days=950)
    MAX_CALLS = 990
    call_count = 0

    for day_offset in range(950):
        if call_count >= MAX_CALLS:
            print("Reached maximum API call limit.")
            break
        date_to_fetch = START_DATE + timedelta(days=day_offset)
        successful_post = False
        while not successful_post:
            weather_data = fetch_weather_data(API_KEY, LATITUDE, LONGITUDE, date_to_fetch)
            if weather_data:
                successful_post = post_weather_data(weather_data, LOCATION_ID)
            if not successful_post:
                print("Retrying due to unsuccessful attempt...")
            call_count += 1  # Increment the call count after each attempt, successful or not


# Locations added:
# 60
# 72
# 63
# 61
# 62
# 64
# 65
# 66
# 67
# 73
