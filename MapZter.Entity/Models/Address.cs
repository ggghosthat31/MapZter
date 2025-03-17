namespace MapZter.Entity.Models;

public struct Address : IEntity
{
    public string Road { get; set; }

    public string Hamlet { get; set; }

    public string Town { get; set; }

    public string City { get; set; }

    public string ISO3166_2_lvl8 { get; set; }

    public string StateDistrict { get; set; }

    public string State { get; set; }

    public string ISO3166_2_lvl4 { get; set; }

    public string Postcode { get; set; }

    public string Country { get; set; }

    public string CountryCode { get; set; }
}