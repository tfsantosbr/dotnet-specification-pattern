namespace Companies.Application.Base;

public interface ISpecificationWithResult<in TEntity>
{
    Result IsSatisfiedBy(TEntity entity);
}
