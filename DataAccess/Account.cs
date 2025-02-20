using System.ComponentModel.DataAnnotations;

public class Account
{
    [Key]
    public long? Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public List<Valori>? Valori { get; set; }
}
