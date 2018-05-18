using System;
using System.Collections.Generic;

namespace SharpMeals
{
    public class Meal
    {
        public string Date { get; protected set; }
        public IReadOnlyCollection<string[]> Lunch { get; protected set; }
        public IReadOnlyCollection<string[]> Dinner { get; protected set; }
        public IReadOnlyCollection<string[]> Alternative { get; protected set; } 
        public _NutritionFacts NutritionFacts { get; protected set; }
        public class _NutritionFacts
        {
            public int EnergyByCal { get; protected set; }
            public int CarbohydratePercentage { get; protected set; }
            public int ProteinPercentage { get; protected set; }
            public int FatPercentage { get; protected set; }
        }
    }
}