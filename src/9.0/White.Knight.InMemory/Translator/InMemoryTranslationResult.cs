namespace White.Knight.InMemory.Translator
{
    /// <summary>
    ///     In practice, this would be something tangibly related to the execution of the command
    ///     e.g. an SQL / CQL / other query string.
    /// </summary>
    public class InMemoryTranslationResult
    {
        public string ExampleQueryLanguageString { get; set; }
    }
}