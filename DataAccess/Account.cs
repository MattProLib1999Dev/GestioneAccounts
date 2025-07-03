using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GestioneAccounts.BE.Domain.Models
{
  public class Account : IdentityUser
  {
    public ICollection<Valore> Valori { get; set; }
    public Role? Role { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string voce { get; set; } = string.Empty;
    public string valoreString { get; set; } = string.Empty;
    public DateTime dataCreazione { get; set; } = DateTime.Now;

  }
}
