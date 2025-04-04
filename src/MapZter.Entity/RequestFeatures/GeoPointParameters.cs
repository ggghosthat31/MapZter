namespace MapZter.Entities.RequestFeatures;

public class GeoPointParameters : RequestParameters
{
    public GeoPointParameters()
    {
        OrderBy = "geo_point_id";
    }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string SearchTerm => $"{this.Latitude};{this.Longitude}";
}