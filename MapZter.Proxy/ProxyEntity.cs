namespace MapZter.Contracts.Interfaces.Proxy;

public record ProxyEntity(
    RepositoryPattern RepositoryPattern,
    Func<bool> RepositoryPredicate,
    Func<IProxyInputEntity,bool> ExecutionExpression);
