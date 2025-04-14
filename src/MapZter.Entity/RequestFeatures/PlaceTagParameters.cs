namespace MapZter.Entities.RequestFeatures;

public class PlaceTagParameters : RequestParameters
{
    public PlaceTagParameters()
    {
        OrderBy = "place_tag_id";
    }

    public string Tourism { get; set; }

    public string Building { get; set; }

    public string Heritage { get; set; }

    public string Wikidata { get; set; }
    
    public string Architect { get; set; }
    
    
    public string SearchTerm { get; set; }
}