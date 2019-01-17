using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using TiaIdentity;

namespace Tambaqui.Models
{
    public class Usuario : Registro, IUsuario
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

        public string Login { get => Nome;  }

		[Required, MaxLength(11)]
		public string CPF { get; set; }

		[Required, MaxLength(20)]
		public string Senha { get; set; } = Guid.NewGuid().ToString();

		[Required, MaxLength(50), EmailAddress]
		public string Email { get; set; }

		public string Hash {get; set; }

		public bool HashUtilizado {get;set;}

        public string Perfil { get; set; }

        public void AlterarSenha(string novaSenha) => this.Senha = CriptografarSenha(novaSenha);

        public void UtilizarHash() => this.HashUtilizado = true;
        
        public void GerarNovaHash()
        {
            this.Hash = Guid.NewGuid().ToString();
            this.HashUtilizado = false;
        }


        public void Atualizar(string nome, string cpf, string email)
        {
            this.Nome=nome;
            this.CPF = cpf;
            this.Email = email;
        }

        internal bool SenhaCorreta(string senha)
        {
            var senhaDigitadaCriptografada = CriptografarSenha(senha);
            return (this.Senha == senhaDigitadaCriptografada);
        }

        private string CriptografarSenha(string txt)
        {            
            var algoritmo = SHA512.Create();
            var senhaEmBytes = Encoding.UTF8.GetBytes(txt);
            var senhaCifrada = algoritmo.ComputeHash(senhaEmBytes);
            
            var sb = new StringBuilder();
            
            foreach (var caractere in senhaCifrada)            
                sb.Append(caractere.ToString("X2"));
            
            return sb.ToString();
        }
    }
}