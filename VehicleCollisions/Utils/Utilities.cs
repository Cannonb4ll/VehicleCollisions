using System;
using System.Security.Cryptography;

namespace VehicleCollisions.Utils
{
    internal class Utilities
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

        public static bool RandomBool(int truePercentage = 50)
        {
            return Between(0, 100) < truePercentage;
        }

        public static int Between(int minimumValue, int maximumValue)
        {
            var randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            var asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            var multiplier = Math.Max(0, asciiValueOfRandomCharacter / 255d - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            var range = maximumValue - minimumValue + 1;

            var randomValueInRange = Math.Floor(multiplier * range);

            return (int) (minimumValue + randomValueInRange);
        }
    }
}