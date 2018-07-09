using System;
using System.ComponentModel.DataAnnotations;

namespace Tambaqui.Models
{
    public class Carro
    {        
        public int Id { get; set; }  

        [Required]      
        public string Modelo { get; set; }
        
        [Required, Display(Name ="Data de Lançamento"), DataType(DataType.Date)]
        public DateTime DataDeLancamento { get; set; }

        [Required, Display(Name ="Cor")]
        public int CorId { get; set; }

        public Cor Cor { get; set; }
        
        public DateTime DataDeCadastro { get; set; } = new DateTime();

        public DateTime? DataDeEdicao { get; set; }

        public override string ToString() => $"Modelo: {this.Modelo}, {this.Cor?.Nome}";
        
    }
}