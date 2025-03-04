using MapZter.Repository;
using MapZter.Entity.Models;
using MapZter.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MapZter.Tests;

public class RepositoryTest : IClassFixture<RepositoryFixture>
{
    private readonly PlaceRepository placeRepository;

    public RepositoryTest(RepositoryFixture repositoryFixture)
    {
        // placeRepository = DatabaseSeeder.GenerateInMemoryDatabase();
        placeRepository = repositoryFixture.PlaceRepository;
    }

    [Fact]
    public void AddPlace_Test()
    {
        var testPlace = new Place
        {
            PlaceId = 16234589,
	        Licence = "Data © OpenStreetMap contributors, ODbL 1.0. http://osm.org/copyright",
	        OsmType = "way",
	        OsmId = 4252452435,
	        Latitude = -34.44,
	        Longitude = -58.70,
	        Class = "highway",
	        Type = "motorway",
	        PlaceRank = 26,
	        Importance = 0.05338152361333635,
	        AddressType = "road",
	        Name = "Autopista Pedro Eugenio Aramburu",
	        DisplayName = "Autopista Pedro Eugenio Aramburu, El Triángulo, Partido de Malvinas Argentinas, Buenos Aires, B1619AGS, Argentina",
            Address = new Address 
            {
                Road = "Autopista Pedro Eugenio Aramburu",
		        Hamlet = "El Triángulo",
		        StateDistrict = "Partido de Malvinas Argentinas",
		        State = "Buenos Aires",
		        ISO3166_2_lvl4 = "AR-B",
		        Postcode = "B1619AGS",
		        Country = "Argentina",
		        CountryCode = "ar"
            },
            BoundingBox = new double[] {-34.4415900, -34.4370994, -58.7086067, -58.7044712},
            PlaceTag = null
        };
    
        System.Console.WriteLine(testPlace.PlaceId);
        placeRepository.CreatePlace(testPlace).Wait();
        var retrievedPlace = placeRepository.GetPlaceAsync(testPlace.PlaceId, false).Result;

        Assert.NotNull(retrievedPlace);
        System.Console.WriteLine(retrievedPlace.Equals(testPlace));
        Assert.Equal(retrievedPlace, testPlace);
    }

    [Fact]
    public void DeletePlace_Test()
    {
        var testPlace = new Place 
        {
            PlaceId = 16281536,
	        Licence = "Data © OpenStreetMap contributors, ODbL 1.0. http://osm.org/copyright",
	        OsmType = "way",
	        OsmId = 280940520,
	        Latitude = -34.44,
	        Longitude = -58.70,
	        Class = "highway",
	        Type = "motorway",
	        PlaceRank = 26,
	        Importance = 0.05338152361333635,
	        AddressType = "road",
	        Name = "Autopista Pedro Eugenio Aramburu",
	        DisplayName = "Autopista Pedro Eugenio Aramburu, El Triángulo, Partido de Malvinas Argentinas, Buenos Aires, B1619AGS, Argentina",
            Address = new Address 
            {
                Road = "Autopista Pedro Eugenio Aramburu",
		        Hamlet = "El Triángulo",
		        StateDistrict = "Partido de Malvinas Argentinas",
		        State = "Buenos Aires",
		        ISO3166_2_lvl4 = "AR-B",
		        Postcode = "B1619AGS",
		        Country = "Argentina",
		        CountryCode = "ar"
            },
            BoundingBox = new double[] {-34.4415900, -34.4370994, -58.7086067, -58.7044712},
            PlaceTag = null
        };
        
        placeRepository.DeletePlace(testPlace).Wait();

        var retrievedPlace = placeRepository.GetPlaceAsync(testPlace.PlaceId, false).Result;
        System.Console.WriteLine(retrievedPlace);
        Assert.Null(retrievedPlace);
    }
}