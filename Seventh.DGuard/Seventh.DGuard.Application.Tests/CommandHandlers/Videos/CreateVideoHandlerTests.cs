using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Seventh.DGuard.Application.CommandHandlers.Videos;
using Seventh.DGuard.Application.Commands.Videos;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.CommandHandlers.Videos
{
    public class CreateVideoHandlerTests
    {
        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly Mock<IVideoRepository> _videoRepositoryMock;
        private readonly CreateVideoHandler _handler;

        public CreateVideoHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();
            _videoRepositoryMock = new Mock<IVideoRepository>();

            var logger = new NullLogger<CreateVideoHandler>();

            _handler = new CreateVideoHandler(_serverRepositoryMock.Object, _videoRepositoryMock.Object, logger);
        }

        [Fact]
        public async Task Retornar_sucesso_ao_criar_video()
        {
            _serverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), false)).ReturnsAsync(new Server("teste", "127.0.0.1", 1234));
            var command = new CreateVideoCommand(Guid.NewGuid(), "video dia 01", "abc");

            var result = await _handler.Handle(command, CancellationToken.None);

            _videoRepositoryMock.Verify(x => x.SaveContent("abc"), Times.Once());
            _serverRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task Retornar_erro_quando_servidor_nao_existe()
        {
            var command = new CreateVideoCommand(Guid.NewGuid(), "video dia 01", "abc");

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }
    }
}
