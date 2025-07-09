using Microsoft.Extensions.DependencyInjection;

namespace White.Knight.Tests.Abstractions.Injection
{
    public partial class InjectionTestContextBase
    {
        public void ActLoadServiceProvider()
        {
            Sut =
                ServiceCollection
                    .BuildServiceProvider();
        }
    }
}