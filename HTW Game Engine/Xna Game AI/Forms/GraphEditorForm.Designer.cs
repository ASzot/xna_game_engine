namespace Xna_Game_AI.Forms
{
    partial class GraphEditorForm
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
            this.graphListBox = new System.Windows.Forms.ListBox();
            this.edgeListBox = new System.Windows.Forms.ListBox();
            this.nodeListBox = new System.Windows.Forms.ListBox();
            this.edgeEditorPanel = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toTxtBox = new System.Windows.Forms.TextBox();
            this.fromTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nodeEditorPanel = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.indexTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.yPosTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xPosTxtBox = new System.Windows.Forms.TextBox();
            this.zPosTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.addElementBtn = new System.Windows.Forms.Button();
            this.deleteElementBtn = new System.Windows.Forms.Button();
            this.addGraphBtn = new System.Windows.Forms.Button();
            this.deleteGraphBtn = new System.Windows.Forms.Button();
            this.generateGraphBtn = new System.Windows.Forms.Button();
            this.edgeEditorPanel.SuspendLayout();
            this.nodeEditorPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphListBox
            // 
            this.graphListBox.FormattingEnabled = true;
            this.graphListBox.Location = new System.Drawing.Point(11, 30);
            this.graphListBox.Name = "graphListBox";
            this.graphListBox.Size = new System.Drawing.Size(120, 173);
            this.graphListBox.TabIndex = 0;
            this.graphListBox.SelectedIndexChanged += new System.EventHandler(this.graphListBox_SelectedIndexChanged);
            // 
            // edgeListBox
            // 
            this.edgeListBox.FormattingEnabled = true;
            this.edgeListBox.Location = new System.Drawing.Point(137, 29);
            this.edgeListBox.Name = "edgeListBox";
            this.edgeListBox.Size = new System.Drawing.Size(120, 173);
            this.edgeListBox.TabIndex = 1;
            this.edgeListBox.SelectedIndexChanged += new System.EventHandler(this.edgeListBox_SelectedIndexChanged);
            this.edgeListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.edgeListBox_MouseUp);
            // 
            // nodeListBox
            // 
            this.nodeListBox.FormattingEnabled = true;
            this.nodeListBox.Location = new System.Drawing.Point(264, 30);
            this.nodeListBox.Name = "nodeListBox";
            this.nodeListBox.Size = new System.Drawing.Size(120, 173);
            this.nodeListBox.TabIndex = 2;
            this.nodeListBox.SelectedIndexChanged += new System.EventHandler(this.nodeListBox_SelectedIndexChanged);
            this.nodeListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.nodeListBox_MouseUp);
            // 
            // edgeEditorPanel
            // 
            this.edgeEditorPanel.Controls.Add(this.label7);
            this.edgeEditorPanel.Controls.Add(this.label6);
            this.edgeEditorPanel.Controls.Add(this.toTxtBox);
            this.edgeEditorPanel.Controls.Add(this.fromTxtBox);
            this.edgeEditorPanel.Controls.Add(this.label5);
            this.edgeEditorPanel.Location = new System.Drawing.Point(26, 208);
            this.edgeEditorPanel.Name = "edgeEditorPanel";
            this.edgeEditorPanel.Size = new System.Drawing.Size(162, 178);
            this.edgeEditorPanel.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Edge:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "From:";
            // 
            // toTxtBox
            // 
            this.toTxtBox.Location = new System.Drawing.Point(44, 27);
            this.toTxtBox.Name = "toTxtBox";
            this.toTxtBox.Size = new System.Drawing.Size(100, 20);
            this.toTxtBox.TabIndex = 5;
            this.toTxtBox.TextChanged += new System.EventHandler(this.txtBox_TxtChanged);
            // 
            // fromTxtBox
            // 
            this.fromTxtBox.Location = new System.Drawing.Point(44, 54);
            this.fromTxtBox.Name = "fromTxtBox";
            this.fromTxtBox.Size = new System.Drawing.Size(100, 20);
            this.fromTxtBox.TabIndex = 6;
            this.fromTxtBox.TextChanged += new System.EventHandler(this.txtBox_TxtChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "To:";
            // 
            // nodeEditorPanel
            // 
            this.nodeEditorPanel.Controls.Add(this.label8);
            this.nodeEditorPanel.Controls.Add(this.indexTxtBox);
            this.nodeEditorPanel.Controls.Add(this.label4);
            this.nodeEditorPanel.Controls.Add(this.groupBox1);
            this.nodeEditorPanel.Location = new System.Drawing.Point(194, 208);
            this.nodeEditorPanel.Name = "nodeEditorPanel";
            this.nodeEditorPanel.Size = new System.Drawing.Size(180, 178);
            this.nodeEditorPanel.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Node:";
            // 
            // indexTxtBox
            // 
            this.indexTxtBox.Location = new System.Drawing.Point(51, 147);
            this.indexTxtBox.Name = "indexTxtBox";
            this.indexTxtBox.ReadOnly = true;
            this.indexTxtBox.Size = new System.Drawing.Size(100, 20);
            this.indexTxtBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Index:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.yPosTxtBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.xPosTxtBox);
            this.groupBox1.Controls.Add(this.zPosTxtBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z:";
            // 
            // yPosTxtBox
            // 
            this.yPosTxtBox.Location = new System.Drawing.Point(29, 43);
            this.yPosTxtBox.Name = "yPosTxtBox";
            this.yPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.yPosTxtBox.TabIndex = 7;
            this.yPosTxtBox.TextChanged += new System.EventHandler(this.txtBox_TxtChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y:";
            // 
            // xPosTxtBox
            // 
            this.xPosTxtBox.Location = new System.Drawing.Point(29, 17);
            this.xPosTxtBox.Name = "xPosTxtBox";
            this.xPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.xPosTxtBox.TabIndex = 5;
            this.xPosTxtBox.TextChanged += new System.EventHandler(this.txtBox_TxtChanged);
            // 
            // zPosTxtBox
            // 
            this.zPosTxtBox.Location = new System.Drawing.Point(29, 69);
            this.zPosTxtBox.Name = "zPosTxtBox";
            this.zPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.zPosTxtBox.TabIndex = 8;
            this.zPosTxtBox.TextChanged += new System.EventHandler(this.txtBox_TxtChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "X:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Graphs:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(134, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Edges:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(261, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Nodes:";
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(305, 464);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(85, 43);
            this.closeBtn.TabIndex = 8;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // addElementBtn
            // 
            this.addElementBtn.Location = new System.Drawing.Point(117, 392);
            this.addElementBtn.Name = "addElementBtn";
            this.addElementBtn.Size = new System.Drawing.Size(85, 43);
            this.addElementBtn.TabIndex = 9;
            this.addElementBtn.Text = "Add Egode";
            this.addElementBtn.UseVisualStyleBackColor = true;
            this.addElementBtn.Click += new System.EventHandler(this.addElementBtn_Click);
            // 
            // deleteElementBtn
            // 
            this.deleteElementBtn.Location = new System.Drawing.Point(117, 441);
            this.deleteElementBtn.Name = "deleteElementBtn";
            this.deleteElementBtn.Size = new System.Drawing.Size(85, 43);
            this.deleteElementBtn.TabIndex = 10;
            this.deleteElementBtn.Text = "Delete Egode";
            this.deleteElementBtn.UseVisualStyleBackColor = true;
            this.deleteElementBtn.Click += new System.EventHandler(this.deleteElementBtn_Click);
            // 
            // addGraphBtn
            // 
            this.addGraphBtn.Location = new System.Drawing.Point(26, 392);
            this.addGraphBtn.Name = "addGraphBtn";
            this.addGraphBtn.Size = new System.Drawing.Size(85, 43);
            this.addGraphBtn.TabIndex = 11;
            this.addGraphBtn.Text = "Add Graph";
            this.addGraphBtn.UseVisualStyleBackColor = true;
            this.addGraphBtn.Click += new System.EventHandler(this.addGraphBtn_Click);
            // 
            // deleteGraphBtn
            // 
            this.deleteGraphBtn.Location = new System.Drawing.Point(26, 490);
            this.deleteGraphBtn.Name = "deleteGraphBtn";
            this.deleteGraphBtn.Size = new System.Drawing.Size(85, 43);
            this.deleteGraphBtn.TabIndex = 12;
            this.deleteGraphBtn.Text = "Delete Graph";
            this.deleteGraphBtn.UseVisualStyleBackColor = true;
            this.deleteGraphBtn.Click += new System.EventHandler(this.deleteGraphBtn_Click);
            // 
            // generateGraphBtn
            // 
            this.generateGraphBtn.Location = new System.Drawing.Point(26, 441);
            this.generateGraphBtn.Name = "generateGraphBtn";
            this.generateGraphBtn.Size = new System.Drawing.Size(83, 43);
            this.generateGraphBtn.TabIndex = 13;
            this.generateGraphBtn.Text = "Generate Graph";
            this.generateGraphBtn.UseVisualStyleBackColor = true;
            this.generateGraphBtn.Click += new System.EventHandler(this.generateGraphBtn_Click);
            // 
            // GraphEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 541);
            this.Controls.Add(this.generateGraphBtn);
            this.Controls.Add(this.deleteGraphBtn);
            this.Controls.Add(this.addGraphBtn);
            this.Controls.Add(this.deleteElementBtn);
            this.Controls.Add(this.addElementBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nodeEditorPanel);
            this.Controls.Add(this.edgeEditorPanel);
            this.Controls.Add(this.nodeListBox);
            this.Controls.Add(this.edgeListBox);
            this.Controls.Add(this.graphListBox);
            this.Name = "GraphEditorForm";
            this.Text = "GraphEditorForm";
            this.edgeEditorPanel.ResumeLayout(false);
            this.edgeEditorPanel.PerformLayout();
            this.nodeEditorPanel.ResumeLayout(false);
            this.nodeEditorPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox graphListBox;
        private System.Windows.Forms.ListBox edgeListBox;
        private System.Windows.Forms.ListBox nodeListBox;
        private System.Windows.Forms.Panel edgeEditorPanel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox toTxtBox;
        private System.Windows.Forms.TextBox fromTxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel nodeEditorPanel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox indexTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox yPosTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xPosTxtBox;
        private System.Windows.Forms.TextBox zPosTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button addElementBtn;
        private System.Windows.Forms.Button deleteElementBtn;
        private System.Windows.Forms.Button addGraphBtn;
        private System.Windows.Forms.Button deleteGraphBtn;
        private System.Windows.Forms.Button generateGraphBtn;
    }
}