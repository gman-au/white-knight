namespace White.Knight.Domain
{
    public class SpecificationByOr<T>(Specification<T> left, Specification<T> right) : Specification<T>
    {
        public Specification<T> Left { get; } = left;

        public Specification<T> Right { get; } = right;

        public override bool IsSatisfiedBy(T entity) => Left.IsSatisfiedBy(entity) || Right.IsSatisfiedBy(entity);
    }
}