using System.Collections;
using System.Text.Json.Serialization;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.Domain.Models;

public class RoleDto
{
  public Guid AccountId { get; set; }
  public List<Ruolo> listaRuoli = Enum.GetValues(typeof(Ruolo)).Cast<Ruolo>().ToList();

  public List<Guid> RolesId { get; set; } = new List<Guid>();


  [JsonIgnore]

    public ICollection<Account> Accounts { get; set; } = new List<Account>();



}
