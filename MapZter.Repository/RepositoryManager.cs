using MapZter.Contracts.Interfaces;

namespace MapZter.Repository;

public class RepostioryManager : IRepositoryManager
{
    private readonly RepositoryContext _repostioryContext;

    public RepostioryManager(RepositoryContext repositoryContext)
    {
        _repostioryContext = repositoryContext;
    }

    public async Task SaveAsync()
    {}
}