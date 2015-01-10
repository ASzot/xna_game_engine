#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms
{
    public partial class DuplicateObjForm : Form
    {
        private Manager.ObjectMgr p_objMgr = null;
        private RenderingSystem.GameObj p_originalObj = null;

        public DuplicateObjForm(RenderingSystem.GameObj pOriginalGameObj, Manager.ObjectMgr pObjMgr)
        {
            InitializeComponent();

            p_originalObj = pOriginalGameObj;
            p_objMgr = pObjMgr;

            xPosTxtBox.Text = p_originalObj.Position.X.ToString();
            yPosTxtBox.Text = p_originalObj.Position.Y.ToString();
            zPosTxtBox.Text = p_originalObj.Position.Z.ToString();

            xRotTxtBox.Text = "0";
            yRotTxtBox.Text = "0";
            zRotTxtBox.Text = "0";
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void duplicateBtn_Click(object sender, EventArgs e)
        {
            float x, y, z, rx, ry, rz;

            bool success = true;
            if (!float.TryParse(xPosTxtBox.Text, out x))
                success = false;
            if (!float.TryParse(yPosTxtBox.Text, out y))
                success = false;
            if (!float.TryParse(zPosTxtBox.Text, out z))
                success = false;
            if (!float.TryParse(xRotTxtBox.Text, out rx))
                success = false;
            if (!float.TryParse(yRotTxtBox.Text, out ry))
                success = false;
            if (!float.TryParse(zRotTxtBox.Text, out rz))
                success = false;
            if (idTxtBox.Text == "" || p_objMgr.ActorIDExists(idTxtBox.Text))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid data input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RenderingSystem.GameObj dupGameObj = p_originalObj.Clone();
            dupGameObj.Position = new Microsoft.Xna.Framework.Vector3(x, y, z);
            var rot = Microsoft.Xna.Framework.Quaternion.CreateFromYawPitchRoll(ry, rx, rz);
            dupGameObj.Rotation = rot;
            dupGameObj.ActorID = idTxtBox.Text;

            GameSystem.GameSys_Instance.AddRenderObj(dupGameObj);

            closeBtn_Click(null, null);
        }
    }
}