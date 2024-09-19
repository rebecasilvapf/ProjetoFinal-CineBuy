using ApiSistemaReservaIngressos.Data;
using ApiSistemaReservaIngressos.Models;
using Dapper;
using System.Data.SqlClient;

namespace ApiSistemaReservaIngressos.Repositories
{
    public class DetalhesReservaRepository
    {
        private readonly DbSession _dbSession;

        public DetalhesReservaRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public IEnumerable<DetalhesReserva> BuscarTodosDetalhesReserva()
        {
            try
            {
                return _dbSession.Connection.Query<DetalhesReserva>("SELECT * FROM DetalhesReserva");
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar todos os detalhes das reservas", ex);
            }      
        }
        public DetalhesReserva BuscarDetalheReserva(int codigo)
        {
            try
            {
                return _dbSession.Connection.QuerySingle<DetalhesReserva>("SELECT * FROM DetalhesReserva WHERE DetalheReservaId = @DetalheReservaId", new { DetalheReservaId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar detalhes da reserva", ex);
            }
        }
        public int AdicionarDetalhesReserva(DetalheReservaRequest request)
        {
            try
            {
                string sql = "INSERT INTO DetalhesReserva (ReservaId, AssentoId) VALUES (@ReservaId, @AssentoId)";

                var param = new
                {
                    ReservaId = request.ReservaId,
                    AssentoId = request.AssentoId,
                };

                return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao adicionar detalhes da reserva", ex);
            }
        }
        public int AlterarDetalhesDaReserva(int codigo, DetalheReservaRequest request)
        {
            try
            {
                string sql = "UPDATE DetalhesReserva SET ReservaId = @ReservaId, AssentoId = @AssentoId WHERE DetalheReservaId = @DetalheReservaId";

                var param = new
                {
                    DetalheReservaId = codigo,
                    ReservaId = request.ReservaId,
                    AssentoId = request.AssentoId   
                };

                return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao alterar os detalhes da reserva", ex);
            }
        }
        public int ExcluirDetalhesdaReserva(int codigo)
        {           
           return _dbSession.Connection.Execute("DELETE FROM DetalhesReserva WHERE DetalheReservaId = @DetalheReservaId", new { DetalheReservaId = codigo });          
        }
    }
}

