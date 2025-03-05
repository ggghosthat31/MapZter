namespace MapZter.Entity.Models;

public interface IPointMatchable<T>
{
    public bool Match(T obj);
}
