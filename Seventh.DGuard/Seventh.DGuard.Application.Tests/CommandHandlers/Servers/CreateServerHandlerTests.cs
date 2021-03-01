using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Seventh.DGuard.Application.CommandHandlers;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Application.CommandValidators;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.CommandHandlers.Servers
{
    public class CreateServerHandlerTests
    {
        private readonly CreateServerCommandValidator _commandValidator;
        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly CreateServerHandler _handler;

        public CreateServerHandlerTests()
        {
            _commandValidator = new CreateServerCommandValidator();

            _serverRepositoryMock = new Mock<IServerRepository>();

            var logger = new NullLogger<CreateServerHandler>();

            _handler = new CreateServerHandler(_commandValidator, _serverRepositoryMock.Object, logger);
        }

        [Fact]
        public async Task Criar_servidor_quando_comando_valido()
        {
            _serverRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Server>())).ReturnsAsync(new Server("teste", "127.0.0.1", 1234));
            var command = new CreateServerCommand("teste", "127.0.0.1", 1234);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.SuccessData.Should().NotBeNull();
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task Retornar_erros_e_nao_criar_servidor_quando_comando_invalido()
        {
            var command = new CreateServerCommand("", "teste", 0);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsFail.Should().BeTrue();
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never());
        }
    }
}
