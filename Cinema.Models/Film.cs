using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Film
    {
        [Key]
        public int Id_film { get; set; }
        public string titolo { get; set; }
        public string genere { get; set; }
        public string descrizione { get; set; }
        public int durata { get; set; }

        [ValidateNever]
        public string? copertina { get; set; }

        [ValidateNever]
        public ICollection<Valuta> Valuta { get; set; }
        [ValidateNever]
        public ICollection<Spettacolo> Spettacolo { get; set; }
    }
}
