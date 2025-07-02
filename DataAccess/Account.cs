using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GestioneAccounts.BE.Domain.Models
{
    public class Account: IdentityUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), JsonIgnore]
        public string Id { get; set; }  // Primary key (non-nullable)

        [StringLength(100, ErrorMessage = "Il nome non può essere più lungo di 100 caratteri.")]
        public string Nome { get; set; } = string.Empty;  // Account Name

        [JsonIgnore]
        public List<Valore>? Valori { get; set; }  // List of related Valore entities

        public string valoreString { get; set; } = string.Empty;  // Additional value (assuming this is a string)

        [StringLength(100, ErrorMessage = "La voce non può essere più lunga di 100 caratteri.")]
        public string voce { get; set; } = string.Empty;  // Additional category or voice (assuming a string)

        public DateTime dataCreazione { get; set; } = DateTime.Now;  // Creation timestamp (defaults to now)

        [StringLength(1, ErrorMessage = "La voce non può essere più lunga di un carattere.")]
        public int Ore_lavorate { get; set; } = 0;
        public Role Role { get; set; } = new();
            public int RoleId { get; set; }  // FK per Role


  }
}
