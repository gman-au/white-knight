using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.Abstractions.Command
{
    public class SingleRecordCommand<T> : ISingleRecordCommand<T>
    {
        public object Key { get; set; }

        public INavigationStrategy<T> NavigationStrategy { get; set; }
    }
}