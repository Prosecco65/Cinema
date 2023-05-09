using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Spettacolo
    {
        [Key]
        public int Id_spettacolo { get; set; }

        public DateTime dataa { get; set; }
        public int Id_sala { get; set; }

        [ValidateNever]
        [ForeignKey("Id_sala")]

        public Sala Sala { get; set; }

        public int Id_film { get; set; }

        [ValidateNever]
        [ForeignKey("Id_film")]
        public Film Film { get; set; }

        [ValidateNever]
        public ICollection<Biglietti> Biglietti { get; set; }
    }
}
