using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventh.DGuard.API.Models;
using Seventh.DGuard.Application.Commands.Recyclers;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Seventh.DGuard.API.Controllers
{
    [ApiController]
    [Route("api/recycler")]
    public class RecyclerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecyclerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna o status do processo de reciclagem dos vídeos
        /// </summary>
        /// <returns></returns>
        [HttpGet("status")]
        [ProducesResponseType(typeof(RecyclerStatusViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            JobStatus lastJobStatus = await _mediator.Send(new GetRecyclerStatusQuery());

            return Ok(new RecyclerStatusViewModel(lastJobStatus.Description));
        }

        /// <summary>
        /// Recicla os vídeos mais antigos que o parâmetro de dias
        /// </summary>
        /// <returns></returns>
        [HttpPost("process/{days}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult Process(UInt32 days)
        {
            BackgroundJob.Enqueue(() => SendCommand(days));

            return Accepted();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Task SendCommand(UInt32 days)
        {
            return _mediator.Send(new RecyclerStartCommand(days));
        }
    }
}