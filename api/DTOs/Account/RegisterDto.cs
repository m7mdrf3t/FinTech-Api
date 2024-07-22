using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Account;

public record RegisterDto
{
    [Required]
    public string? Username { get; set; }

    [Required]
    [EmailAddress]
    public string? EmailAddress { get; set; }

    [Required]
    public string? Password { get; set; }
}
