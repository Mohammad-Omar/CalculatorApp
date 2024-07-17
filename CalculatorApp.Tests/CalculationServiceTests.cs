using CalculatorApp.Services;
using Xunit;

namespace CalculatorApp.Tests {
    public class CalculationServiceTests {
        private readonly ICalculationService _calculationService;

        public CalculationServiceTests() {
            _calculationService = new CalculationService();
        }

        [Fact]
        public void Evaluate_Addition_ReturnsCorrectResult() {
            // Arrange
            string expression = "1 + 2";

            // Act
            double result = _calculationService.Evaluate(expression);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Evaluate_ComplexExpression_ReturnsCorrectResult() {
            // Arrange
            string expression = "(1 * 2) + 3 + (7 / 10)";

            // Act
            double result = _calculationService.Evaluate(expression);

            // Assert
            Assert.Equal(5.7, result, precision: 1);
        }

        [Fact]
        public void Evaluate_InvalidExpression_ThrowsArgumentException() {
            // Arrange
            string expression = "1==4-6=4";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculationService.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_InvalidExpressionWithOperators_ThrowsArgumentException() {
            // Arrange
            string expression = "1+-67+87/2";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculationService.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_ExpressionWithUnbalancedParentheses_ThrowsArgumentException() {
            // Arrange
            string expression = "((2 + 3)";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculationService.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_ExpressionWithConsecutiveOperators_ThrowsArgumentException() {
            // Arrange
            string expression = "2++2";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculationService.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_ExpressionWithMultipleOperatorsInRow_ThrowsArgumentException() {
            // Arrange
            string expression = "1---2";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculationService.Evaluate(expression));
        }

        [Fact]
        public void Evaluate_ExpressionWithMixedOperators_ThrowsArgumentException() {
            // Arrange
            string expression = "1+2*/3";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculationService.Evaluate(expression));
        }
    }
}