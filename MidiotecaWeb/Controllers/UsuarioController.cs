using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Dados inválidos");

        try
        {
            var token = await _usuarioService.AutenticarUsuarioAsync(dto);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { mensagem = ex.Message });
        }
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] UsuarioCriacaoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Dados inválidos.");

        try
        {
            var token = await _usuarioService.CriarUsuarioAsync(dto);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(Guid id, [FromBody] UsuarioEdicaoDto dto)
    {
        try
        {
            await _usuarioService.EditarUsuarioAsync(id, dto);
            return Ok(NoContent()); 
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id}/alterar-senha")]
    public async Task<IActionResult> AlterarSenha(Guid id, [FromBody] UsuarioAlterarSenhaDto dto)
    {
        try
        {
            await _usuarioService.AlterarSenhaAsync(id, dto);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet("{id}/detalhes")]
    [Authorize] 
    public async Task<IActionResult> ObterUsuarioDetalhado(Guid id)
    {
        try
        {
            var usuarioDetalhado = await _usuarioService.ObterUsuarioPorIdAsync(id);

            return Ok(usuarioDetalhado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}

