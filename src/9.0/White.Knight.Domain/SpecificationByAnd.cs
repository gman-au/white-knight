namespace White.Knight.Domain
{
    public class SpecificationByAnd<T>(Specification<T> left, Specification<T> right) : Specification<T>
    {
        public Specification<T> Left { get; } = left;

        public Specification<T> Right { get; } = right;

        public override bool IsSatisfiedBy(T entity)
        {
            return Left.IsSatisfiedBy(entity) && Right.IsSatisfiedBy(entity);
        }
    }
}