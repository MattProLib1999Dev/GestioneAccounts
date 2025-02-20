using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestioneAccounts.DataAccess;

public class Valori
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long? Id { get; set; }

    [Required]
    public DateTime DataCreazione { get; set; } = DateTime.UtcNow;

    public long? AccountId { get; set; }

    [ForeignKey("AccountId")]
    public Account? Account { get; set; }

    public string Nome { get; set; } = string.Empty;
}
