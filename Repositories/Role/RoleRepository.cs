using GestioneAccounts.Abstractions;
using GestioneAccounts.BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestioneAccounts.DataAccess.Repositories
{
  public class RoleRepository(ApplicationDbContext applicationDbContext) : IRoleRepository
  {
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    // Crea un nuovo "Role"
    public async Task<Role> CreateRole(Role role)
    {
      _applicationDbContext.Add(role);
      await _applicationDbContext.SaveChangesAsync();
      return role;
    }

    // Elimina un "Role" per ID
    public async Task<bool> DeleteValori(long roleId)
    {
      var roles = await _applicationDbContext.Roles.FirstOrDefaultAsync(a => a.Id == roleId);
      if (roles == null)
        return false;

      _applicationDbContext.Roles.Remove(roles);
      await _applicationDbContext.SaveChangesAsync();
      return true;
    }

    // Ottieni un "Roles" per ID
    public async Task<Role> GetRoleById(long roleId)
    {
      return await _applicationDbContext.Roles
          .FirstOrDefaultAsync(a => a.Id == roleId) ?? new Role();
    }


    // Ottieni tutti i "Roles"
    public async Task<ICollection<Role>> GetAllRoles()
    {
      var role = await _applicationDbContext.Roles.ToListAsync();
      return role ?? new List<Role>();
    }


    // Aggiorna o crea un "Roles" in base all'account
    public async Task<Role> UpdateRole(Role role, long roleId)
    {
      var modifiedRole = await _applicationDbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

      if (modifiedRole == null || role == null)
      {
        // Se il ruolo non esiste, crea un nuovo oggetto Role
        modifiedRole = new Role
        {
          Admin = role.Admin,
          User = role.User,
          Account = role.Account
        };
           // Crea un nuovo oggetto Valori
        var nuovoRole = new Role
        {
          Admin = modifiedRole.Admin,
          User = modifiedRole.User,
          Account = modifiedRole.Account
        };

        // Aggiungi il nuovo oggetto al contesto
        _applicationDbContext.Roles.Add(nuovoRole);

        // Imposta la variabile valori al nuovo oggetto creato
        modifiedRole = nuovoRole;
      }
      else
      {
        // Aggiorna le proprietà di valori con quelle di account (se necessario)
        modifiedRole.Admin = modifiedRole.Admin;
        modifiedRole.User = modifiedRole.User;
        modifiedRole.Account = modifiedRole.Account;
      }

      // Salva le modifiche nel database
      await _applicationDbContext.SaveChangesAsync();

      // Restituisci l'oggetto aggiornato
      return modifiedRole;
    }

    //Delete
    public async Task<bool> DeleteRole(long roleId)
    {
      var role = await _applicationDbContext.Roles.FirstOrDefaultAsync(a => a.Id == roleId);
      if (role == null)
        return false;

      _applicationDbContext.Roles.Remove(role);
      await _applicationDbContext.SaveChangesAsync();
      return true;


    }
  }
}
