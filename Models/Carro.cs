using System;
using System.ComponentModel.DataAnnotations;

namespace Tambaqui.Models
{
    public class Carro
    {
        public int Id { get; set; }  

        [Required]      
        public string Modelo { get; set; }
        
        [Required, Display(Name ="Cor")]
        public int CorId { get; set; }

        public Cor Cor { get; set; }

    }
}