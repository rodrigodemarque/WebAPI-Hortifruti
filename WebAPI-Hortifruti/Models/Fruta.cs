using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_Hortifruti.Models
{
    public class Fruta : IFruta
    {
        public int Id { get ; set ; }

        [Required]
        [MaxLength(100)]
        public string Nome { get ; set ; }

        [Required]
        public DateTime DataVenc { get ; set ; }
    }
}