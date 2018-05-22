using System;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace SharpMeals
{
    public class MealScrapper
    {
        public const string URL = "http://kafemud.bilkent.edu.tr/monu_eng.html";
        public HtmlNodeCollection FixMenu { get; private set; }
        public HtmlNodeCollection AlternativeMenu { get; private set; }

        public MealScrapper()
        {
            var web = new HtmlWeb();
            var htmlDoc = web.Load(URL);
            var mealContainers = htmlDoc.DocumentNode.SelectNodes("//table[@class='icerik']/tr");
            FixMenu = mealContainers[1].SelectNodes(".//table/tr");
            AlternativeMenu = mealContainers[2].SelectNodes(".//table/tr");
        }
    }
}
