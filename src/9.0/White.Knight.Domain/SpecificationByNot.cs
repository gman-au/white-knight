namespace White.Knight.Domain
{
    public class SpecificationByNot<T>(Specification<T> value) : Specification<T>
    {
        public Specification<T> Spec { get; } = value;

        public override bool IsSatisfiedBy(T entity) => !Spec.IsSatisfiedBy(entity);
    }
}