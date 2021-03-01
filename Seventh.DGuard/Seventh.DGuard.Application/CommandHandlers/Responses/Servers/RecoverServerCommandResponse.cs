using Seventh.DGuard.Application.CommandHandlers.Base;

namespace Seventh.DGuard.Application.CommandHandlers.Responses
{
    public sealed class RecoverServerCommandResponse : CommandResponse<bool, string>
    {
        private RecoverServerCommandResponse()
        {
        }

        public RecoverServerCommandResponse(bool successData, string failData) : base(successData, failData)
        {
        }
    }
}
