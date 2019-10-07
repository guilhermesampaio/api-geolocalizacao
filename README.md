# Geolocalization Api



## Test

Only for test purposes, you can test if your point is inside cover area using [geojson.io]([http://geojson.io) and configuring your data and pasting code below.

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


## Query on mongo db

```
db.Partners.find({
    CoverageArea :{
        $geoIntersects: {
            $geometry: { 
                type: "Point", 
                coordinates: [40,10]
            }
        }
    }
});
```
