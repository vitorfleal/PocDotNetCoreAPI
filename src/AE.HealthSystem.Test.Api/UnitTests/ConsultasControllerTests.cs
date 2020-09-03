using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Services.Api.Controllers;
using AE.HealthSystem.Services.Api.ViewModels.Consulta;
using AutoMapper;
using Bogus;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace AE.HealthSystem.Test.Api.UnitTests
{
    public class ConsultasControllerTests
    {
        public ConsultasController consultasController { get; set; }
        public Mock<IConsultaRepository> mockConsultaRepository { get; set; }
        public Mock<IValidator<Consulta>> mockConsultaValidator { get; set; }
        public Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }
        public Mock<IMapper> mockMapper { get; set; }
        public Mock<ILogger> mockLogger { get; set; }

        public ConsultasControllerTests()
        {
            mockConsultaRepository = new Mock<IConsultaRepository>();
            mockConsultaValidator = new Mock<IValidator<Consulta>>();
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger>();

            consultasController = new ConsultasController(
                 mockConsultaRepository.Object,
                 mockConsultaValidator.Object,
                 mockHttpContextAccessor.Object,
                 mockMapper.Object,
                 mockLogger.Object
                 );
        }

        [Theory]
        [InlineData("Carlos Silva")]
        [Trait("Consulta", "Testes Consultas Controller")]
        public void PacientesController_ObterConsultasPorMedico_RetornarComSucesso(string nome)
        {
            // Arrange
            var listConsultaViewModelFaker = new Faker<ListConsultaViewModel>("pt_BR")
                .RuleFor(r => r.Id, c => c.Random.Long())
                 .RuleFor(r => r.DataAgendamento, c => c.Date.Recent(100))
                 .RuleFor(r => r.MedicoId, c => c.Random.Long())
                 .RuleFor(r => r.PacienteId, c => c.Random.Long())
                 .Generate(5);

            mockMapper.Setup(m => m.Map<IEnumerable<ListConsultaViewModel>>(It.IsAny<IEnumerable<Consulta>>())).Returns(listConsultaViewModelFaker.AsEnumerable());

            // Act
            var result = consultasController.ObterConsultasPorMedico(nome).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Agendar Consultas com sucesso")]
        [Trait("Consulta", "Testes Consultas Controller")]
        public void ConsultassController_AgendarrConsulta_RetornarComSucesso()
        {
            // Arrange
            IEnumerable<Consulta> consulta = null;

            Expression<Func<Consulta, bool>> criterio = y => y.MedicoId == 1 && y.PacienteId == 1;

            var consultaFaker = new Faker<Consulta>("pt_BR")
                .RuleFor(r => r.DataAgendamento, c => c.Date.Future())
                 .RuleFor(r => r.MedicoId, c => c.Random.Long())
                 .RuleFor(r => r.PacienteId, c => c.Random.Long())
                 .Generate();

            var toEntityConsultaViewModelFaker = new Faker<ToEntityConsultaViewModel>("pt_BR")
                .RuleFor(r => r.DataAgendamento, c => c.Date.Future())
                 .RuleFor(r => r.MedicoId, c => c.Random.Long())
                 .RuleFor(r => r.PacienteId, c => c.Random.Long())
                 .Generate();

            mockMapper.Setup(m => m.Map<Consulta>(It.IsAny<ToEntityConsultaViewModel>())).Returns(consultaFaker);
            mockConsultaRepository.Setup(m => m.Buscar(It.Is<Expression<Func<Consulta, bool>>>(y => y == criterio))).Returns(consulta);
            mockConsultaValidator.Setup(m => m.Validate(It.IsAny<Consulta>()).IsValid).Returns(true);

            // Act
            var result = consultasController.Post(toEntityConsultaViewModelFaker).Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
