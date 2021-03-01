using Seventh.DGuard.Application.CommandHandlers.Base;
using Seventh.DGuard.Domain.Entities;
using System.Collections.Generic;

namespace Seventh.DGuard.Application.CommandHandlers.Responses
{
    public sealed class UpdateServerCommandResponse : CommandResponse<Server, IEnumerable<string>>
    {
        private UpdateServerCommandResponse()
        {
        }

        public UpdateServerCommandResponse(Server successData, IEnumerable<string> failData) : base(successData, failData)
        {
        }
    }
}
