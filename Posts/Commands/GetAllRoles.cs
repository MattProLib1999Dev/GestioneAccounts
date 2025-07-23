using GestioneAccounts.BE.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

public class GetAllRoles : IRequest<List<Role>>
{
    public Guid AccountId { get; set; } = Guid.Empty;
    public List<string> Roles { get; set; } = new List<string> { "Admin", "Dipendente" };
}
