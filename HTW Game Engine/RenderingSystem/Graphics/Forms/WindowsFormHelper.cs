#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xna.Framework;



namespace RenderingSystem.Graphics.Forms
{
	/// <summary>
	/// For helping the editor forms display game related data.
	/// </summary>
	public static class WindowsFormHelper
	{
		/// <summary>
		/// A dictionary containing a set of colors with the key being the name of the color.
		/// </summary>
        private static Dictionary<string, Color> _colorDict = new Dictionary<string, Color>();

		/// <summary>
		/// Create a combo box with the name so the particle systems.
		/// </summary>
		/// <param name="cb"></param>
        public static void CreateParticleSystemSelectComboBox(ComboBox cb)
        {
            cb.Items.Add("Smoke Plum");
            cb.Items.Add("Projectile Trail");
            cb.Items.Add("Explosion Smoke");
            cb.Items.Add("Explosion");
        }

		/// <summary>
		/// Get the particle system type the user has selected.
		/// </summary>
		/// <param name="cb">The particle system combo box.</param>
		/// <returns>The string identitifier of the particle system.</returns>
        public static string GetSelectedParticleFilename(ComboBox cb)
        {
            string filename;
            string txt = cb.Text;
            if (txt == "Smoke Plum")
                filename = "SmokePlumeSettings";
            else if (txt == "Projectile Trail")
                filename = "ProjectileTrailSettings";
            else if (txt == "Explosion Smoke")
                filename = "ExplosionSmokeSettings";
            else if (txt == "Explosion")
                filename = "ExplosionSettings";
            else
                return null;

            const string particlesFilename = "particles/";
            return particlesFilename + filename;
        }

