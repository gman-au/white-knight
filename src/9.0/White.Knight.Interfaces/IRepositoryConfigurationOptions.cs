using White.Knight.Domain.Enum;

namespace White.Knight.Interfaces
{
    public interface IRepositoryConfigurationOptions
    {
        public ClientSideEvaluationResponseTypeEnum ClientSideEvaluationResponse { get; set; }
    }
}