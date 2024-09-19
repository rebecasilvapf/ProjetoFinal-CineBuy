using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaReservaIngressos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        //api/Filme/BuscarTodosFilmes
        [HttpGet("BuscarTodosFilmes")]
        public IActionResult BuscarTodosFilmes()
        {
            try
            {
                var todosFilmes = _filmeService.BuscarTodosFilmes();
                return Ok(todosFilmes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //api/Filme/BuscarFilme/codigo
        [HttpGet("BuscarFilme/{codigo}")]
        public IActionResult BuscarFilme(int codigo)
        {
            try
            {
                var filme = _filmeService.BuscarFilme(codigo);
                if (filme == null)
                {
                    return NotFound(); 
                }
                return Ok(filme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        //api/Filme/AdicionarFilme
        [HttpPost("AdicionarFilme")]
        public IActionResult AdicionarFilme(FilmeRequest filmeRequest)
        {
            try
            {
                var novoFilme = _filmeService.AdicionarFilme(filmeRequest);
                return Ok("Total de linhas adicionadas " + novoFilme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //api/Filme/AlterarFilme/codigo
        [HttpPut("AlterarFilme/{codigo}")]
        public IActionResult AlterarFilme(int codigo,FilmeRequest filmeRequest)
        {
            try
            {
                var filmeAlterado = _filmeService.AlterarFilme(codigo,filmeRequest);
                return Ok("Total de linhas alteradas " + filmeAlterado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //api/Filme/ExcluirFilme/codigo
        [HttpDelete("ExcluirFilme/{codigo}")]
        public IActionResult ExcluirFilme(int codigo)
        {
            try
            {
                var filmeDeletado = _filmeService.ExcluirFilme(codigo);
                return Ok("Total de linhas deletadas " + filmeDeletado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
