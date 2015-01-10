#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateExistingParticleSystemForm : Form
    {
        /// <summary>
        /// The on add particle system
        /// </summary>
        public Action<RenderingSystem.GameParticleSystem> OnAddParticleSystem;

        /// <summary>
        /// The on close
        /// </summary>
        public Action OnClose;

        /// <summary>
        /// The _content
        /// </summary>
        private ContentManager _content;

        /// <summary>
        /// The _device
        /// </summary>
        private GraphicsDevice _device;

        /// <summary>
        /// The p_particle MGR
        /// </summary>
        private Manager.ParticleMgr p_particleMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateExistingParticleSystemForm"/> class.
        /// </summary>
        /// <param name="pParticleMgr">The p particle MGR.</param>
        /// <param name="device">The device.</param>
        /// <param name="content">The content.</param>
        public CreateExistingParticleSystemForm(Manager.ParticleMgr pParticleMgr, GraphicsDevice device, ContentManager content)
        {
            InitializeComponent();

            p_particleMgr = pParticleMgr;

            _device = device;
            _content = content;

            particleSystemComboBox.Items.Clear();
            particleSystemComboBox.Items.Add("fire");
            particleSystemComboBox.Items.Add("explosion");
            particleSystemComboBox.Items.Add("explosion smoke");
            particleSystemComboBox.Items.Add("smoke plume");
            particleSystemComboBox.Items.Add("projectile trail");
        }

        /// <summary>
        /// Handles the Click event of the cancelBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if (OnClose != null)
                OnClose();

            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the createBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void createBtn_Click(object sender, EventArgs e)
        {
            string id = idTxtBox.Text;
            if (id == "" || p_particleMgr.ActorIDExists(id))
            {
                MessageBox.Show("Particle system ID already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedParticleSysStr = particleSystemComboBox.Text;
            string filename = null;

            float x, y, z;
            bool success = true;
            if (!float.TryParse(xPosTxtBox.Text, out x))
                success = false;
            if (!float.TryParse(yPosTxtBox.Text, out y))
                success = false;
            if (!float.TryParse(zPosTxtBox.Text, out z))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid position data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedParticleSysStr == "fire")
            {
                filename = "FireSettings";
            }
            else if (selectedParticleSysStr == "explosion")
            {
                filename = "ExplosionSettings";
            }
            else if (selectedParticleSysStr == "explosion smoke")
            {
                filename = "ExplosionSmokeSettings";
            }
            else if (selectedParticleSysStr == "projectile trail")
            {
                filename = "ProjectileTrailSettings";
            }
            else if (selectedParticleSysStr == "smoke plume")
            {
                filename = "SmokePlumeSettings";
            }
            else
            {
                MessageBox.Show("Invalid particle system name was used!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            filename = "particles/" + filename;

            RenderingSystem.GameParticleSystem gps = new RenderingSystem.GameParticleSystem(id);
            gps.LoadContent(_content, _device, filename);
            gps.Position = new Microsoft.Xna.Framework.Vector3(x, y, z);
            gps.ActorID = id;

            p_particleMgr.AddToList(gps);
            if (OnAddParticleSystem != null)
                OnAddParticleSystem(gps);

            cancelBtn_Click(null, null);
        }
    }
}