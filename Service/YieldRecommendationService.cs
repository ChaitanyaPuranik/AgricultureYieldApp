using Frontend.Models;

namespace Frontend.Service
{
    public class YieldRecommendationService
    {
        public string GetYieldCategory(double yield)
        {
            if (yield >= 5)
                return "Excellent Yield";
            else if (yield >= 3.5)
                return "Moderate Yield";
            else
                return "Low Yield";
        }

        public List<string> GetRecommendations(YieldInput input, double predictedYield)
        {
            var list = new List<string>();

            // --- Condition-based recommendations ---
            if (input.Soil_Type == "Sandy")
                list.Add("Sandy soil loses nutrients quickly — add compost or organic fertilizer.");

            if (!input.Fertilizer_Used)
                list.Add("Consider applying fertilizer to improve soil nutrient levels.");

            if (!input.Irrigation_Used && input.Weather_Condition == "Sunny")
                list.Add("No irrigation under sunny conditions — consider drip or sprinkler irrigation.");

            if (input.Rainfall_mm < 120)
                list.Add("Rainfall is low — additional irrigation is recommended.");

            if (input.Rainfall_mm > 500)
                list.Add("Heavy rainfall — consider drainage to avoid root diseases.");

            if (input.Temperature_Celsius > 35)
                list.Add("High temperature — ensure adequate irrigation to reduce heat stress.");

            if (input.Temperature_Celsius < 15)
                list.Add("Low temperature — consider temperature-resistant crop varieties.");

            // --- Yield-based fallback recommendations ---
            if (predictedYield < 3.5)
                list.Add("Yield is low — consider improving soil fertility and irrigation scheduling.");

            if (predictedYield < 3)
                list.Add("Very low yield — consider crop rotation or switching to a more suitable crop.");

            // --- If nothing triggered, still give general advice ---
            if (list.Count == 0)
            {
                list.Add("Your field conditions look stable. Continue with balanced fertilizer use.");
                list.Add("Monitor weather patterns and adjust irrigation scheduling accordingly.");
            }

            return list;
        }

        public double GetImprovedYield(double currentYield, YieldInput input)
        {
            double improvedYield = currentYield;

            if (!input.Fertilizer_Used)
                improvedYield *= 1.15;

            if (!input.Irrigation_Used)
                improvedYield *= 1.20;

            if (input.Soil_Type == "Sandy")
                improvedYield *= 1.10;

            if (input.Temperature_Celsius > 35 && !input.Irrigation_Used)
                improvedYield *= 1.08;

            if (input.Temperature_Celsius < 15)
                improvedYield *= 1.05;

            return Math.Round(improvedYield, 2);
        }
    }
}
