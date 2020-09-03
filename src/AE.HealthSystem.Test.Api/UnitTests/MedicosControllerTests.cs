using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Services.Api.Controllers;
using AE.HealthSystem.Services.Api.ViewModels.Medico;
using AutoMapper;
using Bogus;
using Bogus.DataSets;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System.Collections.Generic;
using Xunit;

namespace AE.HealthSystem.Test.Api.UnitTests
{
    public class MedicosControllerTests
    {
        public MedicosController medicosController { get; set; }
        public Mock<IMedicoRepository> mockMedicoRepository { get; set; }
        public Mock<IValidator<Medico>> mockMedicoValidator { get; set; }
        public Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }
        public Mock<IMapper> mockMapper { get; set; }
        public Mock<ILogger> mockLogger { get; set; }

        public MedicosControllerTests()
        {
            mockMedicoRepository = new Mock<IMedicoRepository>();
            mockMedicoValidator = new Mock<IValidator<Medico>>();
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger>();

            medicosController = new MedicosController(
                 mockMedicoRepository.Object,
                 mockMedicoValidator.Object,
                 mockHttpContextAccessor.Object,
                 mockMapper.Object,
                 mockLogger.Object
                 );
        }

        [Theory]
        [InlineData(1)]
        [Trait("Médico", "Testes Medicos Controller")]
        public void MedicosController_ObterMedicoPorId_RetornarComSucesso(long id)
        {
            // Arrange
            var listMedicoViewModelFaker = new Faker<ListMedicoViewModel>("pt_BR")
                .RuleFor(r => r.Id, c => c.Random.Long())
                 .RuleFor(r => r.Nome, c => c.Name.FullName(Name.Gender.Male))
                 .RuleFor(r => r.Especialidade, c => c.Name.FirstName())
                 .Generate();

            mockMapper.Setup(m => m.Map<ListMedicoViewModel>(It.IsAny<Medico>())).Returns(listMedicoViewModelFaker);

            // Act
            var result = medicosController.ObterMedicoPorId(id).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Obter Médicos com sucesso")]
        [Trait("Médico", "Testes Medicos Controller")]
        public void MedicosController_ObterMedicos_RetornarComSucesso()
        {
            // Arrange
            var listMedicoViewModelFaker = new Faker<ListMedicoViewModel>("pt_BR")
                .RuleFor(r => r.Id, c => c.Random.Long())
                 .RuleFor(r => r.Nome, c => c.Name.FullName(Name.Gender.Male))
                 .RuleFor(r => r.Especialidade, c => c.Name.FirstName())
                 .Generate(5);

            mockMapper.Setup(m => m.Map<IEnumerable<ListMedicoViewModel>>(It.IsAny <IEnumerable<Medico>>())).Returns(listMedicoViewModelFaker);

            // Act
            var result = medicosController.ObterMedicos().Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
