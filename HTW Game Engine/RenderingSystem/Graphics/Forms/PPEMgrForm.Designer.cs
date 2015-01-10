namespace RenderingSystem.Graphics.Forms
{
    partial class PPEMgrForm
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
            this.effectsTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.closeBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.deleteEffectBtn = new System.Windows.Forms.Button();
            this.aaSettingsBtn = new System.Windows.Forms.Button();
            this.effectsTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // effectsTabControl
            // 
            this.effectsTabControl.Controls.Add(this.tabPage1);
            this.effectsTabControl.Controls.Add(this.tabPage2);
            this.effectsTabControl.Location = new System.Drawing.Point(12, 12);
            this.effectsTabControl.Name = "effectsTabControl";
            this.effectsTabControl.SelectedIndex = 0;
            this.effectsTabControl.Size = new System.Drawing.Size(295, 187);
            this.effectsTabControl.TabIndex = 0;
            this.effectsTabControl.SelectedIndexChanged += new System.EventHandler(this.effectsTabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(287, 161);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(287, 161);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(299, 233);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(98, 35);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(12, 233);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(89, 35);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "Add New Effect";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // deleteEffectBtn
            // 
            this.deleteEffectBtn.Location = new System.Drawing.Point(107, 233);
            this.deleteEffectBtn.Name = "deleteEffectBtn";
            this.deleteEffectBtn.Size = new System.Drawing.Size(89, 35);
            this.deleteEffectBtn.TabIndex = 3;
            this.deleteEffectBtn.Text = "Delete Effect";
            this.deleteEffectBtn.UseVisualStyleBackColor = true;
            this.deleteEffectBtn.Click += new System.EventHandler(this.deleteEffectBtn_Click);
            // 
            // aaSettingsBtn
            // 
            this.aaSettingsBtn.Location = new System.Drawing.Point(202, 233);
            this.aaSettingsBtn.Name = "aaSettingsBtn";
            this.aaSettingsBtn.Size = new System.Drawing.Size(89, 35);
            this.aaSettingsBtn.TabIndex = 4;
            this.aaSettingsBtn.Text = "Anti Aliasing Settings";
            this.aaSettingsBtn.UseVisualStyleBackColor = true;
            this.aaSettingsBtn.Click += new System.EventHandler(this.aaSettingsBtn_Click);
            // 
            // PPEMgrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 280);
            this.ControlBox = false;
            this.Controls.Add(this.aaSettingsBtn);
            this.Controls.Add(this.deleteEffectBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.effectsTabControl);
            this.Name = "PPEMgrForm";
            this.Text = "Post Proccess Effect Manager";
            this.effectsTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl effectsTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button deleteEffectBtn;
        private System.Windows.Forms.Button aaSettingsBtn;
    }
}