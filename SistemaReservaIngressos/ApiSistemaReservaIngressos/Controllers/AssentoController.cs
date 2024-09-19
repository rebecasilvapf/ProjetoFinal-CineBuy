using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaReservaIngressos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssentoController : ControllerBase
    {
        private readonly AssentoService _assentoService;

        public AssentoController(AssentoService assentoService)
        {
            _assentoService = assentoService;
        }

        //api/Assento/BuscarTodosAssentos
        [HttpGet("BuscarTodosAssentos")]
        public IActionResult BuscarTodosAssentos()
        {
            try
            {
                var todosAssentos = _assentoService.BuscarTodosAssentos();
                return Ok(todosAssentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //api/Assento/BuscarAssento/codigo
        [HttpGet("BuscarAssento/{codigo}")]
        public IActionResult BuscarAssento(int codigo)
        {
            try
            {
                var assento = _assentoService.BuscarAssento(codigo);
                if(assento == null)
                {
                    return NotFound();
                }
                return Ok(assento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        //api/Assento/AdicionarAssento
        [HttpPost("AdicionarAssento")]
        public IActionResult AdicionarAssento(AssentoRequest assentoRequest)
        {
            try
            {
                var novoAssento = _assentoService.AdicionarAssento(assentoRequest);
                return Ok("Total de linhas adicionadas " + novoAssento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //api/Assento/AlterarAssento/codigo
        [HttpPut("AlterarAssento/{codigo}")]
        public IActionResult AlterarAssento(int codigo, AssentoRequest assentoRequest)
        {
            try
            {
                var assentoAlterado = _assentoService.AlterarAssento(codigo, assentoRequest);
                return Ok("Total de linhas alteradas " + assentoAlterado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //api/Assento/ExcluirAssento/codigo
        [HttpDelete("ExcluirAssento/{codigo}")]
        public IActionResult ExcluirAssento(int codigo)
        {
            try
            {
                var assentoDeletado = _assentoService.ExcluirAssento(codigo);
                return Ok("Total de linhas deletadas " + assentoDeletado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
