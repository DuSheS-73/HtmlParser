using System;
using System.Threading.Tasks;
using HTMLParser.Services;
using System.IO;
using HTMLParser.ConfigurationModels;
using System.Diagnostics;

namespace HTMLParser
{
    class Program
    {
        // Пока программа заточена на парсинг с сайта https://www.tomsguide.com/
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("{0} | Подготовка...", DateTime.Now);
                var parserService = new ParserService("https://www.tomsguide.com/reviews/samsung-galaxy-s21");
                var translatorService = new TranslatorService("ru");

                Console.WriteLine("{0} | Парсинг...", DateTime.Now);
                var body = await parserService.ParseAsync();

                Console.WriteLine("{0} | Перевод...", DateTime.Now);

                var bodyParts = body.Replace("\r", "").Split('\n');
                var translatedBody = await translatorService.TranslateAsync(bodyParts);


                Console.WriteLine("{0} | Запись в файл...", DateTime.Now);
                if (!Directory.Exists(AppSettings.ResultDirectoryPath))
                {
                    Directory.CreateDirectory(AppSettings.ResultDirectoryPath);
                }
                
                var resultPath = $"{AppSettings.ResultDirectoryPath}{DateTime.Now.ToString("ddMMyyhhmmss")}.txt";
                using (var writer = new StreamWriter(resultPath))
                {
                    writer.Write(translatedBody);
                }

                Console.WriteLine("{0} | Парсинг завершен", DateTime.Now);
                Console.WriteLine("Результат: {0}", resultPath);

                Process.Start("notepad.exe", resultPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} | Ошибка: {1}", DateTime.Now, ex.Message);
            }
            
            Console.ReadKey();
        }
    }
}
