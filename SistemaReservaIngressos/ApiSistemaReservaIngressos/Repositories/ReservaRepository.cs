using ApiSistemaReservaIngressos.Data;
using ApiSistemaReservaIngressos.Models;
using Dapper;
using System.Data.SqlClient;
using System.Globalization;

namespace ApiSistemaReservaIngressos.Repositories
{
    public class ReservaRepository
    {
        private readonly DbSession _dboSession;

        public ReservaRepository(DbSession dboSession)
        {
            _dboSession = dboSession;
        }

        public IEnumerable<Reservas> BuscarTodasReservas()
        {
            try
            {
                return _dboSession.Connection.Query<Reservas>("SELECT * FROM Reservas");
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar todas as reservas", ex);
            }
        }

        public Reservas BuscarReserva(int codigo)
        {
            try
            {
                return _dboSession.Connection.QuerySingle<Reservas>("SELECT * FROM Reservas WHERE ReservaId = @ReservaId", new { ReservaId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar a reserva", ex);
            }
        }

        public int AdicionarReserva(ReservaRequest request)
        {
            try
            {
                string sql = "INSERT INTO Reservas (HorarioId, Cliente, DataReserva, Confirmado)  OUTPUT INSERTED.ReservaId VALUES (@HorarioId, @Cliente, @DataReserva, @Confirmado)";

                var param = new
                {
                    HorarioId = request.HorarioId,
                    Cliente = request.Cliente,
                    DataReserva = request.DataReserva,
                    Confirmado = request.Confirmado
                };

                // Executa o SQL e retorna o ID da nova reserva
                return _dboSession.Connection.QuerySingle<int>(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao adicionar a reserva", ex);
            }
        }

        public int AlterarReserva(int codigo, ReservaRequest request)
        {
            try
            {
                string sql = "UPDATE Reservas SET HorarioId = @HorarioId, Cliente = @Cliente, DataReserva = @DataReserva, Confirmado = @Confirmado WHERE ReservaId = @ReservaId";

                var param = new
                {
                    ReservaId = codigo,
                    HorarioId = request.HorarioId,
                    Cliente = request.Cliente,
                    DataReserva = request.DataReserva,
                    Confirmado = request.Confirmado
                };

                return _dboSession.Connection.Execute(sql, param);
            }            
            catch (SqlException ex)
            {
                throw new Exception("Erro ao alterar a reserva", ex);
            }
        }

        public bool CancelarReserva(int codigo)
        {
            using(var transaction = _dboSession.Connection.BeginTransaction()) //Garatir a execução de todas as operações sejam executadas juntas
            {
                try
                {
                    //Recuperar todos os assentos relacionados a reserva
                    string apiUrlDetalhes = "SELECT AssentoId FROM DetalhesReserva WHERE ReservaId = @ReservaId";
                    var assentoIds = _dboSession.Connection.Query<int>(apiUrlDetalhes, new { ReservaId = codigo }, transaction).ToList();

                    if (assentoIds.Any())
                    {
                        // Atualizar a disponibilidade dos assentos para true
                        string apiUrlAssentos = "UPDATE Assentos SET Disponivel = 1 WHERE AssentoId IN @AssentoIds";
                        _dboSession.Connection.Execute(apiUrlAssentos, new { AssentoIds = assentoIds }, transaction);
                    }

                    //Atualizar o estado da reserva para false
                    var deleteDetalhesQuery = "UPDATE Reservas SET Confirmado = 0 WHERE ReservaId = @ReservaId";
                    _dboSession.Connection.Execute(deleteDetalhesQuery, new { ReservaId = codigo }, transaction);

                    // Confirmar a transação
                    transaction.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    // Reverter a transação em caso de erro
                    transaction.Rollback();
                    throw new Exception("Erro ao cancelar a reserva.", ex);
                }
            }         
        }
        public int ExcluirReserva(int codigo)
        {
            try
            {
                return _dboSession.Connection.Execute("DELETE FROM Reservas WHERE ReservaId = @ReservaId", new {ReservaId = codigo});
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao deletar a reserva", ex);
            }
        }
    }
}
