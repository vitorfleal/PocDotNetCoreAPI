using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Domain.ValueObject;
using AE.HealthSystem.Infra.Data.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace AE.HealthSystem.Infra.Data.Repository
{
    public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(HealthSystemContext context) : base(context) { }

        public IEnumerable<Consulta> ObterConsultasPorNomePessoa(string nome, Person person)
        {
            var sql = "SELECT C.Id, C.DataAgendamento, C.MedicoId, C.PacienteId, M.Id, M.Nome, P.Id, P.Nome " +
                       "FROM Consulta C " +
                       "INNER JOIN Medico M " +
                       "ON C.MedicoId = M.Id " +
                       "INNER JOIN Paciente P " +
                       "ON C.PacienteId = P.Id ";

            sql += person == Person.Medico ? "WHERE M.Nome LIKE @psnNome" : "WHERE P.Nome LIKE @psnNome";


            return Db.Database.GetDbConnection().Query<Consulta, Medico, Paciente, Consulta>(sql,
                (c, m, p) =>
                {
                    c.Medico = m;
                    c.Paciente = p;
                    return c;
                }
                , new { psnNome = $"%{nome}%" }
                , commandType: CommandType.Text
                , splitOn: "Id, Id, Id");

        }
    }
}
