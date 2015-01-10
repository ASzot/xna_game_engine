#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RenderingSystem.Graphics.Forms
{
	/// <summary>
	/// Editor form allowing the user to change the post processing effects which are applied and the order in which they are applied.
	/// </summary>
    partial class PPEMgrForm : Form
    {
		/// <summary>
		/// A pointer to the post processing effect manager.
		/// </summary>
        private PPEMgr p_ppeMgr;

		/// <summary>
		/// Fired on the form closing.
		/// </summary>
        public Action OnDlgQuit;

		/// <summary>
		/// Construct the post processing effect manager form
		/// </summary>
		/// <param name="ppeMgr">A pointer to the post processing effect manager.</param>
        public PPEMgrForm(PPEMgr ppeMgr)
        {
            InitializeComponent();

            effectsTabControl.TabPages.Clear();

            p_ppeMgr = ppeMgr;

            UpdateUI();
        }

		/// <summary>
		/// Update the UI based on the data of the post processing effect manager.
		/// </summary>
        private void UpdateUI()
        {
            IEnumerable<PostProcessingEffect> ppes = p_ppeMgr.GetDataElements();
            effectsTabControl.TabPages.Clear();
            foreach (PostProcessingEffect ppe in ppes)
            {
                effectsTabControl.TabPages.Add(ppe.EffectName);
            }

            TabPage selectedTab = effectsTabControl.SelectedTab;
            int selectedIndex = effectsTabControl.SelectedIndex;

            UpdateTabInfo(selectedTab, selectedIndex);
        }

		/// <summary>
		/// Construct a label on the form for dynamic form generation.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		private Label CreateLabel(int x, int y, string text)
		{
			Label lbl = new Label();
			lbl.Top = y;
			lbl.Left = x;
			lbl.Text = text;
			return lbl;
		}

		/// <summary>
		/// Construct a button on the form for dynamic form generation.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="text"></param>
		/// <param name="btnClicked"></param>
		/// <returns></returns>
		private Button CreateButton(int x, int y, int width, int height, string text, Action<object, EventArgs> btnClicked)
		{
			Button btn = new Button();
			btn.Top = y;
			btn.Left = x;
			btn.Width = width;
			btn.Height = height;
			btn.Text = text;
			if (btnClicked != null)
				btn.Click += new EventHandler(btnClicked);

			return btn;
		}

		public Button CreateButton(int x, int y, int width, int height, string text)
		{
			return CreateButton(x, y, width, height, text, null);
		}

		/// <summary>
		/// Updates the selectable tabs based on the post processing effects of the post processing effect manager.
		/// </summary>
		/// <param name="tab"></param>
		/// <param name="selectedIndex"></param>
        private void UpdateTabInfo(TabPage tab, int selectedIndex)
        {
            if (selectedIndex == -1)
            {
                if (tab != null)
                    tab.Controls.Clear();
                return;
            }

            PostProcessingEffect ppe = p_ppeMgr.GetDataElementAt(selectedIndex);
			Button leftArrow = CreateButton(0, 10, 100, 40, "<-", MoveLeftBtnClicked);
			Button rightArrow = CreateButton(120, 10, 100, 40, "->", MoveRightBtnClicked);

            Label label = CreateLabel(40, 60, ppe.EffectName);

			Button createDlgBtn = CreateButton(20, 100, 120, 40, "Edit", ppe.CreateSettingsDlg);

			tab.Controls.Clear();
			tab.Controls.Add(leftArrow);
			tab.Controls.Add(rightArrow);
			tab.Controls.Add(createDlgBtn);
            tab.Controls.Add(label);
        }

		/// <summary>
		/// On the user navigating left readjusting the post processing effect order.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void MoveLeftBtnClicked(object sender, EventArgs e)
        {
            int selectedIndex = effectsTabControl.SelectedIndex;
            if (selectedIndex <= 0)
                return;

            var cur = p_ppeMgr.GetDataElementAt(selectedIndex);

            p_ppeMgr.RemoveDataElement(selectedIndex);
            p_ppeMgr.InsertElement(cur, selectedIndex - 1);

            UpdateUI();
        }

		/// <summary>
		/// On the user navigating right in the order readjusting the post processing effect order.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void MoveRightBtnClicked(object sender, EventArgs e)
        {
            int selectedIndex = effectsTabControl.SelectedIndex;
            if (selectedIndex >= p_ppeMgr.GetNumberOfDataElements() - 1)
                return;

            var cur = p_ppeMgr.GetDataElementAt(selectedIndex);

            p_ppeMgr.RemoveDataElement(selectedIndex);
            p_ppeMgr.InsertElement(cur, selectedIndex + 1);

            UpdateUI();
        }

		/// <summary>
		/// Close the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            OnDlgQuit();
            this.Close();
        }

		/// <summary>
		/// Update the ui with the settings of the post processing effect selected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void effectsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selectedTab = effectsTabControl.SelectedTab;
            int selectedIndex = effectsTabControl.SelectedIndex;

            if (selectedIndex == -1 || selectedTab == null)
                return;

            UpdateTabInfo(selectedTab, selectedIndex);
        }

		/// <summary>
		/// Add a new post processing effect.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void addBtn_Click(object sender, EventArgs e)
        {
            EffectSelectionForm esf = new EffectSelectionForm(p_ppeMgr);

            esf.OnClose += () =>
                {
                    UpdateUI();
                };

            esf.ShowDialog();
        }

		/// <summary>
		/// Delete the post processing effect.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void deleteEffectBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = effectsTabControl.SelectedIndex;

            if (selectedIndex == -1)
                return;

            p_ppeMgr.RemoveDataElement(selectedIndex);

            UpdateUI();
        }

		/// <summary>
		/// Change the anti-alaising effect settings which will always be applied.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void aaSettingsBtn_Click(object sender, EventArgs e)
        {
            AASettingsForm aasf = new AASettingsForm(p_ppeMgr.AntiAliasing);
            aasf.ShowDialog();
        }

    }
}
