using System.ComponentModel.DataAnnotations;

namespace Namespace.GestioneAccounts.Configuration.Models.DTOs;
public class AccountLoginRequestDto
{
  [Required(ErrorMessage = "Email is required.")]
  [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "Password is required.")]
  [StringLength(50, ErrorMessage = "Password cannot be longer than 50 characters.")]
  public string Password { get; set; } = string.Empty;

}
