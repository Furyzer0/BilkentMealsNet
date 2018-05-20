using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace SharpMeals
{
    public class MealFactory
    {
        public IList<Meal> mealList { get; private set; }
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
                var fixLunchTds = scrapper.FixMenu[i].SelectNodes("/td"); 
                var fixDinnerTds = scrapper.FixMenu[i + 1].SelectNodes("/td");
                var alternativeTds = scrapper.AlternativeMenu[1 + (i / 2)].SelectNodes("/td");
                string date = fixLunchTds[0].InnerText.Split(' ')[0];
            }
        }
    }
}