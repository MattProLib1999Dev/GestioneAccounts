using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class CreateAccountDto
{
  [JsonIgnore,NotMapped]
  public Guid Id { get; set; }
  public int AccountId { get; set; }

  [Required]
  public string UserName { get; set; } = string.Empty;

  [Required]
  [EmailAddress]
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;

  public string PhoneNumber { get; set; } = string.Empty;
  public DateTime? LockoutEnd { get; set; }

  public bool LockoutEnabled { get; set; }

  public int AccessFailedCount { get; set; }

  public string Nome { get; set; } = string.Empty;

  public string Voce { get; set; } = string.Empty;

  public string ValoreString { get; set; } = string.Empty;

  public DateTime DataCreazione { get; set; } = DateTime.UtcNow;

  [JsonIgnore, NotMapped]
  public List<ValoriDto> Valori { get; set; } = new();
  public int OreLavorate { get; set; } = 0;

  [JsonIgnore, NotMapped]
  public string? Roles { get; set; } = string.Empty;

}
