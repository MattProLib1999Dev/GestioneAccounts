using System.ComponentModel.DataAnnotations;

namespace GestioneAccounts.BE.Domain.Models
{
    public class Valore
    {
        [Key]
        public long Id { get; set; }

        public string Voce { get; set; } = string.Empty;
        public string ValoreStr { get; set; } = string.Empty;  // ✅ Renamed to avoid conflict with class name

        public long? AccountId { get; set; }  // ✅ Nullable to match optional FK
        public Account? Account { get; set; }  // ✅ Correct navigation property name
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public string Nome { get; set; } = String.Empty;
        public string ValoreString { get; set; } = String.Empty;

    }
}
