namespace RenderingSystem.Graphics.Forms
{
    partial class EffectSelectionForm
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
            this.bloomBtn = new System.Windows.Forms.Button();
            this.lightShaftsBtn = new System.Windows.Forms.Button();
            this.closeeBtn = new System.Windows.Forms.Button();
            this.fxaaBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bloomBtn
            // 
            this.bloomBtn.Location = new System.Drawing.Point(12, 12);
            this.bloomBtn.Name = "bloomBtn";
            this.bloomBtn.Size = new System.Drawing.Size(143, 43);
            this.bloomBtn.TabIndex = 0;
            this.bloomBtn.Text = "Bloom";
            this.bloomBtn.UseVisualStyleBackColor = true;
            this.bloomBtn.Click += new System.EventHandler(this.bloomBtn_Click);
            // 
            // lightShaftsBtn
            // 
            this.lightShaftsBtn.Location = new System.Drawing.Point(12, 61);
            this.lightShaftsBtn.Name = "lightShaftsBtn";
            this.lightShaftsBtn.Size = new System.Drawing.Size(143, 43);
            this.lightShaftsBtn.TabIndex = 1;
            this.lightShaftsBtn.Text = "Light Shafts";
            this.lightShaftsBtn.UseVisualStyleBackColor = true;
            this.lightShaftsBtn.Click += new System.EventHandler(this.lightShaftsBtn_Click);
            // 
            // closeeBtn
            // 
            this.closeeBtn.Location = new System.Drawing.Point(80, 186);
            this.closeeBtn.Name = "closeeBtn";
            this.closeeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeeBtn.TabIndex = 2;
            this.closeeBtn.Text = "Close";
            this.closeeBtn.UseVisualStyleBackColor = true;
            this.closeeBtn.Click += new System.EventHandler(this.closeeBtn_Click);
            // 
            // fxaaBtn
            // 
            this.fxaaBtn.Location = new System.Drawing.Point(12, 110);
            this.fxaaBtn.Name = "fxaaBtn";
            this.fxaaBtn.Size = new System.Drawing.Size(143, 43);
            this.fxaaBtn.TabIndex = 3;
            this.fxaaBtn.Text = "FXAA";
            this.fxaaBtn.UseVisualStyleBackColor = true;
            this.fxaaBtn.Click += new System.EventHandler(this.fxaaBtn_Click);
            // 
            // EffectSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(167, 221);
            this.ControlBox = false;
            this.Controls.Add(this.fxaaBtn);
            this.Controls.Add(this.closeeBtn);
            this.Controls.Add(this.lightShaftsBtn);
            this.Controls.Add(this.bloomBtn);
            this.Name = "EffectSelectionForm";
            this.Text = "Effect Selection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bloomBtn;
        private System.Windows.Forms.Button lightShaftsBtn;
        private System.Windows.Forms.Button closeeBtn;
        private System.Windows.Forms.Button fxaaBtn;
    }
}