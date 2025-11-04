using White.Knight.Interfaces.Command;

namespace White.Knight.Interfaces
{
    public interface ICommandTranslator<TD, out TResponse>
    {
        TResponse Translate(ISingleRecordCommand<TD> command);

        TResponse Translate<TP>(IQueryCommand<TD, TP> command);

        TResponse Translate(IUpdateCommand<TD> command);
    }
}