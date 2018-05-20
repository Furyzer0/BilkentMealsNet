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
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(URL);
            var mealContainers = htmlDoc.DocumentNode.SelectNodes("//table[@class='icerik']/tbody"); //To Do: test this xpath
            FixMenu = mealContainers[1].SelectNodes("/tr[2]//table/tbody/tr");
            AlternativeMenu = mealContainers[2].SelectNodes("/tr[3]//table/tbody/tr");
        }
    }
}
