namespace ApiSistemaReservaIngressos.Models
{
    public class Horarios
    {
        public int HorarioId { get; set; }
        public int FilmeId { get; set; }
        public DateTime DataHora { get; set; }
        public string Sala { get; set; }
        public decimal Preco { get; set; }
    }
}
