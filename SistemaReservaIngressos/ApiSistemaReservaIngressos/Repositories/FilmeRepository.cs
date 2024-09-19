using ApiSistemaReservaIngressos.Data;
using ApiSistemaReservaIngressos.Models;
using Dapper;
using System.Data.SqlClient;

namespace ApiSistemaReservaIngressos.Repositories
{
    public class FilmeRepository
    {
        //Repository: Responsável por interagir diretamente com a base de dados para operações como adicionar, obter, atualizar ou excluir.

        private readonly DbSession _dbSession;

        public FilmeRepository(DbSession dbSession)
        {
           _dbSession = dbSession;
        }

        public IEnumerable<Filmes> BuscarTodosFilmes()
        {
            try
            {
                return _dbSession.Connection.Query<Filmes>("SELECT * FROM Filmes");  //executa uma consulta SQL e mapeia os resultados para objetos
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar todos os filmes", ex);
            }           
        }
        public Filmes BuscarFilme(int codigo)
        {
            try
            {
                return _dbSession.Connection.QuerySingle<Filmes>("SELECT * FROM Filmes WHERE FilmeId = @FilmeId", new { FilmeId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar o filme", ex);
            }

        }

        public int AdicionarFilme(FilmeRequest request)
        {
            try { 
            string sql = "INSERT INTO Filmes (Titulo, Genero, ClassiEtaria, Duracao, Sinopse, ImageUrl) VALUES (@Titulo, @Genero, @ClassiEtaria, @Duracao, @Sinopse, @ImageUrl)";

            var param = new
            {
                Titulo = request.Titulo,
                Genero = request.Genero,
                ClassiEtaria = request.ClassiEtaria,
                Duracao = request.Duracao,
                Sinopse = request.Sinopse,
                ImagemUrl = request.ImageUrl
            };

            return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao adicionar o filme", ex);
            }
        }

        public int AlterarFilme(int codigo, FilmeRequest request)
        {
            try { 
            string sql = "UPDATE Filmes SET Titulo = @Titulo, Genero = @Genero, ClassiEtaria = @ClassiEtaria, Duracao = @Duracao, Sinopse = @Sinopse, ImageUrl = @ImageUrl WHERE FilmeId = @FilmeId ";

            var param = new
            {
                FilmeId = codigo,
                Titulo = request.Titulo,
                Genero = request.Genero,
                ClassiEtaria = request.ClassiEtaria,
                Duracao = request.Duracao,
                Sinopse = request.Sinopse,
                ImagemUrl = request.ImageUrl
            };

            return _dbSession.Connection.Execute(sql, param);
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao alterar o filme", ex);
            }
        }

        public int ExcluirFilme(int codigo)
        {
            try
            {
                return _dbSession.Connection.Execute("DELETE FROM Filmes WHERE FilmeId = @FilmeId", new { FilmeId = codigo });
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao deletar o filme", ex);
            }
        }
    }
}
