using System;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace SharpMeals
{
    public class MealScrapper
    {
        public const string URL = "http://kafemud.bilkent.edu.tr/monu_eng.html";
        public HtmlNode fixMenu { get; private set; }
        public HtmlNode alternativeMenu { get; private set; }

        public MealScrapper()
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(URL);
            var mealContainers = htmlDoc.DocumentNode.SelectNodes("//table[@class='icerik']/tbody"); //To Do: test this xpath
            fixMenu = mealContainers[1].SelectSingleNode("/tr[2]//table/tbody/tr");
            alternativeMenu = mealContainers[2].SelectSingleNode("/tr[3]//table/tbody/tr");
        }
    }
}
