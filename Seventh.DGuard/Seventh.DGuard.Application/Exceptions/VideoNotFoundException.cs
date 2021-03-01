using System;

namespace Seventh.DGuard.Application.Exceptions
{
    public class VideoNotFoundException : Exception
    {
        private const string MESSAGE_TEMPLATE = "Não foi encontrado vídeo com id {0}.";

        public VideoNotFoundException()
        {
        }

        public VideoNotFoundException(Guid serverId) : base(string.Format(MESSAGE_TEMPLATE, serverId))
        {
        }
    }
}
