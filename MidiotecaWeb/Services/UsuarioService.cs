using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MidiotecaWeb.Data;
using MidiotecaWeb.Dto;
using MidiotecaWeb.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UsuarioService : IUsuarioService
{
    private readonly MidiotecaDbContext _context;
    private readonly IConfiguration _configuration;

    public UsuarioService(MidiotecaDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> CriarUsuarioAsync(UsuarioCriacaoDto dto)
    {
        var usuarioExistente = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (usuarioExistente != null)
            throw new Exception("Já existe um usuário com esse email.");

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Ativo = true
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var token = GerarJwt(usuario); 

        return token;
    }

    public async Task<string> AutenticarUsuarioAsync(UsuarioLoginDto dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
            throw new Exception("Credenciais inválidas.");

        var token = GerarJwt(usuario); 

        return token;
    }

    public async Task EditarUsuarioAsync(Guid id, UsuarioEdicaoDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            throw new Exception("Usuário não encontrado.");

        if (!string.IsNullOrEmpty(dto.Nome))
            usuario.Nome = dto.Nome;

        if (!string.IsNullOrEmpty(dto.Email) && usuario.Email != dto.Email)
        {
            var usuarioComEmailExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuarioComEmailExistente != null)
                throw new Exception("Já existe um usuário com esse email.");

            usuario.Email = dto.Email;
        }

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task AlterarSenhaAsync(Guid id, UsuarioAlterarSenhaDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            throw new Exception("Usuário não encontrado.");

        if (!BCrypt.Net.BCrypt.Verify(dto.SenhaAtual, usuario.Senha))
            throw new Exception("Senha atual incorreta.");

        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);
        _context.Usuarios.Update(usuario);        await _context.SaveChangesAsync();
    }

    public async Task<UsuarioDetalhadoDto> ObterUsuarioPorIdAsync(Guid id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.LivrosRelacionados)
            .ThenInclude(lu => lu.Livro)  
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
            throw new Exception("Usuário não encontrado.");

        return new UsuarioDetalhadoDto
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            LivrosRelacionados = usuario.LivrosRelacionados
                .Select(lu => new LivroUsuarioDto
                {
                    LivroId = lu.LivroId,
                    TituloLivro = lu.Livro.Titulo,
                    Autor = lu.Livro.Autor,
                    StatusLeitura = lu.Status.ToString(),
                    Nota = lu.Nota,
                    DataLeitura = lu.DataLeitura
                }).ToList()
        };
    }

    
    private string GerarJwt(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
