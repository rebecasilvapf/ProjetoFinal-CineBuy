using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaReservaIngressos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservaController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        //api/Reserva/BuscarTodasReservas
        [HttpGet("BuscarTodasReservas")]
        public IActionResult BuscarTodasReservas()
        {
            try
            {
                var todosReservas = _reservaService.BuscarTodasReservas();
                return Ok(todosReservas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //api/Reserva/BuscarReserva/codigo
        [HttpGet("BuscarReserva/{codigo}")]
        public IActionResult BuscarReserva(int codigo)
        {
            try
            {
                var reserva = _reservaService.BuscarReserva(codigo);
                if (reserva == null)
                {
                    return NotFound();
                }
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        //api/Reserva/AdicionarReserva
        [HttpPost("AdicionarReserva")]
        public IActionResult AdicionarReserva(ReservaRequest reservaRequest)
        {
            try
            {
                var novaReserva = _reservaService.AdicionarReserva(reservaRequest);
                return Ok("Id da nova reserva: " + novaReserva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //api/Reserva/AlterarReserva/codigo
        [HttpPut("AlterarReserva/{codigo}")]
        public IActionResult AlterarReserva(int codigo, ReservaRequest reservaRequest)
        {
            try
            {
                var reservaAlterada = _reservaService.AlterarReserva(codigo, reservaRequest);
                return Ok("Total de linhas alteradas " + reservaAlterada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //api/Reserva/CancelarReserva/codigo
        [HttpPut("CancelarReserva/{codigo}")]
        public IActionResult CancelarReserva(int codigo)
        {
            try
            {
                var reservaAlterada = _reservaService.CancelarReserva(codigo);
                return Ok("Resultado = " + reservaAlterada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //api/Reserva/ExcluirReserva/codigo
        [HttpDelete("ExcluirReserva/{codigo}")]
        public IActionResult ExcluirReserva(int codigo)
        {
            try
            {
                var reservaDeletada = _reservaService.ExcluiReserva(codigo);
                return Ok("Total de linhas deletadas " + reservaDeletada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
