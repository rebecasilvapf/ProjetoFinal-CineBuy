namespace ApiSistemaReservaIngressos.Models
{
    public class HorarioRequest
    {
        public int FilmeId { get; set; }
        public DateTime DataHora { get; set; }
        public string Sala { get; set; }
        public decimal Preco { get; set; }
    }
}
