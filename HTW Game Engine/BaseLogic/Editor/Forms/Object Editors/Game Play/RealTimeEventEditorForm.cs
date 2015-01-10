#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms.Object_Editors
{
    /// <summary>
    ///
    /// </summary>
    public partial class RealTimeEventEditorForm : Form
    {
        /// <summary>
        /// The p_rte triggers
        /// </summary>
        private List<Process.RealTimeEventTrigger> p_rteTriggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealTimeEventEditorForm"/> class.
        /// </summary>
        public RealTimeEventEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the rte triggers.
        /// </summary>
        /// <param name="rteTriggers">The rte triggers.</param>
        public void SetRTETriggers(List<Process.RealTimeEventTrigger> rteTriggers)
        {
            p_rteTriggers = rteTriggers;

            firedProcsListBox.Enabled = false;
            eventNamesListBox.Enabled = false;
            addBtn.Enabled = false;
            editBtn.Enabled = false;
            deleteBtn.Enabled = false;

            var rteTriggersNames = from rtet in rteTriggers
                                   select rtet.ToString();
            rteProcListBox.Items.Clear();
            rteProcListBox.Items.AddRange(rteTriggersNames.ToArray());
        }

        /// <summary>
        /// Handles the Click event of the addBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addBtn_Click(object sender, EventArgs e)
        {
            int selectedChainIndex = eventNamesListBox.SelectedIndex;
            int selectedRTEIndex = rteProcListBox.SelectedIndex;

            Process.RealTimeEventTrigger rter = p_rteTriggers[selectedRTEIndex];
            string name = rter.GetAllRealTimeEventChainNames()[selectedChainIndex];
            var firedProcs = rter.GetAllGameProccesses(name);

            AddNewGameEvent ange = new AddNewGameEvent(firedProcs);
            ange.ShowDialog();

            var gameProcs = rter.GetAllGameProccesses(name);
            var gameProcNames = from gp in gameProcs
                                select gp.ToString();

            firedProcsListBox.Items.Clear();
            firedProcsListBox.Items.AddRange(gameProcNames.ToArray());
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
        /// Creates the process edit form.
        /// </summary>
        /// <param name="processes">The processes.</param>
        /// <param name="selectedIndex">Index of the selected.</param>
        /// <exception cref="System.ArgumentException">Invalid process editing type!</exception>
        private void CreateProcessEditForm(List<Process.GameProcess> processes, int selectedIndex)
        {
            Process.GameProcess gameProc = processes[selectedIndex];
            if (gameProc is Process.ParticleEffectProcess)
            {
                TimedParticleSystemEditorForm tpsef = new TimedParticleSystemEditorForm(processes, selectedIndex);
                tpsef.ShowDialog();
            }
            else if (gameProc is Process.SoundEffectProcess)
            {
                SoundEffectEditorForm seef = new SoundEffectEditorForm(processes, selectedIndex);
                seef.ShowDialog();
            }
            else if (gameProc is Process.LightOnProcess)
            {
                Process.LightOnProcess lightOnProc = gameProc as Process.LightOnProcess;
                SelectLightForm selectLightForm = new SelectLightForm(GameSystem.GameSys_Instance.LightMgr);
                selectLightForm.OnLightAccept += (RenderingSystem.GameLight gameLight) =>
                    {
                        lightOnProc.Light = gameLight;
                    };
                selectLightForm.ShowDialog();
            }
            else if (gameProc is Process.LightOutProcess)
            {
                Process.LightOutProcess lightOutProc = gameProc as Process.LightOutProcess;
                SelectLightForm selectLightForm = new SelectLightForm(GameSystem.GameSys_Instance.LightMgr);
                selectLightForm.OnLightAccept += (RenderingSystem.GameLight gameLight) =>
                    {
                        lightOutProc.Light = gameLight;
                    };
            }
            else if (gameProc is Process.LightToggleProcess)
            {
                Process.LightToggleProcess lightToggleProc = gameProc as Process.LightToggleProcess;
                SelectLightForm selectLightForm = new SelectLightForm(GameSystem.GameSys_Instance.LightMgr);
                selectLightForm.OnLightAccept += (RenderingSystem.GameLight gameLight) =>
                    {
                        lightToggleProc.Light = gameLight;
                    };
            }
            else
                throw new ArgumentException("Invalid process editing type!");
        }

        /// <summary>
        /// Handles the Click event of the deleteBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int selectedChainIndex = eventNamesListBox.SelectedIndex;
            int selectedRTEIndex = rteProcListBox.SelectedIndex;
            int firedProcIndex = firedProcsListBox.SelectedIndex;

            Process.RealTimeEventTrigger rter = p_rteTriggers[selectedRTEIndex];
            string name = rter.GetAllRealTimeEventChainNames()[selectedChainIndex];

            rter.RemoveRealTimeEvent(name, firedProcIndex);

            firedProcsListBox.SelectedIndex = -1;

            eventNamesListBox_SelectedIndexChanged(null, null);

            var gameProcs = rter.GetAllGameProccesses(name);
            var gameProcNames = from gp in gameProcs
                                select gp.ToString();

            firedProcsListBox.Items.Clear();
            firedProcsListBox.Items.AddRange(gameProcNames.ToArray());
        }

        /// <summary>
        /// Handles the Click event of the editBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void editBtn_Click(object sender, EventArgs e)
        {
            int selectedChainIndex = eventNamesListBox.SelectedIndex;
            int selectedRTEIndex = rteProcListBox.SelectedIndex;
            int firedProcIndex = firedProcsListBox.SelectedIndex;

            if (selectedChainIndex == -1 || selectedRTEIndex == -1 || firedProcIndex == -1)
                throw new ArgumentException();

            Process.RealTimeEventTrigger rter = p_rteTriggers[selectedRTEIndex];
            string name = rter.GetAllRealTimeEventChainNames()[selectedChainIndex];

            List<Process.GameProcess> processes = rter.GetAllGameProccesses(name);

            CreateProcessEditForm(processes, firedProcIndex);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the eventNamesListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void eventNamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedChainIndex = eventNamesListBox.SelectedIndex;
            int selectedRTEIndex = rteProcListBox.SelectedIndex;

            if (selectedRTEIndex == -1)
                throw new ArgumentException();

            firedProcsListBox.Items.Clear();
            if (selectedChainIndex == -1)
            {
                firedProcsListBox.Enabled = false;
                addBtn.Enabled = false;
                return;
            }
            firedProcsListBox.Enabled = true;

            Process.RealTimeEventTrigger rter = p_rteTriggers[selectedRTEIndex];
            string name = rter.GetAllRealTimeEventChainNames()[selectedChainIndex];
            var gameProcs = rter.GetAllGameProccesses(name);
            var gameProcNames = from gp in gameProcs
                                select gp.ToString();

            firedProcsListBox.Items.AddRange(gameProcNames.ToArray());

            addBtn.Enabled = true;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the firedProcsListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void firedProcsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = firedProcsListBox.SelectedIndex;
            if (selectedIndex == -1)
            {
                editBtn.Enabled = false;
                deleteBtn.Enabled = false;
                return;
            }

            editBtn.Enabled = true;
            deleteBtn.Enabled = true;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the rteProcListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rteProcListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = rteProcListBox.SelectedIndex;

            firedProcsListBox.Items.Clear();
            firedProcsListBox.Enabled = false;
            addBtn.Enabled = false;
            deleteBtn.Enabled = false;
            editBtn.Enabled = false;
            eventNamesListBox.Items.Clear();
            if (selectedIndex == -1)
            {
                eventNamesListBox.Enabled = false;
                return;
            }
            eventNamesListBox.Enabled = true;

            Process.RealTimeEventTrigger rter = p_rteTriggers[selectedIndex];
            string[] names = rter.GetAllRealTimeEventChainNames();
            eventNamesListBox.Items.AddRange(names);
        }
    }
}