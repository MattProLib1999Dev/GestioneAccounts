public class ValoriDto
{
  public string AccountId { get; set; } = string.Empty; // Solo l'ID, non l'intera entità
  public string Nome { get; set; } = string.Empty;
    public string valoreString { get; set; } = string.Empty;
    public string voce { get; set; } = string.Empty;
    public DateTime DataCreazione { get; set; }
    public string Descrizione { get; set; } = string.Empty;
  public decimal ValoreNumerico { get; set; } = Decimal.Zero;
}
