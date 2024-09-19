namespace ApiSistemaReservaIngressos.Models
{
    public class Reservas
    {
        public int ReservaId { get; set; }
        public int HorarioId { get; set; }
        public string Cliente { get; set; }
        public DateTime DataReserva { get; set; }
        public bool Confirmado { get; set; }
    }
}
