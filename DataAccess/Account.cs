using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GestioneAccounts.BE.Domain.Models
{
  public class Account : IdentityUser<string>
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public int AccountId { get; set; }

    [JsonIgnore,NotMapped]
    public ICollection<Valore> Valori { get; set; } = new List<Valore>(); // ✅
    [NotMapped]
    public Role? Role { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Voce { get; set; } = string.Empty;
    public string ValoreString { get; set; } = string.Empty;
    public DateTime DataCreazione { get; set; } = DateTime.Now;

    [StringLength(1), MinLength(1)]
    public double OreLavorate { get; set; } = 0.0;

    public string Password { get; set; } = string.Empty;

    [JsonIgnore, NotMapped]
    public ICollection<Role> Roles { get; set; } = new List<Role>();  // ✅



  }
}
