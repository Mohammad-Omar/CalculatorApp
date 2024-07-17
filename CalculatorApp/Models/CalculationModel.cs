using System.ComponentModel.DataAnnotations;

namespace CalculatorApp.Models {
    public class CalculationModel {
        public int Id { get; set; }

        [Display(Name = "Math Expression")]
        [Required(ErrorMessage = "Expression is required")]
        [RegularExpression(@"^[0-9+\-*/\s\(\)]+$",
            ErrorMessage = "Expression can only contain numbers, operators (+, -, *, /), and parentheses ( )")]
        public string MathExpression { get; set; }

        public double Result { get; set; }

        public DateTime Timestamp { get; set; }
    }
}