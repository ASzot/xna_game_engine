#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms.Object_Editors
{
    /// <summary>
    ///
    /// </summary>
    public partial class TimedParticleSystemEditorForm : Form
    {
        /// <summary>
        /// The _particle system
        /// </summary>
        private RenderingSystem.GameParticleSystem _particleSys;

        /// <summary>
        /// The b_edit mode
        /// </summary>
        private bool b_editMode;

        /// <summary>
        /// The p_existing proc
        /// </summary>
        private Process.ParticleEffectProcess p_existingProc;

        /// <summary>
        /// The p_processes
        /// </summary>
        private List<Process.GameProcess> p_processes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedParticleSystemEditorForm"/> class.
        /// </summary>
        /// <param name="processes">The processes.</param>
        /// <param name="existingIndex">Index of the existing.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public TimedParticleSystemEditorForm(List<Process.GameProcess> processes, int existingIndex)
        {
            InitializeComponent();
            p_processes = processes;
            b_editMode = existingIndex != -1;

            setLightBtn.Visible = false;

            if (b_editMode)
            {
                Process.GameProcess process = processes[existingIndex];
                if (!(process is Process.ParticleEffectProcess))
                    throw new ArgumentException();
                p_existingProc = process as Process.ParticleEffectProcess;
                aliveTimeTxtBox.Text = p_existingProc.WaitTime.ToString();
                acceptBtn.Visible = false;
                var particleSys = p_existingProc.ParticleSys;
                xPosTxtBox.Text = particleSys.Position.X.ToString();
                yPosTxtBox.Text = particleSys.Position.Y.ToString();
                zPosTxtBox.Text = particleSys.Position.Z.ToString();
                setLightBtn.Visible = p_existingProc.Light != null;
            }

            particleSystemComboBox.Items.Clear();

            RenderingSystem.Graphics.Forms.WindowsFormHelper.CreateParticleSystemSelectComboBox(particleSystemComboBox);
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            uint lifeMs;
            float x, y, z;

            bool success = true;
            if (!uint.TryParse(aliveTimeTxtBox.Text, out lifeMs))
                success = false;

            if (!float.TryParse(xPosTxtBox.Text, out x))
                success = false;
            if (!float.TryParse(yPosTxtBox.Text, out y))
                success = false;
            if (!float.TryParse(zPosTxtBox.Text, out z))
                success = false;
            if (_particleSys == null)
                success = false;

            if (!success)
            {
                RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }

            _particleSys.Position = new Microsoft.Xna.Framework.Vector3(x, y, z);

            Process.ParticleEffectProcess pep = new Process.ParticleEffectProcess(lifeMs, _particleSys);
            p_processes.Add(pep);
        }

        /// <summary>
        /// Handles the TextChanged event of the aliveTimeTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void aliveTimeTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (!b_editMode)
                return;
            uint lifeMs;
            if (!uint.TryParse(aliveTimeTxtBox.Text, out lifeMs))
                return;

            p_existingProc.WaitTime = lifeMs;
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
        /// Handles the Click event of the customizeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void customizeBtn_Click(object sender, EventArgs e)
        {
            RenderingSystem.GameParticleSystem gps;
            if (b_editMode)
                gps = p_existingProc.ParticleSys;
            else
            {
                if (_particleSys == null)
                {
                    _particleSys = new RenderingSystem.GameParticleSystem();
                    _particleSys.LoadContent(RenderingSystem.ResourceMgr.Content, RenderingSystem.ResourceMgr.Device);
                }
                gps = _particleSys;
            }

            ParticleSystemEditorForm psef = new ParticleSystemEditorForm(gps, GameSystem.GameSys_Instance.ParticleMgr,
               RenderingSystem.ResourceMgr.Device, RenderingSystem.ResourceMgr.Content);
            psef.ShowDialog();

            particleSystemComboBox.Text = "";

            var pos = gps.Position;
            xPosTxtBox.Text = pos.X.ToString();
            yPosTxtBox.Text = pos.Y.ToString();
            zPosTxtBox.Text = pos.Z.ToString();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the particleSystemComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void particleSystemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string particleFilename = RenderingSystem.Graphics.Forms.WindowsFormHelper.GetSelectedParticleFilename(particleSystemComboBox);

            if (particleFilename == null)
                return;

            if (b_editMode)
            {
                RenderingSystem.GameParticleSystem gps = new RenderingSystem.GameParticleSystem(Guid.NewGuid().ToString());
                gps.LoadContent(RenderingSystem.ResourceMgr.Content, RenderingSystem.ResourceMgr.Device, particleFilename);

                float x, y, z;
                var vec = gps.Position;
                if (float.TryParse(xPosTxtBox.Text, out x))
                    gps.Position = new Microsoft.Xna.Framework.Vector3(x, vec.Y, vec.Z);
                if (float.TryParse(yPosTxtBox.Text, out y))
                    gps.Position = new Microsoft.Xna.Framework.Vector3(vec.X, y, vec.Z);
                if (float.TryParse(zPosTxtBox.Text, out z))
                    gps.Position = new Microsoft.Xna.Framework.Vector3(vec.X, vec.Y, z);

                p_existingProc.ResetParticleSystem(gps);
            }
            else
            {
                _particleSys = new RenderingSystem.GameParticleSystem();
                _particleSys.LoadContent(RenderingSystem.ResourceMgr.Content, RenderingSystem.ResourceMgr.Device, particleFilename);
            }
        }

        /// <summary>
        /// Handles the Click event of the setLightBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void setLightBtn_Click(object sender, EventArgs e)
        {
            CreatePointLightForm cplf;
            if (b_editMode)
            {
                cplf = new CreatePointLightForm(GameSystem.GameSys_Instance.LightMgr, p_existingProc.Light);
                cplf.SetPosition(p_existingProc.ParticleSys.Position);
            }
            else
            {
                cplf = new CreatePointLightForm(GameSystem.GameSys_Instance.LightMgr, null, false);
                if (_particleSys != null)
                    cplf.SetPosition(_particleSys.Position);
            }
            cplf.ShowDialog();
        }

        /// <summary>
        /// Handles the TextChanged event of the txtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string text = txtBox.Text;
            string name = txtBox.Name;

            float fParsed;
            bool fParsedSuccess = float.TryParse(text, out fParsed);
            if (!fParsedSuccess)
                return;

            RenderingSystem.GameParticleSystem gps;
            if (b_editMode)
                gps = p_existingProc.ParticleSys;
            else
            {
                if (_particleSys == null)
                    return;
                gps = _particleSys;
            }
            var vec = gps.Position;

            if (name == "xPosTxtBox")
            {
                gps.Position = new Microsoft.Xna.Framework.Vector3(fParsed, vec.Y, vec.Z);
            }
            else if (name == "yPosTxtBox")
            {
                gps.Position = new Microsoft.Xna.Framework.Vector3(vec.X, fParsed, vec.Z);
            }
            else if (name == "zPosTxtBox")
            {
                gps.Position = new Microsoft.Xna.Framework.Vector3(vec.X, vec.Y, fParsed);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the useLightCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void useLightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setLightBtn.Visible = useLightCheckBox.Checked;
            if (!useLightCheckBox.Checked && b_editMode)
                p_existingProc.Light = null;
        }
    }
}