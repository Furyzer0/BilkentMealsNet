using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace SharpMeals
{
    public class MealFactory
    {
        public List<Meal> mealList { get; private set; }

        public Meal MealOfToday 
        {
            get 
            {
                DateTime now = DateTime.Now;
                string today = now.ToString("dd/MM");
                foreach(var meal in mealList)
                {
                    if(meal.Date == today)
                        return meal;
                }   

                return null;
            }
        }
        public MealFactory()
        {
            mealList = new List<Meal>();
            var scrapper = new MealScrapper();
            

            for(int i = 1; i < 15; i += 2)
            {
                var meal = new Meal();
                var fixLunchTds = scrapper.FixMenu[i].SelectNodes("./td"); 
                var fixDinnerTds = scrapper.FixMenu[i + 1].SelectNodes("./td");
                var alternativeTds = scrapper.AlternativeMenu[1 + (i / 2)].SelectNodes("./td");
                var date = fixLunchTds[0].SelectSingleNode(".//b").InnerText;
                meal.Date = date;
                meal.Lunch = ScrapeMeals(fixLunchTds[1]);
                meal.Dinner = ScrapeMeals(fixDinnerTds[0]);
                meal.Alternative = ScrapeMeals(alternativeTds[1]);
                
                /*var replaced = Regex.Replace(fixLunchTds[2].ToString(), "[^\\d\\s]", "").Trim();    
                var nutritions = Regex.Replace(replaced, "\\s{2,}", " ").Split(' ');
                meal.NutritionFacts.EnergyByCal = Int32.Parse(nutritions[0]);
                meal.NutritionFacts.CarbohydratePercentage = Int32.Parse(nutritions[1]);
                meal.NutritionFacts.ProteinPercentage = Int32.Parse(nutritions[2]);
                meal.NutritionFacts.FatPercentage = Int32.Parse(nutritions[3]);*/

                mealList.Add(meal);
            }
        }
        private IList<MealName> ScrapeMeals(HtmlNode node)
        {
            var result = new List<MealName>();
            var htmlDoc = new HtmlDocument();
            var lines = node.OuterHtml
                            .Replace("&nbsp", "")
                            .Replace("\n", "")
                            .Split(new[]{"<br>"}, StringSplitOptions.None);
            foreach(var line in lines)
            {
                htmlDoc.LoadHtml(line);
                var text = htmlDoc.DocumentNode.InnerText;
                if(text.Contains("veya / or") && lines.Length == 5) //fix menu
                {
                    var mealNames = new MealName[2];
                    var textSplitted = text.Split(new []{"veya / or"}, StringSplitOptions.None);
                    var meal0 = textSplitted[0].Split('/');
                    var meal1 = textSplitted[1].Split('/');
                    mealNames[0] = new MealName(meal0[0].Trim(), meal0[1].Trim());
                    mealNames[1] = new MealName(meal1[0].Trim(), meal1[1].Trim());

                    result.AddRange(mealNames);
                }
                else 
                {
                    var meal = text.Split('/');
                    result.Add( 
                        new MealName(
                            meal[0].Trim(),
                            meal[1].Trim()
                        )
                    );
                }
            }

            return result;
        }
    }
}