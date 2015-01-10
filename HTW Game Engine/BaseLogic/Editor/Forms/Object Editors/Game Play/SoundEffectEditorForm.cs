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
    public partial class SoundEffectEditorForm : Form
    {
        /// <summary>
        /// The b_edit mode
        /// </summary>
        private bool b_editMode;

        /// <summary>
        /// The p_existing proc
        /// </summary>
        private Process.SoundEffectProcess p_existingProc;

        /// <summary>
        /// The p_processes
        /// </summary>
        private List<Process.GameProcess> p_processes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundEffectEditorForm"/> class.
        /// </summary>
        /// <param name="pProcs">The p procs.</param>
        /// <param name="selectedIndex">Index of the selected.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public SoundEffectEditorForm(List<Process.GameProcess> pProcs, int selectedIndex)
        {
            InitializeComponent();

            b_editMode = selectedIndex != -1;

            p_processes = pProcs;
            if (b_editMode)
            {
                Process.GameProcess proc = p_processes[selectedIndex];
                if (!(proc is Process.SoundEffectProcess))
                    throw new ArgumentException();
                p_existingProc = proc as Process.SoundEffectProcess;
                acceptBtn.Visible = false;
            }
            else
            {
                // Just some defaults.
                panTxtBox.Text = "0";
                pitchTxtBox.Text = "0";
                volumeTxtBox.Text = "1";
            }
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            float pan, pitch, volume;
            string filename = soundFilenameTxtBox.Text;

            bool success = true;
            if (filename == "")
                success = false;
            if (!float.TryParse(panTxtBox.Text, out pan))
                success = false;
            if (!float.TryParse(pitchTxtBox.Text, out pitch))
                success = false;
            if (!float.TryParse(volumeTxtBox.Text, out volume))
                success = false;

            if (!success)
            {
                RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }
            Process.SoundEffectProcess sep = new Process.SoundEffectProcess(filename);
            sep.Volume = volume;
            sep.Pitch = pitch;
            sep.Pan = pan;

            p_processes.Add(sep);
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
        /// Handles the TextChanged event of the txtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException">Unrecognized text box!</exception>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            if (!b_editMode)
                return;

            TextBox textBox = (TextBox)sender;
            string name = textBox.Name;
            string text = textBox.Text;

            float fParsed;
            bool fParseSuccess = float.TryParse(text, out fParsed);
            if (name == "panTxtBox" && fParseSuccess)
            {
                p_existingProc.Pan = fParsed;
            }
            else if (name == "volumeTxtBox" && fParseSuccess)
            {
                p_existingProc.Volume = fParsed;
            }
            else if (name == "pitchTxtBox" && fParseSuccess)
            {
                p_existingProc.Pitch = fParsed;
            }
            else if (name == "soundFilenameTxtBox")
            {
                p_existingProc.Filename = text;
            }
            else
                throw new ArgumentException("Unrecognized text box!");
        }
    }
}