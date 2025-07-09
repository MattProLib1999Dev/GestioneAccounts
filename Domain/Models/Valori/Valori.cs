using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
// Removed invalid using directive for 'Account' type.
public class Valori
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long? Id { get; set; }
    // Chiave primaria con incremento automatico. `long?` va bene, ma potresti usare `long` se è sempre richiesto.

    [Required]
    public DateTime DataCreazione { get; set; } = DateTime.UtcNow;
    // Campo obbligatorio, inizializzato a ora UTC.

    [JsonIgnore]
    public long? AccountId { get; set; }
    // Campo di collegamento alla foreign key. Ignorato nel JSON.

    [ForeignKey("AccountId")]
    public GestioneAccounts.BE.Domain.Models.Account? Account { get; set; }
    // Navigation property per EF. L'attributo [ForeignKey] è corretto.

    public string Nome { get; set; } = String.Empty;
    public string valoreString { get; set; } = String.Empty;
    public string voce { get; set; } = String.Empty;
    // Tre stringhe opzionali con default a stringa vuota
}
