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
    public class DeleteServerHandlerTests
    {
        private readonly Mock<IServerRepository> _serverRepositoryMock;

        private readonly DeleteServerHandler _handler;

        public DeleteServerHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();
            
            var logger = new NullLogger<DeleteServerHandler>();

            _handler = new DeleteServerHandler(_serverRepositoryMock.Object, logger);
        }

        [Fact]
        public async Task Retornar_erro_quando_identificador_servidor_for_vazio()
        {
            var command = new DeleteServerCommand(Guid.Empty);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.SuccessData.Should().BeFalse();
            result.FailData.Should().Be("Identificador do servidor é obrigatório.");
        }

        [Fact]
        public async Task Retornar_sucesso_quando_excluir_servidor()
        {
            _serverRepositoryMock.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(true);
            var command = new DeleteServerCommand(Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.SuccessData.Should().BeTrue();
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task Retornar_falha_quando_nao_encontrar_servidor()
        {
            var serverId = Guid.NewGuid();
            var command = new DeleteServerCommand(serverId);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.SuccessData.Should().BeFalse();
            result.FailData.Should().Be($"Não foi encontrado servidor com identificador {serverId}");
        }
    }
}
