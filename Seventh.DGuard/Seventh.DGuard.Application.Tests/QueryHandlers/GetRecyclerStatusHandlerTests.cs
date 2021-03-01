using FluentAssertions;
using Moq;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Application.QueryHandlers;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Enums;
using Seventh.DGuard.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.QueryHandlers
{
    public class GetRecyclerStatusHandlerTests
    {
        private readonly Mock<IRecyclerRepository> _recyclerRepository;
        private readonly GetRecyclerStatusHandler _handler;

        public GetRecyclerStatusHandlerTests()
        {
            _recyclerRepository = new Mock<IRecyclerRepository>();

            _handler = new GetRecyclerStatusHandler(_recyclerRepository.Object);
        }

        [Fact]
        public async Task Retornar_nao_rodando_quando_nao_tiver_job()
        {
            var query = new GetRecyclerStatusQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().Be(JobStatus.NotRunning);
        }

        [Fact]
        public async Task Retornar_status_rodando_quando_existir_job_rodando()
        {
            _recyclerRepository.Setup(x => x.GetLastRun()).ReturnsAsync(new Recycler());
            var query = new GetRecyclerStatusQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().Be(JobStatus.Running);
        }

        [Fact]
        public async Task Retornar_status_nao_rodando_quando_existir_job_parado()
        {
            var lastRecycler = new Recycler();
            lastRecycler.Executed();
            _recyclerRepository.Setup(x => x.GetLastRun()).ReturnsAsync(lastRecycler);
            var query = new GetRecyclerStatusQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().Be(JobStatus.NotRunning);
        }
    }
}
