namespace MapZter.Entities.RequestFeatures;

public class PlaceParameters : RequestParameters
{
    public PlaceParameters()
    {
        OrderBy = "place_id";
    }

    public string SearchTerm { get; set; }

    public string Road { get; set; }

    public string Hamlet { get; set; }

    public string Town { get; set; }

    public string City { get; set; }

    public string StateDistrict { get; set; }

    public string State { get; set; }

    public string Postcode { get; set; }

    public string Country { get; set; }

    public string CountryCode { get; set; }
}