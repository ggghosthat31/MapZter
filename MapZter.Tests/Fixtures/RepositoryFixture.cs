using MapZter.Repository;
using MapZter.Entity.Models;
using MapZter.Contracts.Interfaces;
using MapZter.Contracts.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MapZter.Tests.Fixtures;

public class RepositoryFixture
{
    public RepositoryFixture()
    {
        RepositoryManager = DatabaseSeeder.GenerateDatabaseConnection();
        Console.WriteLine("Fixture Constructor Executed Once");    
    }

    public PlaceRepository PlaceRepository { get; private set; }

    public IRepositoryManager RepositoryManager { get; private set; }
}