using System.ComponentModel.DataAnnotations;
using matteo.proietti.progetti.Progetti_personali_e_esercitazioni.esercitazioni_dotnet.GestioneAccounts.BE.Domain.Models;

namespace GestioneAccounts.DataAccess
{
	public class Account
	{
                [Key]
                public long ?Id { get; set; }
                public string? Nome { get; set; } = String.Empty;
                public List<Valore>? Valori { get; set; }
	}
}


