namespace ApiSistemaReservaIngressos.Models
{
    public class ReservaRequest
    {
        public int HorarioId { get; set; }
        public string Cliente { get; set; }
        public DateTime DataReserva { get; set; }
        public bool Confirmado { get; set; }
    }
}
