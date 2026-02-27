using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace PrimerProyecto.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailConfirmacion(string emailDestino, string token)
        {
            var mensaje = new MimeMessage();

            mensaje.From.Add(new MailboxAddress(
                _configuration["Email:SenderName"],
                _configuration["Email:SenderEmail"]
            ));

            mensaje.To.Add(new MailboxAddress("", emailDestino));
            mensaje.Subject = "Confirma tu cuenta - Gestor de Tareas";

            var urlConfirmacion = $"http://localhost:3000/confirmar-email?token={token}";

            mensaje.Body = new TextPart("html")
            {
                Text = $@"
                    <h2>¡Bienvenido al Gestor de Tareas!</h2>
                    <p>Por favor confirma tu cuenta haciendo clic en el siguiente enlace:</p>
                    <a href='{urlConfirmacion}' style='background:#1976d2;color:white;padding:10px 20px;text-decoration:none;border-radius:5px;display:inline-block;'>
                        Confirmar Email
                    </a>
                    <p>O copia y pega este enlace en tu navegador:</p>
                    <p>{urlConfirmacion}</p>
                    <p>Este enlace expirará en 24 horas.</p>
                "
            };

            using var cliente = new SmtpClient();
            await cliente.ConnectAsync(
                _configuration["Email:SmtpServer"],
                int.Parse(_configuration["Email:SmtpPort"]!),
                SecureSocketOptions.StartTls
            );

            await cliente.AuthenticateAsync(
                _configuration["Email:SenderEmail"],
                _configuration["Email:SenderPassword"]
            );

            await cliente.SendAsync(mensaje);
            await cliente.DisconnectAsync(true);
        }
    }
}