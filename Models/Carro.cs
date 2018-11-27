using System;
using System.ComponentModel.DataAnnotations;

namespace Tambaqui.Models
{
    public class Carro : Registro
    {        

        [Required]      
        public string Modelo { get; set; }
        
        [Required, Display(Name ="Data de Lançamento"), DataType(DataType.Date)]
        public DateTime DataDeLancamento { get; set; }

        [Required, Display(Name ="Cor")]
        public int CorId { get; set; }

        public Cor Cor { get; set; }       
        
    }
}