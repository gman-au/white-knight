﻿using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerNameContains(string value)
        : SpecificationByTextContains<Customer>(value, o => o.CustomerName);
}