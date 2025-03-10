namespace MapZter.Contracts.Interfaces.Proxy;

public class Proxy : IProxy
{
    private readonly Dictionary<RepositoryPattern, ProxyEntity> _repositoryPatterns = [];

    public Proxy()
    {}

    public void SetRepositoryPattern(RepositoryPattern repositoryPattern, ProxyEntity proxyEntity)
    {
        if (_repositoryPatterns.ContainsKey(repositoryPattern))
            _repositoryPatterns.Remove(repositoryPattern);

        _repositoryPatterns.Add(repositoryPattern, proxyEntity);
    }

    public bool Execute(RepositoryPattern repositoryPattern, IProxyInputEntity proxyInputEntity)
    {
        var repositoryEntity = _repositoryPatterns[repositoryPattern];

        if (repositoryEntity.RepositoryPredicate())
            return repositoryEntity.ExecutionExpression(proxyInputEntity);

        return false;
    }

    public void Dispose()
    {
        _repositoryPatterns.Clear();
    }
}