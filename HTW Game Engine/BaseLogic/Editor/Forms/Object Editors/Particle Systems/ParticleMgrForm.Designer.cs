namespace BaseLogic.Editor.Forms
{
    partial class ParticleMgrForm
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
            this.particleSystemsListBox = new System.Windows.Forms.ListBox();
            this.editBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.createExistingBtn = new System.Windows.Forms.Button();
            this.createNewBtn = new System.Windows.Forms.Button();
            this.deleteSelectedBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // particleSystemsListBox
            // 
            this.particleSystemsListBox.FormattingEnabled = true;
            this.particleSystemsListBox.Location = new System.Drawing.Point(12, 12);
            this.particleSystemsListBox.Name = "particleSystemsListBox";
            this.particleSystemsListBox.Size = new System.Drawing.Size(159, 225);
            this.particleSystemsListBox.TabIndex = 0;
            this.particleSystemsListBox.SelectedIndexChanged += new System.EventHandler(this.particleSystemsListBox_SelectedIndexChanged);
            this.particleSystemsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.particleSystemsListBox_KeyPress);
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(194, 74);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(100, 38);
            this.editBtn.TabIndex = 1;
            this.editBtn.Text = "Edit Selected";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(194, 201);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(100, 38);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Close";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // createExistingBtn
            // 
            this.createExistingBtn.Location = new System.Drawing.Point(194, 118);
            this.createExistingBtn.Name = "createExistingBtn";
            this.createExistingBtn.Size = new System.Drawing.Size(100, 36);
            this.createExistingBtn.TabIndex = 3;
            this.createExistingBtn.Text = "Create Pre-Existing";
            this.createExistingBtn.UseVisualStyleBackColor = true;
            this.createExistingBtn.Click += new System.EventHandler(this.createExistingBtn_Click);
            // 
            // createNewBtn
            // 
            this.createNewBtn.Location = new System.Drawing.Point(194, 161);
            this.createNewBtn.Name = "createNewBtn";
            this.createNewBtn.Size = new System.Drawing.Size(100, 34);
            this.createNewBtn.TabIndex = 4;
            this.createNewBtn.Text = "Create New";
            this.createNewBtn.UseVisualStyleBackColor = true;
            this.createNewBtn.Click += new System.EventHandler(this.createNewBtn_Click);
            // 
            // deleteSelectedBtn
            // 
            this.deleteSelectedBtn.Location = new System.Drawing.Point(194, 32);
            this.deleteSelectedBtn.Name = "deleteSelectedBtn";
            this.deleteSelectedBtn.Size = new System.Drawing.Size(100, 36);
            this.deleteSelectedBtn.TabIndex = 5;
            this.deleteSelectedBtn.Text = "Delete Selected";
            this.deleteSelectedBtn.UseVisualStyleBackColor = true;
            this.deleteSelectedBtn.Click += new System.EventHandler(this.deleteSelectedBtn_Click);
            // 
            // ParticleMgrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 252);
            this.ControlBox = false;
            this.Controls.Add(this.deleteSelectedBtn);
            this.Controls.Add(this.createNewBtn);
            this.Controls.Add(this.createExistingBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.particleSystemsListBox);
            this.Name = "ParticleMgrForm";
            this.Text = "Particle System Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox particleSystemsListBox;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button createExistingBtn;
		private System.Windows.Forms.Button createNewBtn;
        private System.Windows.Forms.Button deleteSelectedBtn;
    }
}