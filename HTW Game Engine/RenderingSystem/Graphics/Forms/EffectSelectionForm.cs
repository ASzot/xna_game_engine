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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RenderingSystem.Graphics.Forms
{
    /// <summary>
    /// Allows the user to select a post processing effect type from a list of options represented as buttons.
    /// </summary>
    public partial class EffectSelectionForm : Form
    {
        /// <summary>
        /// A pointer to the post processing effect manager.
        /// </summary>
        private PPEMgr p_ppeMgr;

        /// <summary>
        /// Fired on the form closing.
        /// </summary>
        public Action OnClose;
        
        /// <summary>
        /// Construct the form. Storing the pointer supplied.
        /// </summary>
        /// <param name="pPPeMgr">A pointer to the post processing effect manager.</param>
        public EffectSelectionForm(PPEMgr pPPeMgr)
        {
            p_ppeMgr = pPPeMgr;
            InitializeComponent();
        }

        /// <summary>
        /// On the form closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeeBtn_Click(object sender, EventArgs e)
        {
            OnClose();
            this.Close();
        }

        /// <summary>
        /// The user selected bloom effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bloomBtn_Click(object sender, EventArgs e)
        {
            BloomPPE bloomPPE = new BloomPPE();
            bloomPPE.LoadContent(ResourceMgr.Device, ResourceMgr.Content);
            p_ppeMgr.AddToList(bloomPPE);

            closeeBtn_Click(null, null);
        }

        /// <summary>
        /// The user selected light shaft effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lightShaftsBtn_Click(object sender, EventArgs e)
        {
            LightShaftPPE lightShafts = new LightShaftPPE();
            lightShafts.LoadContent(ResourceMgr.Device, ResourceMgr.Content);
            p_ppeMgr.AddToList(lightShafts);

            closeeBtn_Click(null, null);
        }

        /// <summary>
        /// The user selected the anti-alaising effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fxaaBtn_Click(object sender, EventArgs e)
        {
            FxaaPPE fxaaPpe = new FxaaPPE();
            fxaaPpe.LoadContent(ResourceMgr.Device, ResourceMgr.Content);
            p_ppeMgr.AddToList(fxaaPpe);

            closeeBtn_Click(null, null);
        }
    }
}
