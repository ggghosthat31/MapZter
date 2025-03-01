using MapZter.Entities.RequestFeatures;
using MapZter.Entity.Models;
using MapZter.Repository.Utilities;

using System.Linq.Dynamic.Core;

namespace MapZter.Repository.Extensions;

public static class PlaceRepositoryExtensions
{
    private static bool Match(Address address, PlaceParameters placeParameters) =>
        address.Country == placeParameters.Country &&
        address.CountryCode == placeParameters.CountryCode &&
        address.Hamlet == placeParameters.Hamlet &&
        address.Town == placeParameters.Town &&
        address.StateDistrict == placeParameters.StateDistrict &&
        address.State == placeParameters.State &&
        address.Postcode == placeParameters.Postcode;

    public static IQueryable<Place> FilterPlaces(this IQueryable<Place> places, PlaceParameters placeParameters)
    {
        return places.Where(p => Match(p.Address, placeParameters));
    }

    public static IQueryable<Place> Search(this IQueryable<Place> places, string searchTerm)
    {
        if(string.IsNullOrEmpty(searchTerm))
            return places;

        var lowerCase = searchTerm.Trim().ToLower();
        // need 'Levenshtein distance' based implementation 
        return places.Where(p => p.DisplayName.ToLower().Contains(lowerCase));
    }

    public static IQueryable<Place> Sort(this IQueryable<Place> places, string orderByQueryString)
    {
        if(string.IsNullOrWhiteSpace(orderByQueryString))
            return places.OrderBy(c => c.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Place>(orderByQueryString);

        if(string.IsNullOrWhiteSpace(orderQuery))
            return places.OrderBy(c => c.Name);

        return places.OrderBy(orderQuery);
    }
}