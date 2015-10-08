using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NorwegianVehicleNet
{
    public class RegistrationNumber
    {
        private const string LettersRegexPattern = "[a-z|A-Z]{1,2}";
        private const string NumbersRegexPattern = @"\d{4,5}";

        private const string InvalidLettersErrorMessage = "Invalid letters";
        private const string InvalidNumbersErrorMessage = "Invalid numbers";

        public RegistrationNumber(string letters, int numbers)
        {
            if (IsValidLetters(letters)) throw new ArgumentException(InvalidLettersErrorMessage);

            if (IsValidNumbers(numbers)) throw new ArgumentException(InvalidNumbersErrorMessage);

            this.Letters = letters;
            this.Numbers = numbers;
        }

        public RegistrationNumber(string registration)
        {
            var letters = Regex.Match(registration, LettersRegexPattern).Value;
            var numbers = int.Parse(Regex.Match(registration, NumbersRegexPattern).Value);

            if (IsValidLetters(letters)) throw new ArgumentException(InvalidLettersErrorMessage);

            if (IsValidNumbers(numbers)) throw new ArgumentException(InvalidNumbersErrorMessage);

            this.Letters = letters;
            this.Numbers = numbers;
        }

        private bool IsValidLetters(string letters) => Regex.Match(letters, LettersRegexPattern).Value != letters;

        private bool IsValidNumbers(int numbers) => Regex.Match(numbers.ToString(), NumbersRegexPattern).Value != numbers.ToString();

        public string Letters { get; private set; }
        public int Numbers { get; private set; }

        public override string ToString() => Letters + Numbers.ToString();

        public static implicit operator string(RegistrationNumber rn) => rn.ToString();

        public static implicit operator RegistrationNumber(string s) => new RegistrationNumber(s);
    }
}
