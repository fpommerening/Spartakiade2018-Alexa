
using System;
using System.Net.Http;

namespace Function
{
    public class FunctionHandler
    {
        public void Handle(string input)
        {
            string url = Environment.GetEnvironmentVariable("relay_target_url");

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.PostAsync(url, new StringContent(input)).Result)
                using (HttpContent content = response.Content)
                {
                    string result = content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }
            }
        }
    }
}
