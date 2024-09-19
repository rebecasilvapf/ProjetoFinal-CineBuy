using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaReservaIngressos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalhesReservaController : ControllerBase
    {
        private readonly DetalhesReservaService _detalhesReservaService;

        public DetalhesReservaController(DetalhesReservaService detalhesReservaService)
        {
            _detalhesReservaService = detalhesReservaService;
        }

        //api/DetalhesReserva/BuscarTodosDetalhesReserva
        [HttpGet("BuscarTodosDetalhesReserva")]
        public IActionResult BuscarTodosDetalhesReserva()
        {
            try
            {
                var todosDetalhes = _detalhesReservaService.BuscarTodosDetalhesReserva();              
                return Ok(todosDetalhes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //api/DetalhesReserva/BuscarDetalheReserva/codigo
        [HttpGet("BuscarDetalheReserva/{codigo}")]
        public IActionResult BuscarDetalheReserva(int codigo)
        {
            try
            {
                var detalheReserva = _detalhesReservaService.BuscarDetalheReserva(codigo);
                if (detalheReserva == null)
                {
                    return NotFound();
                }
                return Ok(detalheReserva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        //api/DetalhesReserva/AdicionarDetalhesReserva
        [HttpPost("AdicionarDetalhesReserva")]
        public IActionResult AdicionarDetalhesReserva(DetalheReservaRequest detalhesReservaRequest)
        {
            try
            {
                var detalheReserva = _detalhesReservaService.AdicionarDetalhesReserva(detalhesReservaRequest);
                return Ok("Total de linhas adicionadas " + detalheReserva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //api/DetalhesReserva/AlterarDetalhesDaReserva/codigo
        [HttpPut("AlterarReserva/{codigo}")]
        public IActionResult AlterarDetalhesDaReserva(int codigo, DetalheReservaRequest detalhesReservaRequest)
        {
            try
            {
                var detalheAlterado = _detalhesReservaService.AlterarDetalhesDaReserva(codigo, detalhesReservaRequest);
                return Ok("Total de linhas alteradas " + detalheAlterado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //api/DetalhesReserva/ExcluirDetalhesdaReserva/codigo
        [HttpDelete("ExcluirDetalhesdaReserva/{codigo}")]
        public IActionResult ExcluirDetalhesdaReserva(int codigo)
        {
            try
            {
                var excluirDetalhe = _detalhesReservaService.ExcluirDetalhesdaReserva(codigo);
                return Ok("Total de linhas deletadas " + excluirDetalhe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

