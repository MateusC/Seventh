using FluentAssertions;
using Seventh.DGuard.Application.Commands;
using Seventh.DGuard.Application.CommandValidators;
using System.Threading.Tasks;
using Xunit;

namespace Seventh.DGuard.Application.Tests.CommandValidators
{
    public class CreateServerCommandValidatorTests
    {
        private readonly CreateServerCommandValidator _validator;

        public CreateServerCommandValidatorTests()
        {
            _validator = new CreateServerCommandValidator();
        }

        [Fact]
        public async Task Validar_comando_correto()
        {
            var command = new CreateServerCommand("teste", "127.0.0.1", 80);

            var result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Retornar_erro_quando_comando_nao_tiver_nome()
        {
            var command = new CreateServerCommand(null, "127.0.0.1", 80);

            var result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Nome é obrigatório");
        }

        [Fact]
        public async Task Retornar_erro_quando_comando_nao_tiver_ip()
        {
            var command = new CreateServerCommand("teste", null, 80);

            var result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "IP é obrigatório");
        }

        [Fact]
        public async Task Retornar_erro_quando_comando_tiver_ip_invalido()
        {
            var command = new CreateServerCommand("teste", "google.com.br", 80);

            var result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "IP inválido");
        }

        [Fact]
        public async Task Retornar_erro_quando_comando_tiver_porta_zerada()
        {
            var command = new CreateServerCommand("teste", "127.0.0.1", 0);

            var result = await _validator.ValidateAsync(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Porta informada deve ser maior que 0");
        }
    }
}
