namespace BaseLogic.Editor.Forms.Object_Editors
{
    partial class AddNewGameEvent
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
            this.gameEventsListBox = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(141, 345);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(112, 35);
            this.closeBtn.TabIndex = 14;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // gameEventsListBox
            // 
            this.gameEventsListBox.FormattingEnabled = true;
            this.gameEventsListBox.ItemHeight = 20;
            this.gameEventsListBox.Location = new System.Drawing.Point(13, 13);
            this.gameEventsListBox.Name = "gameEventsListBox";
            this.gameEventsListBox.Size = new System.Drawing.Size(255, 324);
            this.gameEventsListBox.TabIndex = 15;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(21, 345);
            this.addBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(112, 35);
            this.addBtn.TabIndex = 16;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // AddNewGameEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 394);
            this.ControlBox = false;
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.gameEventsListBox);
            this.Controls.Add(this.closeBtn);
            this.Name = "AddNewGameEvent";
            this.Text = "Add New Game Event";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.ListBox gameEventsListBox;
        private System.Windows.Forms.Button addBtn;
    }
}