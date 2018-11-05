using Microsoft.Extensions.Options;
using Tambaqui.Interfaces;
using System.Threading.Tasks;
using Tambaqui.Models;
using System.Net.Mail;
using System.Net;

namespace Tambaqui.Services
{
    public class Gmail : IEmail
    {
        public ConfiguracaoDeEmail configuracoesDeEmail { get; }

        public Gmail(IOptions<ConfiguracaoDeEmail> configuracoesDeEmail)
        {
            this.configuracoesDeEmail = configuracoesDeEmail.Value;
        }

        public async Task EnviarAsync(string destino, string assunto, string mensagem)
        {            
            var mail = new MailMessage()
            {
                From = new MailAddress(configuracoesDeEmail.EmailDoRemetente, configuracoesDeEmail.NomeDoRemetente),
                Subject = assunto,
                Body = mensagem,
                IsBodyHtml = true                
            };

            mail.To.Add(new MailAddress(destino));                        
            
            using (SmtpClient smtp = new SmtpClient(configuracoesDeEmail.Dominio, configuracoesDeEmail.Porta))
            {
                smtp.Credentials = new NetworkCredential(configuracoesDeEmail.EmailDoRemetente,configuracoesDeEmail.Senha);
                smtp.EnableSsl = configuracoesDeEmail.SSL;
                
                await smtp.SendMailAsync(mail);
            }
        }

    }
}