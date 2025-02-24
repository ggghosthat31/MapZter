namespace MapZter.Entity.Models;

public class Place
{
    public long PlaceId { get; set; }

    public string Licence { get; set; }

    public string OsmType { get; set; }

    public long OsmId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Class { get; set; }

    public string Type { get; set; }

    public int PlaceRank { get; set; }

    public double Importance { get; set; }

    public string AddressType { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public Address Address { get; set; }

    public IEnumerable<double> BoundingBox { get; set; }
}