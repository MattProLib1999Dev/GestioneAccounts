using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GestioneAccounts.BE.Domain.Models
{
    public class Account
    {
        [Key]
        public long Id { get; set; }  // Primary key (non-nullable)

        public string Nome { get; set; } = string.Empty;  // Account Name

        public List<Valore>? Valori { get; set; }  // List of related Valore entities

        public string valoreString { get; set; } = string.Empty;  // Additional value (assuming this is a string)

        public string voce { get; set; } = string.Empty;  // Additional category or voice (assuming a string)

        public DateTime dataCreazione { get; set; } = DateTime.Now;  // Creation timestamp (defaults to now)
    }
}