		/// <summary>
		/// Initialize all of the colors.
		/// </summary>
		public static void Init()
		{
            if (_colorDict.Count != 0)
                return;
			_colorDict.Add("AliceBlue",Color.AliceBlue);
            _colorDict.Add("AntiqueWhite", Color.AntiqueWhite);
            _colorDict.Add("Aqua", Color.Aqua);
            _colorDict.Add("Aquamarine", Color.Aquamarine);
            _colorDict.Add("Azure", Color.Azure);
            _colorDict.Add("Beige", Color.Beige);
            _colorDict.Add("Bisque", Color.Bisque);
            _colorDict.Add("BlanchedAlmond", Color.BlanchedAlmond);
            _colorDict.Add("Blue", Color.Blue);
            _colorDict.Add("BlueViolet", Color.BlueViolet);
            _colorDict.Add("Brown", Color.Brown);
            _colorDict.Add("BurlyWood", Color.BurlyWood);
            _colorDict.Add("CadetBlue", Color.CadetBlue);
            _colorDict.Add("Chartreuse", Color.Chartreuse);
            _colorDict.Add("Chocolate", Color.Chocolate);
            _colorDict.Add("Coral", Color.Coral);
            _colorDict.Add("CornflowerBlue", Color.CornflowerBlue);
            _colorDict.Add("Cornsilk", Color.Cornsilk);
            _colorDict.Add("Crimson", Color.Crimson);
            _colorDict.Add("Cyan", Color.Cyan);
            _colorDict.Add("DarkBlue", Color.DarkBlue);
            _colorDict.Add("DarkCyan", Color.DarkCyan);
            _colorDict.Add("DarkGoldenrod", Color.DarkGoldenrod);
            _colorDict.Add("DarkGray", Color.DarkGray);
            _colorDict.Add("DarkGreen", Color.DarkGreen);
            _colorDict.Add("DarkKhaki", Color.DarkKhaki);
            _colorDict.Add("DarkMagenta", Color.DarkMagenta);
            _colorDict.Add("DarkOliveGreen", Color.DarkOliveGreen);
            _colorDict.Add("DarkOrange", Color.DarkOrange);
            _colorDict.Add("DarkOrchid", Color.DarkOrchid);
            _colorDict.Add("DarkRed", Color.DarkRed);
            _colorDict.Add("DarkSalmon", Color.DarkSalmon);
            _colorDict.Add("DarkSeaGreen", Color.DarkSeaGreen);
            _colorDict.Add("DarkSlateBlue", Color.DarkSlateBlue);
            _colorDict.Add("DarkSlateGray", Color.DarkSlateGray);
            _colorDict.Add("DarkTurquoise", Color.DarkTurquoise);
            _colorDict.Add("DarkViolet", Color.DarkViolet);
            _colorDict.Add("DeepPink", Color.DeepPink);
            _colorDict.Add("DeepSkyBlue", Color.DeepSkyBlue);
            _colorDict.Add("DimGray", Color.DimGray);
            _colorDict.Add("DodgerBlue", Color.DodgerBlue);
            _colorDict.Add("Firebrick", Color.Firebrick);
            _colorDict.Add("FloralWhite", Color.FloralWhite);
            _colorDict.Add("ForestGreen", Color.ForestGreen);
            _colorDict.Add("Fuchsia", Color.Fuchsia);
            _colorDict.Add("Gainsboro", Color.Gainsboro);
            _colorDict.Add("GhostWhite", Color.GhostWhite);
            _colorDict.Add("Gold", Color.Gold);
            _colorDict.Add("Goldenrod", Color.Goldenrod);
            _colorDict.Add("Gray", Color.Gray);
            _colorDict.Add("Green", Color.Green);
            _colorDict.Add("GreenYellow", Color.GreenYellow);
            _colorDict.Add("Honeydew", Color.Honeydew);
            _colorDict.Add("HotPink", Color.HotPink);
            _colorDict.Add("IndianRed", Color.IndianRed);
            _colorDict.Add("Indigo", Color.Indigo);
            _colorDict.Add("Ivory", Color.Ivory);
            _colorDict.Add("Khaki", Color.Khaki);
            _colorDict.Add("Lavender", Color.Lavender);
            _colorDict.Add("LavenderBlush", Color.LavenderBlush);
            _colorDict.Add("LawnGreen", Color.LawnGreen);
            _colorDict.Add("LemonChiffon", Color.LemonChiffon);
            _colorDict.Add("LightBlue", Color.LightBlue);
            _colorDict.Add("LightCoral", Color.LightCoral);
            _colorDict.Add("LightCyan", Color.LightCyan);
            _colorDict.Add("LightGoldenrodYellow", Color.LightGoldenrodYellow);
            _colorDict.Add("LightGray", Color.LightGray);
            _colorDict.Add("LightGreen", Color.LightGreen);
            _colorDict.Add("LightPink", Color.LightPink);
            _colorDict.Add("LightSalmon", Color.LightSalmon);
            _colorDict.Add("LightSeaGreen", Color.LightSeaGreen);
            _colorDict.Add("LightSkyBlue", Color.LightSkyBlue);
            _colorDict.Add("LightSlateGray", Color.LightSlateGray);
            _colorDict.Add("LightSteelBlue", Color.LightSteelBlue);
            _colorDict.Add("LightYellow", Color.LightYellow);
            _colorDict.Add("Lime", Color.Lime);
            _colorDict.Add("LimeGreen", Color.LimeGreen);
            _colorDict.Add("Linen", Color.Linen);
            _colorDict.Add("Magenta", Color.Magenta);
            _colorDict.Add("Maroon", Color.Maroon);
            _colorDict.Add("MediumAquamarine", Color.MediumAquamarine);
            _colorDict.Add("MediumBlue", Color.MediumBlue);
            _colorDict.Add("MediumOrchid", Color.MediumOrchid);
            _colorDict.Add("MediumPurple", Color.MediumPurple);
            _colorDict.Add("MediumSeaGreen", Color.MediumSeaGreen);
            _colorDict.Add("MediumSlateBlue", Color.MediumSlateBlue);
            _colorDict.Add("MediumSpringGreen", Color.MediumSpringGreen);
            _colorDict.Add("MediumTurquoise", Color.MediumTurquoise);
            _colorDict.Add("MediumVioletRed", Color.MediumVioletRed);
            _colorDict.Add("MidnightBlue", Color.MidnightBlue);
            _colorDict.Add("MintCream", Color.MintCream);
            _colorDict.Add("MistyRose", Color.MistyRose);
            _colorDict.Add("Moccasin", Color.Moccasin);
            _colorDict.Add("NavajoWhite", Color.NavajoWhite);
            _colorDict.Add("Navy", Color.Navy);
            _colorDict.Add("OldLace", Color.OldLace);
            _colorDict.Add("Olive", Color.Olive);
            _colorDict.Add("OliveDrab", Color.OliveDrab);
            _colorDict.Add("Orange", Color.Orange);
            _colorDict.Add("OrangeRed", Color.OrangeRed);
            _colorDict.Add("Orchid", Color.Orchid);
            _colorDict.Add("PaleGoldenrod", Color.PaleGoldenrod);
            _colorDict.Add("PaleGreen", Color.PaleGreen);
            _colorDict.Add("PaleTurquoise", Color.PaleTurquoise);
            _colorDict.Add("PaleVioletRed", Color.PaleVioletRed);
            _colorDict.Add("PapayaWhip", Color.PapayaWhip);
            _colorDict.Add("PeachPuff", Color.PeachPuff);
            _colorDict.Add("Peru", Color.Peru);
            _colorDict.Add("Pink", Color.Pink);
            _colorDict.Add("Plum", Color.Plum);
            _colorDict.Add("PowderBlue", Color.PowderBlue);
            _colorDict.Add("Purple", Color.Purple);
            _colorDict.Add("Red", Color.Red);
            _colorDict.Add("RosyBrown", Color.RosyBrown);
            _colorDict.Add("RoyalBlue", Color.RoyalBlue);
            _colorDict.Add("SaddleBrown", Color.SaddleBrown);
            _colorDict.Add("Salmon", Color.Salmon);
            _colorDict.Add("SandyBrown", Color.SandyBrown);
            _colorDict.Add("SeaGreen", Color.SeaGreen);
            _colorDict.Add("SeaShell", Color.SeaShell);
            _colorDict.Add("Sienna", Color.Sienna);
            _colorDict.Add("Silver", Color.Silver);
            _colorDict.Add("SkyBlue", Color.SkyBlue);
            _colorDict.Add("SlateBlue", Color.SlateBlue);
            _colorDict.Add("SlateGray", Color.SlateGray);
            _colorDict.Add("Snow", Color.Snow);
            _colorDict.Add("SpringGreen", Color.SpringGreen);
            _colorDict.Add("SteelBlue", Color.SteelBlue);
            _colorDict.Add("Tan", Color.Tan);
            _colorDict.Add("Teal", Color.Teal);
            _colorDict.Add("Thistle", Color.Thistle);
            _colorDict.Add("Tomato", Color.Tomato);
            _colorDict.Add("Turquoise", Color.Turquoise);
            _colorDict.Add("Violet", Color.Violet);
            _colorDict.Add("Wheat", Color.Wheat);
            _colorDict.Add("White", Color.White);
            _colorDict.Add("WhiteSmoke", Color.WhiteSmoke);
            _colorDict.Add("Yellow", Color.Yellow);
            _colorDict.Add("YellowGreen", Color.YellowGreen);
		}

