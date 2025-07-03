using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestioneAccounts.BE.Domain.Models
{
  public class Valore
  {
    [Key, JsonIgnore, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string AccountId { get; set; } = String.Empty; // Foreign key esplicita

    public Account Account { get; set; }  // Navigation property
    public string Nome { get; set; } = string.Empty;
    public string Descrizione { get; set; } = string.Empty;
    public decimal ValoreNumerico { get; set; } = 0.0m;
    public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
    public string? ValoreStr { get; set; } = null;
    public string? Voce { get; set; } = null;
  }
}
