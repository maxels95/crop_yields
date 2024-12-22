import requests

def create_crop():
    url = 'http://localhost:5045/api/Crop'
    headers = {'Content-Type': 'application/json'}
    payload = {
        "name": "Coffee",
        "growthCycles": [2],
        "locations": [60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76]
    }

    response = requests.post(url, json=payload, headers=headers)
    if response.status_code == 201:
        print("Successfully added the crop.")
    else:
        print(f"Failed to add the crop. Status code: {response.status_code}, Response: {response.text}")

if __name__ == "__main__":
    create_crop()
