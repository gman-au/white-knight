namespace White.Knight.Interfaces.Command
{
	public interface ISingleRecordCommand<T> : ICommand<T>
	{
		public object Key { get; set; }
	}
}