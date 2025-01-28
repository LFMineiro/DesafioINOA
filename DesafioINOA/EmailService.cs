using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DesafioINOA
{
    internal class EmailService
    {
        public string server;
        public int port;
        public string username;
        public string password;

        public EmailService(string server, int port, string username, string password)
        {
            this.server = server;
            this.port = port;
            this.username = username;
            this.password = password;
        }

        public static bool EmailAddressValidation(string emailAddress)
        {
            try
            {
                Regex regexExpression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                if (regexExpression.IsMatch(emailAddress))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string CreateBody(decimal price, string ticker, string action)
        {
            string body = "O atual preço do " + ticker + " eh de " + price + ", portanto, deve-se " + action ;
            return body;
        }

        public void SendEmailMessage(string sender, List<string> recipients, Stock stock, string ticker, string action)
        {
            SmtpClient client;

            try
            {
                client = new SmtpClient(this.server, this.port)
                {
                    Credentials = new NetworkCredential(this.username, this.password),
                    EnableSsl = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid SMTP Credential: " + ex);
                return;
            }

            string subject = "Alert - " + ticker;

            foreach (string recipient in recipients)
            {
                if (!EmailAddressValidation(recipient))
                {
                    Console.WriteLine("Invalid email recipient: " + recipient);
                    return;
                }

                string body = CreateBody(stock.Price, ticker, action);

                try
                {
                    MailMessage emailMessage = new MailMessage(sender, recipient, subject, body);
                    client.Send(emailMessage);

                    Console.WriteLine("Email sent to " + recipient + " at " + DateTime.Now.ToString() + ".");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error sending email: " + ex);
                    return;
                }
            }
            client.Dispose();
        }
    }
}