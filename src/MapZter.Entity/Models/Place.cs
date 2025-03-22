using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MapZter.Entity.Models;

public class Place : IEntity
{
    [Key]
    [JsonProperty("place_id")]
    public long PlaceId { get; set; }

    [JsonProperty("license")]
    public string Licence { get; set; }

    [JsonProperty("osm_type")]
    public string OsmType { get; set; }

    [JsonProperty("osm_id")]
    public long OsmId { get; set; }

    [JsonProperty("lat")]
    public double Latitude { get; set; }

    [JsonProperty("lon")]
    public double Longitude { get; set; }

    [JsonProperty("class")]
    public string Class { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("place_rank")]
    public int PlaceRank { get; set; }

    [JsonProperty("importance")]
    public double Importance { get; set; }

    [JsonProperty("addresstype")]
    public string AddressType { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("display_name")]
    public string DisplayName { get; set; }

    [JsonProperty("address")]
    public Address Address { get; set; }

    [JsonProperty("extratags")]
    public PlaceTag PlaceTag { get; set; }

    [JsonProperty("boundingbox")]
    public double[] BoundingBox { get; set; }

    public GeoPoint GetGeoPoint()
    {
        if (Latitude is 0 || Longitude is 0)
            return new GeoPoint(0,0);

        return new GeoPoint(Latitude, Longitude);
    }

    public virtual bool Equals(Place? inputPlace)
    {                
        if (inputPlace == null) 
            return false;

        return PlaceId == inputPlace.PlaceId && 
            OsmType == inputPlace.OsmType &&
            OsmId == inputPlace.OsmId;
    }
}