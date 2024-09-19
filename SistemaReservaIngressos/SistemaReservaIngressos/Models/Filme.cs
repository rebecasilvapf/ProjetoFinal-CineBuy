using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReservaIngressos.Models
{
    public class Filme
    {
        public int FilmeId { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string ClassiEtaria { get; set; }
        public int Duracao { get; set; }
        public string Sinopse { get; set; }
        public string ImageUrl { get; set; }

    }
}