using Seventh.DGuard.Application.CommandHandlers.Base;

namespace Seventh.DGuard.Application.CommandHandlers.Responses.Videos
{
    public sealed class RecoverVideoCommandResponse : CommandResponse<bool, string>
    {
        private RecoverVideoCommandResponse()
        {
        }

        public RecoverVideoCommandResponse(bool successData, string failData) : base(successData, failData)
        {
        }
    }
}
