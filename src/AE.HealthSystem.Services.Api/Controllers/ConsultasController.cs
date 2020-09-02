using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Domain.ValueObject;
using AE.HealthSystem.Services.Api.ViewModels.Consulta;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AE.HealthSystem.Services.Api.Controllers
{
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IValidator<Consulta> _consultaValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ConsultasController(
            IConsultaRepository consultaRepository,
            IValidator<Consulta> consultaValidator,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            ILogger logger)
        {
            _consultaValidator = consultaValidator;
            _consultaRepository = consultaRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("obter-consultas-medico/{nome}")]
        public async Task<IActionResult> ObterConsultasPorMedico([FromRoute] string nome)
        {
            try
            {
                var consulta = _mapper.Map<IEnumerable<ListConsultaViewModel>>(_consultaRepository.ObterConsultasPorNomePessoa(nome, Person.Medico));

                _logger.Information("Iniciando o processo de consulta na base de dados.");

                if (consulta.Count() == 0)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Consulta para o médico {nome}, não localizada na base de dados.");
                    return NotFound();
                }
                else
                {
                    _logger.Information($"Consulta retornada para o médico: {nome}.");

                    return Ok(consulta.Select(c => new { Medico = c.Medico.Nome, Paciente = c.Paciente.Nome, c.DataAgendamento }));
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao obter a Consulta por Médico: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("agendar-consulta")]
        public async Task<IActionResult> Post([FromBody] ToEntityConsultaViewModel consultaViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Parâmetros inválidos.");
                return StatusCode(400, "Parâmetros da chamada inválidos");
            }

            try
            {
                _logger.Information("Inciando o processo de agendamento da consulta.");

                var consulta = _mapper.Map<Consulta>(consultaViewModel);

                var consulta_indisponivel = _consultaRepository.Buscar(c => c.MedicoId == consulta.MedicoId && c.DataAgendamento == consulta.DataAgendamento).Any();

                if (consulta_indisponivel)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Indisponibilidade para o agendamento da consulta.");
                    return StatusCode(406, "Data e Horário indisponíveis para a agendamento.");
                }

                if (!_consultaValidator.Validate(consulta).IsValid)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Entidade inválida.");
                    return StatusCode(400, _consultaValidator.Validate(consulta).Errors.ToList());
                }

                _consultaRepository.Adicionar(consulta);

                return Ok(new { message = "Consulta agendada com sucesso.", data = consulta.Id });
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao agendar a consulta: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
