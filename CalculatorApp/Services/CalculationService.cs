using System.Data;
using System.Globalization;

namespace CalculatorApp.Services {
    public class CalculationService : ICalculationService {
        public double Evaluate(string expression) {
            if (string.IsNullOrEmpty(expression)) throw new ArgumentException("Expression cannot be empty");

            if (!IsValidOperandOrder(expression) || !AreParenthesesBalanced(expression)) {
                throw new ArgumentException("Invalid expression");
            }

            try {
                var dataTable = new DataTable();
                var result = dataTable.Compute(expression, String.Empty);
                return Convert.ToDouble(result);
            }
            catch (Exception e) {
                throw new ArgumentException("Invalid expression", e);
            }
        }

        private bool IsValidOperandOrder(string expression) {
            for (int i = 0; i < expression.Length - 1; i++) {
                char current = expression[i];
                char next = expression[i + 1];

                if (IsOperator(current) && IsOperator(next)) return false;
            }

            return true;
        }

        private bool AreParenthesesBalanced(string expression) {
            int balance = 0;

            foreach (char c in expression) {
                if (c == '(') balance++;
                else if (c == ')') balance--;

                if (balance < 0) return false; // closing parenthesis without a matching opening parenthesis
            }

            return balance == 0;
        }

        private bool IsOperator(char c) => c == '+' || c == '-' || c == '*' || c == '/';
    }
}