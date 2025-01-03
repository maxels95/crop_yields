import requests
import random
from datetime import datetime

def add_health_score(data):
    url = 'http://localhost:5045/api/HealthScore'
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Successfully added health score for location ID: {data['locationId']}")
    else:
        print(f"Failed to add health score for location ID: {data['locationId']}. Status code: {response.status_code}, Response: {response.text}")

def main():
    location_ids = list(range(1, 18))  # Location IDs from 1 to 17
    date = datetime.now().isoformat()  # Current date-time in ISO 8601 format

    for location_id in location_ids:
        health_score = {
            "id": 0,  # Placeholder, assuming the API assigns the ID
            "cropId": 1,  # Placeholder crop ID
            "locationId": location_id,
            "date": date,
            "score": round(random.uniform(0, 100), 2)  # Random score between 0 and 100
        }
        add_health_score(health_score)

if __name__ == "__main__":
    main()
