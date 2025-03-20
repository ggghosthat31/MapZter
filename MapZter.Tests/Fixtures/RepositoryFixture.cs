using MapZter.Repository;
using MapZter.Entity.Models;
using MapZter.Contracts.Interfaces;
using MapZter.Contracts.Interfaces.Logger;
using MapZter.Contracts.Interfaces.Repository;
using MapZter.Contracts.Interfaces.RepositoryProxy;
using MapZter.Logger;
using MapZter.Proxy;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MapZter.Tests.Fixtures;

public class RepositoryFixture
{
    public RepositoryFixture()
    {
        LoggerManager = new LoggerManager();
        RepositoryManager = DatabaseSeeder.GenerateDatabaseConnection();
        RepositoryProxy = new RepositoryProxy(RepositoryManager, LoggerManager);
        Console.WriteLine("Fixture Constructor Executed Once");    
    }

    public ILoggerManager LoggerManager { get; set; }

    public IRepositoryManager RepositoryManager { get; private set; }

    public IRepositoryProxy RepositoryProxy { get; private set; }
}