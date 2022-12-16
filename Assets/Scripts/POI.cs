using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI
{
    public int id;
    // (longitude, latitude) correspond to (X, Y) in data csv
    public double latitude;
    public double longitude;
    // conversion of latitude and longitude using transverse mercator
    public Point point;
    public string name;
    public string description;
    public Texture2D image;

    public POI(string[] row, LatLon MapCenter)
    {
        int.TryParse(row[0], out id);
        double.TryParse(row[1], out longitude);
        double.TryParse(row[2], out latitude);
        name = row[3];
        description = "Description: " + row[4];
        image = Resources.Load<Texture2D>("images/" + row[5].TrimEnd());
        TransverseMercator projection = new TransverseMercator(MapCenter.latitude, MapCenter.longitude);
        point = projection.fromGeographic(latitude, longitude);
    }
}
