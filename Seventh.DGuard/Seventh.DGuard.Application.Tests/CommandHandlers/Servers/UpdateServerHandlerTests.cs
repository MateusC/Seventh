using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Seventh.DGuard.Application.CommandHandlers;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Application.CommandValidators;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.CommandHandlers.Servers
{
    public class UpdateServerHandlerTests
    {
        private readonly UpdateServerCommandValidator _validator;
        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly UpdateServerHandler _handler;

        public UpdateServerHandlerTests()
        {
            _validator = new UpdateServerCommandValidator();

            _serverRepositoryMock = new Mock<IServerRepository>();

            var logger = new NullLogger<UpdateServerHandler>();

            _handler = new UpdateServerHandler(_validator, _serverRepositoryMock.Object, logger);
        }

        [Fact]
        public async Task Retornar_servidor_atualizado_quando_sucesso()
        {
            _serverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), false)).ReturnsAsync(new Server("velho", "127.0.0.1", 1234));
            var command = new UpdateServerCommand(Guid.NewGuid(), "novo", "127.0.0.1", 4321);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.SuccessData.Should().NotBeNull();
            _serverRepositoryMock.Verify(x => x.Update(It.Is<Server>(s => s.Name == "novo" && s.Port == 4321)), Times.Once());
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task Retornar_erro_quando_servidor_nao_existir()
        {
            var command = new UpdateServerCommand(Guid.NewGuid(), "novo", "127.0.0.1", 4321);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsFail.Should().BeTrue();
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Retornar_erro_quando_comando_for_invalido()
        {
            var command = new UpdateServerCommand(Guid.Empty, "", "abc.0.0.1", 0);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsFail.Should().BeTrue();
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never());
        }
    }
}
