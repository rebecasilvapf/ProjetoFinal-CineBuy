using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Repositories;

namespace ApiSistemaReservaIngressos.Services
{
    public class AssentoService
    {
        private readonly AssentoRepository _assentoRepository;

        public AssentoService(AssentoRepository assentoRepository)
        {
            _assentoRepository = assentoRepository;
        }

        public IEnumerable<Assentos> BuscarTodosAssentos()
        {
            return _assentoRepository.BuscarTodosAssentos();
        }
        public Assentos BuscarAssento(int codigo)
        {
            return _assentoRepository.BuscarAssento(codigo);
        }

        public int AdicionarAssento(AssentoRequest assentoRequest)
        {
            if (assentoRequest.HorarioId <= 0 || assentoRequest.Numero <=0)
            {
                throw new ArgumentException("O valor precisa ser maior que 0");
            }
            if (assentoRequest.Fileira == '\0' || !char.IsLetter(assentoRequest.Fileira))
            {
                throw new ArgumentException("A fileira deve ser uma letra e não pode estar vazia");
            }                   

            return _assentoRepository.AdicionarAssento(assentoRequest);
        }

        public int AlterarAssento(int codigo, AssentoRequest assentoRequest)
        {
            if (assentoRequest.HorarioId <= 0 || assentoRequest.Numero <= 0)
            {
                throw new ArgumentException("O valor precisa ser maior que 0");
            }
            if (assentoRequest.Fileira == '\0' || !char.IsLetter(assentoRequest.Fileira))
            {
                throw new ArgumentException("A fileira deve ser uma letra e não pode estar vazia");
            }

            return _assentoRepository.AlterarAssento(codigo, assentoRequest);
        }

        public int ExcluirAssento(int codigo)
        {
            return _assentoRepository.ExcluirAssento(codigo);
        }

    }
}
