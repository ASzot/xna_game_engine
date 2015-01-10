#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;

namespace BaseLogic.Core
{
    /// <summary>
    ///
    /// </summary>
    public static class GameMath
    {
        /// <summary>
        /// The rand
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Angles the between.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns></returns>
        public static float AngleBetween(Vector3 v1, Vector3 v2)
        {
            return (float)Math.Atan2(v2.Z - v1.Z, v2.X - v1.X);
        }

        /// <summary>
        /// Gets the interpolation points.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="intervalDistance">The interval distance.</param>
        /// <returns></returns>
        public static Vector3[] GetInterpolationPoints(Vector3 from, Vector3 to, float intervalDistance)
        {
            Vector3 v = to - from;

            float vDist = (float)Math.Sqrt(v.LengthSquared());

            int numberOfIntervals = (int)Math.Round(vDist / intervalDistance);

            Vector3[] interpolationPoints = new Vector3[numberOfIntervals];
            for (int i = 0; i < numberOfIntervals; ++i)
            {
                float currentDist = intervalDistance * (float)i;

                float distancePerc = currentDist / vDist;

                Vector3 interpolatePoint = from + (distancePerc * v);

                interpolationPoints[i] = interpolatePoint;
            }

            return interpolationPoints;
        }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <returns></returns>
        public static Random GetRandom()
        {
            return rand;
        }

        /// <summary>
        /// Gets the random point inside bounds.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public static Vector3 GetRandomPointInsideBounds(Vector3 min, Vector3 max)
        {
            float x = (float)NextDoubleInRange(min.X, max.X);
            float y = (float)NextDoubleInRange(min.Y, max.Y);
            float z = (float)NextDoubleInRange(min.Z, max.Z);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Maps the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fromSetMin">From set minimum.</param>
        /// <param name="fromSetMax">From set maximum.</param>
        /// <param name="toSetMin">To set minimum.</param>
        /// <param name="toSetMax">To set maximum.</param>
        /// <returns></returns>
        public static float Map(float value, float fromSetMin, float fromSetMax, float toSetMin, float toSetMax)
        {
            float fromSetRange = fromSetMax - fromSetMin;
            float toSetRange = toSetMax - toSetMin;

            float valueScaled = (value - fromSetMin) / fromSetRange;
            float valueMapped = (valueScaled * toSetRange) + toSetMin;

            return valueMapped;
        }

        /// <summary>
        /// Nexts the double in range.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public static double NextDoubleInRange(double min, double max)
        {
            return rand.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Randoms the clamped.
        /// </summary>
        /// <returns></returns>
        public static float RandomClamped()
        {
            double d = rand.NextDouble();
            // Convert from the [0,1] to the [-1,1] range.
            d = (2 * d) - 1;
            return (float)d;
        }

        /// <summary>
        /// Rounds the vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <param name="digits">The digits.</param>
        /// <returns></returns>
        public static Vector3 RoundVec(this Vector3 vec, int digits)
        {
            return new Vector3((float)Math.Round(vec.X, digits), (float)Math.Round(vec.Y, digits), (float)Math.Round(vec.Z, digits));
        }

        /// <summary>
        /// Vecs to string round.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <param name="digits">The digits.</param>
        /// <returns></returns>
        public static string VecToStringRound(this Vector3 vec, int digits)
        {
            string formatStr = "F" + digits.ToString();
            return "{" + vec.X.ToString(formatStr) + "," + vec.Y.ToString(formatStr) + "," + vec.Z.ToString(formatStr) + "}";
        }
    }
}