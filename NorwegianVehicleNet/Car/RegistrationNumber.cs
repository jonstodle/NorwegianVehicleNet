using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NorwegianVehicleNet.Car
{
    public class RegistrationNumber
    {
        private const string LettersRegexPattern = "[a-z|A-Z]{1,2}";
        private const string digitsRegexPattern = @"\d{4,5}";

        private const string InvalidLettersErrorMessage = "Invalid letters";
        private const string InvalidDigitsErrorMessage = "Invalid digits";

        /// <summary>
        /// Instantiates a new object
        /// </summary>
        /// <param name="letters">A string containing the letters of the registration number</param>
        /// <param name="digits">An int containing the digits of the registration number</param>
        public RegistrationNumber(string letters, int digits)
        {
            if (IsValidLetters(letters)) throw new ArgumentException(InvalidLettersErrorMessage);

            if (IsValidDigits(digits)) throw new ArgumentException(InvalidDigitsErrorMessage);

            this.Letters = letters;
            this.digits = digits;
        }

        /// <summary>
        /// Instantiates a new object
        /// </summary>
        /// <param name="registration">A string containing both letters and digits representing a registration number</param>
        public RegistrationNumber(string registration)
        {
            var letters = Regex.Match(registration, LettersRegexPattern).Value;
            var digits = int.Parse(Regex.Match(registration, digitsRegexPattern).Value);

            if (IsValidLetters(letters)) throw new ArgumentException(InvalidLettersErrorMessage);

            if (IsValidDigits(digits)) throw new ArgumentException(InvalidDigitsErrorMessage);

            this.Letters = letters;
            this.digits = digits;
        }

        /// <summary>
        /// Checks whether a string only contains valid registration number letters
        /// </summary>
        /// <param name="letters">A string to check</param>
        /// <returns>A bool indicating whether the letters are valid</returns>
        private bool IsValidLetters(string letters) => Regex.Match(letters, LettersRegexPattern).Value != letters;

        /// <summary>
        /// Check whether an int only contains valid registration number digits
        /// </summary>
        /// <param name="digits">An int to check</param>
        /// <returns>A bool indicating whether the digits are valid</returns>
        private bool IsValidDigits(int digits) => Regex.Match(digits.ToString(), digitsRegexPattern).Value != digits.ToString();

        public string Letters { get; private set; }
        public int digits { get; private set; }

        /// <summary>
        /// Get a string representation of the registration number
        /// </summary>
        /// <returns>String containing both letters and digits</returns>
        public override string ToString() => Letters + digits.ToString();

        /// <summary>
        /// Implicitly converts an object into a string by using the ToString() method
        /// </summary>
        /// <param name="rn">The object to convert</param>
        public static implicit operator string(RegistrationNumber rn) => rn.ToString();

        /// <summary>
        /// Implicitly converts a string into a new object
        /// </summary>
        /// <param name="s">The string to convert</param>
        public static implicit operator RegistrationNumber(string s) => new RegistrationNumber(s);
    }
}
