using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Posti
    {
        [Key]
        public int Id_posti { get; set; }
        public double costo { get; set; }
        public int Id_sala { get; set; }

        [ValidateNever]
        [ForeignKey("Id_sala")]
        public Sala Sala { get; set; }

        [ValidateNever]
        public ICollection<Biglietti> Biglietti { get; set;}
    }
}
