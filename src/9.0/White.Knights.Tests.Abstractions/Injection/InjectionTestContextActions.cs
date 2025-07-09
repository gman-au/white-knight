using Microsoft.Extensions.DependencyInjection;

namespace White.Knights.Tests.Abstractions.Injection
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