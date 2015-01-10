namespace BaseLogic.Editor.Forms
{
    partial class ObjPhysEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.constraintsListBox = new System.Windows.Forms.ListBox();
            this.deleteConstraintBtn = new System.Windows.Forms.Button();
            this.editConstraintBtn = new System.Windows.Forms.Button();
            this.addRevJointConstraint = new System.Windows.Forms.Button();
            this.massTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.makeImmovableBtn = new System.Windows.Forms.Button();
            this.physicsSettingsFilenameTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectPhysicsSettingsFilenameBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.editInertiaBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // constraintsListBox
            // 
            this.constraintsListBox.FormattingEnabled = true;
            this.constraintsListBox.Location = new System.Drawing.Point(12, 207);
            this.constraintsListBox.Name = "constraintsListBox";
            this.constraintsListBox.Size = new System.Drawing.Size(198, 186);
            this.constraintsListBox.TabIndex = 0;
            // 
            // deleteConstraintBtn
            // 
            this.deleteConstraintBtn.Location = new System.Drawing.Point(12, 442);
            this.deleteConstraintBtn.Name = "deleteConstraintBtn";
            this.deleteConstraintBtn.Size = new System.Drawing.Size(96, 37);
            this.deleteConstraintBtn.TabIndex = 2;
            this.deleteConstraintBtn.Text = "Delete Constraint";
            this.deleteConstraintBtn.UseVisualStyleBackColor = true;
            this.deleteConstraintBtn.Click += new System.EventHandler(this.deleteConstraintBtn_Click);
            // 
            // editConstraintBtn
            // 
            this.editConstraintBtn.Location = new System.Drawing.Point(114, 399);
            this.editConstraintBtn.Name = "editConstraintBtn";
            this.editConstraintBtn.Size = new System.Drawing.Size(96, 37);
            this.editConstraintBtn.TabIndex = 3;
            this.editConstraintBtn.Text = "Edit Constraint";
            this.editConstraintBtn.UseVisualStyleBackColor = true;
            this.editConstraintBtn.Click += new System.EventHandler(this.editConstraintBtn_Click);
            // 
            // addRevJointConstraint
            // 
            this.addRevJointConstraint.Location = new System.Drawing.Point(12, 399);
            this.addRevJointConstraint.Name = "addRevJointConstraint";
            this.addRevJointConstraint.Size = new System.Drawing.Size(96, 37);
            this.addRevJointConstraint.TabIndex = 4;
            this.addRevJointConstraint.Text = "Add Revolute Joint";
            this.addRevJointConstraint.UseVisualStyleBackColor = true;
            this.addRevJointConstraint.Click += new System.EventHandler(this.addRevJointConstraint_Click);
            // 
            // massTxtBox
            // 
            this.massTxtBox.Location = new System.Drawing.Point(12, 116);
            this.massTxtBox.Name = "massTxtBox";
            this.massTxtBox.Size = new System.Drawing.Size(100, 20);
            this.massTxtBox.TabIndex = 5;
            this.massTxtBox.TextChanged += new System.EventHandler(this.massTxtBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mass:";
            // 
            // makeImmovableBtn
            // 
            this.makeImmovableBtn.Location = new System.Drawing.Point(12, 142);
            this.makeImmovableBtn.Name = "makeImmovableBtn";
            this.makeImmovableBtn.Size = new System.Drawing.Size(100, 37);
            this.makeImmovableBtn.TabIndex = 7;
            this.makeImmovableBtn.Text = "Make Immovable";
            this.makeImmovableBtn.UseVisualStyleBackColor = true;
            this.makeImmovableBtn.Click += new System.EventHandler(this.makeImmovableBtn_Click);
            // 
            // physicsSettingsFilenameTxtBox
            // 
            this.physicsSettingsFilenameTxtBox.Location = new System.Drawing.Point(6, 36);
            this.physicsSettingsFilenameTxtBox.Name = "physicsSettingsFilenameTxtBox";
            this.physicsSettingsFilenameTxtBox.ReadOnly = true;
            this.physicsSettingsFilenameTxtBox.Size = new System.Drawing.Size(239, 20);
            this.physicsSettingsFilenameTxtBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Physics Settings Filename:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectPhysicsSettingsFilenameBtn);
            this.groupBox1.Controls.Add(this.physicsSettingsFilenameTxtBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 98);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Body Settings";
            // 
            // selectPhysicsSettingsFilenameBtn
            // 
            this.selectPhysicsSettingsFilenameBtn.Location = new System.Drawing.Point(60, 62);
            this.selectPhysicsSettingsFilenameBtn.Name = "selectPhysicsSettingsFilenameBtn";
            this.selectPhysicsSettingsFilenameBtn.Size = new System.Drawing.Size(75, 23);
            this.selectPhysicsSettingsFilenameBtn.TabIndex = 10;
            this.selectPhysicsSettingsFilenameBtn.Text = "Select File";
            this.selectPhysicsSettingsFilenameBtn.UseVisualStyleBackColor = true;
            this.selectPhysicsSettingsFilenameBtn.Click += new System.EventHandler(this.selectPhysicsSettingsFilenameBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Constraints:";
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(200, 494);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 12;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // editInertiaBtn
            // 
            this.editInertiaBtn.Location = new System.Drawing.Point(118, 142);
            this.editInertiaBtn.Name = "editInertiaBtn";
            this.editInertiaBtn.Size = new System.Drawing.Size(100, 37);
            this.editInertiaBtn.TabIndex = 13;
            this.editInertiaBtn.Text = "Edit Inertia";
            this.editInertiaBtn.UseVisualStyleBackColor = true;
            this.editInertiaBtn.Click += new System.EventHandler(this.editInertiaBtn_Click);
            // 
            // ObjPhysEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(287, 529);
            this.ControlBox = false;
            this.Controls.Add(this.editInertiaBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.makeImmovableBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.massTxtBox);
            this.Controls.Add(this.addRevJointConstraint);
            this.Controls.Add(this.editConstraintBtn);
            this.Controls.Add(this.deleteConstraintBtn);
            this.Controls.Add(this.constraintsListBox);
            this.Name = "ObjPhysEditorForm";
            this.Text = "Object Physics Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox constraintsListBox;
        private System.Windows.Forms.Button deleteConstraintBtn;
        private System.Windows.Forms.Button editConstraintBtn;
        private System.Windows.Forms.Button addRevJointConstraint;
        private System.Windows.Forms.TextBox massTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button makeImmovableBtn;
        private System.Windows.Forms.TextBox physicsSettingsFilenameTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button selectPhysicsSettingsFilenameBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button editInertiaBtn;
    }
}