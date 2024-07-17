using CalculatorApp.Models;
using CalculatorApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApp.Controllers {
    public class CalculatorController : Controller {
        private static List<CalculationModel> _calculationHistory = [];
        private ICalculationService _calculatorService;

        public CalculatorController(ICalculationService calculatorService) {
            _calculatorService = calculatorService;
        }

        [HttpGet]
        public IActionResult Calculate() {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(CalculationModel model) {
            if (ModelState.IsValid) {
                try {
                    var result = _calculatorService.Evaluate(model.MathExpression);
                    var calculation = new CalculationModel {
                        Id = _calculationHistory.Count + 1,
                        MathExpression = model.MathExpression,
                        Result = result,
                        Timestamp = DateTime.UtcNow
                    };
                    _calculationHistory.Add(calculation);
                    return View(calculation);
                }
                catch (ArgumentException ex) {
                    ModelState.AddModelError("MathExpression", ex.Message);
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult History() {
            return View(_calculationHistory);
        }
    }
}