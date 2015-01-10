namespace Xna_Game_AI.Forms
{
	partial class AddEdgeForm
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
			this.fromNodeListBox = new System.Windows.Forms.ListBox();
			this.toNodeListBox = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.addEdgeBtn = new System.Windows.Forms.Button();
			this.closeBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// fromNodeListBox
			// 
			this.fromNodeListBox.FormattingEnabled = true;
			this.fromNodeListBox.Location = new System.Drawing.Point(11, 34);
			this.fromNodeListBox.Name = "fromNodeListBox";
			this.fromNodeListBox.Size = new System.Drawing.Size(120, 95);
			this.fromNodeListBox.TabIndex = 0;
			// 
			// toNodeListBox
			// 
			this.toNodeListBox.FormattingEnabled = true;
			this.toNodeListBox.Location = new System.Drawing.Point(138, 34);
			this.toNodeListBox.Name = "toNodeListBox";
			this.toNodeListBox.Size = new System.Drawing.Size(120, 95);
			this.toNodeListBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "From Node:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(135, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "To Node:";
			// 
			// addEdgeBtn
			// 
			this.addEdgeBtn.Location = new System.Drawing.Point(56, 154);
			this.addEdgeBtn.Name = "addEdgeBtn";
			this.addEdgeBtn.Size = new System.Drawing.Size(75, 36);
			this.addEdgeBtn.TabIndex = 4;
			this.addEdgeBtn.Text = "Add Edge";
			this.addEdgeBtn.UseVisualStyleBackColor = true;
			this.addEdgeBtn.Click += new System.EventHandler(this.addEdgeBtn_Click);
			// 
			// closeBtn
			// 
			this.closeBtn.Location = new System.Drawing.Point(138, 154);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(75, 36);
			this.closeBtn.TabIndex = 5;
			this.closeBtn.Text = "Close";
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// AddEdgeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(290, 214);
			this.ControlBox = false;
			this.Controls.Add(this.closeBtn);
			this.Controls.Add(this.addEdgeBtn);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toNodeListBox);
			this.Controls.Add(this.fromNodeListBox);
			this.Name = "AddEdgeForm";
			this.Text = "Add Edge";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox fromNodeListBox;
		private System.Windows.Forms.ListBox toNodeListBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button addEdgeBtn;
		private System.Windows.Forms.Button closeBtn;
	}
}