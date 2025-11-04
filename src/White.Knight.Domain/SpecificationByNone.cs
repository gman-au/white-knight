namespace White.Knight.Domain
{
    public class SpecificationByNone<T> : Specification<T>
    {
        public override bool IsSatisfiedBy(T entity)
        {
            return false;
        }
    }
}