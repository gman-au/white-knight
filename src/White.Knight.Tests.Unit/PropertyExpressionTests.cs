using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using White.Knight.Abstractions.Extensions;
using White.Knight.Tests.Domain;
using Xunit;

namespace White.Knight.Tests.Unit
{
    public class PropertyExpressionTests
    {
        private readonly TestContext _context = new();

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test_Validation(Expression ex, string separator, bool lookForAlias, string result)
        {
            _context
                .ActGetPropertyExpression(
                    ex,
                    separator,
                    lookForAlias
                );

            _context
                .AssertExpectedPath(result);
        }

        public static IEnumerable<object[]> TestData()
        {
            Expression<Func<Customer, object>> customerExp;

            customerExp = o => o.CustomerId;
            yield return
            [
                customerExp,
                ".",
                false,
                "CustomerId"
            ];

            customerExp = o => o.CustomerId;
            yield return
            [
                customerExp,
                ".",
                true,
                "id"
            ];

            customerExp = o => o.CustomerType;
            yield return
            [
                customerExp,
                ".",
                true,
                "CustomerType"
            ];

            customerExp = o => o.OtherGuid;
            yield return
            [
                customerExp,
                "/",
                true,
                "my_other_guid"
            ];

            customerExp = o => o.OtherGuid;
            yield return
            [
                customerExp,
                "/",
                false,
                "OtherGuid"
            ];

            customerExp = o => o.CustomerCreated;
            yield return
            [
                customerExp,
                ".",
                false,
                "CustomerCreated"
            ];

            customerExp = o => o.FavouriteOrder.OrderKey;
            yield return
            [
                customerExp,
                ".",
                false,
                "FavouriteOrder.OrderKey"
            ];

            customerExp = o => o.FavouriteOrder.OrderId;
            yield return
            [
                customerExp,
                "|",
                true,
                "not_what_you_expected|id"
            ];

            customerExp = o => o.FavouriteOrder.OrderId;
            yield return
            [
                customerExp,
                "|",
                false,
                "FavouriteOrder|OrderId"
            ];

            customerExp = o => o.FavouriteOrder.Customer.CustomerAlias;
            yield return
            [
                customerExp,
                ".",
                false,
                "FavouriteOrder.Customer.CustomerAlias"
            ];
        }

        private class TestContext
        {
            private string _result = string.Empty;

            public void ActGetPropertyExpression(Expression ex, string separator, bool lookForAlias)
            {
                _result =
                    ex
                        .GetPropertyExpressionPath(
                            ref _result,
                            separator,
                            lookForAlias
                        );
            }

            public void AssertExpectedPath(string expected)
            {
                Assert.Equal(expected, _result);
            }
        }
    }
}