using AE.HealthSystem.Services.Api;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AE.HealthSystem.Test.Api.IntegrationTests
{
    public class PacientesControllerTests : IClassFixture<Environment<Startup>>
    {
        private HttpClient _client;

        public PacientesControllerTests(Environment<Startup> environment)
        {
            _client = environment.Client;
        }

        [Theory]
        [InlineData("Carlos Silva")]
        [Trait("Paciente", "Testes Pacientes Controller")]
        public async Task PacientesController_ObterConsultasPorPaciente_RetornarComSucesso(string nome)
        {
            // Arrange
            var request = $"api/obter-consultas-paciente/{nome}";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();

        }

    }
}
