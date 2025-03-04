using MapZter.Repository;
using MapZter.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MapZter.Tests.Fixtures;

public class RepositoryFixture
{
    
    public RepositoryFixture()
    {
        PlaceRepository = DatabaseSeeder.GenerateInMemoryDatabase();
        Console.WriteLine("Fixture Constructor Executed Once");
    
    }

    public PlaceRepository PlaceRepository { get; private set; }

}