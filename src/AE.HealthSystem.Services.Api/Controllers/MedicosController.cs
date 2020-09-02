using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Services.Api.ViewModels.Medico;
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
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IValidator<Medico> _medicoValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public MedicosController(
            IMedicoRepository medicoRepository,
            IValidator<Medico> medicoValidator,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            ILogger logger)
        {
            _medicoValidator = medicoValidator;
            _medicoRepository = medicoRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [Route("obter-medico/{id}")]
        public async Task<IActionResult> ObterMedicoPorId([FromRoute] long id)
        {
            try
            {
                var medico = _mapper.Map<ListMedicoViewModel>(_medicoRepository.ObterPorId(id));

                _logger.Information("Iniciando o processo de consulta na base de dados.");

                if (medico == null)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Consulta para o médico id: {id}, não localizada na base de dados.");
                    return NotFound();
                }
                else
                {
                    _logger.Information($"Consulta retornada para o médico id: {id}.");

                    return Ok(medico);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao obter o médico por id. Ex: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("obter-medicos")]
        public async Task<IActionResult> ObterMedicos()
        {
            try
            {
                var medicos = _mapper.Map<IEnumerable<ListMedicoViewModel>>(_medicoRepository.ObterTodos());

                _logger.Information("Iniciando o processo de consulta na base de dados.");

                if (medicos.Count() == 0)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Não há médicos cadastrados na base de dados.");
                    return NotFound();
                }
                else
                {
                    _logger.Information($"Consulta retornada: Total de médicos: {medicos.Count()}.");

                    return Ok(medicos);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao obter o médico por id. Ex: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("cadastrar-medico")]
        public async Task<IActionResult> Post([FromBody] ToEntityMedicoViewModel medicoViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Parâmetros inválidos.");
                return StatusCode(400, "Parâmetros da chamada inválidos");
            }

            try
            {
                _logger.Information("Inciando o processo de cadastro do médico.");

                var medico = _mapper.Map<Medico>(medicoViewModel);

                if (!_medicoValidator.Validate(medico).IsValid)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Entidade inválida.");
                    return StatusCode(400, _medicoValidator.Validate(medico).Errors.ToList());
                }

                _medicoRepository.Adicionar(medico);

                return Ok(new { message = "Médico cadastrado com sucesso.", data = medico.Id });
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao cadastrar o médico: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("atualizar-medico")]
        public async Task<IActionResult> Put([FromBody] ToEntityMedicoViewModel medicoViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Parâmetros inválidos.");
                return StatusCode(400, "Parâmetros da chamada inválidos");
            }

            try
            {
                _logger.Information("Inciando o processo de atualização do médico.");

                var medico_obtido = _medicoRepository.ObterPorId(medicoViewModel.Id);

                if (medico_obtido == null)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Médico não localizado na base de dados.");
                    return StatusCode(404, "Médico inexistente");
                }

                var Medico = _mapper.Map<Medico>(medicoViewModel);

                if (!_medicoValidator.Validate(Medico).IsValid)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Entidade inválida.");
                    return StatusCode(400, _medicoValidator.Validate(Medico).Errors.ToList());
                }

                _medicoRepository.Atualizar(Medico);

                return Ok(new { message = "Médico atualizado com sucesso.", data = Medico });
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao atualizar o médico: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("excluir-medico/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                _logger.Information("Inciando o processo de exclusão do médico.");

                var medico_obtido = _medicoRepository.ObterPorId(id);

                if (medico_obtido == null)
                {
                    _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Médico não localizado na base de dados.");
                    return StatusCode(404, "Médico inexistente");
                }

                _medicoRepository.Remover(id);

                return Ok(new { message = "Médico excluído com sucesso.", data = medico_obtido });
            }
            catch (Exception ex)
            {
                _logger.Error($"Erro: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress} - Algo deu errado ao excluir o médico: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
