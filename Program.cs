using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Інтеграція_з_сервісом_погоди
{
    class ResponseWeather
    {
        public ResponseTemperature Main {get; set;}
        public string Name { get; set; }
    }
    class ResponseTemperature
    {

        public double Temp { get; set; }
    }
    class Program
    {
        public static string NameKyiv = "Kyiv";
        public static string NameOdessa ="Odessa";
        public static double GetTemperature(string name)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={name}&units=metric&appid=01b687258940cf240adfebbbe043ac1a";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string responce;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                responce = streamReader.ReadToEnd(); 
            }
            ResponseWeather responseWeather = JsonConvert.DeserializeObject<ResponseWeather>(responce);
            Console.WriteLine("Temperature in {0}: {1} C", responseWeather.Name, responseWeather.Main.Temp);
            
            return responseWeather.Main.Temp;
        }
        static void Main(string[] args)
        {
            GetTemperature(NameKyiv);
            GetTemperature(NameOdessa);
        }
    }
}
