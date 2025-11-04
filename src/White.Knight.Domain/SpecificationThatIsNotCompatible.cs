namespace White.Knight.Domain
{
    /// <summary>
    /// This is a dummy type that is only used to test translator implementations for expected exceptions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpecificationThatIsNotCompatible<T> : Specification<T>
    {
        public override bool IsSatisfiedBy(T entity)
        {
            return false;
        }
    }
}