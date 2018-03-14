using System.ComponentModel.DataAnnotations;
using Tambaqui.Extensions;

namespace Tambaqui.Models
{
        public class CPFVM
        {
            [Required, CPF, Display(Name="CPF")]
            public string cpf { get; set; }
            

            [Required, Display(Name="Data"), DataType(DataType.Date)]
            public System.DateTime data { get; set; }


            [Required, Display(Name="Decimal")]
            public double numero { get; set; }


            [Required, Display(Name = "Email"), EmailAddress]
            public string email { get; set; }

        }
}