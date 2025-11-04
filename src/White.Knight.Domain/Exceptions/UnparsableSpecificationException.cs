using System;

namespace White.Knight.Domain.Exceptions
{
    public class UnparsableSpecificationException()
        : Exception("Could not parse specification");
}