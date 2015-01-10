#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms.Object_Editors
{
    /// <summary>
    ///
    /// </summary>
    public partial class SelectLightForm : Form
    {
        /// <summary>
        /// The on light accept
        /// </summary>
        public Action<RenderingSystem.GameLight> OnLightAccept;

        /// <summary>
        /// The p_light MGR
        /// </summary>
        private Manager.LightManager p_lightMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectLightForm"/> class.
        /// </summary>
        /// <param name="pLightMgr">The p light MGR.</param>
        public SelectLightForm(Manager.LightManager pLightMgr)
        {
            InitializeComponent();
            p_lightMgr = pLightMgr;

            selectLightListBox.Items.Clear();
            foreach (var gameLight in p_lightMgr.GetDataElements())
            {
                selectLightListBox.Items.Add(gameLight.LightID);
            }
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = selectLightListBox.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select a light!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            RenderingSystem.GameLight gameLight = p_lightMgr.GetDataElementAt(selectedIndex);
            OnLightAccept(gameLight);
        }

        /// <summary>
        /// Handles the Click event of the closeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}