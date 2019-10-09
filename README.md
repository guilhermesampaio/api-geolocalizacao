# Partners Api

This is an API for create and search partners. You can search them using a generated id or your coordinates. There are some exemples below.

## Create partners

POST: /partners

Body:
```
{
    "tradingName": "Boteco do Guisampa",
    "ownerName": "Guisampa",
    "document": "1432132123891/0001",
    "coverageArea": {
        "type": "MultiPolygon",
        "coordinates": [
            [
                [
                    [
                        -46,
                        -20
                    ],
                    [
                        -46,
                        -25
                    ],
                    [
                        -45,
                        -20
                    ],
                    [
                        -46,
                        -20
                    ]
                ]
            ]
        ]
    },
    "address": {
        "type": "Point",
        "coordinates": [
            -46.57421,
            -21.785741
        ]
    }
}

```

The response should be a 201 http status code with partner created.

## Get partners by id

GET: /partners/**{partnerId}**

The **{partnerId}** will be generated after you create a partner successfully. For example: http://localhost:8000/partners/5d9d76305dcf9a00010a3415

Expected result:

```
{
    "id": "5d9d76305dcf9a00010a3415",
    "tradingName": "Boteco do Guisampa",
    "ownerName": "Guisampa",
    "document": "1432132123891/0001",
    "coverageArea": {
        "coordinates": [
            [
                [
                    [
                        -46.0,
                        -20.0
                    ],
                    [
                        -46.0,
                        -25.0
                    ],
                    [
                        -45.0,
                        -20.0
                    ],
                    [
                        -46.0,
                        -20.0
                    ]
                ]
            ]
        ],
        "type": "MultiPolygon"
    },
    "address": {
        "coordinates": [
            -46.57421,
            -21.785741
        ],
        "type": "Point"
    }
}
```

## Get partner by coordinates

GET: /partners/?latitude=**{lat}**&longitude=**{lon}**

Exemple: http://localhost:8000/partners/?latitude=50&longitude=30

It will return a partner if you are inside of his coverage area.


---

## Instructions to run

This solution is available on docker, so to run it you should have docker installed on your machine. To facilitate the execution, there is available a **docker-compose.yaml** file that will start the api and database.

``` docker compose up ```



To see unit tests execution you can build yourself the Dockerfile and see the tests runing within test step. To build an image you have to be on solution directory and run command below:

``` docker build . -t geolocation-api```







## Test

Only for test purposes, you can test if your point is inside cover area using [geojson.io](http://geojson.io). Below an geojson example with multipolygons and you position.


```
{
  "type": "FeatureCollection",
  "features": [
    {
      "type": "Feature",
      "geometry": {
        "type": "Point",
        "coordinates": [31, 21]
       },
      "properties": {
        "name": "Minha posição"
      }
    },
    {
      "type": "Feature",
      "geometry": { 
         "type": "MultiPolygon", 
         "coordinates": [
           [[[30, 20], [45, 40], [10, 40], [30, 20]]], 
           [[[15, 5], [40, 10], [10, 20], [5, 10], [15, 5]]]
         ]
       },
      "properties": {
        "name": "Area de cobertura"
      }
    }
  ]
}
```
