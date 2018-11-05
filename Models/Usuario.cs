using System;
using System.ComponentModel.DataAnnotations;

namespace Tambaqui.Models
{
    public class Usuario : Registro
	{		
        public Usuario()
        {
            
        }

		public Usuario(string nome, string cpf, string email)
		{
			GerarNovaHash();
            Nome = nome;
            CPF = cpf;
            Email = email;
		}
		
        [Required, MaxLength(50)]
        public string Nome { get; set; }

		[Required, MaxLength(11)]
		public string CPF { get; set; }

		[Required, MaxLength(20)]
		public string Senha { get; set; } = Guid.NewGuid().ToString();

		[Required, MaxLength(50), EmailAddress]
		public string Email { get; set; }

		public string Hash {get; set; }

		public bool HashUtilizado {get;set;}

        public bool ehAdmin { get; set; }

        public void AlterarSenha(string senhaCriptografada)
        {
            this.Senha = senhaCriptografada;
        }

        public void GerarNovaHash()
        {
            this.Hash = Guid.NewGuid().ToString();
            this.HashUtilizado = false;
        }

        public void UtilizarHash()
        {
            this.HashUtilizado = true;
        }

        public void Atualizar(string nome, string cpf, string email)
        {
            this.Nome=nome;
            this.CPF = cpf;
            this.Email = email;
        }
    }
}