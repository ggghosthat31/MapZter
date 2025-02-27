using System.Diagnostics.CodeAnalysis;

namespace MapZter.Entity.Models;

public struct GeoPoint(double lat, double lon)
{
    public double Latitude { get; set; } = lat;

    public double Longitude { get; set; } = lon;

    private static double SliceVector(double value) => 
        Math.Round(value, 2, MidpointRounding.ToZero);

    public override bool Equals([NotNullWhen(true)] object obj)
    {
        if (obj is GeoPoint geoPoint)
        {
            var eqInputLat = SliceVector(geoPoint.Latitude);
            var eqInputLon = SliceVector(geoPoint.Longitude);

            var eqSelfLat = SliceVector(Latitude);
            var eqSelfLon = SliceVector(Longitude);

            return eqSelfLat == eqInputLat && eqSelfLon == eqInputLon;
        }

        return false;
    }

    public override string ToString()
    {
        return $"{Latitude} {Longitude}";
    }
}

public static class GeoPointExtenssions
{
    public static double SliceGeoVector(this double geoVector) => Math.Round(geoVector, 2, MidpointRounding.ToZero);

    public static GeoPoint SliceGeoPoint(this GeoPoint geoPoint, int fracFrame = 2)
    {
        var eqInputLat = Math.Round(geoPoint.Latitude, fracFrame, MidpointRounding.ToZero);
        var eqInputLon = Math.Round(geoPoint.Longitude, fracFrame, MidpointRounding.ToZero);

        geoPoint.Latitude = eqInputLat;
        geoPoint.Longitude = eqInputLon;

        return geoPoint;
    }
}