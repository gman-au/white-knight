using White.Knight.Domain.Enum;
using White.Knight.Interfaces;

namespace White.Knight.Abstractions.Options
{
    public class RepositoryConfigurationOptions : IRepositoryConfigurationOptions
    {
        public ClientSideEvaluationResponseTypeEnum ClientSideEvaluationResponse { get; set; } = ClientSideEvaluationResponseTypeEnum.Warn;
    }
}