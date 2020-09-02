using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AE.HealthSystem.Infra.Data.Context;
using AE.HealthSystem.Infra.Data.Repository;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Aggregate;
using FluentValidation;

namespace AE.HealthSystem.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMapper, Mapper>();
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();

            services.AddScoped<IValidator<Consulta>, ConsultaValidator<Consulta>>();
            services.AddScoped<IValidator<Pessoa>, PessoaValidator<Pessoa>>();
            services.AddScoped<IValidator<Paciente>, PacienteValidator<Pessoa>>();
            services.AddScoped<IValidator<Medico>, MedicoValidator<Pessoa>>();

            services.AddScoped<HealthSystemContext>();
        }
    }
}
