namespace Companies.Application;

public interface ISpecification <in TEntity>
{
    bool IsSatisfiedBy(TEntity entity);
}
