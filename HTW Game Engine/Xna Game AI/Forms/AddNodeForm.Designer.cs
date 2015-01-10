namespace Xna_Game_AI.Forms
{
	partial class AddNodeForm
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
            this.addNodeBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.zPosTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xPosTxtBox = new System.Windows.Forms.TextBox();
            this.yPosTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(93, 134);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 39);
            this.closeBtn.TabIndex = 4;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // addNodeBtn
            // 
            this.addNodeBtn.Location = new System.Drawing.Point(12, 134);
            this.addNodeBtn.Name = "addNodeBtn";
            this.addNodeBtn.Size = new System.Drawing.Size(75, 39);
            this.addNodeBtn.TabIndex = 3;
            this.addNodeBtn.Text = "Add Node";
            this.addNodeBtn.UseVisualStyleBackColor = true;
            this.addNodeBtn.Click += new System.EventHandler(this.addNodeBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.zPosTxtBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.xPosTxtBox);
            this.groupBox1.Controls.Add(this.yPosTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Y:";
            // 
            // zPosTxtBox
            // 
            this.zPosTxtBox.Location = new System.Drawing.Point(30, 71);
            this.zPosTxtBox.Name = "zPosTxtBox";
            this.zPosTxtBox.Size = new System.Drawing.Size(120, 20);
            this.zPosTxtBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "X:";
            // 
            // xPosTxtBox
            // 
            this.xPosTxtBox.Location = new System.Drawing.Point(30, 19);
            this.xPosTxtBox.Name = "xPosTxtBox";
            this.xPosTxtBox.Size = new System.Drawing.Size(120, 20);
            this.xPosTxtBox.TabIndex = 0;
            // 
            // yPosTxtBox
            // 
            this.yPosTxtBox.Location = new System.Drawing.Point(30, 45);
            this.yPosTxtBox.Name = "yPosTxtBox";
            this.yPosTxtBox.Size = new System.Drawing.Size(120, 20);
            this.yPosTxtBox.TabIndex = 1;
            // 
            // AddNodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 199);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.addNodeBtn);
            this.Controls.Add(this.closeBtn);
            this.Name = "AddNodeForm";
            this.Text = "Add Node";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button closeBtn;
		private System.Windows.Forms.Button addNodeBtn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox zPosTxtBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox xPosTxtBox;
		private System.Windows.Forms.TextBox yPosTxtBox;
	}
}