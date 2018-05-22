using System;
using System.Collections.Generic;

namespace SharpMeals
{
    public class Meal
    {
        public string Date { get; internal set; }
        public IList<string[]> Lunch { get; internal set; }
        public IList<string[]> Dinner { get; internal set; }
        public IList<string[]> Alternative { get; internal set; } 
        internal _NutritionFacts NutritionFacts { get; set; }
        internal class _NutritionFacts
        {
            public int EnergyByCal { get; internal set; }
            public int CarbohydratePercentage { get; internal set; }
            public int ProteinPercentage { get; internal set; }
            public int FatPercentage { get; internal set; }
        }
    }
}