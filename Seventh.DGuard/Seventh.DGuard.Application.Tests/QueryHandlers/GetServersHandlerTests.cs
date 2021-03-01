using FluentAssertions;
using Moq;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.QueryHandlers
{
    public class GetServersHandlerTests
    {
        private readonly Mock<IServerRepository> _serverRepositoryMock;
        private readonly GetServersHandler _handler;

        public GetServersHandlerTests()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();

            _handler = new GetServersHandler(_serverRepositoryMock.Object);
        }

        [Fact]
        public async Task Retornar_todos_servidores()
        {
            var query = new GetServersQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().BeNull();
            _serverRepositoryMock.Verify(x => x.GetAll(false), Times.Once());
        }
    }
}
