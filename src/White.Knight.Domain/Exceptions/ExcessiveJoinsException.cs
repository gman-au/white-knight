using System;

namespace White.Knight.Domain.Exceptions
{
    public class ExcessiveJoinsException()
        : Exception("Root query contained too many joins");
}