using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesafioINOA
{
    public class API
    {
        public string Token { get; set; }
        public int Delay { get; set; }
    }
    public class Settings
    {
        public API Api { get; set; }
        public SmtpSettings Smtp { get; set; }
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }
    }

    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
