using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GestioneAccounts.BE.Domain.Models;

public class Role
{
  [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
  public int Id { get; set; }
  public string Admin { get; set; } = "Admin";
  public string User { get; set; } = "User";

  [JsonIgnore]
   public ICollection<Account>? Accounts { get; set; }

}
