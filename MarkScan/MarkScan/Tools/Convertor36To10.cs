using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MarkScan.Tools
{
    internal static class Convertor36To10String
    {
        internal static string Convert(string code)
        {
            BigInteger bigIntFromDouble = new BigInteger(0);

            double n = 0;

            int count = code.Length - 1;

            foreach (var c in code)
            {
                if (c >= '0' && c <= '9')
                    n = (double)(c - '0') * (double)Math.Pow(36, count);
                else if (c >= 'A' && c <= 'Z')
                    n = (double)(c - 'A' + 10) * (double)Math.Pow(36, count);
                else if (c >= 'a' && c <= 'z')
                    n = (double)(c - 'a' + 10) * (double)Math.Pow(36, count);
                else
                    break;

                bigIntFromDouble = bigIntFromDouble + new BigInteger(n);

                n = 0;
                count--;
            }
            return bigIntFromDouble.ToString();
        }
    }
}
