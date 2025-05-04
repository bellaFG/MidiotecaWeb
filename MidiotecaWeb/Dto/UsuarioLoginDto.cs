﻿using System.ComponentModel.DataAnnotations;

public class UsuarioLoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }
    
}