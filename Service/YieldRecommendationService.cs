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

        public List<string> GetRecommendations(YieldInput input)
        {
            var list = new List<string>();

            if (input.Soil_Type == "Sandy")
                list.Add("Sandy soil loses nutrients quickly — add compost or organic fertilizer.");

            if (!input.Fertilizer_Used)
                list.Add("Consider using fertilizers for improved plant nutrition.");

            if (!input.Irrigation_Used && input.Weather_Condition == "Sunny")
                list.Add("Sunny weather and no irrigation — water stress may reduce yield.");

            if (input.Rainfall_mm < 120)
                list.Add("Low rainfall — consider drip irrigation.");

            if (input.Rainfall_mm > 500)
                list.Add("High rainfall — monitor for fungal infections.");

            if (input.Temperature_Celsius > 35)
                list.Add("Very high temperature detected — ensure proper irrigation.");

            if (input.Temperature_Celsius < 15)
                list.Add("Low temperature — choose cold-resistant crop varieties.");

            return list;
        }
    }

}
