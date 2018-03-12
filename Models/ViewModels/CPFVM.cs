using System.ComponentModel.DataAnnotations;
using Tambaqui.Extensions;

namespace Tambaqui.Models
{
        public class CPFVM
        {
            [Required, CPF]
            public string cpf { get; set; }
        }
}