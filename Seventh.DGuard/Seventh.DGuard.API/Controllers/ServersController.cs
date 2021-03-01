using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Application.Queries;
using Seventh.DGuard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seventh.DGuard.API.Controllers
{
    [ApiController]
    [Route("api/servers")]
    public class ServersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna todos os servidores
        /// </summary>
        /// <returns>Lista dos servidores</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Server>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var servers = await _mediator.Send(new GetServersQuery());

            return Ok(servers);
        }

        /// <summary>
        /// Cria um servidor
        /// </summary>
        /// <param name="command">Parâmetros para criação de um servidor</param>
        /// <returns>Servidor criado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Server), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<String>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateServerCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Created($"/servers/{result.SuccessData.Id}", result.SuccessData);

            return BadRequest(result.FailData);
        }

        /// <summary>
        /// Atualiza um servidor
        /// </summary>
        /// <param name="command">Parâmetros para atualização de um servidor</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<String>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateServerCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result.FailData);
        }

        /// <summary>
        /// Remove um servidor
        /// </summary>
        /// <param name="serverId">Identificador do servidor</param>
        /// <returns></returns>
        [HttpDelete("{serverId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid serverId)
        {
            var result = await _mediator.Send(new DeleteServerCommand(serverId));

            if (result.IsFail)
                return BadRequest(result.FailData);

            return Accepted();
        }

        /// <summary>
        /// Recupera um servidor existente
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{serverId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Recover(Guid serverId)
        {
            var result = await _mediator.Send(new RecoverServerCommand(serverId));

            if (result.IsFail)
                return BadRequest(result.FailData);

            return Accepted();
        }

        /// <summary>
        /// Verifica a disponibilidade de um servidor
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        [HttpPost("available/{serverId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<IActionResult> CheckServer(Guid serverId)
        {
            var isAlive = await _mediator.Send(new CheckServerStatusCommand(serverId));

            if (isAlive)
                return Ok();

            return BadRequest("Não foi possível se conectar ao servidor.");
        }
    }
}
