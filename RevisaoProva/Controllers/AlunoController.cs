using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevisaoProva.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RevisaoProva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private RevisaoContext _context;

        public AlunoController(RevisaoContext context)
        {
            _context = context;
        }

        // GET: api/<AlunoController>
        [HttpGet("BuscarTodos")]
        public async Task<IActionResult> BuscarTodos()
        {
            var result = await _context.Alunos.ToListAsync();
            var resultApplication = new ResultApplication();
            resultApplication.Success = true;
            resultApplication.Dados = result;
            return Ok(resultApplication);
        }

        // GET api/<AlunoController>/5
        [HttpGet("Buscar({id})")]
        public async Task<IActionResult> Buscar(int id)
        {
            var result = await _context.Alunos.FindAsync(id);
            var resultApplication = new ResultApplication();

            if (result == null) {
                resultApplication.Message = "Registro não encontrado!";
                return BadRequest(resultApplication);
            }

            resultApplication.Success = true; 
            resultApplication.Aluno = result;
            return Ok(resultApplication);
        }

        // POST api/<AlunoController>
        [HttpPost("Inserir")]
        public async Task<IActionResult> Inserir([FromBody] Aluno aluno)
        {
            var resultApplication = new ResultApplication();

            try
            {
                await _context.AddAsync(aluno);
                await _context.SaveChangesAsync();
                resultApplication.Success = true;
                resultApplication.Message = "Registro salvo com sucesso!";
                return Ok(resultApplication);

            }catch (Exception ex)
            {
                resultApplication.Message = "Ocorreu um erro interno!";
                resultApplication.Error = ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError,
                    resultApplication);
            }

        }
        
        [HttpPut("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] Aluno aluno)
        {
            var resultApplication = new ResultApplication();

            try
            {
                var result = await _context.Alunos.FindAsync(aluno.Codigo);
                if (result != null)
                {
                    result.Nome = aluno.Nome;

                    await Task.FromResult(_context.Alunos.Update(result));
                    await _context.SaveChangesAsync();
                    resultApplication.Success = true;
                    resultApplication.Message = "Registro salvo com sucesso!";
                    return Ok(resultApplication);
                }
                else
                {
                    resultApplication.Message = "Aluno não encontrado!";
                    return BadRequest(resultApplication);
                }

            }
            catch (Exception ex)
            {
                resultApplication.Message = "Ocorreu um erro interno!";
                resultApplication.Error = ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError,
                    resultApplication);
            }
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("Excluir({id})")]
        public async Task<IActionResult> Excluir(int id)
        {
            var resultApplication = new ResultApplication();

            try
            {
                var aluno = await _context.Alunos.FindAsync(id);
                if (aluno != null)
                {
                    await Task.FromResult(_context.Alunos.Remove(aluno));
                    await _context.SaveChangesAsync();
                    resultApplication.Success = true;
                    resultApplication.Message = "Registro excluído com sucesso!";
                    return Ok(resultApplication);
                }
                else
                {
                    resultApplication.Message = "Aluno não encontrado!";
                    return BadRequest(resultApplication);
                }


            }
            catch (Exception ex)
            {
                resultApplication.Message = "Ocorreu um erro interno!";
                resultApplication.Error = ex.InnerException.Message;
                return StatusCode(StatusCodes.Status500InternalServerError,
                    resultApplication);
            }
        }
    }
}
