using Seventh.DGuard.Application.CommandHandlers.Base;
using Seventh.DGuard.Domain.Entities;

namespace Seventh.DGuard.Application.CommandHandlers.Responses.Videos
{
    public sealed class CreateVideoCommandResponse : CommandResponse<Video, string>
    {
        private CreateVideoCommandResponse()
        {
        }

        public CreateVideoCommandResponse(Video successData, string failData) : base(successData, failData)
        {
        }
    }
}
