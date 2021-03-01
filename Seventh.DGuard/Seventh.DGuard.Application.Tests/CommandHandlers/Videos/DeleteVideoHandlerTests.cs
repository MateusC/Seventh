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
    public class DeleteVideoHandlerTests
    {
        private readonly DeleteVideoHandler _handler;

        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly Mock<IVideoRepository> _videoRepositoryMock;

        public DeleteVideoHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();
            _videoRepositoryMock = new Mock<IVideoRepository>();

            var logger = new NullLogger<DeleteVideoHandler>();

            _handler = new DeleteVideoHandler(_serverRepositoryMock.Object, _videoRepositoryMock.Object, logger);
        }

        [Fact]
        public async Task Remover_video()
        {
            var server = BuildServer();
            _serverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), false)).ReturnsAsync(server);
            var command = new DeleteVideoCommand(Guid.NewGuid(), server.Videos.First().Id);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _videoRepositoryMock.Verify(x => x.RemoveContent(string.Empty), Times.Once());
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task Retornar_erro_quando_video_nao_existir()
        {
            var server = BuildServer();
            _serverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), false)).ReturnsAsync(server);
            var command = new DeleteVideoCommand(Guid.NewGuid(), Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task Retornar_erro_quando_servidor_nao_existir()
        {
            var command = new DeleteVideoCommand(Guid.NewGuid(), Guid.NewGuid());

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
