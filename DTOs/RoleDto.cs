using System.Text.Json.Serialization;
using GestioneAccounts.BE.Domain.Models;

public class RoleDto
{
  public Guid AccountId { get; set; }
  public List<string> Roles { get; set; } = new List<string>() { "Admin", "Dipendente" };
  public List<Guid> RolesId { get; set; } = new List<Guid>();


  [JsonIgnore]

    public ICollection<Account> Accounts { get; set; } = new List<Account>();



}
