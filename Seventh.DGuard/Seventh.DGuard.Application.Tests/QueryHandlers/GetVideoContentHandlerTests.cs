using Moq;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Application.QueryHandlers;
using Seventh.DGuard.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.QueryHandlers
{
    public class GetVideoContentHandlerTests
    {
        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly Mock<IVideoRepository> _videoRepositoryMock;
        private readonly GetVideoContentHandler _handler;

        public GetVideoContentHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();
            _videoRepositoryMock = new Mock<IVideoRepository>();

            _handler = new GetVideoContentHandler(_serverRepositoryMock.Object, _videoRepositoryMock.Object);
        }

        [Fact]
        public async Task Retornar_conteudo_video()
        {
            var query = new GetVideoContentQuery(Guid.NewGuid(), Guid.NewGuid());

            var result = await _handler.Handle(query, CancellationToken.None);

            _serverRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>(), false), Times.Once());
            _videoRepositoryMock.Verify(x => x.GetContent(It.IsAny<string>()), Times.Once());
        }
    }
}
