using adintelSiteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace adintelSiteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendEmailController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {
            try
            {
                // Configurações de e-mail (substitua pelos seus próprios valores)
                string smtpServer = "localhost";
                int smtpPort = 25; // Porta SMTP
                //string smtpUsername = "seu_email";
                //string smtpPassword = "sua_senha";

                // Criando o cliente SMTP
                using var client = new SmtpClient(smtpServer, smtpPort)
                {
                    EnableSsl = false,
                    //Credentials = new NetworkCredential(smtpUsername, smtpPassword)
                };

                // Construindo a mensagem de e-mail
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(model.From),
                    Subject = model.Subject,
                    Body = model.Body,
                    IsBodyHtml = true
                };

                // Adicionando destinatários
                foreach (var to in model.To)
                {
                    mailMessage.To.Add(to);
                }

                // Enviando o e-mail
                await client.SendMailAsync(mailMessage);

                return Ok("E-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Falha ao enviar o e-mail: {ex.Message}");
            }
        }
    }
}
