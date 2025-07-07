namespace White.Knight.Interfaces.Command
{
	public interface ICommand<T>
	{
		public INavigationStrategy<T> NavigationStrategy { get; set; }
	}
}