using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tambaqui.Models
{
    public class Cor : Registro
    {
        [Required]
        public string Nome { get; set; }
        
        public DateTime dataDeCadastro { get; set; } = new DateTime();

        public DateTime? dataDeEdicao { get; set; }

        public ICollection<Carro> Carros {get; set;}

        public override string ToString() => $"{this.Nome}";
    }
}