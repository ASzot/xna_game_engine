#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;

using Microsoft.Xna.Framework;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    /// Form for editing matrices.
    /// </summary>
    public partial class MatrixEditorForm : Form
    {
        /// <summary>
        /// Fired when the user clicks the accept button.
        /// </summary>
        public Action<Matrix> OnMatrixAccepted;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixEditorForm"/> class.
        /// </summary>
        /// <param name="m">The m.</param>
        public MatrixEditorForm(Matrix m)
            : this()
        {
            SetUI(m);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixEditorForm"/> class.
        /// </summary>
        public MatrixEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            Matrix mat = new Matrix();

            bool success = true;
            if (!float.TryParse(m_11_txtBox.Text, out mat.M11))
                success = false;
            if (!float.TryParse(m_12_txtBox.Text, out mat.M12))
                success = false;
            if (!float.TryParse(m_13_txtBox.Text, out mat.M13))
                success = false;
            if (!float.TryParse(m_14_txtBox.Text, out mat.M14))
                success = false;

            if (!float.TryParse(m_21_txtBox.Text, out mat.M21))
                success = false;
            if (!float.TryParse(m_22_txtBox.Text, out mat.M22))
                success = false;
            if (!float.TryParse(m_23_txtBox.Text, out mat.M23))
                success = false;
            if (!float.TryParse(m_24_txtBox.Text, out mat.M24))
                success = false;

            if (!float.TryParse(m_31_txtBox.Text, out mat.M31))
                success = false;
            if (!float.TryParse(m_32_txtBox.Text, out mat.M32))
                success = false;
            if (!float.TryParse(m_33_txtBox.Text, out mat.M33))
                success = false;
            if (!float.TryParse(m_34_txtBox.Text, out mat.M34))
                success = false;

            if (!float.TryParse(m_41_txtBox.Text, out mat.M41))
                success = false;
            if (!float.TryParse(m_42_txtBox.Text, out mat.M42))
                success = false;
            if (!float.TryParse(m_43_txtBox.Text, out mat.M43))
                success = false;
            if (!float.TryParse(m_44_txtBox.Text, out mat.M44))
                success = false;

            if (!success)
            {
                RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }

            if (OnMatrixAccepted != null)
                OnMatrixAccepted(mat);

            closeBtn_Click(null, null);
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
        /// Handles the Click event of the identityBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void identityBtn_Click(object sender, EventArgs e)
        {
            SetUI(Matrix.Identity);
        }

        /// <summary>
        /// Sets the UI.
        /// </summary>
        /// <param name="m">The m.</param>
        private void SetUI(Matrix m)
        {
            m_11_txtBox.Text = m.M11.ToString();
            m_12_txtBox.Text = m.M12.ToString();
            m_13_txtBox.Text = m.M13.ToString();
            m_14_txtBox.Text = m.M14.ToString();

            m_21_txtBox.Text = m.M21.ToString();
            m_22_txtBox.Text = m.M22.ToString();
            m_23_txtBox.Text = m.M23.ToString();
            m_24_txtBox.Text = m.M24.ToString();

            m_31_txtBox.Text = m.M31.ToString();
            m_32_txtBox.Text = m.M32.ToString();
            m_33_txtBox.Text = m.M33.ToString();
            m_34_txtBox.Text = m.M34.ToString();

            m_41_txtBox.Text = m.M41.ToString();
            m_42_txtBox.Text = m.M42.ToString();
            m_43_txtBox.Text = m.M43.ToString();
            m_44_txtBox.Text = m.M44.ToString();
        }
    }
}