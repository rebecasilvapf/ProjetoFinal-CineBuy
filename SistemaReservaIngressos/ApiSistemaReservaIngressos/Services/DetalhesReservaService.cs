using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Repositories;

namespace ApiSistemaReservaIngressos.Services
{
    public class DetalhesReservaService
    {
        private readonly DetalhesReservaRepository _detalhesReservaRepository;

        public DetalhesReservaService(DetalhesReservaRepository detalhesReservaRepository)
        {
            _detalhesReservaRepository = detalhesReservaRepository;
        }

        public IEnumerable<DetalhesReserva> BuscarTodosDetalhesReserva()
        {
            return _detalhesReservaRepository.BuscarTodosDetalhesReserva();
        }
        public DetalhesReserva BuscarDetalheReserva(int codigo)
        {
            return _detalhesReservaRepository.BuscarDetalheReserva(codigo);
        }
        public int AdicionarDetalhesReserva(DetalheReservaRequest detalhesReservaRequest)
        {
            if(detalhesReservaRequest.ReservaId <= 0 || detalhesReservaRequest.AssentoId <=0)
            {
                throw new ArgumentException("Campo precisa ter o valor maior que 0");
            }
            return _detalhesReservaRepository.AdicionarDetalhesReserva(detalhesReservaRequest);
        }
        public int AlterarDetalhesDaReserva(int codigo, DetalheReservaRequest detalhesReservaRequest)
        {
            if (detalhesReservaRequest.ReservaId <= 0 || detalhesReservaRequest.AssentoId <= 0)
            {
                throw new ArgumentException("Campo precisa ter o valor maior que 0");
            }
            return _detalhesReservaRepository.AlterarDetalhesDaReserva(codigo, detalhesReservaRequest);
        }
        public int ExcluirDetalhesdaReserva(int codigo)
        {
            try
            {
                return _detalhesReservaRepository.ExcluirDetalhesdaReserva(codigo);
            }
            catch (Exception ex)
            {
                // Logar exceção ou manipular erro de outra forma
                throw new ApplicationException("Erro ao buscar todos os detalhes das reservas", ex);
            }


        }
    }
}
