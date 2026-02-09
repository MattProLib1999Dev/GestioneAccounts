using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class GetAccountDto
{
  [NotMapped, JsonIgnore]
  public string Id { get; set; }
  public int AccountId { get; set; } = 0;
  [Required]
  public string UserName { get; set; } = string.Empty;

  [Required]
  [EmailAddress]
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;

  public string PhoneNumber { get; set; } = string.Empty;
  public string Nome { get; set; } = string.Empty;

  public string Voce { get; set; } = string.Empty;

  public string ValoreString { get; set; } = string.Empty;
  public DateTime DataCreazione { get; set; } = DateTime.UtcNow;

  public List<ValoriDto> Valori { get; set; } = new();
  public double OreLavorate { get; set; } = 0;
  public string Roles { get; set; }

}
