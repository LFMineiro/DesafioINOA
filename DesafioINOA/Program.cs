// See https://aka.ms/new-console-template for more information
using DesafioINOA;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Text.Json;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;


namespace DesafioInoaAlert
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Deve conter 3 argumentos.");
                return;
            }

            string ticker = args[0];
            decimal sellPrice = Convert.ToDecimal(args[1], new CultureInfo("en-US") );
            decimal buyPrice = Convert.ToDecimal(args[2], new CultureInfo("en-US"));

            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) 
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
               .Build();
            
            Settings settings = configuration.Get<Settings>();

            EmailService email = new(settings.Smtp!.Server!, settings.Smtp!.Port!, settings.Smtp!.Username!, settings.Smtp.Password!);


            BrapiAPI api = new BrapiAPI(settings.Api!.Token!);

            while(true)
            {
                try
                {
                    Stock stock = await api.GetPrice(ticker);

                    if (stock.Price >= sellPrice)
                    {
                        Console.WriteLine($"Preço atual do ativo {ticker}: {stock.Price}. O preço de venda é {sellPrice} e o preço de compra é {buyPrice}");
                        email.SendEmailMessage(settings.Sender!, settings.Recipients!, stock, ticker, "vender");
                    }
                    else if (stock.Price <= buyPrice)
                    {
                        Console.WriteLine($"Preço atual do ativo {ticker}: {stock.Price}. O preço de venda é {sellPrice} e o preço de compra é {buyPrice}");
                        email.SendEmailMessage(settings.Sender!, settings.Recipients!, stock, ticker, "vender");
                    }

                    await Task.Delay(settings.Api!.Delay!);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

