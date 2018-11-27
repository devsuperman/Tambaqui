using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tambaqui.Models
{
    public class Cor : Registro
    {
        [Required]
        public string Nome { get; set; }
        
        public ICollection<Carro> Carros {get; set;}        
    }
}