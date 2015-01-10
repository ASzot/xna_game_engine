#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Threading;

namespace BaseLogic.Core
{
    /// <summary>
    ///
    /// </summary>
    public static class PrimitiveTypeHelper
    {
        /// <summary>
        /// Gets the decimal part.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        public static int GetDecimalPart(this double d)
        {
            string decimalPartStr = d.ToString("F5").Split('.')[1].Substring(0, 5);

            return int.Parse(decimalPartStr);
        }

        /// <summary>
        /// Gets the number of decimals.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">You fool there are no decimals in this string.</exception>
        public static int GetNumberOfDecimals(this float d)
        {
            string dStr = d.ToString();

            int decimalIndex = -1;
            for (int i = 0; i < dStr.Length; ++i)
            {
                if (dStr[i] == '.')
                {
                    decimalIndex = i;
                }
            }

            if (decimalIndex == -1)
                throw new ArgumentException("You fool there are no decimals in this string.");

            string afterDecimalStr = dStr.Substring(decimalIndex, dStr.Length - decimalIndex);

            return afterDecimalStr.Length;
        }

        /// <summary>
        /// Gets the whole part.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        public static int GetWholePart(this double d)
        {
            return (int)d;
        }

        /// <summary>
        /// Shuffles the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class ThreadSafeRandom
    {
        /// <summary>
        /// The local
        /// </summary>
        [ThreadStatic]
        private static Random Local;

        /// <summary>
        /// Gets the this threads random.
        /// </summary>
        /// <value>
        /// The this threads random.
        /// </value>
        public static Random ThisThreadsRandom
        {
            get
            {
                return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));
            }
        }

        /// <summary>
        /// Nexts the double.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public static double NextDouble(double min, double max)
        {
            return ThisThreadsRandom.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Nexts the int.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public static int NextInt(int min, int max)
        {
            return ThisThreadsRandom.Next(min, max);
        }
    }
}