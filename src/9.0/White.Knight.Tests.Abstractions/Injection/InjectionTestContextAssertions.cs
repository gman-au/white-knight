using System;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Interfaces;
using White.Knight.Tests.Domain;
using Xunit;

namespace White.Knight.Tests.Abstractions.Injection
{
    public partial class InjectionTestContextBase
    {
        public virtual void AssertRepositoryResolved()
        {
            Assert.NotNull(Sut.GetService<IRepository<Customer>>());
            Assert.Null(Sut.GetService<IRepository<Address>>());
            Assert.NotNull(Sut.GetService<IKeylessRepository<Address>>());
        }

        public virtual void AssertExceptionRethrowerResolved()
        {
            Assert.NotNull(Sut.GetService<IRepositoryExceptionRethrower>());
        }

        public virtual void AssertExceptionRethrowerNotResolved()
        {
            Assert.Null(Sut.GetService<IRepositoryExceptionRethrower>());
        }

        public virtual void AssertLoggerFactoryResolved()
        {
            throw new NotImplementedException("Override this method in your implementation");
        }

        public virtual void AssertRepositoryFeaturesResolved()
        {
            var options =
                Sut
                    .GetRequiredService<IRepositoryFeatures>();

            Assert.NotNull(options);
        }
    }
}