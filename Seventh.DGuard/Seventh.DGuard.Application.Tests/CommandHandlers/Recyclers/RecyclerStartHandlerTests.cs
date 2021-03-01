using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Seventh.DGuard.Application.CommandHandlers.Recyclers;
using Seventh.DGuard.Application.Commands.Recyclers;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using Seventh.DGuard.Domain.Util;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.CommandHandlers.Recyclers
{
    public class RecyclerStartHandlerTests
    {
        private readonly RecyclerStartHandler _handler;

        private readonly Mock<IServerRepository> _serverRepository;
        private readonly Mock<IVideoRepository> _videoRepositoryMock;
        private readonly Mock<IRecyclerRepository> _recyclerRepositoryMock;

        public RecyclerStartHandlerTests()
        {
            _serverRepository = new Mock<IServerRepository>();
            _videoRepositoryMock = new Mock<IVideoRepository>();
            _recyclerRepositoryMock = new Mock<IRecyclerRepository>();

            var logger = new NullLogger<RecyclerStartHandler>();

            _handler = new RecyclerStartHandler(
                _serverRepository.Object, 
                _videoRepositoryMock.Object, 
                _recyclerRepositoryMock.Object, 
                logger);
        }

        [Fact]
        public async Task Retornar_sucesso_quando_reciclagem_ocorrer()
        {
            _serverRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Server>());

            var result = await _handler.Handle(new RecyclerStartCommand(10), CancellationToken.None);

            _recyclerRepositoryMock.Verify(x => x.Add(It.IsAny<Recycler>()), Times.Once);
            _recyclerRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.AtLeastOnce());
        }

        [Fact]
        public async Task Percorrer_todos_servidores_processo_reciclagem()
        {
            var servers = new List<Server>()
            {
                new Server("teste", "127.0.0.1", 1234)
            };

            _serverRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(servers);

            var result = await _handler.Handle(new RecyclerStartCommand(10), CancellationToken.None);

            _serverRepository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Remover_todos_videos_anteriores_data_estipulada()
        {
            Clock.Subtract(TimeSpan.FromDays(5));

            var server = new Server("teste", "127.0.0.1", 1234);
            var video = server.AddVideo(String.Empty, String.Empty);
            var servers = new List<Server>()
            {
                server
            };

            Clock.Reset();

            _serverRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(servers);

            var result = await _handler.Handle(new RecyclerStartCommand(5), CancellationToken.None);

            _serverRepository.Verify(x => x.GetAllAsync(), Times.Once);
            _serverRepository.Verify(x => x.RemoveVideo(video), Times.Once);
            _videoRepositoryMock.Verify(x => x.RemoveContent(video.Path), Times.Once);
        }
    }
}
