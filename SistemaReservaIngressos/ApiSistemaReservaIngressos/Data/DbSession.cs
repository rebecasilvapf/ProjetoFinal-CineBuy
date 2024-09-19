using System.Data.SqlClient;
using System.Data;

namespace ApiSistemaReservaIngressos.Data
{
    public class DbSession
    {
        public IDbConnection Connection { get; }
        public DbSession(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("conexao") ?? throw new ArgumentNullException("ConnectionString não possui um valor válido");

            try
            {
                Connection = new SqlConnection(connectionString);

                Connection.Open();
            }
            catch (SqlException ex)
            {

                throw new Exception("Erro ao abrir a conexão com o banco de dados.", ex);

            }
        }
        public void Dispose() => Connection?.Dispose();
    }
}
