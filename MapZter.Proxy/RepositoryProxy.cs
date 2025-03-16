namespace MapZter.Contracts.Interfaces.RepositoryProxy;

public class RepositoryProxy : IRepositoryProxy
{
    private readonly Dictionary<RepositoryPattern, RepositoryProxyEntity> _repositoryPatterns = [];

    public RepositoryProxy()
    {}

    public void SetRepositoryPattern(RepositoryProxyEntity proxyEntity)
    {
        if (_repositoryPatterns.ContainsKey(proxyEntity.RepositoryPattern))
            _repositoryPatterns.Remove(proxyEntity.RepositoryPattern);

        _repositoryPatterns.Add(proxyEntity.RepositoryPattern, proxyEntity);
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