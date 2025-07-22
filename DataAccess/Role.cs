using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GestioneAccounts.BE.Domain.Models;
using Microsoft.AspNetCore.Identity;

public class Role : IdentityUser
{
  [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
  public int Id { get; set; }
  public Guid AccountId { get; set; } = Guid.Empty;
  public ICollection<Account> Accounts { get; set; } = new List<Account>();  // ✅
  public List<string> Roles { get; set; } = new List<string>() { "Admin", "Dipendente" };

  public string Name { get; set; }  = string.Empty;





}
