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

using BaseLogic.Process;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class LightModifierEditor : Form
    {
        /// <summary>
        /// The p_light MGR
        /// </summary>
        private Manager.LightManager p_lightMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightModifierEditor"/> class.
        /// </summary>
        /// <param name="pLightMgr">The p light MGR.</param>
        public LightModifierEditor(Manager.LightManager pLightMgr)
        {
            InitializeComponent();

            p_lightMgr = pLightMgr;

            UpdateObjsAppliedListBox();
            UpdateModifiersListBox();

            addModifierBtn.Enabled = false;
            deleteModifierBtn.Enabled = false;
            editModifierBtn.Enabled = false;
        }

        /// <summary>
        /// Updates the objs applied ListBox.
        /// </summary>
        public void UpdateObjsAppliedListBox()
        {
            int selectedIndex = modifierListBox.SelectedIndex;

            appliedToListBox.Items.Clear();
            foreach (var gl in p_lightMgr.GetDataElements())
            {
                appliedToListBox.Items.Add(gl.LightID);
            }
            if (selectedIndex == -1)
                return;

            IEnumerable<GameProcess> gameProcs = GameSystem.GameSys_Instance.ProcessMgr.GameProcesses;
            var flashingLights = from gp in gameProcs
                                 where gp is FlashingLightProcess
                                 select gp as FlashingLightProcess;

            FlashingLightProcess flp = flashingLights.ElementAt(selectedIndex);
            RenderingSystem.GameLight gameLight = flp.GetModifyingLight();

            int indexOfLight = p_lightMgr.IndexOfLight(gameLight);
            appliedToListBox.SelectedIndex = indexOfLight;
        }

        /// <summary>
        /// Handles the Click event of the addModifierBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addModifierBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = appliedToListBox.SelectedIndex;
            var gameLight = p_lightMgr.GetDataElementAt(selectedIndex);

            FlashingLightProcess flp = new FlashingLightProcess(0, 0, gameLight);

            EditFlashingLightForm eflf = new EditFlashingLightForm(true, flp);
            eflf.OnAccept += () =>
                {
                    GameSystem.GameSys_Instance.AddGameProcess(flp);
                };
            eflf.ShowDialog();

            UpdateModifiersListBox();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the appliedToListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void appliedToListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = appliedToListBox.SelectedIndex;
            if (selectedIndex == -1)
                addModifierBtn.Enabled = false;
            else
                addModifierBtn.Enabled = true;
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
        /// Handles the Click event of the deleteModifierBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteModifierBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = modifierListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            IEnumerable<GameProcess> gameProcs = GameSystem.GameSys_Instance.ProcessMgr.GameProcesses;
            var flashingLights = from gp in gameProcs
                                 where gp is FlashingLightProcess
                                 select gp as FlashingLightProcess;

            FlashingLightProcess flp = flashingLights.ElementAt(selectedIndex);

            GameSystem.GameSys_Instance.ProcessMgr.GameProcesses.Remove(flp);

            UpdateModifiersListBox();
        }

        /// <summary>
        /// Handles the Click event of the editModifierBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editModifierBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = modifierListBox.SelectedIndex;

            IEnumerable<GameProcess> gameProcs = GameSystem.GameSys_Instance.ProcessMgr.GameProcesses;
            var flashingLights = from gp in gameProcs
                                 where gp is FlashingLightProcess
                                 select gp as FlashingLightProcess;

            FlashingLightProcess flp = flashingLights.ElementAt(selectedIndex);

            EditFlashingLightForm eflf = new EditFlashingLightForm(false, flp);
            eflf.ShowDialog();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the modifierListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void modifierListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = modifierListBox.SelectedIndex;
            if (selectedIndex == -1)
            {
                deleteModifierBtn.Enabled = false;
                editModifierBtn.Enabled = false;
            }
            else
            {
                deleteModifierBtn.Enabled = true;
                editModifierBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Updates the modifiers ListBox.
        /// </summary>
        private void UpdateModifiersListBox()
        {
            IEnumerable<GameProcess> gameProcs = GameSystem.GameSys_Instance.ProcessMgr.GameProcesses;

            modifierListBox.Items.Clear();

            var flashingLights = from gp in gameProcs
                                 where gp is FlashingLightProcess
                                 select gp as FlashingLightProcess;
            for (int i = 0; i < flashingLights.Count(); ++i)
            {
                modifierListBox.Items.Add("Flashing Light " + (++i).ToString());
            }

            int selectedIndex = modifierListBox.SelectedIndex;
            if (selectedIndex == -1)
            {
                deleteModifierBtn.Enabled = false;
                editModifierBtn.Enabled = false;
            }
            else
            {
                deleteModifierBtn.Enabled = true;
                editModifierBtn.Enabled = true;
            }
        }
    }
}