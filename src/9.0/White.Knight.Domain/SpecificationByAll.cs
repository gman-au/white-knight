namespace White.Knight.Domain
{
    public class SpecificationByAll<T> : Specification<T>
    {
        public override bool IsSatisfiedBy(T entity)
        {
            return true;
        }
    }
}