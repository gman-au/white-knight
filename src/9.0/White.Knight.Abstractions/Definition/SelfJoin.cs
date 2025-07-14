namespace White.Knight.Abstractions.Definition
{
    public class SelfJoin
    {
        public string ChildSet { get; set; }
        
        public string Alias { get; set; }
        
        public ISubQuery SubQuery { get; set; }
    }
}