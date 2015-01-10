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
using Microsoft.Xna.Framework;
using RenderingSystem.Graphics.Forms;

namespace BaseLogic.Editor.Forms
{
    public partial class DuplicateGroupForm : Form
    {
        private Manager.ObjectMgr p_objMgr;

        public DuplicateGroupForm(Manager.ObjectMgr pObjMgr)
        {
            p_objMgr = pObjMgr;

            InitializeComponent();
            selectedObjsListBox.SelectionMode = SelectionMode.MultiExtended;

            UpdateUI();
        }

        public void SetSelectedIndicies(IEnumerable<int> selectedIndices)
        {
            selectedObjsListBox.SelectedIndices.Clear();
            foreach (int index in selectedIndices)
                selectedObjsListBox.SelectedIndices.Add(index);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void duplicateBtn_Click(object sender, EventArgs e)
        {
            // First parse the offset position.
            Vector3 offsetPos;
            bool parseSuccess = true;
            if (!float.TryParse(xOffsetTxtBox.Text, out offsetPos.X))
                parseSuccess = false;
            if (!float.TryParse(yOffsetTxtBox.Text, out offsetPos.Y))
                parseSuccess = false;
            if (!float.TryParse(zOffsetTxtBox.Text, out offsetPos.Z))
                parseSuccess = false;

            if (!parseSuccess)
            {
                WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }

            // Get the selected game objects.
            List<RenderingSystem.GameObj> selectedGameObjs = new List<RenderingSystem.GameObj>();
            for (int i = 0; i < selectedObjsListBox.Items.Count; ++i)
            {
                if (selectedObjsListBox.GetSelected(i))
                {
                    RenderingSystem.GameObj gameObj = p_objMgr.GetDataElementAt(i);
                    selectedGameObjs.Add(gameObj);
                }
            }

            // Clone each of the objects.
            IEnumerable<RenderingSystem.GameObj> clonedGameObjs = from go in selectedGameObjs
                                                                  select go.Clone();

            // Offset each of the objects.
            int clonedCount = clonedGameObjs.Count();
            RenderingSystem.GameObj[] finalCloned = new RenderingSystem.GameObj[clonedCount];
            for (int i = 0; i < clonedCount; ++i)
            {
                RenderingSystem.GameObj gameObj = clonedGameObjs.ElementAt(i);
                gameObj.Position += offsetPos;
                finalCloned[i] = gameObj;
            }

            p_objMgr.AddRange(finalCloned);
        }

        private void UpdateUI()
        {
            selectedObjsListBox.Items.Clear();
            foreach (var gameObj in p_objMgr.GetDataElements())
            {
                selectedObjsListBox.Items.Add(gameObj.ActorID);
            }
        }
    }
}