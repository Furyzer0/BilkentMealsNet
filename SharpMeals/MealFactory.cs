using System;
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
        private const int ROW_START = 1; //First row is placeholder
        private const int ROW_COUNT = 15;
        private const int ROW_SPAN = 2;

        public MealFactory()
        {
            mealList = new List<Meal>();
            var scrapper = new MealScrapper();

            for(int i = ROW_START; i < ROW_COUNT; i += ROW_SPAN)
            {
                var meal = new Meal();
                var fixLunchTds = scrapper.FixMenu[i].SelectNodes("./td"); 
                var fixDinnerTds = scrapper.FixMenu[i + 1].SelectNodes("./td");
                var alternativeTds = scrapper.AlternativeMenu[1 + (i / 2)].SelectNodes("/td");
                var date = fixLunchTds[0].SelectSingleNode(".//b").InnerText;
                meal.Date = date;
                meal.Lunch = ScrapeMeals(fixLunchTds[1]);
                meal.Dinner = ScrapeMeals(fixDinnerTds[0]);
                meal.Alternative = ScrapeMeals(alternativeTds[1]);
                
                var replaced = Regex.Replace(fixLunchTds[2].ToString(), "[^\\d\\s]", "").Trim();    
                var nutritions = Regex.Replace(replaced, "\\s{2,}", " ").Split(' ');
                meal.NutritionFacts.EnergyByCal = Int32.Parse(nutritions[0]);
                meal.NutritionFacts.CarbohydratePercentage = Int32.Parse(nutritions[1]);
                meal.NutritionFacts.ProteinPercentage = Int32.Parse(nutritions[2]);
                meal.NutritionFacts.FatPercentage = Int32.Parse(nutritions[3]);

                mealList.Add(meal);
            }
        }

        //To Do: test if working
        private IList<string[]> ScrapeMeals(HtmlNode nodes)
        {
            var result = new List<string[]>();
            var lines = nodes.InnerText.Replace("&nbsp;", ", ").Split( ("<br>").ToCharArray() );
            for(int i = 1; i < lines.Length; ++i) 
            {
                var line = lines[i];

                if(line.Contains("veya / or") && lines.Length == 5)
                {
                    var secondLine = line
                                        .Replace("veya / or veya / or", "veya / or")
                                        .Split("veya / or".ToCharArray());
                    for(int j = 0; j < 2; ++j) {
                        var secondLineSplitted = secondLine[i].Split('/');
                        var food = new string[] {   
                            secondLineSplitted[0].Trim(),
                            secondLineSplitted[1].Trim()
                        };
                        result.Add(food);
                    }
                }
                else 
                {
                    var lineSplitted = line.Replace("veya / or", ", ").Split('/');
                    var food = new string[] {
                        lineSplitted[0].Trim(),
                        lineSplitted[1].Trim()
                    };
                    result.Add(food);
                }
            }

            return result;
        }
    }
}