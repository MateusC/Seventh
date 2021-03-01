namespace Seventh.DGuard.Application.CommandHandlers.Base
{
    public abstract class CommandResponse<SuccessResponse, FailResponse>
    {
        protected CommandResponse()
        {
        }

        public CommandResponse(SuccessResponse successData, FailResponse failData)
        {
            SuccessData = successData;
            FailData = failData;
        }

        public SuccessResponse SuccessData { get; set; }

        public FailResponse FailData { get; set; }

        public bool IsSuccess => SuccessData != null;

        public bool IsFail => FailData != null;
    }
}
