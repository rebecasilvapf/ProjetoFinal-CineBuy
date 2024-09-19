using ApiSistemaReservaIngressos.Data;
using ApiSistemaReservaIngressos.Models;
using Dapper;
using System.Data.SqlClient;

namespace ApiSistemaReservaIngressos.Repositories
{
    public class HorarioRepository
    {
        private readonly DbSession _dbSession;

        public HorarioRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public IEnumerable<Horarios> BuscarTodosHorarios()
        {
            try
            {
                return _dbSession.Connection.Query<Horarios>("SELECT * FROM Horarios");
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar todos os horários", ex);
            }
        }
        public IEnumerable<Horarios> BuscarHorariosFilme(int codigo)
        {
            try
            {
                return _dbSession.Connection.Query<Horarios>("SELECT * FROM Horarios WHERE FilmeId = @FilmeId", new { FilmeId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar todos os horários", ex);
            }
        }

        public Horarios BuscarHorario(int codigo)
        {
            try
            {
                return _dbSession.Connection.QuerySingle<Horarios>("SELECT * FROM Horarios WHERE HorarioId = @HorarioId", new {HorarioId = codigo});
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar o horário", ex);
            }
        }

        public int AdicionarHorario(HorarioRequest request)
        {
            try
            {
                string sql = "INSERT INTO Horarios(FilmeId, DataHora, Sala, Preco) VALUES (@FilmeId, @DataHora, @Sala, @Preco)";

                var param = new
                {
                    FilmeId = request.FilmeId,
                    DataHora = request.DataHora,
                    Sala = request.Sala,
                    Preco = request.Preco
                };

                return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao adicionar o horário", ex);
            }
        }

        public int AlterarHorario(int codigo, HorarioRequest request)
        {
            try
            {
                string sql = "UPDATE Horarios SET FilmeId = @FilmeId, DataHora = @DataHora, Sala = @Sala, Preco = @Preco WHERE HorarioId = @HorarioId";

                var param = new
                {
                    HorarioId = codigo,
                    FilmeId = request.FilmeId,
                    DataHora = request.DataHora,
                    Sala = request.Sala,
                    Preco = request.Preco
                };

                return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao alterar o horário", ex);
            }
        }

        public int ExcluirHorario(int codigo)
        {
            try
            {
                return _dbSession.Connection.Execute("DELETE FROM Horarios WHERE HorarioId = @HorarioId", new { HorarioId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao deletar o horário", ex);
            }
        }
     }   
}
