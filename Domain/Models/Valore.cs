using System.ComponentModel.DataAnnotations;

namespace GestioneAccounts.BE.Domain.Models
{
    public class Valore
    {
        [Key]
        public long Id { get; set; }

        public string Voce { get; set; } = string.Empty;
        public string ValoreStr { get; set; } = string.Empty;

        public long? AccountId { get; set; }
        public Account? Account { get; set; }
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public string Nome { get; set; } = String.Empty;

    }
}
