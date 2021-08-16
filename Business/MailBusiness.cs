using System;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Business.Contracts;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Business
{
    public class MailBusiness : BaseBusiness, IMailBusiness
    {
        private readonly IConfiguration _configuration;

        public MailBusiness(UserManager<User> userManager, ClaimsPrincipal user, IServiceProvider serviceProvider,
            IConfiguration configuration) : base(userManager, user, serviceProvider)
        {
            _configuration = configuration;
        }

        public void SignInConfirmationMail(User user, string url)
        {
            var configurationSection = _configuration.GetSection("Smtp");
            var host = configurationSection.GetSection("Server").GetSection("Host").ToString();
            if (host is null or "")
            {
                throw new SmtpException("No SMTP Host set in application.json");
            }
            var client = new SmtpClient(host);
            var email = configurationSection.GetSection("From").GetSection("Email").ToString();
            var name = configurationSection.GetSection("From").GetSection("Name").ToString();
            if (email is null || !Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                throw new SmtpException("No sender email set in application.json");
            }

            if (name is null or "")
            {
                name = email;
            }

            var from = new MailAddress(email, name);

            var to = new MailAddress(user.Email, $"{user.FirstName} ${user.LastName}");

            var message = new MailMessage(from, to);

            message.Body =
                $"Greetings {user.UserName} and welcome to Overwatch Tracer, in order to start using this service, please validate you're mail by following this link <a href='{url}'>{url}</a>";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = $"Welcome to Overwatch Tracker {user.UserName}";
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            client.SendAsync(message, $"Confirmation mail for user {user.Id}");
        }
    }
}