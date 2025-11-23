using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Frontend.Service;

namespace Frontend.Controllers
{

    public class YieldController : Controller
    {
        private readonly YieldPredictionService _predictor;

        public YieldController(YieldPredictionService predictor)
        {
            _predictor = predictor;
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

            double predictedYield = await _predictor.PredictYieldAsync(input);

            ViewBag.Result = $"{predictedYield:F2} tons/hectare";

            return View(input);
        }
    }

}
