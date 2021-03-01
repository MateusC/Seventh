using MediatR;
using Microsoft.Extensions.Logging;
using Seventh.DGuard.Application.Commands.Recyclers;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Repositories;
using Seventh.DGuard.Domain.Util;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seventh.DGuard.Application.CommandHandlers.Recyclers
{
    public class RecyclerStartHandler : IRequestHandler<RecyclerStartCommand>
    {
        private readonly ILogger<RecyclerStartHandler> _logger;
        private readonly IRecyclerRepository _recyclerRepository;
        private readonly IServerRepository _serverRepository;
        private readonly IVideoRepository _videoRepository;

        public RecyclerStartHandler(
            IServerRepository serverRepository, 
            IVideoRepository videoRepository, 
            IRecyclerRepository recyclerRepository,
            ILogger<RecyclerStartHandler> logger)
        {
            _logger = logger;
            _recyclerRepository = recyclerRepository;
            _serverRepository = serverRepository;
            _videoRepository = videoRepository;
        }

        public async Task<Unit> Handle(RecyclerStartCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Iniciando processo de reciclagem de vídeos com mais de {request.Days} dias.");

            var recycler = new Recycler();

            _recyclerRepository.Add(recycler);

            await _recyclerRepository.SaveChangesAsync();

            var servers = await _serverRepository.GetAllAsync();

            foreach (var server in servers)
            {
                var date = Clock.Now.Date.AddDays(-request.Days);

                var oldVideos = server.Videos.Where(x => x.CreatedDate.Date <= date);

                _logger.LogInformation($"Reciclando {oldVideos.Count()} vídeos do servidor {server.Name}.");

                foreach (var video in oldVideos)
                {
                    await _videoRepository.RemoveContent(video.Path);

                    _serverRepository.RemoveVideo(video);

                    _logger.LogInformation($"Vídeo {video.Description} removido do servidor {server.Name}");
                }
            }

            recycler.Executed();

            await _recyclerRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
