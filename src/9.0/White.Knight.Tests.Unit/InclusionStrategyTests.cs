using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using AutoFixture;
using AutoFixture.AutoMoq;
using White.Knight.Abstractions.Extensions;
using White.Knight.Tests.Domain;
using Xunit;

namespace White.Knight.Tests.Unit
{
    public class InclusionStrategyTests
    {
        private readonly TestContext _context = new();

        [Fact]
        public void Test_Default_Behaviour()
        {
            _context.ArrangeDistinctSourceAndTarget();
            _context.ArrangeNoInclusions();
            _context.ArrangeNoExclusions();
            _context.ActMapStrategy();
            _context.AssertAllIdentical();
        }

        [Fact]
        public void Test_Full_Exclusions_Identical_Objects()
        {
            _context.ArrangeIdenticalSourceAndTarget();
            _context.ArrangeNoInclusions();
            _context.ArrangeAllExclusions();
            _context.ActMapStrategy();
            _context.AssertAllIdentical();
        }

        [Fact]
        public void Test_Customer_Name_And_Number_Only()
        {
            _context.ArrangeDistinctSourceAndTarget();
            _context.ArrangeNameAndNumberOnlyInclusion();
            _context.ArrangeNoExclusions();
            _context.ActMapStrategy();
            _context.AssertNamesAndNumbersOnlyIdentical();
        }

        [Fact]
        public void Test_Exclude_Name_Include_Number()
        {
            _context.ArrangeDistinctSourceAndTarget();
            _context.ArrangeNameInclusion();
            _context.ArrangeNumberExclusion();
            _context.ActMapStrategy();
            _context.AssertNamesIdentical();
            _context.AssertNumbersNotIdentical();
        }

        private class TestContext
        {
            private Customer _sourceEntity;
            private Customer _targetEntity;
            private readonly IFixture _fixture;
            private Expression<Func<Customer, object>>[] _fieldsToInclude;
            private Expression<Func<Customer, object>>[] _fieldsToExclude;

            public TestContext()
            {
                _fixture =
                    new Fixture()
                        .Customize(new AutoMoqCustomization());

                _fixture
                    .Behaviors.OfType<ThrowingRecursionBehavior>()
                    .ToList()
                    .ForEach(b => _fixture.Behaviors.Remove(b));

                _fixture
                    .Behaviors
                    .Add(new OmitOnRecursionBehavior());
            }

            public void ArrangeIdenticalSourceAndTarget()
            {
                _sourceEntity =
                    _fixture
                        .Build<Customer>()
                        .With(o => o.CustomerName, "Simon")
                        .Create();

                _targetEntity =
                    DeepClone(_sourceEntity);
            }

            public void ArrangeDistinctSourceAndTarget()
            {
                _sourceEntity =
                    _fixture
                        .Build<Customer>()
                        .With(o => o.CustomerName, "Simon")
                        .Create();

                _targetEntity =
                    _fixture
                        .Build<Customer>()
                        .With(o => o.CustomerName, "Timothy")
                        .Create();
            }

            public void ArrangeNoInclusions() => _fieldsToInclude = null;
            public void ArrangeNoExclusions() => _fieldsToExclude = null;

            public void ArrangeAllExclusions()
            {
                _fieldsToExclude =
                [
                    o => o.CustomerId,
                    o => o.CustomerAlias,
                    o => o.CustomerName,
                    o => o.CustomerType,
                    o => o.CustomerNumber,
                    o => o.CustomerCreated,
                    o => o.CustomerPrivateDetail,
                    o => o.Addresses,
                    o => o.Orders
                ];
            }

            public void ArrangeNameAndNumberOnlyInclusion()
            {
                _fieldsToInclude =
                [
                    o => o.CustomerName,
                    o => o.CustomerNumber
                ];
            }

            public void ArrangeNameInclusion()
            {
                _fieldsToInclude =
                [
                    o => o.CustomerName
                ];
            }

            public void ArrangeNumberExclusion()
            {
                _fieldsToExclude =
                [
                    o => o.CustomerNumber
                ];
            }

            public void ActMapStrategy()
            {
                _targetEntity =
                    _sourceEntity
                        .ApplyInclusionStrategy(
                            _targetEntity,
                            _fieldsToInclude,
                            _fieldsToExclude
                        );
            }

            public void AssertAllIdentical()
            {
                Assert.Equal(_sourceEntity.CustomerId, _targetEntity.CustomerId);
                Assert.Equal(_sourceEntity.CustomerCreated, _targetEntity.CustomerCreated);
                Assert.Equal(_sourceEntity.CustomerType, _targetEntity.CustomerType);
                Assert.Equal(_sourceEntity.CustomerNumber, _targetEntity.CustomerNumber);
                Assert.Equal(_sourceEntity.CustomerAlias, _targetEntity.CustomerAlias);
                Assert.Equal(_sourceEntity.CustomerName, _targetEntity.CustomerName);
                Assert.Equal(_sourceEntity.CustomerPrivateDetail, _targetEntity.CustomerPrivateDetail);
            }

            public void AssertNamesAndNumbersOnlyIdentical()
            {
                Assert.NotEqual(_sourceEntity.CustomerId, _targetEntity.CustomerId);
                Assert.NotEqual(_sourceEntity.CustomerCreated, _targetEntity.CustomerCreated);
                Assert.NotEqual(_sourceEntity.CustomerType, _targetEntity.CustomerType);
                Assert.Equal(_sourceEntity.CustomerNumber, _targetEntity.CustomerNumber);
                Assert.NotEqual(_sourceEntity.CustomerAlias, _targetEntity.CustomerAlias);
                Assert.Equal(_sourceEntity.CustomerName, _targetEntity.CustomerName);
                Assert.NotEqual(_sourceEntity.CustomerPrivateDetail, _targetEntity.CustomerPrivateDetail);
            }

            private static T DeepClone<T>(T obj)
            {
                if (obj == null) return default;

                var serialized = JsonSerializer.Serialize(obj);
                var cloned = JsonSerializer.Deserialize<T>(serialized);

                return cloned;
            }

            public void AssertNamesIdentical()
            {
                Assert.Equal(_sourceEntity.CustomerName, _targetEntity.CustomerName);
            }

            public void AssertNumbersNotIdentical()
            {
                Assert.NotEqual(_sourceEntity.CustomerNumber, _targetEntity.CustomerNumber);
            }
        }
    }
}