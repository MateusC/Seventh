using Seventh.DGuard.Application.CommandHandlers.Base;
using System;

namespace Seventh.DGuard.Application.CommandHandlers.Responses.Videos
{
    public class DeleteVideoCommandResponse : CommandResponse<bool, Exception>
    {
        public DeleteVideoCommandResponse()
        {
        }

        public DeleteVideoCommandResponse(bool successData, Exception failData) : base(successData, failData)
        {
        }
    }
}
