#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateLightStartForm : Form
    {
        /// <summary>
        /// The p_light MGR
        /// </summary>
        private Manager.LightManager p_lightMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateLightStartForm"/> class.
        /// </summary>
        /// <param name="pLightMgr">The p light MGR.</param>
        public CreateLightStartForm(Manager.LightManager pLightMgr)
        {
            InitializeComponent();

            p_lightMgr = pLightMgr;
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            CreateDirLightForm cdlf = new CreateDirLightForm(p_lightMgr);
            cdlf.ShowDialog();
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

        /// <summary>
        /// Handles the Click event of the pointLightBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pointLightBtn_Click(object sender, EventArgs e)
        {
            CreatePointLightForm cplf = new CreatePointLightForm(p_lightMgr);
            cplf.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the spotLightBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void spotLightBtn_Click(object sender, EventArgs e)
        {
            CreateSpotLightForm cslf = new CreateSpotLightForm(p_lightMgr);
            cslf.ShowDialog();
        }
    }
}