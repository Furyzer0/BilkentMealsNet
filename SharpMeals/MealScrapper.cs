using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;

namespace SharpMeals
{
    public class MealScrapper
    {
        private const string URL = "http://kafemud.bilkent.edu.tr/monu_eng.html";
        public HtmlNodeCollection FixMenu { get; private set; }
        public HtmlNodeCollection AlternativeMenu { get; private set; }

        public MealScrapper()
        {
            var client = new WebClient();
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(client.OpenRead(URL), Encoding.UTF8);

            Console.WriteLine(htmlDoc.Encoding);
            //Console.OutputEncoding = Encoding.UTF8;
            //Console.WriteLine(htmlDoc.DocumentNode.OuterHtml);
            var mealContainers = htmlDoc.DocumentNode.SelectNodes("//table[@class='icerik']/tr");
            FixMenu = mealContainers[1].SelectNodes(".//table/tr");
            AlternativeMenu = mealContainers[2].SelectNodes(".//table/tr");
        }
    }
}
