using MediatR;

public class CreateValoreRequest(int accountId, string nome) : IRequest<Valori>
{
  public int AccountId { get; set; } = accountId;
  public string Nome { get; set; } = nome;
  public string? Descrizione { get; set; }
  public decimal? ValoreNumerico { get; set; }
}
