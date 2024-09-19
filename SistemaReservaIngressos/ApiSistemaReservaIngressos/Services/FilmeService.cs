using ApiSistemaReservaIngressos.Data;
using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Repositories;

namespace ApiSistemaReservaIngressos.Services
{
    public class FilmeService
    {
        //Service = Serviço que usa o repositório para implementar a lógica de negócios relacionada a reservas.

        private readonly FilmeRepository _filmeRepository;

        public FilmeService(FilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public IEnumerable<Filmes>BuscarTodosFilmes()
        {
            return _filmeRepository.BuscarTodosFilmes();
        }
        public Filmes BuscarFilme(int codigo)
        {
            return _filmeRepository.BuscarFilme(codigo);
        }

        public int AdicionarFilme(FilmeRequest filmeRequest)
        {
            if (string.IsNullOrEmpty(filmeRequest.Titulo) ||
                string.IsNullOrEmpty(filmeRequest.Genero) ||
                string.IsNullOrEmpty(filmeRequest.ClassiEtaria) ||
                string.IsNullOrEmpty(filmeRequest.Sinopse) ||
                string.IsNullOrEmpty(filmeRequest.ImageUrl)) 

            {
                throw new ArgumentException("Todos os campos precisam ser preenchidos");
            }
            if (filmeRequest.Duracao <= 0)
            {
                throw new ArgumentException("Duração do filme deve ser maior que 0 minutos");
            }

            return _filmeRepository.AdicionarFilme(filmeRequest);
        }

        public int AlterarFilme(int codigo, FilmeRequest filmeRequest)
        {
            if (string.IsNullOrEmpty(filmeRequest.Titulo) ||
                string.IsNullOrEmpty(filmeRequest.Genero) ||
                string.IsNullOrEmpty(filmeRequest.ClassiEtaria) ||
                string.IsNullOrEmpty(filmeRequest.Sinopse) ||
                string.IsNullOrEmpty(filmeRequest.ImageUrl))
            {
                throw new ArgumentException("Todos os campos precisam ser preenchidos");
            }
            if (filmeRequest.Duracao <= 0)
            {
                throw new ArgumentException("Duração do filme deve ser maior que 0 minutos");
            }

            return _filmeRepository.AlterarFilme(codigo, filmeRequest);
        }

        public int ExcluirFilme(int codigo)
        {
            return _filmeRepository.ExcluirFilme(codigo);
        }

    }
}
