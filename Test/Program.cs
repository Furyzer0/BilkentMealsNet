using System;
using System.Collections.Generic;
using System.Linq;
using SharpMeals;
using HtmlAgilityPack;

namespace Test
{
    class Program
    {
        public const string URL = "http://kafemud.bilkent.edu.tr/monu_eng.html";


        static void Main(string[] args)
        {
            TestScrapper();
            TestFactory();
        }

        private static void TestScrapper()
        {
            var scrapper = new MealScrapper();
            //var fixLunchTds = scrapper.FixMenu[1].SelectNodes("./td");
            //Console.WriteLine(fixLunchTds[0].SelectSingleNode(".//b").InnerText);                            var 
            var meals = scrapper.FixMenu.Concat(scrapper.AlternativeMenu);

            using(var file = System.IO.File.Create("context.txt"))
            using(var writer = new System.IO.StreamWriter(file))
            {
                foreach (var meal in meals)
                    writer.Write(meal.InnerText);   
            }

            using(var file = System.IO.File.Create("doc.html"))
            using(var writer = new System.IO.StreamWriter(file, System.Text.Encoding.UTF8))
            {
                
                foreach (var doc in meals) 
                    writer.Write(doc.OuterHtml);                        
            }
        }

        private static void TestFactory()
        {
            var factory = new MealFactory();

            using(var file = System.IO.File.Create("meals.txt"))
            using(var writer = new System.IO.StreamWriter(file, System.Text.Encoding.UTF8))
            {
                factory.mealList.ForEach( m => writer.Write(m.ToString()) );
            }
        }
    }
}
