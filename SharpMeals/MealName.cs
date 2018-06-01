using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpMeals
{
    public class MealName
    {
        public string TurkishName { get; private set; }
        public string EnglishName { get; private set; }

        public MealName()
        {

        }

        public MealName(string tr, string eng)
        {
            TurkishName = tr;
            EnglishName = eng;
        }


        public static IEnumerable<string> GetMealsByLanguage(IEnumerable<MealName> mealNames, Language lang = Language.TURKISH)
        {
            var result = new List<string>();
            
            if(lang == Language.TURKISH)
                result.AddRange(mealNames.Select( m => m.TurkishName ));
            else
                result.AddRange(mealNames.Select( m => m.EnglishName ));

            return result;
        }
    }

    public enum Language
    {
        TURKISH, 
        ENGLISH
    }
}