import requests

def add_location(data):
    url = 'http://localhost:5045/api/HealthScore'
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Successfully added location: {data['name']}")
    else:
        print(f"Failed to add location {data['name']}. Status code: {response.status_code}, Response: {response.text}")

def main():
    locations = [
        {"id": 0, "country": "Brazil", "name": "Minas Gerais, Brazil", "latitude": -18.5122, "longitude": -44.5550},
        {"id": 0, "country": "Vietnam", "name": "Lam Dong, Vietnam", "latitude": 11.5754, "longitude": 108.1429},
        {"id": 0, "country": "Ethiopia", "name": "Sidamo, Ethiopia", "latitude": 6.4128, "longitude": 38.7046},
        {"id": 0, "country": "Colombia", "name": "Cauca, Colombia", "latitude": 2.4549, "longitude": -76.6092},
        {"id": 0, "country": "Guatemala", "name": "Huehuetenango, Guatemala", "latitude": 15.3197, "longitude": -91.4704},
        {"id": 0, "country": "Brazil", "name": "São Paulo, Brazil", "latitude": -23.5505, "longitude": -46.6333},
        {"id": 0, "country": "Brazil", "name": "Paraná, Brazil", "latitude": -25.2521, "longitude": -52.0215},
        {"id": 0, "country": "Vietnam", "name": "Dak Lak, Vietnam", "latitude": 12.6904, "longitude": 108.0378},
        {"id": 0, "country": "Vietnam", "name": "Gia Lai, Vietnam", "latitude": 13.8079, "longitude": 108.1094},
        {"id": 0, "country": "Colombia", "name": "Antioquia, Colombia", "latitude": 6.2518, "longitude": -75.5636},
        {"id": 0, "country": "Colombia", "name": "Huila, Colombia", "latitude": 2.5359, "longitude": -75.5277},
        {"id": 0, "country": "Colombia", "name": "Tolima, Colombia", "latitude": 4.0925, "longitude": -75.1545},
        {"id": 0, "country": "Indonesia", "name": "Sumatra, Indonesia", "latitude": 1.4686, "longitude": 102.1411},
        {"id": 0, "country": "Indonesia", "name": "Java, Indonesia", "latitude": -7.6145, "longitude": 110.7122},
        {"id": 0, "country": "Indonesia", "name": "Sulawesi, Indonesia", "latitude": -2.3969, "longitude": 120.8247},
        {"id": 0, "country": "Ethiopia", "name": "Yirgacheffe, Ethiopia", "latitude": 6.1623, "longitude": 38.2036},
        {"id": 0, "country": "Ethiopia", "name": "Harrar, Ethiopia", "latitude": 9.3190, "longitude": 42.0798}
    ]

    for location in locations:
        # Adding a HealthScore to each location
        location["HealthScore"] = {
            "id": location["id"],
            "country": location["country"],
            "name": location["name"],
            "latitude": location["latitude"],
            "longitude": location["longitude"]
        }
        add_location(location)

if __name__ == "__main__":
    main()
