using Seventh.DGuard.Application.CommandHandlers.Base;

namespace Seventh.DGuard.Application.CommandHandlers.Responses
{
    public sealed class DeleteServerCommandResponse : CommandResponse<bool, string>
    {
        private DeleteServerCommandResponse()
        {
        }

        public DeleteServerCommandResponse(bool successData, string failData) : base(successData, failData)
        {
        }
    }
}
