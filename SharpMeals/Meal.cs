using System;
using System.Collections.Generic;
using System.Text;

namespace SharpMeals
{
    public class Meal
    {
        public Meal()
        {
            NutritionFacts = new _NutritionFacts();
        }
        public string Date { get; set; }
        public IList<MealName> Lunch { get; internal set; }
        public IList<MealName> Dinner { get; internal set; }
        public IList<MealName> Alternative { get; internal set; } 
        public _NutritionFacts NutritionFacts { get; internal set; }
        public class _NutritionFacts
        {
            public int EnergyByCal { get; internal set; }
            public int CarbohydratePercentage { get; internal set; }
            public int ProteinPercentage { get; internal set; }
            public int FatPercentage { get; internal set; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Date: "); 
            sb.AppendLine(Date);
            sb.AppendLine("Lunch meals: ");
            foreach(var name in Lunch) //To Do: make this a function call and call it for each lunch, dinner and alternative
                sb.AppendLine(name.TurkishName + " (" + name.EnglishName + ")");
            sb.AppendLine("Dinner meals: ");
            foreach(var name in Dinner)
                sb.AppendLine(name.TurkishName + " (" + name.EnglishName + ")");
            sb.AppendLine("Alternative meals: ");
            foreach(var name in Alternative)
                sb.AppendLine(name.TurkishName + " (" + name.EnglishName + ")");

            return sb.ToString();
        }
    }
}