using ApiSistemaReservaIngressos.Models;
using ApiSistemaReservaIngressos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaReservaIngressos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly HorarioService _horarioService;

        public HorarioController(HorarioService horarioService)
        {
            _horarioService = horarioService;
        }


        //api/Horario/BuscarTodosHorarios
        [HttpGet("BuscarTodosHorarios")]
        public IActionResult BuscarTodosHorarios()
        {
            try
            {
                var todosHorarios = _horarioService.BuscarTodosHorarios();
                return Ok(todosHorarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //api/Horario/BuscarHorariosFilme/codigo
        [HttpGet("BuscarHorariosFilme/{codigo}")]
        public IActionResult BuscarHorariosFilme(int codigo)
        {
            try
            {
                var horariosFilme = _horarioService.BuscarHorariosFilme(codigo);
                if (horariosFilme == null)
                {
                    return NotFound();
                }
                return Ok(horariosFilme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //api/Horario/BuscarHorario/codigo
        [HttpGet("BuscarHorario/{codigo}")]
        public IActionResult BuscarHorario(int codigo)
        {
            try
            {
                var horario = _horarioService.BuscarHorario(codigo);
                if (horario == null)
                {
                    return NotFound();
                }
                return Ok(horario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        //api/Horario/AdicionarHorario
        [HttpPost("AdicionarHorario")]
        public IActionResult AdicionarHorario(HorarioRequest horarioRequest)
        {
            try
            {
                var novoHorario = _horarioService.AdicionarHorario(horarioRequest);
                return Ok("Total de linhas adicionadas " + novoHorario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //api/Filme/AlterarHorario/codigo
        [HttpPut("AlterarHorario/{codigo}")]
        public IActionResult AlterarHorario(int codigo, HorarioRequest horarioRequest)
        {
            try
            {
                var horarioAlterado = _horarioService.AlterarHorario(codigo, horarioRequest);
                return Ok("Total de linhas alteradas " + horarioAlterado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //api/Filme/ExcluirHorario/codigo
        [HttpDelete("ExcluirHorario/{codigo}")]
        public IActionResult ExcluirHorario(int codigo)
        {
            try
            {
                var horarioDeletado = _horarioService.ExcluirFilme(codigo);
                return Ok("Total de linhas deletadas " + horarioDeletado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
