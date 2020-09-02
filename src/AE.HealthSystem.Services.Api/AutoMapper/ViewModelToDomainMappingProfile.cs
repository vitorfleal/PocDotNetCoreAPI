using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Services.Api.ViewModels.Consulta;
using AE.HealthSystem.Services.Api.ViewModels.Medico;
using AE.HealthSystem.Services.Api.ViewModels.Paciente;
using AutoMapper;

namespace AE.HealthSystem.Services.Api.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ListConsultaViewModel, Consulta>();
            CreateMap<ToEntityConsultaViewModel, Consulta>();
            CreateMap<ListMedicoViewModel, Medico>();
            CreateMap<ToEntityMedicoViewModel, Medico>();
            CreateMap<ListPacienteViewModel, Paciente>();
            CreateMap<ToEntityPacienteViewModel, Paciente>();
        }
    }
}