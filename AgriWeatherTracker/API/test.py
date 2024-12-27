import requests

# Approximate coordinates for each region
locations = [
    # Wheat regions
    {"name": "Kansas, USA", "latitude": 38.500000, "longitude": -98.000000},
    {"name": "Saskatchewan, Canada", "latitude": 54.000000, "longitude": -106.000000},
    {"name": "Henan, China", "latitude": 34.000000, "longitude": 113.000000},
    {"name": "Punjab, India", "latitude": 31.000000, "longitude": 75.000000},
    {"name": "Western Australia, Australia", "latitude": -27.000000, "longitude": 121.000000},
    # Soybean regions
    {"name": "Mato Grosso, Brazil", "latitude": -13.000000, "longitude": -56.000000},
    {"name": "Iowa, USA", "latitude": 42.000000, "longitude": -93.000000},
    {"name": "Heilongjiang, China", "latitude": 48.000000, "longitude": 128.000000},
    {"name": "Buenos Aires, Argentina", "latitude": -34.600000, "longitude": -58.383333},
    {"name": "Madhya Pradesh, India", "latitude": 23.000000, "longitude": 78.000000},
    # Coffee regions
    {"name": "Minas Gerais, Brazil", "latitude": -18.000000, "longitude": -45.000000},
    {"name": "Santander, Colombia", "latitude": 7.000000, "longitude": -73.000000},
    {"name": "Sidamo, Ethiopia", "latitude": 6.000000, "longitude": 38.000000},
    {"name": "Lam Dong, Vietnam", "latitude": 11.500000, "longitude": 108.000000},
    {"name": "Central Highlands, Guatemala", "latitude": 15.500000, "longitude": -90.250000},
    # Cocoa regions
    {"name": "Côte d'Ivoire", "latitude": 7.540000, "longitude": -5.547080},
    {"name": "Ashanti, Ghana", "latitude": 6.666600, "longitude": -1.616270},
    {"name": "Sulawesi, Indonesia", "latitude": -2.000000, "longitude": 121.000000},
    {"name": "São Tomé", "latitude": 0.186360, "longitude": 6.613081},
    {"name": "Bahia, Brazil", "latitude": -12.000000, "longitude": -38.500000},
    # Corn regions
    {"name": "Iowa, USA", "latitude": 42.000000, "longitude": -93.000000},
    {"name": "Jilin, China", "latitude": 43.000000, "longitude": 126.000000},
    {"name": "Mato Grosso, Brazil", "latitude": -13.000000, "longitude": -56.000000},
    {"name": "Ukraine", "latitude": 49.000000, "longitude": 32.000000},
    {"name": "Bihar, India", "latitude": 25.000000, "longitude": 85.000000}
]

url = "http://localhost:5045/api/Location"

for location in locations:
    response = requests.post(url, json={
        "id": 0,  # ID is auto-generated by the database
        "name": location["name"],
        "latitude": location["latitude"],
        "longitude": location["longitude"]
    })
    if response.status_code == 201:
        print(f"Successfully added location: {location['name']}")
    else:
        print(f"Failed to add {location['name']} - Status code: {response.status_code}, Response: {response.text}")