		/// <summary>
		/// Create the data elements of a color data combo box.
		/// </summary>
		/// <param name="comboBox"></param>
		public static void SetColorComboBox(ComboBox comboBox)
		{
			var colorNames = from c in _colorDict.Keys
							 orderby c
							 select c;

			comboBox.Items.Clear();
			comboBox.Items.AddRange(colorNames.ToArray());
		}

		/// <summary>
		/// Set the correct value of a color data combo box.
		/// </summary>
		/// <param name="comboBox">The combo box to set.</param>
		/// <param name="selectedColor">The color to be selected.</param>
        public static void SetColorComboBoxColor(ComboBox comboBox, Color selectedColor)
        {
            if (!_colorDict.ContainsValue(selectedColor))
                throw new ArgumentException();

            var colors = from k in _colorDict
                             orderby k.Key
                             select k;

            for (int i = 0; i < colors.Count(); ++i)
            {
                var keyValPair = colors.ElementAt(i);

                if (keyValPair.Value == selectedColor)
                    comboBox.SelectedIndex = i;
            }
        }

		/// <summary>
		/// Create a color combo box from the specified data.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
        public static ComboBox CreateColorComboBox(string name, int x, int y, int width, int height)
		{
            

			var colorNames = from c in _colorDict.Keys
							 orderby c
							 select c;
			ComboBox comboBox = new ComboBox();
			comboBox.Name = name;
			comboBox.Top = y;
			comboBox.Left = x;
			comboBox.Width = width;
			comboBox.Height = height;
			comboBox.Items.AddRange(colorNames.ToArray());

			return comboBox;
		}

		/// <summary>
		/// Display a standard UI parse error.
		/// </summary>
        public static void DisplayParseErrorMsg()
        {
            MessageBox.Show("Invalid data input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

		/// <summary>
		/// Get the input of a color combo box.
		/// </summary>
		/// <param name="selectedName"></param>
		/// <param name="color"></param>
		/// <returns></returns>
		public static bool GetColorComboBoxInput(string selectedName, out Color color)
		{
            color = Color.Transparent;

			if (!_colorDict.ContainsKey(selectedName))
				return false;

			color = _colorDict[selectedName];
			return true;
		}
	}
}
