using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReservaIngressos.Models
{
    public class Assentos
    {
        public int AssentoId { get; set; }
        public int HorarioId { get; set; }
        public char Fileira { get; set; }
        public int Numero { get; set; }
        public bool Disponivel { get; set; }
    }
}