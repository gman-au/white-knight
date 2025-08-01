namespace White.Knight.Domain
{
    public abstract class Specification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);

        public static Specification<T> operator &(Specification<T> a, Specification<T> b)
        {
            return new SpecificationByAnd<T>(a, b);
        }

        public static Specification<T> operator |(Specification<T> a, Specification<T> b)
        {
            return new SpecificationByOr<T>(a, b);
        }
    }
}