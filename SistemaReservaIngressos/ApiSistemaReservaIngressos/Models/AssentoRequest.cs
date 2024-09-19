namespace ApiSistemaReservaIngressos.Models
{
    public class AssentoRequest
    {
        public int HorarioId { get; set; }
        public char Fileira { get; set; }
        public int Numero { get; set; }
        public bool Disponivel { get; set; }
    }
}
