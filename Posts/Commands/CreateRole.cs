using MediatR;
public class CreateRoleCommand : IRequest<Role>
{
    public Guid AccountId { get; set; }
    public List<string> Roles { get; set; }  = new List<string>() { "Admin", "Dipendente" };
    public string Name { get; set; } = String.Empty;
}
