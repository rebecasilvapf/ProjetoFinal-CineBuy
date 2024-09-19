namespace ApiSistemaReservaIngressos.Models
{
    public class FilmeRequest
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string ClassiEtaria { get; set; }
        public int Duracao { get; set; }
        public string Sinopse { get; set; }
        public string ImageUrl { get; set; }
    }
}
