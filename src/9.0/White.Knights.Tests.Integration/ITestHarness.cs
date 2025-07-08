using System.Threading.Tasks;

namespace White.Knights.Tests.Integration
{
    public interface ITestHarness
    {
        public Task LoadTableDataAsync();
    }
}