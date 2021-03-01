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
    public class GetVideoHandlerTests
    {
        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly GetVideoHandler _hander;

        public GetVideoHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();

            _hander = new GetVideoHandler(_serverRepositoryMock.Object);
        }

        [Fact]
        public async Task Retornar_video()
        {
            var query = new GetVideoQuery(Guid.Empty, Guid.Empty);

            await _hander.Handle(query, CancellationToken.None);

            _serverRepositoryMock.Verify(x => x.GetAsync(Guid.Empty, false), Times.Once());
        }
    }
}
