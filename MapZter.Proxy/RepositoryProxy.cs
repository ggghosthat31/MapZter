namespace MapZter.Contracts.Interfaces.RepositoryProxy;

public class RepositoryProxy : IRepositoryProxy
{
    private readonly Dictionary<RepositoryPattern, RepositoryProxyEntity> _repositoryPatterns = [];

    public RepositoryProxy()
    {}

    public void SetRepositoryPattern(RepositoryPattern repositoryPattern, RepositoryProxyEntity proxyEntity)
    {
        if (_repositoryPatterns.ContainsKey(repositoryPattern))
            _repositoryPatterns.Remove(repositoryPattern);

        _repositoryPatterns.Add(repositoryPattern, proxyEntity);
    }

    public bool Execute(RepositoryPattern repositoryPattern, IInputEntity proxyInputEntity)
    {
        var proxyEntity = _repositoryPatterns[repositoryPattern];

        if (proxyEntity.RepositoryPredicate())
            return proxyEntity.ExecutionExpression(proxyInputEntity);

        return false;
    }

    public Task<bool> ExecuteAsync(RepositoryPattern repositoryPattern, IInputEntity proxyInputEntity)
    {
        var proxyEntity = _repositoryPatterns[repositoryPattern];

        if (proxyEntity.RepositoryPredicate())
            return Task.FromResult<bool>(proxyEntity.ExecutionExpression(proxyInputEntity));

        return Task.FromResult<bool>(false);
    }

    public void Dispose()
    {
        _repositoryPatterns.Clear();
    }
}