using System;

namespace Seventh.DGuard.Application.Exceptions
{
    public class ServerNotFoundException : Exception
    {
        private const string MESSAGE_TEMPLATE = "Não foi encontrado servidor com id {0}.";

        public ServerNotFoundException()
        {
        }

        public ServerNotFoundException(Guid serverId) : base(string.Format(MESSAGE_TEMPLATE, serverId))
        {
        }
    }
}
