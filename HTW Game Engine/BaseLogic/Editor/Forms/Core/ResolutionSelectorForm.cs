#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms.Core
{
    public partial class ResolutionSelectorForm : Form
    {
        private int i_width;
        private int i_height;

        public int Width
        {
            get { return i_width; }
        }

        public int Height
        {
            get { return i_height; }
        }

        public ResolutionSelectorForm()
        {
            InitializeComponent();

            resWidthTxtBox.Text = "1920";
            resHeightTxtBox.Text = "1080";
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            int width, height;
            bool success = true;
            if (!int.TryParse(resWidthTxtBox.Text, out width))
                success = false;
            if (!int.TryParse(resHeightTxtBox.Text, out height))
                success = false;

            if (!success)
            {
                RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }

            i_width = width;
            i_height = height;

            Close();
        }
    }
}
