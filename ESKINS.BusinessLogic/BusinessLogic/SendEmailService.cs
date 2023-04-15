using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using ESKINS.BusinessLogic.Interfaces;

namespace ESKINS.BusinessLogic.BusinessLogic
{
    /// <summary>
    /// Class handles email services.
    /// </summary>
    public class SendEmailService : ISendEmailService
    {
        #region Class Variables

        private const string _DefaultSmtpHost = "smtp-relay.gmail.com";
        private const int _DefaultSmtpPort = 587;
        private const string _DefaultEmailSender = "noreply.support@eskins.com";

        private SmtpClient _smtpClient;
        private string _emailSender;

        #endregion

        #region Constructor
        public SendEmailService()
        {
            // Setup SMTP host and port from environment if possible, otherwise use a default.
            var hostFromEnv = Environment.GetEnvironmentVariable("SmtpHost");
            var portFromEnv = Environment.GetEnvironmentVariable("SmtpPort");
            _smtpClient = new SmtpClient(
                hostFromEnv ?? _DefaultSmtpHost,
                StringToInt(portFromEnv, _DefaultSmtpPort)
            )
            {
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            // Get whether or not to use SSL from the environment if possible.
            var enableSslFromEnv = Environment.GetEnvironmentVariable("EnableSsl");
            if (enableSslFromEnv != null)
            {
                _smtpClient.EnableSsl = enableSslFromEnv == "true";
            }

            // Get network credentials from the environment if possible.
            var userNameFromEnv = Environment.GetEnvironmentVariable("EmailUsername");
            var passwordFromEnv = Environment.GetEnvironmentVariable("EmailPassword");
            if (userNameFromEnv != null && passwordFromEnv != null)
            {
                _smtpClient.Credentials = new NetworkCredential(userNameFromEnv, passwordFromEnv);
            }
            _emailSender = userNameFromEnv ?? _DefaultEmailSender;
        }

        #endregion

        #region Interface Call

        /// <inheritdoc />
        public bool SendMessage(string[] recipients, string subject, string body, string[] cc = null)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_emailSender),
                Subject = subject,
                Body = body
            };

            foreach (var rec in recipients)
            {
                mail.To.Add(new MailAddress(rec));
            }
            if (cc != null)
            {
                foreach (var rec in cc)
                {
                    mail.CC.Add(new MailAddress(rec));
                }
            }
            try
            {
                _smtpClient.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine($"EmailService.SendMessage() threw WebException: {e}");
                return false;
            }
        }

        #endregion

        #region Helper

        /// <summary>
        /// Returns the input string argument as an int, or returns a default value argument instead if the input
        /// argument is unpopulated.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Int value of either the input argument, or default depending on validation.</returns>
        private int StringToInt(string input, int defaultValue)
        {
            try
            {
                return int.Parse(input);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        #endregion
    }
}
