using MapZter.Repository;
using MapZter.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MapZter.Tests;

public static class DatabaseSeeder
{
    private static PlaceRepository GeneratePlaceRepository()
    {
        var options = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: "RepositoryTestDatabase")
            .Options;

        var context = new RepositoryContext(options);
        return new PlaceRepository(context);
    }

    private static void SeedDatabse(ref PlaceRepository placeRepository)
    {
        var testPlace1 = new Place 
        {
            PlaceId = 16281533,
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
        placeRepository.CreatePlace(testPlace1);
        
        var testPlace2 = new Place 
        {
            PlaceId = 16281534,
	        Licence = "Data © OpenStreetMap contributors, ODbL 1.0. http://osm.org/copyright",
	        OsmType = "way",
	        OsmId = 280940521,
	        Latitude = -34.24,
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
		        Country = "Brazil",
		        CountryCode = "ar"
            },
            BoundingBox = new double[] {-34.4415900, -34.4370994, -58.7086067, -58.7044712},
            PlaceTag = null
        };
        placeRepository.CreatePlace(testPlace2);

        var testPlace3 = new Place 
        {
            PlaceId = 16281536,
	        Licence = "Data © OpenStreetMap contributors, ODbL 1.0. http://osm.org/copyright",
	        OsmType = "way",
	        OsmId = 280940530,
	        Latitude = -34.45,
	        Longitude = -58.77,
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
        placeRepository.CreatePlace(testPlace3);
    }

    public static PlaceRepository GenerateInMemoryDatabase()
    {
        //place repository
        var placeRepository = GeneratePlaceRepository();
        SeedDatabse(ref placeRepository);

        return placeRepository;
    }
}