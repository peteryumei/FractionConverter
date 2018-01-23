using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionConverter
{
    public static class Converter
    {
        public static string Convert(decimal pvalue, bool skip_rounding = true)
        {
            decimal value = pvalue;
            decimal dplaces = (decimal)0.03125;

            if (!skip_rounding)
                value = Converter.DecimalRound(pvalue, dplaces);

            if (value == Math.Round(value, 0)) // whole number check
                return value.ToString();

            // get the whole value of the fraction
            decimal mWhole = Math.Truncate(value);

            // get the fractional value
            decimal mFraction = value - mWhole;

            // initialize a numerator and denomintar
            uint mNumerator = 0;
            uint mDenomenator = 1;

            // ensure that there is actual a fraction
            if (mFraction > 0m)
            {
                // convert the value to a string 
                string strFraction = mFraction.ToString().Remove(0, 2);

                // store the number of decimal places
                uint intFractLength = (uint)strFraction.Length;

                // set the numerator to have the proper amount of zeros
                mNumerator = (uint)Math.Pow(10, intFractLength);

                // parse the fraction value to an integer that equals
                // [fraction value] * 10^[number of decimal places]
                uint.TryParse(strFraction, out mDenomenator);

                // get the greatest common divisor for both numbers
                uint gcd = GreatestCommonDivisor(mDenomenator, mNumerator);

                // divide the numerator and the denominator by the greatest common divisor
                mNumerator = mNumerator / gcd;
                mDenomenator = mDenomenator / gcd;
            }
    
            StringBuilder mBuilder = new StringBuilder();
            // add the whole number if it's greater than 0
            if (mWhole > 0m)
            {
                mBuilder.Append(mWhole);
            }

            // add the fraction if it's greater than 0m
            if (mFraction > 0m)
            {
                if (mBuilder.Length > 0)
                {
                    mBuilder.Append(" ");
                }

                mBuilder.Append(mDenomenator);
                mBuilder.Append("/");
                mBuilder.Append(mNumerator);
            }

            return mBuilder.ToString();
        }

        private static uint GreatestCommonDivisor(uint valA, uint valB)
        {
            if (valA == 0 &&
              valB == 0)
            {
                return 0;
            }
            else if (valA == 0 &&
                  valB != 0)
            {
                return valB;
            }
            else if (valA != 0 && valB == 0)
            {
                return valA;
            }
            // actually find the GSD
            else
            {
                uint first = valA;
                uint second = valB;

                while (first != second)
                {
                    if (first > second)
                    {
                        first = first - second;
                    }
                    else
                    {
                        second = second - first;
                    }
                }

                return first;
            }
        }

        private static decimal DecimalRound(decimal val, decimal places)
        {
            string sPlaces = Converter.Convert(places, true);
            string[] s = sPlaces.Split('/');

            if (s.Count() == 2)
            {
                int nPlaces = System.Convert.ToInt32(s[1]);
                decimal d = Math.Round(val * nPlaces);
                return d / nPlaces;
            }

            return val;
        }
    }


}
