using MapZter.Contracts.Interfaces;
using MapZter.Contracts.Interfaces.Repository;
using MapZter.Repository;
using MapZter.Entity.Models;
using MapZter.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MapZter.Tests;

public class RepositoryTest : IClassFixture<RepositoryFixture>
{
    private IRepositoryManager _repositoryManager;
    private PlaceRepository placeRepository => _repositoryManager.PlaceRepository as PlaceRepository;

    public RepositoryTest(RepositoryFixture repositoryFixture)
    {
        _repositoryManager = DatabaseSeeder.GenerateDatabaseConnection();
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
    
        placeRepository.Create(testPlace).Wait();
        var retrievedPlace = placeRepository.GetPlaceAsync(testPlace.PlaceId, false).Result;
        var retrievedPlacePoint = retrievedPlace.GetGeoPoint();
        var testPlacePoint = testPlace.GetGeoPoint();
        var similarPlace = retrievedPlace.Equals(testPlace);
        var similarPoint = retrievedPlacePoint.Match(testPlacePoint);

        Assert.NotNull(retrievedPlace);
        Assert.True(similarPlace && similarPoint);
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
        
        placeRepository.Delete(testPlace).Wait();

        var retrievedPlace = placeRepository.GetPlaceAsync(testPlace.PlaceId, false).Result;
        System.Console.WriteLine(retrievedPlace);
        Assert.Null(retrievedPlace);
    }

    [Fact]
    public void UpdatePlace_Test()
    {
        var testPlace = new Place
        {
            PlaceId = 16281533,
	        Licence = "http://osm.org/copyright",
	        OsmType = "way",
	        OsmId = 280940520,
	        Latitude = -24.44,
	        Longitude = -28.70,
	        Class = "highway",
	        Type = "motorway",
	        PlaceRank = 26,
	        Importance = 0.05338152361333635,
	        AddressType = "road",
	        Name = "Diego Eugenio Aramburu",
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
            BoundingBox = new double[] {-34.1215900, -34.4370994, -58.7086067, -58.7044712},
            PlaceTag = null
        };
        
        var retrievedPlace = placeRepository.GetPlaceAsync(testPlace.PlaceId, false).Result;
        placeRepository.Update(retrievedPlace, testPlace).Wait();

        var retrievedPlace2 = placeRepository.FindByCondition( placeRepository => placeRepository.PlaceId.Equals(testPlace.PlaceId), true);

        var equality = retrievedPlace.Equals(retrievedPlace2);
        Assert.False(equality);
    }
}