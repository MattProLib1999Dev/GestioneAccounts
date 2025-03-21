using MediatR;

public class CreateValoreRequest(long accountId, string nome) : IRequest<Valori>
{
  public long AccountId { get; set; } = accountId;
  public string Nome { get; set; } = nome;
}
