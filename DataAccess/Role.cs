using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GestioneAccounts.BE.Domain.Models;

public class Role
{
  [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
  public int Id { get; set; }
  public string Admin { get; set; } = null!;
  public string User { get; set; } = null!;

  public string? AccountId { get; set; }  // <-- CAMBIATO: deve essere string?
  public Account? Account { get; set; }   // Navigation property

}
