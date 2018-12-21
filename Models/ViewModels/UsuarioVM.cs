using System;
using System.ComponentModel.DataAnnotations;
using Tambaqui.Extensions;
using Tambaqui.Models;

namespace Tambaqui.ViewModels
{
    public class UsuarioVM
    {
        public UsuarioVM()
        {
            
        }
        public UsuarioVM(Usuario usuario)
        {
            this.Id = usuario.Id;
            this.Nome = usuario.Nome;
            this.Email = usuario.Email;
            this.CPF = usuario.CPF;            
        }

        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }

		[Required, MaxLength(11), CPF]
		public string CPF { get; set; }

		[Required, MaxLength(50), EmailAddress]
		public string Email { get; set; }

        [Required]
        public string Perfil { get; set; }

    }
}