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

        [DataType(DataType.Currency)]
        public double Salario { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime DataDeCadastro { get; set; } = new DateTime();
        public DateTime? DataDeEdicao { get; set; }    

        public Endereco Endereco { get; set; }            
    }
}