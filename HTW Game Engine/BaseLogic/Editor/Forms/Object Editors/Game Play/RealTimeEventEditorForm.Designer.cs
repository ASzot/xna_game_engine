namespace BaseLogic.Editor.Forms.Object_Editors
{
    partial class RealTimeEventEditorForm
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
            this.closeBtn = new System.Windows.Forms.Button();
            this.rteProcListBox = new System.Windows.Forms.ListBox();
            this.eventNamesListBox = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.firedProcsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(716, 435);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(112, 35);
            this.closeBtn.TabIndex = 13;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // rteProcListBox
            // 
            this.rteProcListBox.FormattingEnabled = true;
            this.rteProcListBox.ItemHeight = 20;
            this.rteProcListBox.Location = new System.Drawing.Point(12, 33);
            this.rteProcListBox.Name = "rteProcListBox";
            this.rteProcListBox.Size = new System.Drawing.Size(267, 284);
            this.rteProcListBox.TabIndex = 14;
            this.rteProcListBox.SelectedIndexChanged += new System.EventHandler(this.rteProcListBox_SelectedIndexChanged);
            // 
            // eventNamesListBox
            // 
            this.eventNamesListBox.FormattingEnabled = true;
            this.eventNamesListBox.ItemHeight = 20;
            this.eventNamesListBox.Location = new System.Drawing.Point(285, 33);
            this.eventNamesListBox.Name = "eventNamesListBox";
            this.eventNamesListBox.Size = new System.Drawing.Size(267, 284);
            this.eventNamesListBox.TabIndex = 15;
            this.eventNamesListBox.SelectedIndexChanged += new System.EventHandler(this.eventNamesListBox_SelectedIndexChanged);
            // 
            // addBtn
            // 
            this.addBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.addBtn.Location = new System.Drawing.Point(577, 325);
            this.addBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(112, 35);
            this.addBtn.TabIndex = 16;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.deleteBtn.Location = new System.Drawing.Point(632, 370);
            this.deleteBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(112, 35);
            this.deleteBtn.TabIndex = 17;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.editBtn.Location = new System.Drawing.Point(697, 325);
            this.editBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(112, 35);
            this.editBtn.TabIndex = 18;
            this.editBtn.Text = "Edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // firedProcsListBox
            // 
            this.firedProcsListBox.FormattingEnabled = true;
            this.firedProcsListBox.ItemHeight = 20;
            this.firedProcsListBox.Location = new System.Drawing.Point(558, 33);
            this.firedProcsListBox.Name = "firedProcsListBox";
            this.firedProcsListBox.Size = new System.Drawing.Size(267, 284);
            this.firedProcsListBox.TabIndex = 19;
            this.firedProcsListBox.SelectedIndexChanged += new System.EventHandler(this.firedProcsListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Real Time Event Processes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Process Events";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(554, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "On Event Fired  Processes";
            // 
            // RealTimeEventEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(841, 484);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.firedProcsListBox);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.eventNamesListBox);
            this.Controls.Add(this.rteProcListBox);
            this.Controls.Add(this.closeBtn);
            this.Name = "RealTimeEventEditorForm";
            this.Text = "Real Time Event Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.ListBox rteProcListBox;
        private System.Windows.Forms.ListBox eventNamesListBox;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.ListBox firedProcsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}