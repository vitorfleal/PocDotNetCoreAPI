using AE.HealthSystem.Services.Api;
using AE.HealthSystem.Services.Api.ViewModels.Consulta;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AE.HealthSystem.Test.Api.IntegrationTests
{
    public class ConsultasControllerTests : IClassFixture<Environment<Startup>>
    {
        private HttpClient _client;

        public ConsultasControllerTests(Environment<Startup> environment)
        {
            _client = environment.Client;
        }

        [Theory]
        [InlineData("Pedro Santos")]
        [Trait("Consulta", "Testes Consultas Controller")]
        public async Task ConsultasController_ObterConsultasPorMedico_RetornarComSucesso(string nome)
        {
            // Arrange
            var request = $"api/obter-consultas-medico/{nome}";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();

        }

        [Fact(DisplayName = "Agendar Consulta com sucesso")]
        [Trait("Consulta", "Testes Consultas Controller")]
        public async Task ConsultasController_AgendarConsulta_RetornarComSucesso()
        {
            // Arrange
            var request = new
            {
                Url = "api/agendar-consulta",
                Body = new ToEntityConsultaViewModel()
                {
                    DataAgendamento = DateTime.Now.AddDays(10),
                    MedicoId = 2,
                    PacienteId = 1
                }
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
