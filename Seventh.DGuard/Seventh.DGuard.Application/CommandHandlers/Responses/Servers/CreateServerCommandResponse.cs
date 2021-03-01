using Seventh.DGuard.Application.CommandHandlers.Base;
using Seventh.DGuard.Domain.Entities;
using System.Collections.Generic;

namespace Seventh.DGuard.Application.CommandHandlers.Responses
{
    public sealed class CreateServerCommandResponse : CommandResponse<Server, IEnumerable<string>>
    {
        private CreateServerCommandResponse()
        {
        }

        public CreateServerCommandResponse(Server successData, IEnumerable<string> failData) : base(successData, failData)
        {
        }
    }
}
