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
                string sql = "INSERT INTO Reservas (HorarioId, Cliente, DataReserva, Confirmado) VALUES (@HorarioId, @Cliente, @DataReserva, @Confirmado)";

                var param = new
                {
                    HorarioId = request.HorarioId,
                    Cliente = request.Cliente,
                    DataReserva = request.DataReserva,
                    Confirmado = request.Confirmado
                };

                return _dboSession.Connection.Execute(sql, param);
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
