using System;
using System.ComponentModel.DataAnnotations;
using Tambaqui.Extensions;

namespace Tambaqui.Models
{
    public class Pessoa
    {   
        public int Id { get; set; }        

        [Required]      
        public string Nome { get; set; }
        
        [CPF]
        public string CPF { get; set; }

        [Required, Display(Name ="Data"), DataType(DataType.Date)]
        public DateTime DataDeContratação { get; set; }

        public Endereco Endereco { get; set; }   

        public bool Ativo { get; set; } = true;     

        public void InverterAtivo() => Ativo = !Ativo;         
    }
}