using ApiSistemaReservaIngressos.Data;
using ApiSistemaReservaIngressos.Models;
using Dapper;
using System.Data.SqlClient;

namespace ApiSistemaReservaIngressos.Repositories
{
    public class AssentoRepository
    {
        private readonly DbSession _dbSession;

        public AssentoRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public IEnumerable<Assentos> BuscarTodosAssentos()
        {
            try
            {
                return _dbSession.Connection.Query<Assentos>("SELECT * FROM Assentos");
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar todos os assentos", ex);
            }
        }     
        public Assentos BuscarAssento(int codigo)
        {
            try
            {
                return _dbSession.Connection.QuerySingle<Assentos>("SELECT * FROM Assentos WHERE AssentoId = @AssentoId", new { AssentoId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar o assento", ex);
            }
        }
        public IEnumerable<Assentos> BuscarAssentosPorHorario(int codigo)
        {
            try
            {
                return _dbSession.Connection.Query<Assentos>("SELECT * FROM Assentos WHERE HorarioId = @HorarioId", new { HorarioId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar os assentos referente ao horario", ex);
            }
        }

        public int AdicionarAssento(AssentoRequest request)
        {
            try
            {
                string sql = "INSERT INTO Assentos(HorarioId, Fileira , Numero , Disponivel) OUTPUT INSERTED.AssentoId VALUES (@HorarioId, @Fileira, @Numero, @Disponivel)";

                var param = new
                {
                    HorarioId = request.HorarioId,
                    Fileira = request.Fileira,
                    Numero = request.Numero,
                    Disponivel = request.Disponivel
                };
              
                return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao adicionar o assento", ex);
            }
        }

        public int AlterarAssento(int codigo, AssentoRequest request)
        {
            try
            {
                string sql = "UPDATE Assentos SET HorarioId = @HorarioId, Fileira = @Fileira, Numero = @Numero, Disponivel = @Disponivel WHERE AssentoId = @AssentoId";

                var param = new
                {
                    AssentoId = codigo,
                    HorarioId = request.HorarioId,
                    Fileira = request.Fileira,
                    Numero = request.Numero,
                    Disponivel = request.Disponivel
                };

                return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao alterar o assento", ex);
            }
        }

        public int AtualizarDisponibilidade(int codigo, bool disponivel)
        {
            try
            {
                string sql = "UPDATE Assentos SET Disponivel = @Disponivel WHERE AssentoId = @AssentoId";

                var param = new
                {
                    AssentoId = codigo,
                    Disponivel = disponivel
                };

                return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao alterar a disponibilidade do assento", ex);
            }
        }

        public int ExcluirAssento(int codigo)
        {
            try
            {
                return _dbSession.Connection.Execute("DELETE FROM Assentos WHERE AssentoId = @AssentoId", new { AssentoId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao deletar o assento", ex);
            }
        }
    }
}

