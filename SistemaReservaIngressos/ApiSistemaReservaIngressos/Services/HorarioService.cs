using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Repositories;

namespace ApiSistemaReservaIngressos.Services
{
    public class HorarioService
    {
        private readonly HorarioRepository _horarioRepository;

        public HorarioService(HorarioRepository repository)
        {
            _horarioRepository = repository;
        }

        public IEnumerable<Horarios> BuscarTodosHorarios()
        {
            return _horarioRepository.BuscarTodosHorarios();
        }

        public IEnumerable<Horarios> BuscarHorariosFilme(int codigo)
        {
            return _horarioRepository.BuscarHorariosFilme(codigo);
        }

        public Horarios BuscarHorario(int codigo)
        {
            return _horarioRepository.BuscarHorario(codigo);
        }

        public int AdicionarHorario(HorarioRequest horarioRequest)
        {
            if (horarioRequest.FilmeId <= 0 || horarioRequest.Preco <= 0)
            {
                throw new ArgumentException("Valor precisa ser maior que 0");
            }
            if (string.IsNullOrEmpty(horarioRequest.Sala))
            {
                throw new ArgumentException("Campo sala precisa ser preenchido");
            }
            if (horarioRequest.DataHora <= DateTime.Now)
            {
                throw new ArgumentException("A data e hora devem ser posteriores ao momento atual.");
            }
            return _horarioRepository.AdicionarHorario(horarioRequest);
        }

        public int AlterarHorario(int codigo, HorarioRequest horarioRequest)
        {
            if (horarioRequest.FilmeId <= 0 || horarioRequest.Preco <= 0)
            {
                throw new ArgumentException("Valor precisa ser maior que 0");
            }
            if (string.IsNullOrEmpty(horarioRequest.Sala))
            {
                throw new ArgumentException("Campo sala precisa ser preenchido");
            }
            if (horarioRequest.DataHora <= DateTime.Now)
            {
                throw new ArgumentException("A data e hora devem ser posteriores ao momento atual.");
            }
            return _horarioRepository.AlterarHorario(codigo, horarioRequest);
        }
        public int ExcluirFilme(int codigo)
        {
            return _horarioRepository.ExcluirHorario(codigo);
        }
    }
}
