using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReservaIngressos.Models
{
    public class Horario
    {
        public int HorarioId { get; set; }
        public int FilmeId { get; set; }
        public DateTime DataHora { get; set; }
        public string Sala { get; set; }
        public decimal Preco { get; set; }
    }
}