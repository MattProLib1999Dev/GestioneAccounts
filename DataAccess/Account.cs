using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace GestioneAccounts.BE.Domain.Models
{
    public class Account
    {
        [Key]
        public long Id { get; set; }  // ✅ Non-nullable primary key

        public string Nome { get; set; } = string.Empty;

        public List<Valore>? Valori { get; set; }  // ✅ Match the `Valore` class name
        public string valoreString { get; set; } = String.Empty;
        public string voce { get; set; } = String.Empty;
        public DateTime dataCreazione { get; set; } = DateTime.Now;

    }
}
