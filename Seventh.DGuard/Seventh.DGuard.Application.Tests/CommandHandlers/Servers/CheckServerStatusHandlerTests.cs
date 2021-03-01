using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Seventh.DGuard.Application.CommandHandlers;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.CommandHandlers.Servers
{
    public class CheckServerStatusHandlerTests
    {
        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly CheckServerStatusHandler _handler;

        public CheckServerStatusHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();

            var logger = new NullLogger<CheckServerStatusHandler>();

            _handler = new CheckServerStatusHandler(_serverRepositoryMock.Object, logger);
        }

        [Fact]
        public async Task Retornar_falso_quando_nao_encontrar_servidor()
        {
            var command = new CheckServerStatusCommand(Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse(); 
        }

        [Fact]
        public async Task Retornar_status_servidor_quando_for_encontrado()
        {
            _serverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), false)).ReturnsAsync(new Domain.Entities.Server("teste", "localhost", 123));

            var command = new CheckServerStatusCommand(Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
        }
    }
}
