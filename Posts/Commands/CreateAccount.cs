using System.ComponentModel.DataAnnotations;
using GestioneAccounts.BE.Domain.Models;
using GestioneAccounts.DataAccess;
using MediatR;

namespace GestioneAccounts.Posts.Commands
{
	public class CreateAccount: IRequest<Account>
	{
        [Key]
        public int Id { get; set; }  // Primary key (non-nullable)

        [StringLength(100, ErrorMessage = "Il nome non può essere più lungo di 100 caratteri.")]
        public string Nome { get; set; } = string.Empty;  // Account Name

        //public List<Valore>? Valori { get; set; }  // List of related Valore entities

        public string valoreString { get; set; } = string.Empty;  // Additional value (assuming this is a string)

        [StringLength(100, ErrorMessage = "La voce non può essere più lunga di 100 caratteri.")]
        public string voce { get; set; } = string.Empty;  // Additional category or voice (assuming a string)

        public DateTime dataCreazione { get; set; } = DateTime.Now;  // Creation timestamp (defaults to now)

	}
}
