using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Domain.ValueObject;
using AE.HealthSystem.Services.Api.ViewModels.Consulta;
using AE.HealthSystem.Services.Api.ViewModels.Paciente;
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
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IConsultaRepository _consultaRepository;
        private readonly IValidator<Paciente> _pacienteValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PacientesController(
            IPacienteRepository pacienteRepository,
            IConsultaRepository consultaRepository,
            IValidator<Paciente> pacienteValidator,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            ILogger logger)
        {
            _pacienteValidator = pacienteValidator;
            _pacienteRepository = pacienteRepository;
            _consultaRepository = consultaRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("obter-consultas-paciente/{nome}")]
        public async Task<IActionResult> ObterConsultasPorPaciente([FromRoute] string nome)
        {
            try
            {
                var consulta = _mapper.Map<IEnumerable<ListConsultaViewModel>>(_consultaRepository.ObterConsultasPorNomePessoa(nome, Person.Paciente));

                _logger.Information("Iniciando o processo de consulta na base de dados.");

                if (consulta.Count() == 0)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Consulta para o paciente {nome}, não localizada na base de dados.");
                    return NotFound();
                }
                else
                {
                    _logger.Information($"Consulta retornada para o paciente: {nome}.");

                    return Ok(consulta.Select(c => new { Paciente = c.Paciente.Nome, Medico = c.Medico.Nome, c.DataAgendamento }));
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao obter a consulta por paciente: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("cadastrar-paciente")]
        public async Task<IActionResult> Post([FromBody] ToEntityPacienteViewModel pacienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Parâmetros inválidos.");
                return StatusCode(400, "Parâmetros da chamada inválidos");
            }

            try
            {
                _logger.Information("Inciando o processo de cadastro do paciente.");

                var paciente = _mapper.Map<Paciente>(pacienteViewModel);

                if (!_pacienteValidator.Validate(paciente).IsValid)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Entidade inválida.");
                    return StatusCode(400, _pacienteValidator.Validate(paciente).Errors.ToList());
                }

                _pacienteRepository.Adicionar(paciente);

                return Ok(new { message = "Paciente cadastrado com sucesso.", data = paciente.Id });
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao cadastrar o paciente: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("atualizar-paciente")]
        public async Task<IActionResult> Put([FromBody] ToEntityPacienteViewModel pacienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Parâmetros inválidos.");
                return StatusCode(400, "Parâmetros da chamada inválidos");
            }

            try
            {
                _logger.Information("Inciando o processo de atualização do paciente.");

                var paciente_obtido = _pacienteRepository.ObterPorId(pacienteViewModel.Id);

                if (paciente_obtido == null)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Paciente não localizado na base de dados.");
                    return StatusCode(404, "Paciente inválido");
                }

                var paciente = _mapper.Map<Paciente>(pacienteViewModel);

                if (!_pacienteValidator.Validate(paciente).IsValid)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Entidade inválida.");
                    return StatusCode(400, _pacienteValidator.Validate(paciente).Errors.ToList());
                }

                _pacienteRepository.Atualizar(paciente);

                return Ok(new { message = "Paciente atualizado com sucesso.", data = paciente });
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao atualizar o paciente: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("excluir-paciente/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                _logger.Information("Inciando o processo de exclusão do paciente.");

                var paciente_obtido = _pacienteRepository.ObterPorId(id);

                if (paciente_obtido == null)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Paciente não localizado na base de dados.");
                    return StatusCode(404, "Paciente inválido");
                }

                _pacienteRepository.Remover(id);

                return Ok(new { message = "Paciente excluído com sucesso.", data = paciente_obtido });
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao excluir o paciente: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
