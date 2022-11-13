using System.Net;
using System.Text;
using Serilog;

namespace ExchangeSeq
{
    class Program
    {
        public static void Main(string[] args)
        {
            var course = 58.3d;
            var commission = 3.8d;

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Seq("http://localhost:5341/", apiKey: "Ie3LopFncjaACOhjYQAV")
            .CreateLogger();

            while (true)
            {
                Log.Information("Пользователь вводит количество долларов.");
                Console.Write("Введите количество долларов: ");
                var dollars = Console.ReadLine();

                double.TryParse(dollars, out var value);

                var roubles = value * course;

                if (value > 500.0d)
                {
                    Log.Information($"Пользователь ввёл {value}.");
                    Console.WriteLine("При курсе доллара = " + course + " руб. У Вас " + roubles.ToString("0.00") + " руб.");
                    Console.WriteLine("С комиссией сервиса " + commission + "% у вас останется " + (roubles - roubles * commission / 100).ToString("0.00") + " руб.");
                    Log.Information("Операция выполнена!");
                    break;
                }
                else if (value > 0.14d)
                {
                    Log.Information($"Пользователь ввёл {value}.");
                    Console.WriteLine("При курсе доллара = " + course + " руб. У Вас " + roubles.ToString("0.00") + " руб.");
                    Console.WriteLine("С комиссией сервиса 8 руб. у вас останется " + (roubles - 8.0).ToString("0.00") + " руб.");
                    Log.Information("Операция выполнена!");
                    break;
                }
                else
                {
                    Log.Error($"Пользователь ввёл слишком маленькое число!");
                    Console.WriteLine("Вы ввели слишком маленькое число!");
                }
            }
            Log.CloseAndFlushAsync();
        }
    }
}
