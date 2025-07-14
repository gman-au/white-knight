using System;

namespace White.Knight.Abstractions.Definition
{
    public class QuerySubQuery : ISubQuery
    {
        public object OperandLeft { get; set; }
        
        public string Operator { get; set; }
        
        public object OperandRight { get; set; }
        
        public Type OperandType { get; set; }
    }
}