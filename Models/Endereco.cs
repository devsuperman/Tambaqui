using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tambaqui.Models
{    
    [Owned]
    public class Endereco
    {   
        [MaxLength(10)]
        public string CEP { get; set; }

        [MaxLength(50)]
        public string Rua { get; set; }

        [Display(Name = "Número"), MaxLength(10)]
        public string Numero { get; set; }

        [MaxLength(50)]
        public string Bairro { get; set; }

        [MaxLength(100)]
        public string Complemento { get; set; }

        [MaxLength(50)]
        public string Cidade { get; set; }

        [MaxLength(50)]
        public string Estado { get; set; }

    }
}