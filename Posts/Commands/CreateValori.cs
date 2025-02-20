using MediatR;

public class CreateValoreRequest : IRequest<Valori>
{
    public long AccountId { get; set; }
    public string Nome { get; set; }

    public CreateValoreRequest(long accountId, string nome)
    {
        AccountId = accountId;
        Nome = nome;
    }
}
