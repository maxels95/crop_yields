import requests
from datetime import datetime, timezone

def post_growth_stage(data):
    url = 'http://localhost:5045/api/GrowthStage'
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Successfully added Growth Stage: {data['StageName']}")
    else:
        print(f"Failed to add Growth Stage {data['StageName']}. Status code: {response.status_code}, Message: {response.text}")

def utc_datetime(year, month, day):
    # Create a datetime object in local time
    local_dt = datetime(year, month, day)
    # Convert local datetime to UTC
    return local_dt.astimezone(timezone.utc)

def main():
    growth_stages = [
        {
            "Id": 1,
            "StageName": "Coffee Flowering",
            "StartDate": utc_datetime(datetime.now().year, 3, 1).isoformat(),
            "EndDate": utc_datetime(datetime.now().year, 4, 30).isoformat(),   # Assuming April 30th as the end
            "OptimalConditions": 3,
            "AdverseConditions": 1,
            "ResilienceDurationInDays": 4,
            "HistoricalAdverseImpactScore": 2.5
        },
        {
            "Id": 2,
            "StageName": "Coffee Fruit Development",
            "StartDate": utc_datetime(datetime.now().year, 5, 1).isoformat(),
            "EndDate": utc_datetime(datetime.now().year, 9, 30).isoformat(),   # Assuming September 30th as the end
            "OptimalConditions": 4,
            "AdverseConditions": 2,
            "ResilienceDurationInDays": 3,
            "HistoricalAdverseImpactScore": 3.0
        }
    ]

    for stage in growth_stages:
        post_growth_stage(stage)

if __name__ == "__main__":
    main()