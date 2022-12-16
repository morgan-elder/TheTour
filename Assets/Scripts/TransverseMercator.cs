using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TransverseMercator
{
    const int Radius = 6378137;
    public double latitude = 0;
    public double longitude = 0;
    public int k = 1;
    
    public TransverseMercator(double lat, double lon, int k = 1)
    {
        this.latitude = lat;
        this.longitude = lon;
        this.k = k;
    }

    public Point fromGeographic(double lat, double lon)
    {
        Point point = new Point();
        lat = toRadians(lat);
        lon = toRadians(lon - this.longitude);
        double B = Math.Sin(lon) * Math.Cos(lat);
        point.x = (float) (0.5 * this.k * Radius * Math.Log((1 + B) / (1 - B)));
        point.y = (float) (this.k * Radius * (Math.Atan(Math.Tan(lat) / Math.Cos(lon)) - toRadians(this.latitude)));
        return point;
    }

    public double toRadians(double degrees)
    {
        return (Math.PI / 180) * degrees;
    }
}
