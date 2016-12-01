using System;
using System.Collections.Generic;

namespace SecuredMail.DataGen
{
    /// <summary>
    /// Calculation logic
    /// </summary>
    public class Calculation
    {
        public static IEnumerable<int> RunEratosthenesSieve(int upperBound)
        {
            int upperBoundSquareRoot = (int)Math.Sqrt(upperBound);
            bool[] isComposite = new bool[upperBound + 1];

            for (int m = 2; m <= upperBoundSquareRoot; m++)
            {
                if (!isComposite[m])
                {
                    yield return m;

                    for (int k = m * m; k <= upperBound; k += m)
                        isComposite[k] = true;
                }
            }

            for (int m = upperBoundSquareRoot; m <= upperBound; m++)
            {
                if (!isComposite[m])
                {
                    yield return m;
                }
            }
        }
    }
}
