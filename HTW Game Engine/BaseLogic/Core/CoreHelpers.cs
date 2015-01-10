#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Linq;

using Microsoft.Xna.Framework;

namespace BaseLogic
{
    /// <summary>
    ///
    /// </summary>
    public struct CircleShape
    {
        /// <summary>
        /// The center
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// The radius
        /// </summary>
        public float Radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircleShape"/> struct.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        public CircleShape(Vector2 center, float radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        /// <summary>
        /// Determines whether the specified point contains point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public bool ContainsPoint(Vector2 point)
        {
            float distance = Vector2.Distance(Center, point);
            return distance <= Radius;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public struct RectangleShape
    {
        /// <summary>
        /// The maximum position
        /// </summary>
        public Vector2 MaxPos;

        /// <summary>
        /// The minimum position
        /// </summary>
        public Vector2 MinPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleShape"/> struct.
        /// </summary>
        /// <param name="minPos">The minimum position.</param>
        /// <param name="maxPos">The maximum position.</param>
        public RectangleShape(Vector2 minPos, Vector2 maxPos)
        {
            this.MinPos = minPos;
            this.MaxPos = maxPos;
        }

        /// <summary>
        /// Determines whether the specified point contains point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public bool ContainsPoint(Vector2 point)
        {
            if (point.X > MinPos.X && point.X < MaxPos.X)
            {
                if (point.Y > MinPos.Y && point.Y < MaxPos.Y)
                    return true;
            }
            return false;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct TypePair<T>
    {
        /// <summary>
        /// The data1
        /// </summary>
        public T Data1;

        /// <summary>
        /// The data2
        /// </summary>
        public T Data2;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypePair{T}"/> struct.
        /// </summary>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        public TypePair(T t1, T t2)
        {
            Data1 = t1;
            Data2 = t2;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// All colors
        /// </summary>
        public static Color[] AllColors =
        {
            Color.AliceBlue,
            Color.AntiqueWhite,
            Color.Aqua,
            Color.Aquamarine,
            Color.Azure,
            Color.Beige,
            Color.Bisque,
            Color.BlanchedAlmond,
            Color.Blue,
            Color.BlueViolet,
            Color.Brown,
            Color.BurlyWood,
            Color.CadetBlue,
            Color.Chartreuse,
            Color.Chocolate,
            Color.Coral,
            Color.CornflowerBlue,
            Color.Cornsilk,
            Color.Crimson,
            Color.Cyan,
            Color.DarkBlue,
            Color.DarkCyan,
            Color.DarkGoldenrod,
            Color.DarkGray,
            Color.DarkGreen,
            Color.DarkKhaki,
            Color.DarkMagenta,
            Color.DarkOliveGreen,
            Color.DarkOrange,
            Color.DarkOrchid,
            Color.DarkRed,
            Color.DarkSalmon,
            Color.DarkSeaGreen,
            Color.DarkSlateBlue,
            Color.DarkSlateGray,
            Color.DarkTurquoise,
            Color.DarkViolet,
            Color.DeepPink,
            Color.DeepSkyBlue,
            Color.DimGray,
            Color.DodgerBlue,
            Color.Firebrick,
            Color.FloralWhite,
            Color.ForestGreen,
            Color.Fuchsia,
            Color.Gainsboro,
            Color.GhostWhite,
            Color.Gold,
            Color.Goldenrod,
            Color.Gray,
            Color.Green,
            Color.GreenYellow,
            Color.Honeydew,
            Color.HotPink,
            Color.IndianRed,
            Color.Indigo,
            Color.Ivory,
            Color.Khaki,
            Color.Lavender,
            Color.LavenderBlush,
            Color.LawnGreen,
            Color.LemonChiffon,
            Color.LightBlue,
            Color.LightCoral,
            Color.LightCyan,
            Color.LightGoldenrodYellow,
            Color.LightGray,
            Color.LightGreen,
            Color.LightPink,
            Color.LightSalmon,
            Color.LightSeaGreen,
            Color.LightSkyBlue,
            Color.LightSlateGray,
            Color.LightSteelBlue,
            Color.LightYellow,
            Color.Lime,
            Color.LimeGreen,
            Color.Linen,
            Color.Magenta,
            Color.Maroon,
            Color.MediumAquamarine,
            Color.MediumBlue,
            Color.MediumOrchid,
            Color.MediumPurple,
            Color.MediumSeaGreen,
            Color.MediumSlateBlue,
            Color.MediumSpringGreen,
            Color.MediumTurquoise,
            Color.MediumVioletRed,
            Color.MidnightBlue,
            Color.MintCream,
            Color.MistyRose,
            Color.Moccasin,
            Color.NavajoWhite,
            Color.Navy,
            Color.OldLace,
            Color.Olive,
            Color.OliveDrab,
            Color.Orange,
            Color.OrangeRed,
            Color.Orchid,
            Color.PaleGoldenrod,
            Color.PaleGreen,
            Color.PaleTurquoise,
            Color.PaleVioletRed,
            Color.PapayaWhip,
            Color.PeachPuff,
            Color.Peru,
            Color.Pink,
            Color.Plum,
            Color.PowderBlue,
            Color.Purple,
            Color.Red,
            Color.RosyBrown,
            Color.RoyalBlue,
            Color.SaddleBrown,
            Color.Salmon,
            Color.SandyBrown,
            Color.SeaGreen,
            Color.SeaShell,
            Color.Sienna,
            Color.Silver,
            Color.SkyBlue,
            Color.SlateBlue,
            Color.SlateGray,
            Color.Snow,
            Color.SpringGreen,
            Color.SteelBlue,
            Color.Tan,
            Color.Teal,
            Color.Thistle,
            Color.Tomato,
            Color.Turquoise,
            Color.Violet,
            Color.Wheat,
            Color.White,
            Color.WhiteSmoke,
            Color.Yellow,
            Color.YellowGreen,
        };

        // My personal favorites.
        /// <summary>
        /// The useful colors
        /// </summary>
        public static Color[] UsefulColors =
        {
            Color.Red,
            Color.Fuchsia,
            Color.Blue,
            Color.Violet,
            Color.HotPink,
            Color.Green,
            Color.LightBlue,
            Color.Coral,
            Color.Gold,
            Color.Purple,
        };

        /// <summary>
        /// Gets the random color.
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomColor()
        {
            int randomIndex = Core.ThreadSafeRandom.NextInt(0, AllColors.Count());
            return AllColors[randomIndex];
        }

        /// <summary>
        /// Gets the random color of the useful.
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomUsefulColor()
        {
            int randomIndex = Core.ThreadSafeRandom.NextInt(0, UsefulColors.Count());
            return UsefulColors[randomIndex];
        }
    }
}