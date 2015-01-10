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
    public partial class AddNewGameEvent : Form
    {
        /// <summary>
        /// The p_processes
        /// </summary>
        private List<Process.GameProcess> p_processes;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNewGameEvent"/> class.
        /// </summary>
        /// <param name="processes">The processes.</param>
        public AddNewGameEvent(List<Process.GameProcess> processes)
        {
            InitializeComponent();

            p_processes = processes;

            gameEventsListBox.Items.Add("Timed Particle System");
            gameEventsListBox.Items.Add("Sound Effect");
            gameEventsListBox.Items.Add("Turn Light On");
            gameEventsListBox.Items.Add("Turn Light Off");
            gameEventsListBox.Items.Add("Toggle Light");
        }

        /// <summary>
        /// Handles the Click event of the addBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = gameEventsListBox.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    TimedParticleSystemEditorForm tpsef
                        = new TimedParticleSystemEditorForm(p_processes, -1);
                    tpsef.ShowDialog();
                    break;

                case 1:
                    SoundEffectEditorForm seef = new SoundEffectEditorForm(p_processes, -1);
                    seef.ShowDialog();
                    break;

                case 2:
                    SelectLightForm selectOnLightForm =
                        new SelectLightForm(GameSystem.GameSys_Instance.LightMgr);
                    selectOnLightForm.OnLightAccept += (RenderingSystem.GameLight gameLight) =>
                        {
                            Process.LightOnProcess lightOnProc =
                                new Process.LightOnProcess(gameLight);
                            p_processes.Add(lightOnProc);
                        };
                    selectOnLightForm.ShowDialog();
                    break;

                case 3:
                    SelectLightForm selectOutLightForm
                        = new SelectLightForm(GameSystem.GameSys_Instance.LightMgr);
                    selectOutLightForm.OnLightAccept += (RenderingSystem.GameLight gameLight) =>
                    {
                        Process.LightOutProcess lightOutProc =
                            new Process.LightOutProcess(gameLight);
                        p_processes.Add(lightOutProc);
                    };
                    selectOutLightForm.ShowDialog();
                    break;

                case 4:
                    SelectLightForm selectToggleLightForm
                        = new SelectLightForm(GameSystem.GameSys_Instance.LightMgr);
                    selectToggleLightForm.OnLightAccept += (RenderingSystem.GameLight gameLight) =>
                    {
                        Process.LightToggleProcess lightOutProc =
                            new Process.LightToggleProcess(gameLight);
                        p_processes.Add(lightOutProc);
                    };
                    selectToggleLightForm.ShowDialog();
                    break;

                default:
                    RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                    return;
            }
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