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

            // 1. Actual model prediction
            double predictedYield = await _predictor.PredictYieldAsync(input);

            ViewBag.PredictedYield = $"{predictedYield:F2} tons/hectare";
            ViewBag.YieldValue = predictedYield;

            // 2. Yield after recommendations
            double improvedYield = _advisor.GetImprovedYield(predictedYield, input);
            ViewBag.ImprovedYield = improvedYield;

            // Categories
            ViewBag.YieldCategory = _advisor.GetYieldCategory(predictedYield);

            // Textual Recommendations
            ViewBag.Recommendations = _advisor.GetRecommendations(input, predictedYield);


            return View(input);
        }

    }
}
