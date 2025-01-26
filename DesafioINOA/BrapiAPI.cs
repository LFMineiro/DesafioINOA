using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DesafioINOA
{
    public class Stock
    {
        public decimal Price { get;  }
        public Stock(decimal price)
        {
           Price = price;
        }
    }

    public class BrapiAPI
    {
        private const string token = "fRWFrtT59zdC6i33ekJbEL";
        
        public async Task<Stock> GetPrice(string ticker)
        {
            Stock stock;
            decimal price = 0.0M;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    string requestUrl = requestUrl = $"https://brapi.dev/api/quote/{ticker}?token={token}";

                    HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                    try
                    {
                        response.EnsureSuccessStatusCode();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error Requesting API:\n" + ex.Message);
                    }

                    string responseContent = await response.Content.ReadAsStringAsync();

                    var json = JsonNode.Parse(responseContent)!.AsObject();

                    var priceString = json["results"]?[0]?["regularMarketPrice"]?.ToString();
                    price = Convert.ToDecimal(priceString, new CultureInfo("en-US"));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            stock = new Stock(price);

            return stock;

        }
        }

    }

