using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Services.Api.Controllers;
using AE.HealthSystem.Services.Api.ViewModels.Consulta;
using AE.HealthSystem.Services.Api.ViewModels.Paciente;
using AutoMapper;
using Bogus;
using Bogus.DataSets;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AE.HealthSystem.Test.Api.UnitTests
{
    public class PacientesControllerTests
    {
        public PacientesController pacientesController { get; set; }
        public Mock<IPacienteRepository> mockPacienteRepository { get; set; }
        public Mock<IConsultaRepository> mockConsultaRepository { get; set; }
        public Mock<IValidator<Paciente>> mockPacienteValidator { get; set; }
        public Mock<IHttpContextAccessor> mockHttpContextAccessor { get; set; }
        public Mock<IMapper> mockMapper { get; set; }
        public Mock<ILogger> mockLogger { get; set; }

        public PacientesControllerTests()
        {
            mockPacienteRepository = new Mock<IPacienteRepository>();
            mockConsultaRepository = new Mock<IConsultaRepository>();
            mockPacienteValidator = new Mock<IValidator<Paciente>>();
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger>();

            pacientesController = new PacientesController(
                 mockPacienteRepository.Object,
                 mockConsultaRepository.Object,
                 mockPacienteValidator.Object,
                 mockHttpContextAccessor.Object,
                 mockMapper.Object,
                 mockLogger.Object
                 );
        }

        [Fact(DisplayName = "Obter Consultas Por Paciente com sucesso")]
        [Trait("Paciente", "Testes Pacientes Controller")]
        public void PacientesController_ObterConsultasPorPaciente_RetornarComSucesso()
        {
            const string nome = "Carlos Silva";

            var listConsultaViewModelFaker = new Faker<ListConsultaViewModel>("pt_BR")
                .RuleFor(r => r.Id, c => c.Random.Long())
                 .RuleFor(r => r.DataAgendamento, c => c.Date.Recent(100))
                 .RuleFor(r => r.MedicoId, c => c.Random.Long())
                 .RuleFor(r => r.PacienteId, c => c.Random.Long())
                 .Generate(5);

            mockMapper.Setup(m => m.Map<IEnumerable<ListConsultaViewModel>>(It.IsAny<IEnumerable<Consulta>>())).Returns(listConsultaViewModelFaker.AsEnumerable());

            var result = pacientesController.ObterConsultasPorPaciente(nome).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Cadastrar Paciente com sucesso")]
        [Trait("Paciente", "Testes Pacientes Controller")]
        public void PacientesController_CadastrarPaciente_RetornarComSucesso()
        {
            var pacienteFaker = new Faker<Paciente>("pt_BR")
                .RuleFor(r => r.Nome, c => c.Name.FullName(Name.Gender.Male))
                 .RuleFor(r => r.Enfermidade, c => c.Name.FirstName())
                 .Generate();

            var toEntityPacienteViewModelFaker = new Faker<ToEntityPacienteViewModel>("pt_BR")
                .RuleFor(r => r.Nome, c => c.Name.FullName(Name.Gender.Male))
                 .RuleFor(r => r.Enfermidade, c => c.Name.FirstName())
                 .Generate();

            mockMapper.Setup(m => m.Map<Paciente>(It.IsAny<ToEntityPacienteViewModel>())).Returns(pacienteFaker);
            mockPacienteValidator.Setup(m => m.Validate(It.IsAny<Paciente>()).IsValid).Returns(true);

            var result = pacientesController.Post(toEntityPacienteViewModelFaker).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Atualizar Paciente com sucesso")]
        [Trait("Paciente", "Testes Pacientes Controller")]
        public void PacientesController_AtualizarPaciente_RetornarComSucesso()
        {
            var pacienteFaker = new Faker<Paciente>("pt_BR")
                .RuleFor(r => r.Id, c => c.Random.Long())
                .RuleFor(r => r.Nome, c => c.Name.FullName(Name.Gender.Male))
                 .RuleFor(r => r.Enfermidade, c => c.Name.FirstName())
                 .Generate();

            var toEntityPacienteViewModelFaker = new Faker<ToEntityPacienteViewModel>("pt_BR")
                .RuleFor(r => r.Id, c => c.Random.Long())
                .RuleFor(r => r.Nome, c => c.Name.FullName(Name.Gender.Male))
                 .RuleFor(r => r.Enfermidade, c => c.Name.FirstName())
                 .Generate();

            mockPacienteRepository.Setup(m => m.ObterPorId(It.IsAny<long>())).Returns(pacienteFaker);
            mockMapper.Setup(m => m.Map<Paciente>(It.IsAny<ToEntityPacienteViewModel>())).Returns(pacienteFaker);
            mockPacienteValidator.Setup(m => m.Validate(It.IsAny<Paciente>()).IsValid).Returns(true);

            var result = pacientesController.Put(toEntityPacienteViewModelFaker).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Excluir Paciente com sucesso")]
        [Trait("Paciente", "Testes Pacientes Controller")]
        public void PacientesController_ExcluirPaciente_RetornarComSucesso()
        {
            const long id = 1;

            var pacienteFaker = new Faker<Paciente>("pt_BR")
                .RuleFor(r => r.Id, c => c.Random.Long())
                .RuleFor(r => r.Nome, c => c.Name.FullName(Name.Gender.Male))
                 .RuleFor(r => r.Enfermidade, c => c.Name.FirstName())
                 .Generate();

            mockPacienteRepository.Setup(m => m.ObterPorId(It.IsAny<long>())).Returns(pacienteFaker);

            var result = pacientesController.Delete(id).Result;

            Assert.IsType<OkObjectResult>(result);
        }

    }
}
