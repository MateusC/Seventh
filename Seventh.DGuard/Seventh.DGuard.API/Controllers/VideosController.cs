using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventh.DGuard.API.Models;
using Seventh.DGuard.Application.Commands.Videos;
using Seventh.DGuard.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seventh.DGuard.API.Controllers
{
    [ApiController]
    [Route("api/servers/{serverId}/videos")]
    public class VideosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adicionar um vídeo ao servidor
        /// </summary>
        /// <param name="serverId">Identificador do servidor</param>
        /// <param name="command">Comando com os parâmetros do vídeo</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add(Guid serverId, [FromBody] CreateVideoCommand command)
        {
            command.ServerId = serverId;

            try
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    var viewModel = new VideoViewModel(result.SuccessData.Id, result.SuccessData.Description);

                    return Created($"/servers/{serverId}/videos/{viewModel.Id}", viewModel);
                }

                return NotFound(result.FailData);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível adicionar o vídeo, tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Remover um vídeo
        /// </summary>
        /// <param name="serverId">Identificador do servidor</param>
        /// <param name="videoId">Identificador do vídeo</param>
        /// <returns></returns>
        [HttpDelete("{videoId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid serverId, Guid videoId)
        {
            var result = await _mediator.Send(new DeleteVideoCommand(serverId, videoId));

            if (result.IsFail)
            {
                return NotFound(result.FailData.Message);
            }

            return Accepted();
        }

        /// <summary>
        /// Recupera um video
        /// </summary>
        /// <param name="serverId">Identificador do servidor</param>
        /// <param name="videoId">Identificador do vídeo</param>
        /// <returns></returns>
        [HttpPatch("{videoId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Recover(Guid serverId, Guid videoId)
        {
            var result = await _mediator.Send(new RecoverVideoCommand(serverId, videoId));

            if (result.IsFail)
                return BadRequest(result.FailData);

            return Accepted();
        }

        /// <summary>
        /// Retorna os dados cadastrais de todos os vídeos de um servidor
        /// </summary>
        /// <param name="serverId">Identificador do servidor</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<VideoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid serverId)
        {
            var videos = await _mediator.Send(new ListVideosQuery(serverId));

            if (videos.Any())
            {
                var viewModels = videos.Select(x => new VideoViewModel(x.Id, x.Description));

                return Ok(viewModels);
            }

            return NotFound($"Não foi encontrado vídeos no servidor {serverId}");
        }

        /// <summary>
        /// Retorna os dados cadastrais de um vídeo
        /// </summary>
        /// <param name="serverId">Identificador do servidor</param>
        /// <param name="videoId">Identificador do vídeo</param>
        /// <returns></returns>
        [HttpGet("{videoId}")]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid serverId, Guid videoId)
        {
            var video = await _mediator.Send(new GetVideoQuery(serverId, videoId));

            if (video is null)
                return NotFound($"Não foi encontrado vídeo {videoId} no servidor {serverId}");

            return Ok(new VideoViewModel(video.Id, video.Description));
        }

        /// <summary>
        /// Retorna o conteúdo de um vídeo
        /// </summary>
        /// <param name="serverId">Identificador do servidor</param>
        /// <param name="videoId">Identificador do vídeo</param>
        /// <returns></returns>
        [HttpGet("{videoId}/binary")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContent(Guid serverId, Guid videoId)
        {
            var videoContent = await _mediator.Send(new GetVideoContentQuery(serverId, videoId));

            if (videoContent is null)
                return NotFound($"Não foi encontrado o conteúdo do vídeo {videoId} no servidor {serverId}");

            return Ok(videoContent);
        }
    }
}