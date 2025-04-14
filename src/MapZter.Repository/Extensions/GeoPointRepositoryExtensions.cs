using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;

using System.Linq.Dynamic.Core;

namespace MapZter.Repository.Extensions;

public static class GeoPointExtensions
{
    private static bool Match(GeoPoint geoPoint, GeoPointParameters geoPointParameters) =>
        geoPoint.Latitude == geoPointParameters.Latitude &&
        geoPoint.Longitude == geoPointParameters.Longitude;

    public static IQueryable<GeoPoint> FilterPlaces(this IQueryable<GeoPoint> places, GeoPointParameters geoPointParameters)
    {
        return places.Where(p => Match(p, geoPointParameters));
    }

    public static IQueryable<GeoPoint> Search(this IQueryable<GeoPoint> geoPoints, string searchTerm)
    {
        if(string.IsNullOrEmpty(searchTerm) || !searchTerm.Contains(";"))
            return geoPoints;

        var lowerCase = searchTerm.Trim().ToLower();


        double longitude = Double.Parse(searchTerm.Split(";")[0]);
        double latitude = Double.Parse(searchTerm.Split(";")[0]);

        // need 'Levenshtein distance' based implementation 
        return geoPoints.Where(p => p.Longitude == longitude || p.Latitude == latitude);
    }
}