using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Repositories;

namespace ApiSistemaReservaIngressos.Services
{
    public class ReservaService
    {
       private readonly ReservaRepository _reservaRepository;

       public ReservaService(ReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public IEnumerable<Reservas> BuscarTodasReservas()
        {
           return _reservaRepository.BuscarTodasReservas();
        }
        public Reservas BuscarReserva(int codigo)
        {
            return _reservaRepository.BuscarReserva(codigo);
        }
        public int AdicionarReserva(ReservaRequest reservaRequest)
        {
            if (reservaRequest.HorarioId <= 0)
            {
                throw new ArgumentException("O valor precisa ser maior que 0");
            }
            if (string.IsNullOrEmpty(reservaRequest.Cliente))
            {
                throw new ArgumentException("Campo cliente precisa ser preenchido");
            }
            if (reservaRequest.DataReserva == DateTime.MinValue)
            {
                throw new ArgumentException("O horário deve ser válido");
            }
            
            return _reservaRepository.AdicionarReserva(reservaRequest);
        }

        public int AlterarReserva(int codigo, ReservaRequest reservaRequest)
        {
            if (reservaRequest.HorarioId <= 0)
            {
                throw new ArgumentException("O valor precisa ser maior que 0");
            }
            if (string.IsNullOrEmpty(reservaRequest.Cliente))
            {
                throw new ArgumentException("Campo cliente precisa ser preenchido");
            }
            if (reservaRequest.DataReserva == DateTime.MinValue)
            {
                throw new ArgumentException("O horário deve ser válido");
            }

            return _reservaRepository.AlterarReserva(codigo, reservaRequest);
        }

        public bool CancelarReserva(int codigo)
        {
            return _reservaRepository.CancelarReserva(codigo);
        }

        public int ExcluiReserva(int codigo)
        {
            return _reservaRepository.ExcluirReserva(codigo);
        }
    }
}
