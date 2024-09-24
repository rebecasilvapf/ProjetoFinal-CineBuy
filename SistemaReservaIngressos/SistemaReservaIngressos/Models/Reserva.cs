using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaReservaIngressos.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public int HorarioId { get; set; }
        public string Cliente { get; set; }
        public DateTime DataReserva { get; set; }
        public bool Confirmado { get; set; }
    }
}
