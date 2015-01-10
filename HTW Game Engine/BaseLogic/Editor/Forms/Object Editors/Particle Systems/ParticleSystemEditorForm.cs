#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;
using RenderingSystem;
using RenderingSystem.RendererImpl;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class ParticleSystemEditorForm : Form
    {
        /// <summary>
        /// The on accept
        /// </summary>
        public Action OnAccept;

        /// <summary>
        /// The on close
        /// </summary>
        public Action OnClose;

        /// <summary>
        /// The _content
        /// </summary>
        private Microsoft.Xna.Framework.Content.ContentManager _content;

        /// <summary>
        /// The _device
        /// </summary>
        private Microsoft.Xna.Framework.Graphics.GraphicsDevice _device;

        /// <summary>
        /// The _particle system
        /// </summary>
        private GameParticleSystem _particleSystem;

        /// <summary>
        /// The p_particle MGR
        /// </summary>
        private Manager.ParticleMgr p_particleMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleSystemEditorForm"/> class.
        /// </summary>
        /// <param name="particleSystem">The particle system.</param>
        /// <param name="pParticleMgr">The p particle MGR.</param>
        /// <param name="device">The device.</param>
        /// <param name="content">The content.</param>
        public ParticleSystemEditorForm(GameParticleSystem particleSystem, Manager.ParticleMgr pParticleMgr,
            Microsoft.Xna.Framework.Graphics.GraphicsDevice device, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            InitializeComponent();

            _device = device;
            _content = content;

            p_particleMgr = pParticleMgr;

            _particleSystem = particleSystem;

            string[] blendStates = { "Additive", "AlphaBlend", "NonPremultiplied", "Opaque" };
            blendStateComboBox.DataSource = blendStates;

            if (_particleSystem.IsParticleSystemSet())
            {
                ParticleSettings parSettings = particleSystem.GetInfo();

                if (parSettings.TextureName != null)
                    textureNameTxtBox.Text = parSettings.TextureName;
                maxParticlesTxtBox.Text = parSettings.MaxParticles.ToString();
                durationTxtBox.Text = parSettings.Duration.TotalSeconds.ToString();
                durationRandTxtBox.Text = parSettings.DurationRandomness.ToString();
                emitVelSensTxtBox.Text = parSettings.EmitterVelocitySensitivity.ToString();
                minHorizVelTxtBox.Text = parSettings.MinHorizontalVelocity.ToString();
                maxHorizTxtBox.Text = parSettings.MaxHorizontalVelocity.ToString();
                minVertVelTxtBox.Text = parSettings.MinVerticalVelocity.ToString();
                maxVertVelTxtBox.Text = parSettings.MaxVerticalVelocity.ToString();
                gravityXTxtBox.Text = parSettings.Gravity.X.ToString();
                gravityYTxtBox.Text = parSettings.Gravity.Y.ToString();
                gravityZTxtBox.Text = parSettings.Gravity.Z.ToString();
                endVelTxtBox.Text = parSettings.EndVelocity.ToString();
                minRotSpeedTxtBox.Text = parSettings.MinRotateSpeed.ToString();
                maxRotSpeedTxtBox.Text = parSettings.MaxRotateSpeed.ToString();
                minStartSizeTxtBox.Text = parSettings.MinStartSize.ToString();
                maxStartSizeTxtBox.Text = parSettings.MaxStartSize.ToString();
                minEndSizeTxtBox.Text = parSettings.MinEndSize.ToString();
                maxEndSizeTxtBox.Text = parSettings.MaxEndSize.ToString();
                posXTxtBox.Text = particleSystem.Position.X.ToString();
                posYTxtBox.Text = particleSystem.Position.Y.ToString();
                posZTxtBox.Text = particleSystem.Position.Z.ToString();
                emitRateTxtBox.Text = parSettings.EmissionRate.ToString();

                idTxtBox.Text = particleSystem.ActorID;

                SetBlendStateComboBox(parSettings.BlendState, blendStateComboBox);
            }

            //string[] colors = { "White", "Black" };
            //minColorComboBox.DataSource = colors;
            //SetColorComboBox(parSettings.MinColor, minColorComboBox);

            //maxColorComboBox.DataSource = colors;
            //SetColorComboBox(parSettings.MaxColor, maxColorComboBox);
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            applyBtn_Click(null, null);
            cancelBtn_Click_1(null, null);
        }

        /// <summary>
        /// Handles the Click event of the applyBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void applyBtn_Click(object sender, EventArgs e)
        {
            string id = idTxtBox.Text;
            if ((id == "" || p_particleMgr.ActorIDExists(id)) && _particleSystem.ActorID != id)
            {
                MessageBox.Show("Particle system ID already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ParticleSettings partSettings = new ParticleSettings();
            bool success = true;
            if (textureNameTxtBox.Text == "")
                success = false;
            partSettings.TextureName = textureNameTxtBox.Text;
            if (!int.TryParse(maxParticlesTxtBox.Text, out partSettings.MaxParticles))
                success = false;
            double seconds = 0.0;
            if (!double.TryParse(durationTxtBox.Text, out seconds))
                success = false;
            partSettings.Duration = TimeSpan.FromSeconds(seconds);
            if (!float.TryParse(durationRandTxtBox.Text, out partSettings.DurationRandomness))
                success = false;
            if (!float.TryParse(emitVelSensTxtBox.Text, out partSettings.EmitterVelocitySensitivity))
                success = false;
            if (!float.TryParse(minHorizVelTxtBox.Text, out partSettings.MinHorizontalVelocity))
                success = false;
            if (!float.TryParse(maxHorizTxtBox.Text, out partSettings.MaxHorizontalVelocity))
                success = false;
            if (!float.TryParse(minVertVelTxtBox.Text, out partSettings.MinVerticalVelocity))
                success = false;
            if (!float.TryParse(maxVertVelTxtBox.Text, out partSettings.MaxVerticalVelocity))
                success = false;
            if (!float.TryParse(gravityXTxtBox.Text, out partSettings.Gravity.X))
                success = false;
            if (!float.TryParse(gravityYTxtBox.Text, out partSettings.Gravity.Y))
                success = false;
            if (!float.TryParse(gravityZTxtBox.Text, out partSettings.Gravity.Z))
                success = false;
            if (!float.TryParse(endVelTxtBox.Text, out partSettings.EndVelocity))
                success = false;
            if (!float.TryParse(emitRateTxtBox.Text, out partSettings.EmissionRate))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid Data Input.", "Error");
                return;
            }

            if (!float.TryParse(minRotSpeedTxtBox.Text, out partSettings.MinRotateSpeed))
                success = false;
            if (!float.TryParse(maxRotSpeedTxtBox.Text, out partSettings.MaxRotateSpeed))
                success = false;
            if (!float.TryParse(minStartSizeTxtBox.Text, out partSettings.MinStartSize))
                success = false;
            if (!float.TryParse(maxStartSizeTxtBox.Text, out partSettings.MaxStartSize))
                success = false;
            if (!float.TryParse(minEndSizeTxtBox.Text, out partSettings.MinEndSize))
                success = false;
            if (!float.TryParse(maxEndSizeTxtBox.Text, out partSettings.MaxEndSize))
                success = false;

            Microsoft.Xna.Framework.Vector3 pos = new Microsoft.Xna.Framework.Vector3();
            if (!float.TryParse(posXTxtBox.Text, out pos.X))
                success = false;
            if (!float.TryParse(posYTxtBox.Text, out pos.Y))
                success = false;
            if (!float.TryParse(posZTxtBox.Text, out pos.Z))
                success = false;

            // Get the data from the combo boxes.
            //partSettings.MinColor = GetColorComboBox(minColorComboBox);
            //partSettings.MaxColor = GetColorComboBox(maxColorComboBox);
            partSettings.BlendState = GetBlendStateComboBox(blendStateComboBox);

            if (!success)
            {
                MessageBox.Show("Invalid data input!", "Error");
                return;
            }

            _particleSystem.SetInfo(_content, _device, partSettings);
            _particleSystem.Position = pos;
            _particleSystem.ActorID = id;

            if (OnAccept != null)
                OnAccept();
        }

        /// <summary>
        /// Handles the 1 event of the cancelBtn_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            if (OnClose != null)
                OnClose();

            this.Close();
        }

        /// <summary>
        /// Gets the blend state ComboBox.
        /// </summary>
        /// <param name="cb">The cb.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        private Microsoft.Xna.Framework.Graphics.BlendState GetBlendStateComboBox(ComboBox cb)
        {
            switch (cb.SelectedIndex)
            {
                case 0:
                    return Microsoft.Xna.Framework.Graphics.BlendState.Additive;
                case 1:
                    return Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend;
                case 2:
                    return Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied;
                case 3:
                    return Microsoft.Xna.Framework.Graphics.BlendState.Opaque;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Gets the color ComboBox.
        /// </summary>
        /// <param name="cb">The cb.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        private Microsoft.Xna.Framework.Color GetColorComboBox(ComboBox cb)
        {
            if (cb.SelectedIndex == 0)
                return Microsoft.Xna.Framework.Color.White;
            else if (cb.SelectedIndex == 1)
                return Microsoft.Xna.Framework.Color.Black;
            else
                throw new ArgumentException();
        }

        /// <summary>
        /// Sets the blend state ComboBox.
        /// </summary>
        /// <param name="blendState">State of the blend.</param>
        /// <param name="cb">The cb.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void SetBlendStateComboBox(Microsoft.Xna.Framework.Graphics.BlendState blendState, ComboBox cb)
        {
            if (blendState == Microsoft.Xna.Framework.Graphics.BlendState.Additive)
                cb.SelectedIndex = 0;
            else if (blendState == Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend)
                cb.SelectedIndex = 1;
            else if (blendState == Microsoft.Xna.Framework.Graphics.BlendState.NonPremultiplied)
                cb.SelectedIndex = 2;
            else if (blendState == Microsoft.Xna.Framework.Graphics.BlendState.Opaque)
                cb.SelectedIndex = 3;
            else
                throw new ArgumentException();
        }

        /// <summary>
        /// Sets the color ComboBox.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="cb">The cb.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void SetColorComboBox(Microsoft.Xna.Framework.Color color, ComboBox cb)
        {
            if (color == Microsoft.Xna.Framework.Color.White)
                cb.SelectedIndex = 0;
            else if (color == Microsoft.Xna.Framework.Color.Black)
                cb.SelectedIndex = 0;
            else
                throw new ArgumentException();
        }
    }
}