using FluentAssertions;
using Seventh.DGuard.Domain.Entities;
using Seventh.DGuard.Domain.Enums;
using System;
using Xunit;

namespace Seventh.DGuard.Domain.Tests.Entities
{
    public class RecyclerTests
    {
        [Fact]
        public void Deve_ter_status_rodando_quando_criar_processo_reciclagem()
        {
            var recycler = new Recycler();

            recycler.CreatedDate.Should().BeAfter(DateTime.MinValue);
            recycler.Status.Should().Be(JobStatus.Running);
        }

        [Fact]
        public void Deve_atualizar_status_executado()
        {
            var recycler = new Recycler();

            recycler.Executed();

            recycler.UpdatedDate.Should().HaveValue();
            recycler.Status.Should().Be(JobStatus.Executed);
        }
    }
}
