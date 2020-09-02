using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Services.Api.ViewModels.Consulta;
using AE.HealthSystem.Services.Api.ViewModels.Medico;
using AE.HealthSystem.Services.Api.ViewModels.Paciente;
using AutoMapper;

namespace AE.HealthSystem.Services.Api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Consulta, ListConsultaViewModel>();
            CreateMap<Medico, ListMedicoViewModel>();
            CreateMap<Paciente, ListPacienteViewModel>();
        }
    }
}
