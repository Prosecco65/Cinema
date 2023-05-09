using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Biglietti
    {
        [Key]
        public int Id_biglietti { get; set; }

        public string Id { get; set; }

        [ValidateNever]
        [ForeignKey("Id")]
        public Utente Utente { get; set; }

        public int Id_posti { get; set; }

        [ValidateNever]
        [ForeignKey("Id_posti")]
        public Posti Posti { get; set; }

        public int Id_spettacolo { get; set; }

        [ValidateNever]
        [ForeignKey("Id_spettacolo")]
        
        public Spettacolo Spettacolo { get; set; }

    }
}
