using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Valuta
    {
        [Key]
        public int Id_valutazione { get; set; }
        public string? valutazione { get; set; }

        public int Id_film { get; set;  }

        [ValidateNever]
        [ForeignKey("Id_film")]
        public Film Film { get; set; }

        public string Id { get; set; }

        [ValidateNever]
        [ForeignKey("Id")]
        public Utente Utente { get; set; }

    }
}
