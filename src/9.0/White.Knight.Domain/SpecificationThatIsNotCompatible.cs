namespace White.Knight.Domain
{
    // This is a dummy type that is only used to test translator implementations for expected exceptions.
    public class SpecificationThatIsNotCompatible<T> : Specification<T>
    {
        public override bool IsSatisfiedBy(T entity)
        {
            return false;
        }
    }
}