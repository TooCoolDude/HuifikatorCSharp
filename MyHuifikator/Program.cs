using System.Text.RegularExpressions;

namespace MyHuifikator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Хуификатор локализации War Thunder by TooCoolDude");
            Console.WriteLine(@"Введите расположение файлов локализации (...\WarThunder\lang):");
            string path = Console.ReadLine();
            Console.WriteLine("Хуифицируем...");
            var files = Directory.GetFiles(@$"{path}");
            foreach (var file in files)
            {
                var input = File.ReadAllText(file);
                string hui = Huificator.Huificate(input);
                File.WriteAllText(file, hui);
            }
            Console.WriteLine("Теперь всё ахуенно");
            Console.ReadKey();
            //Console.WriteLine(Huificator.Huificate(File.ReadAllText("E:\\Старый ЖД\\AllHuinya\\localiz\\testLoc\\menu.csv")));
        }
}
}