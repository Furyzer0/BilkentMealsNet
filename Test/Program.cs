using System;
using SharpMeals;
using HtmlAgilityPack;

namespace Test
{
    class Program
    {
        public const string URL = "http://kafemud.bilkent.edu.tr/monu_eng.html";


        static void Main(string[] args)
        {
            var scrapper = new MealScrapper();
            
            using(var file = System.IO.File.Create("doc.html"))
            using(var writer = new System.IO.StreamWriter(file))
            {
                foreach (var fix in scrapper.FixMenu)
                    writer.Write(fix.InnerText);
                foreach (var alternative in scrapper.AlternativeMenu)
                    writer.Write(alternative.InnerText);
            }
        }
    }
}
