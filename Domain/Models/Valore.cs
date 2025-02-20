using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace matteo.proietti.progetti.Progetti_personali_e_esercitazioni.esercitazioni_dotnet.GestioneAccounts.BE.Domain.Models
{
	public class Valore
	{

	 [Key]
	 public long Id { get; set; }
	 public string Voce { get; set; } = String.Empty;
	 public string valore { get; set; } = String.Empty;
	 public long AccountId { get; set; }
 	}
}