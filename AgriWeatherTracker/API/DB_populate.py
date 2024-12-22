import requests
import logging
from datetime import datetime, timedelta

# Set up logging
logging.basicConfig(level=logging.DEBUG, filename='weather_data_fetch.log', filemode='w',
                    format='%(name)s - %(levelname)s - %(message)s')

# API Configuration
api_key = '29400f483282572ff46891992cc1d3aa'
weather_api_url = 'https://api.openweathermap.org/data/3.0/onecall/timemachine'  # Adjusted to 3.0 if correct endpoint exists
my_api_url = 'http://localhost:5045/api/Weather'

# Define locations
locations = [
    {"name": "Antioquia, Colombia", "lat": 6.2518, "lon": -75.5636},
    {"name": "Sumatra, Indonesia", "lat": -0.5897, "lon": 101.3431}
]

# Function to fetch and post data
def fetch_and_post_weather(location):
    for day in range(5):  # last year's data
        dt = int((datetime.utcnow() - timedelta(days=day)).timestamp())
        params = {
            'lat': location['lat'],
            'lon': location['lon'],
            'dt': dt,
            'appid': api_key,
            'units': 'metric'  # Fetch metric units
        }
        try:
            response = requests.get(weather_api_url, params=params)
            response.raise_for_status()  # Will raise an exception for HTTP errors
            weather_data = response.json()

            if 'current' in weather_data:
                weather_post = {
                    'date': datetime.utcfromtimestamp(dt).isoformat(),
                    'temperature': weather_data['current']['temp'],
                    'humidity': weather_data['current']['humidity'],
                    'rainfall': weather_data['current'].get('rain', {}).get('1h', 0),
                    'location': location['name']
                }
                post_response = requests.post(my_api_url, json=weather_post)
                post_response.raise_for_status()
            else:
                logging.error(f"No weather data available for {location['name']} on {datetime.utcfromtimestamp(dt).isoformat()}")

        except requests.exceptions.HTTPError as http_err:
            logging.error(f"HTTP error occurred: {http_err} - {response.text}")
        except requests.exceptions.ConnectionError as conn_err:
            logging.error(f"Connection error occurred: {conn_err}")
        except Exception as err:
            logging.error(f"An error occurred: {err}")

# Iterate over locations and fetch/post data
for location in locations:
    fetch_and_post_weather(location)

logging.info("Weather data fetching and posting completed.")
