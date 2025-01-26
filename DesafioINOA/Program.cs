// See https://aka.ms/new-console-template for more information
using DesafioINOA;

namespace DesafioInoaAlert
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("informe o ticker do ativo como argumento.");
                return;
            }

            string ticker = args[0];

            BrapiAPI api = new BrapiAPI();
            Stock stock = await api.GetPrice(ticker);

            if (stock.Price > 0)
            {
                Console.WriteLine($"Preço atual do ativo {ticker}: {stock.Price}");
            }
            else
            {
                Console.WriteLine("Não foi possível obter o preço do ativo.");
            }
        }
    }
}

