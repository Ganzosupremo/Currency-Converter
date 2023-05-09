using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Currency_Converter
{
    public class Rate
    {
        /// <summary>
        /// Euro
        /// </summary>
        public double EUR { get; set; }
        /// <summary>
        /// U.S Dollar
        /// </summary>
        public double USD { get; set; }
        /// <summary>
        /// Bitcoin
        /// </summary>
        public double BTC { get; set; }
        /// <summary>
        /// Japanese Yen
        /// </summary>
        public double JPY { get; set; }
        /// <summary>
        /// Guatemalan Quetzal
        /// </summary>
        public double GTQ { get; set; }
        /// <summary>
        /// Danish Krone
        /// </summary>
        public double DKK { get; set; }
        /// <summary>
        /// Egyptian Pound
        /// </summary>
        public double EGP { get; set; }
        /// <summary>
        /// British Pound Sterling
        /// </summary>
        public double GBP { get; set; }
        /// <summary>
        /// Philippine Peso. Not the Programming Language
        /// </summary>
        public double PHP { get; set; }

        public static async UniTask<ExchangeRate> GetRates<T>(string url)
        {
            ExchangeRate exchangeRate = new ExchangeRate();

            try
            {
                using (var client = new HttpClient())
                {
                    // Time to wait before the request timeouts
                    client.Timeout = TimeSpan.FromMinutes(1);
                    HttpResponseMessage message = await client.GetAsync(url);

                    // Check the API response
                    if (message.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Serialize the HTTP content to a string
                        string ResponseString = await message.Content.ReadAsStringAsync();
                        // Deserialize the response string
                        ExchangeRate ResponseObject = JsonConvert.DeserializeObject<ExchangeRate>(ResponseString);

                        //MessageBox.Show($"Timestamp {ResponseObject.timestamp}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Return the API response
                        return ResponseObject;
                    }
                    return exchangeRate;
                }
            }
            catch (Exception)
            {
                return exchangeRate;
            }
        }
    }
}
