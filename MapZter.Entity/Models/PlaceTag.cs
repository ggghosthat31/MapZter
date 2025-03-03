using Newtonsoft.Json;

namespace MapZter.Entity.Models;

public class PlaceTag
{
    [JsonProperty("PlaceTagId")]
    public long PlaceTagId {get; set;}
    
    [JsonProperty("image")]
    public string Image { get; set; }
    
    [JsonProperty("tourism")]
    public string Tourism { get; set; }
    
    [JsonProperty("building")]
    public string Building { get; set; }
    
    [JsonProperty("heritage")]
    public string Heritage { get; set; }
    
    [JsonProperty("wikidata")]
    public string Wikidata { get; set; }
    
    [JsonProperty("architect")]
    public string Architect { get; set; }
    
    [JsonProperty("wikipedia")]
    public string Wikipedia { get; set; }
    
    [JsonProperty("wheelchair")]
    public string WheelChair { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("building:colour")]
    public string BuildingColour { get; set; }

    [JsonProperty("heritage:website")]
    public string HeritageWebsite { get; set; }

    [JsonProperty("heritage:operator")]
    public string HeritageOperator { get; set; }

    [JsonProperty("architect:wikidata")]
    public string ArchitectWikidata { get; set; }

    [JsonProperty("year_of_construction")]
    public string ConstructionYear { get; set; }
}