using GestioneAccounts.Abstractions;
using GestioneAccounts.BE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestioneAccounts.DataAccess.Repositories
{
    public class ValoriRepository(ApplicationDbContext applicationDbContext) : IValoriRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    // Crea un nuovo "Valori"
    public async Task<Valori> CreateValori(Valori valori)
        {
            _applicationDbContext.Add(valori);
            await _applicationDbContext.SaveChangesAsync();
            return valori;
        }

        // Elimina un "Valori" per ID
        public async Task<bool> DeleteValori(long valoriId)
        {
            var valori = await _applicationDbContext.Valori.FirstOrDefaultAsync(a => a.Id == valoriId);
            if (valori == null)
                return false;

            _applicationDbContext.Valori.Remove(valori);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        // Ottieni un "Valori" per ID
        public async Task<Valore> GetValoriById(long valoriId)
        {
            return await _applicationDbContext.Valori
                .FirstOrDefaultAsync(a => a.Id == valoriId) ?? new Valore();
        }


        // Ottieni tutti i "Valori"
     public async Task<ICollection<Valore>> GetAllValori()
     {
          var valori = await _applicationDbContext.Valori.ToListAsync();
          return valori ?? new List<Valore>();
     }


        // Aggiorna o crea un "Valori" in base all'account
        public async Task<Valore> UpdateValori(Account account, long valoriId)
        {
            var valore = await _applicationDbContext.Valore.FirstOrDefaultAsync(v => v.Id == valoriId);

            if (valore == null)
            {
                // Crea un nuovo oggetto Valori
                var nuovoValore = new Valore
                {
                    DataCreazione = DateTime.Now,
                    Nome = account.Nome,
                    ValoreStr = account.valoreString,
                    Voce = account.voce
                };

                // Aggiungi il nuovo oggetto al contesto
                _applicationDbContext.Valori.Add(nuovoValore);

                // Imposta la variabile valori al nuovo oggetto creato
                valore = nuovoValore;
            }
            else
            {
                // Aggiorna le proprietà di valori con quelle di account (se necessario)
                valore.Nome = account.Nome;
                valore.ValoreStr = account.valoreString;
                valore.Voce = account.voce;
            }

            // Salva le modifiche nel database
            await _applicationDbContext.SaveChangesAsync();

            // Restituisci l'oggetto aggiornato
            return valore;
        }

    Task<ICollection<Valori>> IValoriRepository.GetAllValori()
    {
      throw new NotImplementedException();
    }

    Task<Valori> IValoriRepository.GetValoriById(long valoriId)
    {
      throw new NotImplementedException();
    }

    Task<Valori> IValoriRepository.UpdateValori(Account account, long valoriId)
    {
      throw new NotImplementedException();
    }
  }
}
