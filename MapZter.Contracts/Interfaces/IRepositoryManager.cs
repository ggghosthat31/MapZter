using System.Threading.Tasks;

namespace MapZter.Contracts.Interfaces;

public interface IRepositoryManager 
{
    Task SaveAsync();
}