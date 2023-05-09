using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cinema.Models
{
    public class Utente : IdentityUser
    {
        public string nome { get; set; }
        public string cognome { get; set; }
        public string sesso { get; set; }
        public DateTime data_nascita { get; set; }
        public string residenza { get; set; }

        [ValidateNever]
        public ICollection<Valuta> Valuta { get; set; }

        [ValidateNever]
        public ICollection<Biglietti> Biglietti { get; set;}

    }
}
