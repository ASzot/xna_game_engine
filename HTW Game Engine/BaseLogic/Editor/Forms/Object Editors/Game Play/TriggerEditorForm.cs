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
    public partial class TriggerEditorForm : Form
    {
        /// <summary>
        /// The _trigger processes
        /// </summary>
        private List<Process.GameProcess> _triggerProcesses;

        /// <summary>
        /// The p_process MGR
        /// </summary>
        private Manager.ProcessMgr p_processMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerEditorForm"/> class.
        /// </summary>
        /// <param name="pProcMgr">The p proc MGR.</param>
        public TriggerEditorForm(Manager.ProcessMgr pProcMgr)
        {
            InitializeComponent();

            p_processMgr = pProcMgr;
            UpdateTriggerListBox();
        }

        /// <summary>
        /// Handles the Click event of the addBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addBtn_Click(object sender, EventArgs e)
        {
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
        /// Handles the Click event of the deleteBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = triggerListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;
            Process.GameProcess procToDelete = _triggerProcesses[selectedIndex];
        }

        /// <summary>
        /// Handles the Click event of the editBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = triggerListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;
            Process.GameProcess procToEdit = _triggerProcesses[selectedIndex];
        }

        /// <summary>
        /// Updates the trigger ListBox.
        /// </summary>
        private void UpdateTriggerListBox()
        {
            var actorTriggers = p_processMgr.GetProcessesOfType(typeof(Process.ActorTrigger));
            triggerListBox.Items.Clear();
            int areaTriggerCount = 1;
            int triggerCount = 1;
            foreach (Process.GameProcess gameProc in actorTriggers)
            {
                if (gameProc is Process.ActorTriggerRectangle)
                    triggerListBox.Items.Add("Rectangle Trigger " + (areaTriggerCount++).ToString());
                else
                    triggerListBox.Items.Add("Trigger " + (triggerCount++).ToString());
            }
            _triggerProcesses = actorTriggers.ToList();
        }
    }
}