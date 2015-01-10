#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Linq;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    partial class ParticleMgrForm : Form
    {
        /// <summary>
        /// The _content
        /// </summary>
        private Microsoft.Xna.Framework.Content.ContentManager _content;

        /// <summary>
        /// The _device
        /// </summary>
        private Microsoft.Xna.Framework.Graphics.GraphicsDevice _device;

        /// <summary>
        /// The p_particle MGR
        /// </summary>
        private Manager.ParticleMgr p_particleMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleMgrForm"/> class.
        /// </summary>
        /// <param name="pParticleMgr">The p particle MGR.</param>
        /// <param name="device">The device.</param>
        /// <param name="content">The content.</param>
        public ParticleMgrForm(Manager.ParticleMgr pParticleMgr, Microsoft.Xna.Framework.Graphics.GraphicsDevice device, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            InitializeComponent();

            p_particleMgr = pParticleMgr;

            _device = device;
            _content = content;

            UpdateListBox();

            deleteSelectedBtn.Enabled = false;
            editBtn.Enabled = false;
        }

        /// <summary>
        /// Handles the Click event of the cancelBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the createExistingBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void createExistingBtn_Click(object sender, EventArgs e)
        {
            CreateExistingParticleSystemForm cepsf = new CreateExistingParticleSystemForm(p_particleMgr, _device, _content);
            cepsf.OnClose += () =>
                {
                    UpdateListBox();
                };
            cepsf.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the createNewBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void createNewBtn_Click(object sender, EventArgs e)
        {
            RenderingSystem.GameParticleSystem particleSys = new RenderingSystem.GameParticleSystem();
            particleSys.LoadContent(_content, _device);

            p_particleMgr.AddToList(particleSys);

            ParticleSystemEditorForm psef = new ParticleSystemEditorForm(particleSys, p_particleMgr, _device, _content);
            psef.OnClose += () =>
                {
                    UpdateListBox();
                };
            psef.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the deleteSelectedBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteSelectedBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = particleSystemsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            p_particleMgr.RemoveDataElement(selectedIndex);

            particleSystemsListBox.SelectedIndex = -1;

            UpdateListBox();
        }

        /// <summary>
        /// Handles the Click event of the editBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = particleSystemsListBox.SelectedIndex;

            var particleSystem = p_particleMgr.GetDataElementAt(selectedIndex);

            ParticleSystemEditorForm psef = new ParticleSystemEditorForm(particleSystem, p_particleMgr, _device, _content);
            psef.OnClose += () =>
                {
                    UpdateListBox();
                };

            psef.ShowDialog();
        }

        /// <summary>
        /// Handles the KeyPress event of the particleSystemsListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void particleSystemsListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                deleteSelectedBtn_Click(null, null);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the particleSystemsListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void particleSystemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (particleSystemsListBox.SelectedIndex != -1)
            {
                editBtn.Enabled = true;
                deleteSelectedBtn.Enabled = true;
            }
            else
            {
                editBtn.Enabled = false;
                deleteSelectedBtn.Enabled = false;
            }
        }

        /// <summary>
        /// Updates the ListBox.
        /// </summary>
        private void UpdateListBox()
        {
            var particleSystems = p_particleMgr.GetDataElements();
            var particleSystemNames = from ps in particleSystems
                                      select ps.ActorID;

            particleSystemsListBox.Items.Clear();
            particleSystemsListBox.Items.AddRange(particleSystemNames.ToArray());
        }
    }
}