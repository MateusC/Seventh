using FluentAssertions;
using Moq;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Application.QueryHandlers;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.QueryHandlers
{
    public class ListVideosHandlerTests
    {
        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly ListVideosHandler _hander;

        public ListVideosHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();

            _hander = new ListVideosHandler(_serverRepositoryMock.Object);
        }

        [Fact]
        public async Task Retornar_videos_do_servidor()
        {
            _serverRepositoryMock.Setup(x => x.GetAsync(Guid.Empty, false)).ReturnsAsync(new Server(string.Empty, string.Empty, 1234));
            var query = new ListVideosQuery(Guid.Empty);

            var videos = await _hander.Handle(query, CancellationToken.None);

            videos.Should().BeEmpty();
            _serverRepositoryMock.Verify(x => x.GetAsync(Guid.Empty, false), Times.Once());
        }

        [Fact]
        public async Task Retornar_lista_vazia_videos_quando_servidor_nao_existir()
        {
            var query = new ListVideosQuery(Guid.Empty);

            var videos = await _hander.Handle(query, CancellationToken.None);

            videos.Should().BeEmpty();
            _serverRepositoryMock.Verify(x => x.GetAsync(Guid.Empty, false), Times.Once());
        }
    }
}
