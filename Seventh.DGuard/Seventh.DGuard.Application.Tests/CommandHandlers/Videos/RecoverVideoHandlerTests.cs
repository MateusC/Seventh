using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Seventh.DGuard.Application.CommandHandlers.Videos;
using Seventh.DGuard.Application.Commands.Videos;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.CommandHandlers.Videos
{
    public class RecoverVideoHandlerTests
    {
        private readonly RecoverVideoHandler _handler;

        private readonly Mock<IServerRepository> _serverRepositoryMock;

        public RecoverVideoHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();

            var logger = new NullLogger<RecoverVideoHandler>();

            _handler = new RecoverVideoHandler(_serverRepositoryMock.Object, logger);
        }

        [Fact]
        public async Task Recuperar_video()
        {
            var server = BuildServer();
            _serverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), true)).ReturnsAsync(server);
            var command = new RecoverVideoCommand(server.Id, server.Videos.First().Id);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _serverRepositoryMock.Verify(x => x.RecoverVideo(It.IsAny<Video>()), Times.Once());
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task Retornar_erro_quando_video_nao_existir()
        {
            var server = BuildServer();
            _serverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), true)).ReturnsAsync(server);
            var command = new RecoverVideoCommand(Guid.NewGuid(), Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsFail.Should().BeTrue();
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Retornar_erro_quando_servidor_nao_existir()
        {
            var command = new RecoverVideoCommand(Guid.NewGuid(), Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        private static Server BuildServer()
        {
            var server = new Server("teste", "127.0.0.1", 1234);

            server.AddVideo(string.Empty, string.Empty);

            return server;
        }
    }
}
