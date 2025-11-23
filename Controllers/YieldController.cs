using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Frontend.Service;

namespace Frontend.Controllers
{
    public class YieldController : Controller
    {
        private readonly YieldPredictionService _predictor;
        private readonly YieldRecommendationService _advisor;

        public YieldController(
            YieldPredictionService predictor,
            YieldRecommendationService advisor)
        {
            _predictor = predictor;
            _advisor = advisor;
        }

        [HttpGet]
        public IActionResult Predict()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Predict(YieldInput input)
        {
            if (!ModelState.IsValid)
                return View(input);

            // 1. predict yield
            double predictedYield = await _predictor.PredictYieldAsync(input);

            // 2. return formatted result
            ViewBag.PredictedYield = $"{predictedYield:F2} tons/hectare";

            // 3. category (Good/Moderate/Low)
            ViewBag.YieldCategory = _advisor.GetYieldCategory(predictedYield);

            // 4. recommendations
            ViewBag.Recommendations = _advisor.GetRecommendations(input);

            // 5. for chart
            ViewBag.YieldValue = predictedYield;

            return View(input);
        }
    }
}
