#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;
using BaseLogic.Manager;
using Henge3D.Physics;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class ObjPhysEditorForm : Form
    {
        /// <summary>
        /// The physic s_ dat a_ folde r_ name
        /// </summary>
        private const string PHYSICS_DATA_FOLDER_NAME = "Physics Data";

        /// <summary>
        /// The p_game object
        /// </summary>
        private StaticObj p_gameObj;

        /// <summary>
        /// The p_obj MGR
        /// </summary>
        private Manager.ObjectMgr p_objMgr;

        /// <summary>
        /// The p_physics
        /// </summary>
        private Henge3D.Physics.PhysicsManager p_physics;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjPhysEditorForm"/> class.
        /// </summary>
        /// <param name="pGameObj">The p game object.</param>
        /// <param name="pObjMgr">The p object MGR.</param>
        public ObjPhysEditorForm(StaticObj pGameObj, Manager.ObjectMgr pObjMgr)
        {
            p_gameObj = pGameObj;
            p_physics = pGameObj.RigidBody.Manager;
            p_objMgr = pObjMgr;

            InitializeComponent();

            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of the addRevJointConstraint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addRevJointConstraint_Click(object sender, EventArgs e)
        {
            AddRevoluteJointForm arjf = new AddRevoluteJointForm(p_objMgr, p_gameObj);
            arjf.OnClose += () =>
                {
                    UpdateUI();
                };
            arjf.ShowDialog();
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
        /// Handles the Click event of the deleteConstraintBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteConstraintBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = constraintsListBox.SelectedIndex;

            if (selectedIndex == -1)
                return;

            var constraint = p_gameObj.RigidBody.Constraints[selectedIndex];
            p_physics.Remove(constraint);

            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of the editConstraintBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException">Unsupported constraint!</exception>
        private void editConstraintBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = constraintsListBox.SelectedIndex;

            if (selectedIndex == -1)
                return;

            Henge3D.Physics.Constraint constraint = p_gameObj.RigidBody.Constraints[selectedIndex];

            if (constraint is GameRevoluteJoint)
            {
                GameRevoluteJoint revoluteJoint = constraint as GameRevoluteJoint;
                p_physics.Remove(revoluteJoint);
                AddRevoluteJointForm arjf = new AddRevoluteJointForm(p_objMgr, p_gameObj, revoluteJoint);
                arjf.ShowDialog();
            }
            else
                throw new ArgumentException("Unsupported constraint!");
        }

        /// <summary>
        /// Handles the Click event of the editInertiaBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editInertiaBtn_Click(object sender, EventArgs e)
        {
            var massProps = p_gameObj.RigidBody.MassProperties;
            var inertia = massProps.Inertia;

            MatrixEditorForm matrixEditorForm = new MatrixEditorForm(inertia);
            matrixEditorForm.OnMatrixAccepted += (Microsoft.Xna.Framework.Matrix matrix) =>
                {
                    p_gameObj.SetMass(new MassProperties(massProps.Mass, matrix));
                };

            matrixEditorForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the makeImmovableBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void makeImmovableBtn_Click(object sender, EventArgs e)
        {
            if (p_gameObj is StaticObj)
            {
                StaticObj staticObj = p_gameObj as StaticObj;
                staticObj.SetMass(MassProperties.Immovable);
            }
            else
            {
                p_gameObj.RigidBody.MassProperties = MassProperties.Immovable;
            }

            UpdateUI();
        }

        /// <summary>
        /// Handles the TextChanged event of the massTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void massTxtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = textBox.Text;

            float fParsed;
            bool fParseSuccess = float.TryParse(text, out fParsed);

            MassProperties massProps = new MassProperties(fParsed, Microsoft.Xna.Framework.Matrix.Identity);

            if (p_gameObj is StaticObj)
            {
                StaticObj staticObj = p_gameObj as StaticObj;
                staticObj.SetMass(massProps);
            }
            else
            {
                p_gameObj.RigidBody.MassProperties = massProps;
            }
        }

        /// <summary>
        /// Handles the Click event of the selectPhysicsSettingsFilenameBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void selectPhysicsSettingsFilenameBtn_Click(object sender, EventArgs e)
        {
            string startingDir = Object.FileHelper.GetContentSaveLocation() + PHYSICS_DATA_FOLDER_NAME;

            OpenFileDialog ofd = new OpenFileDialog();
            Object.FileHelper.GetSaveLevelLocationCompleteFilename();
            ofd.InitialDirectory = startingDir;
            ofd.Filter = "XML Files|*.xml";
            ofd.FilterIndex = 0;
            // If this was false our working directory would change which would alter the file loading  code.
            ofd.RestoreDirectory = true;
            ofd.ShowDialog();
            if (ofd.FileName == "")
            {
                // The user didn't select anything.
                return;
            }

            System.IO.Stream stream = ofd.OpenFile();
            string selectedFilename = ofd.FileName;
            string actualFilename = Object.FileHelper.StripName(selectedFilename);
            p_gameObj.LoadPhysicsFromFile(stream, selectedFilename);
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <exception cref="System.ArgumentException">Unsupported constraint!</exception>
        private void UpdateUI()
        {
            constraintsListBox.Items.Clear();

            var constraints = p_gameObj.RigidBody.Constraints;
            foreach (Henge3D.Physics.Constraint constraint in constraints)
            {
                string name = null;
                if (constraint is GameRevoluteJoint)
                    name = "Revolute Joint";
                else
                    throw new ArgumentException("Unsupported constraint!");

                constraintsListBox.Items.Add(name);
            }

            float mass = p_gameObj.RigidBody.MassProperties.Mass;

            massTxtBox.Text = mass.ToString();

            if (p_gameObj is StaticObj)
            {
                StaticObj staticObj = p_gameObj as StaticObj;
                string filename = staticObj.PhysicsFilename;
                if (filename != null)
                    physicsSettingsFilenameTxtBox.Text = filename;
                else
                    physicsSettingsFilenameTxtBox.Text = "(Nothing loaded)";
            }
        }
    }
}