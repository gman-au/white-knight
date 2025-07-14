using System.Collections.Generic;

namespace White.Knight.Abstractions.Definition
{
    public class RootQuery
    {
        public ISubQuery Query { get; set; }
        
        public string Alias { get; set; }

        public ICollection<SelfJoin> Joins { get; set; }
    }
}