namespace MapZter.Contracts.Interfaces.RepositoryProxy;

public record RepositoryProxyEntity(
    RepositoryPattern RepositoryPattern,
    Func<bool> RepositoryPredicate,
    Func<IInputEntity,bool> ExecutionExpression);