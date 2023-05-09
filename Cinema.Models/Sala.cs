using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Sala
    {
        [Key]
        public int Id_sala { get; set; }

        public int numeroPosti { get; set; }

        public bool isense { get; set; }

        [ValidateNever]
        public ICollection<Spettacolo> Spettacolo { get; set; }

        [ValidateNever]
        public ICollection<Posti> Posti { get; set; }
    }
}
