using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PokemonShop.DataTransferObjects;

namespace PokemonShop.Services.MessageSender
{
    public class EmailMessageSender : IMessageSender
    {
        #region Fields

        private readonly MailConfiguration _mailConfig;

        #endregion

        #region Ctor

        public EmailMessageSender(IOptions<MailConfiguration> mailConfig)
        {
            _mailConfig = mailConfig.Value;
        }

        #endregion

        public async Task PokemonOrdered(UserDto user, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Заказ успешно оформлен", "order@pockemonShop.ua"));
            emailMessage.To.Add(new MailboxAddress("", user.Email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Здравствуйте, {user.Name}! \r\n {message}" 
            };

            using (var client = new SmtpClient())
            {
                if (_mailConfig.IsValid)
                {
                    await client.ConnectAsync(_mailConfig.SMTPServer, _mailConfig.SMTPPort, false);
                    await client.AuthenticateAsync(_mailConfig.Login, _mailConfig.Password);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }    
            }
        }
    }
}