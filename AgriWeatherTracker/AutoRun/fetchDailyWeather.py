import requests
from datetime import datetime
import statistics

# Fetch weather data for a specific date and location
def fetch_weather_data(api_key, lat, lon):
    url = f"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={api_key}&units=metric"
    attempts = 0
    max_attempts = 5  # Max attempts per location

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

# Post weather data to the API
def post_weather_data(weather_data, location_id):
    url = 'http://localhost:5045/api/Weather'
    if weather_data:
        # Extract relevant data from the weather API response
        temp_values = [weather_data['daily'][0]['temp']['morn'], weather_data['daily'][0]['temp']['day'],
                       weather_data['daily'][0]['temp']['eve'], weather_data['daily'][0]['temp']['night']]
        median_temperature = statistics.median(temp_values)

        data = {
            "date": datetime.now().strftime('%Y-%m-%d'),
            "temperature": median_temperature,
            "humidity": weather_data['daily'][0]['humidity'],
            "rainfall": weather_data['daily'][0].get('rain', 0),  # Use 0 if no rainfall data
            "windSpeed": weather_data['daily'][0]['wind_speed'],
            "locationId": location_id
        }
        try:
            response = requests.post(url, json=data)
            if response.status_code == 201:
                print(f"Weather data added successfully for location ID {location_id}.")
                return True
            else:
                print(f"Failed to add weather data. Status code: {response.status_code}, Message: {response.text}")
        except requests.RequestException as e:
            print(f"Request failed: {e}")
    return False

# Fetch all locations from the database
def get_locations():
    url = 'http://localhost:5045/api/Location'
    try:
        response = requests.get(url, timeout=10)
        if response.status_code == 200:
            return response.json()
        else:
            print(f"Failed to fetch locations. Status code: {response.status_code}, Message: {response.text}")
    except requests.RequestException as e:
        print(f"Request failed: {e}")
    return []

if __name__ == "__main__":
    API_KEY = '29400f483282572ff46891992cc1d3aa'

    # Fetch locations from the API
    locations = get_locations()

    if not locations:
        print("No locations retrieved. Exiting...")
    else:
        for location in locations:
            lat = location['latitude']
            lon = location['longitude']
            location_id = location['id']

            # Fetch weather data for the current day
            weather_data = fetch_weather_data(API_KEY, lat, lon)

            if weather_data:
                # Post the daily weather data
                post_weather_data(weather_data, location_id)
            else:
                print(f"Skipping location ID {location_id} due to failed weather data retrieval.")
