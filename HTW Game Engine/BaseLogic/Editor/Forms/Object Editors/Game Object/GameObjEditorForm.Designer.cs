namespace BaseLogic.Editor.Forms
{
    partial class GameObjEditorForm
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
            this.objsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.positionGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.zPosTxtBox = new System.Windows.Forms.TextBox();
            this.yPosTxtBox = new System.Windows.Forms.TextBox();
            this.xPosTxtBox = new System.Windows.Forms.TextBox();
            this.objModifierPanel = new System.Windows.Forms.Panel();
            this.resetOriginBtn = new System.Windows.Forms.Button();
            this.originLbl = new System.Windows.Forms.Label();
            this.setOriginBtn = new System.Windows.Forms.Button();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.editTypeBtn = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tagComboBox = new System.Windows.Forms.ComboBox();
            this.editPhysBtn = new System.Windows.Forms.Button();
            this.duplicateBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.applyBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.rotXTxtBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rotYTxtBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rotZTxtBox = new System.Windows.Forms.TextBox();
            this.matEditPanel = new System.Windows.Forms.Panel();
            this.editMatBtn = new System.Windows.Forms.Button();
            this.selectedMatComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.idTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.scaleTxtBox = new System.Windows.Forms.TextBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.addNewObjTxtBox = new System.Windows.Forms.Button();
            this.deleteSelectedObjTxtBox = new System.Windows.Forms.Button();
            this.addAnimObjBtn = new System.Windows.Forms.Button();
            this.addOtherObjBtn = new System.Windows.Forms.Button();
            this.duplicateGroupBtn = new System.Windows.Forms.Button();
            this.editGroupBtn = new System.Windows.Forms.Button();
            this.searchBtn = new System.Windows.Forms.Button();
            this.positionGroupBox.SuspendLayout();
            this.objModifierPanel.SuspendLayout();
            this.gamePanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.matEditPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // objsListBox
            // 
            this.objsListBox.FormattingEnabled = true;
            this.objsListBox.Location = new System.Drawing.Point(12, 34);
            this.objsListBox.Name = "objsListBox";
            this.objsListBox.Size = new System.Drawing.Size(183, 394);
            this.objsListBox.TabIndex = 0;
            this.objsListBox.SelectedIndexChanged += new System.EventHandler(this.objsListBox_SelectedIndexChanged);
            this.objsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.objsListBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selected Object:";
            // 
            // positionGroupBox
            // 
            this.positionGroupBox.Controls.Add(this.label4);
            this.positionGroupBox.Controls.Add(this.label3);
            this.positionGroupBox.Controls.Add(this.label2);
            this.positionGroupBox.Controls.Add(this.zPosTxtBox);
            this.positionGroupBox.Controls.Add(this.yPosTxtBox);
            this.positionGroupBox.Controls.Add(this.xPosTxtBox);
            this.positionGroupBox.Location = new System.Drawing.Point(20, 3);
            this.positionGroupBox.Name = "positionGroupBox";
            this.positionGroupBox.Size = new System.Drawing.Size(200, 100);
            this.positionGroupBox.TabIndex = 2;
            this.positionGroupBox.TabStop = false;
            this.positionGroupBox.Text = "Position";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Z:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X:";
            // 
            // zPosTxtBox
            // 
            this.zPosTxtBox.Location = new System.Drawing.Point(35, 73);
            this.zPosTxtBox.Name = "zPosTxtBox";
            this.zPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.zPosTxtBox.TabIndex = 2;
            this.zPosTxtBox.TextChanged += new System.EventHandler(this.zPosTxtBox_TextChanged);
            // 
            // yPosTxtBox
            // 
            this.yPosTxtBox.Location = new System.Drawing.Point(35, 46);
            this.yPosTxtBox.Name = "yPosTxtBox";
            this.yPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.yPosTxtBox.TabIndex = 1;
            this.yPosTxtBox.TextChanged += new System.EventHandler(this.yPosTxtBox_TextChanged);
            // 
            // xPosTxtBox
            // 
            this.xPosTxtBox.Location = new System.Drawing.Point(35, 19);
            this.xPosTxtBox.Name = "xPosTxtBox";
            this.xPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.xPosTxtBox.TabIndex = 0;
            this.xPosTxtBox.TextChanged += new System.EventHandler(this.xPosTxtBox_TextChanged);
            // 
            // objModifierPanel
            // 
            this.objModifierPanel.Controls.Add(this.resetOriginBtn);
            this.objModifierPanel.Controls.Add(this.originLbl);
            this.objModifierPanel.Controls.Add(this.setOriginBtn);
            this.objModifierPanel.Controls.Add(this.gamePanel);
            this.objModifierPanel.Controls.Add(this.editPhysBtn);
            this.objModifierPanel.Controls.Add(this.duplicateBtn);
            this.objModifierPanel.Controls.Add(this.groupBox1);
            this.objModifierPanel.Controls.Add(this.matEditPanel);
            this.objModifierPanel.Controls.Add(this.label6);
            this.objModifierPanel.Controls.Add(this.idTxtBox);
            this.objModifierPanel.Controls.Add(this.label5);
            this.objModifierPanel.Controls.Add(this.scaleTxtBox);
            this.objModifierPanel.Controls.Add(this.positionGroupBox);
            this.objModifierPanel.Location = new System.Drawing.Point(191, 34);
            this.objModifierPanel.Name = "objModifierPanel";
            this.objModifierPanel.Size = new System.Drawing.Size(278, 501);
            this.objModifierPanel.TabIndex = 3;
            // 
            // resetOriginBtn
            // 
            this.resetOriginBtn.Location = new System.Drawing.Point(110, 442);
            this.resetOriginBtn.Name = "resetOriginBtn";
            this.resetOriginBtn.Size = new System.Drawing.Size(96, 23);
            this.resetOriginBtn.TabIndex = 14;
            this.resetOriginBtn.Text = "Reset Origin";
            this.resetOriginBtn.UseVisualStyleBackColor = true;
            this.resetOriginBtn.Click += new System.EventHandler(this.resetOriginBtn_Click);
            // 
            // originLbl
            // 
            this.originLbl.AutoSize = true;
            this.originLbl.Location = new System.Drawing.Point(5, 478);
            this.originLbl.Name = "originLbl";
            this.originLbl.Size = new System.Drawing.Size(119, 13);
            this.originLbl.TabIndex = 13;
            this.originLbl.Text = "This Object is the Origin";
            // 
            // setOriginBtn
            // 
            this.setOriginBtn.Location = new System.Drawing.Point(8, 442);
            this.setOriginBtn.Name = "setOriginBtn";
            this.setOriginBtn.Size = new System.Drawing.Size(96, 23);
            this.setOriginBtn.TabIndex = 12;
            this.setOriginBtn.Text = "Set As Origin";
            this.setOriginBtn.UseVisualStyleBackColor = true;
            this.setOriginBtn.Click += new System.EventHandler(this.setOriginBtn_Click);
            // 
            // gamePanel
            // 
            this.gamePanel.Controls.Add(this.editTypeBtn);
            this.gamePanel.Controls.Add(this.label11);
            this.gamePanel.Controls.Add(this.tagComboBox);
            this.gamePanel.Location = new System.Drawing.Point(8, 305);
            this.gamePanel.Margin = new System.Windows.Forms.Padding(2);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(268, 51);
            this.gamePanel.TabIndex = 12;
            // 
            // editTypeBtn
            // 
            this.editTypeBtn.Location = new System.Drawing.Point(169, 8);
            this.editTypeBtn.Name = "editTypeBtn";
            this.editTypeBtn.Size = new System.Drawing.Size(87, 28);
            this.editTypeBtn.TabIndex = 13;
            this.editTypeBtn.Text = "Edit Type";
            this.editTypeBtn.UseVisualStyleBackColor = true;
            this.editTypeBtn.Click += new System.EventHandler(this.editTypeBtn_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 8);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Type:";
            // 
            // tagComboBox
            // 
            this.tagComboBox.FormattingEnabled = true;
            this.tagComboBox.Location = new System.Drawing.Point(37, 6);
            this.tagComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.tagComboBox.Name = "tagComboBox";
            this.tagComboBox.Size = new System.Drawing.Size(110, 21);
            this.tagComboBox.TabIndex = 0;
            this.tagComboBox.SelectedIndexChanged += new System.EventHandler(this.tagComboBox_SelectedIndexChanged);
            // 
            // editPhysBtn
            // 
            this.editPhysBtn.Location = new System.Drawing.Point(54, 267);
            this.editPhysBtn.Name = "editPhysBtn";
            this.editPhysBtn.Size = new System.Drawing.Size(87, 28);
            this.editPhysBtn.TabIndex = 9;
            this.editPhysBtn.Text = "Edit Physics";
            this.editPhysBtn.UseVisualStyleBackColor = true;
            this.editPhysBtn.Click += new System.EventHandler(this.editPhysBtn_Click);
            // 
            // duplicateBtn
            // 
            this.duplicateBtn.Location = new System.Drawing.Point(147, 267);
            this.duplicateBtn.Name = "duplicateBtn";
            this.duplicateBtn.Size = new System.Drawing.Size(87, 28);
            this.duplicateBtn.TabIndex = 8;
            this.duplicateBtn.Text = "Duplicate";
            this.duplicateBtn.UseVisualStyleBackColor = true;
            this.duplicateBtn.Click += new System.EventHandler(this.duplicateBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.applyBtn);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.rotXTxtBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rotYTxtBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.rotZTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(20, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotation";
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(142, 33);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 45);
            this.applyBtn.TabIndex = 12;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Z:";
            // 
            // rotXTxtBox
            // 
            this.rotXTxtBox.Location = new System.Drawing.Point(30, 19);
            this.rotXTxtBox.Name = "rotXTxtBox";
            this.rotXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotXTxtBox.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Y:";
            // 
            // rotYTxtBox
            // 
            this.rotYTxtBox.Location = new System.Drawing.Point(30, 46);
            this.rotYTxtBox.Name = "rotYTxtBox";
            this.rotYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotYTxtBox.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "X:";
            // 
            // rotZTxtBox
            // 
            this.rotZTxtBox.Location = new System.Drawing.Point(30, 73);
            this.rotZTxtBox.Name = "rotZTxtBox";
            this.rotZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotZTxtBox.TabIndex = 8;
            // 
            // matEditPanel
            // 
            this.matEditPanel.Controls.Add(this.editMatBtn);
            this.matEditPanel.Controls.Add(this.selectedMatComboBox);
            this.matEditPanel.Controls.Add(this.label7);
            this.matEditPanel.Location = new System.Drawing.Point(8, 358);
            this.matEditPanel.Name = "matEditPanel";
            this.matEditPanel.Size = new System.Drawing.Size(231, 78);
            this.matEditPanel.TabIndex = 10;
            // 
            // editMatBtn
            // 
            this.editMatBtn.Location = new System.Drawing.Point(141, 3);
            this.editMatBtn.Name = "editMatBtn";
            this.editMatBtn.Size = new System.Drawing.Size(87, 72);
            this.editMatBtn.TabIndex = 9;
            this.editMatBtn.Text = "Edit Selected Material";
            this.editMatBtn.UseVisualStyleBackColor = true;
            this.editMatBtn.Click += new System.EventHandler(this.editMatBtn_Click);
            // 
            // selectedMatComboBox
            // 
            this.selectedMatComboBox.FormattingEnabled = true;
            this.selectedMatComboBox.Location = new System.Drawing.Point(4, 22);
            this.selectedMatComboBox.Name = "selectedMatComboBox";
            this.selectedMatComboBox.Size = new System.Drawing.Size(121, 21);
            this.selectedMatComboBox.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Selected Material:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "ID:";
            // 
            // idTxtBox
            // 
            this.idTxtBox.Location = new System.Drawing.Point(50, 241);
            this.idTxtBox.Name = "idTxtBox";
            this.idTxtBox.Size = new System.Drawing.Size(190, 20);
            this.idTxtBox.TabIndex = 5;
            this.idTxtBox.TextChanged += new System.EventHandler(this.idTxtBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Scale:";
            // 
            // scaleTxtBox
            // 
            this.scaleTxtBox.Location = new System.Drawing.Point(50, 215);
            this.scaleTxtBox.Name = "scaleTxtBox";
            this.scaleTxtBox.Size = new System.Drawing.Size(100, 20);
            this.scaleTxtBox.TabIndex = 3;
            this.scaleTxtBox.TextChanged += new System.EventHandler(this.scaleTxtBox_TextChanged);
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(387, 595);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(88, 34);
            this.closeBtn.TabIndex = 4;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // addNewObjTxtBox
            // 
            this.addNewObjTxtBox.Location = new System.Drawing.Point(9, 541);
            this.addNewObjTxtBox.Name = "addNewObjTxtBox";
            this.addNewObjTxtBox.Size = new System.Drawing.Size(113, 39);
            this.addNewObjTxtBox.TabIndex = 5;
            this.addNewObjTxtBox.Text = "Add Static Obj";
            this.addNewObjTxtBox.UseVisualStyleBackColor = true;
            this.addNewObjTxtBox.Click += new System.EventHandler(this.addNewObjTxtBox_Click);
            // 
            // deleteSelectedObjTxtBox
            // 
            this.deleteSelectedObjTxtBox.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.deleteSelectedObjTxtBox.Location = new System.Drawing.Point(130, 586);
            this.deleteSelectedObjTxtBox.Name = "deleteSelectedObjTxtBox";
            this.deleteSelectedObjTxtBox.Size = new System.Drawing.Size(113, 39);
            this.deleteSelectedObjTxtBox.TabIndex = 6;
            this.deleteSelectedObjTxtBox.Text = "Delete Selected Obj";
            this.deleteSelectedObjTxtBox.UseVisualStyleBackColor = true;
            this.deleteSelectedObjTxtBox.Click += new System.EventHandler(this.deleteSelectedObjTxtBox_Click);
            // 
            // addAnimObjBtn
            // 
            this.addAnimObjBtn.Location = new System.Drawing.Point(127, 541);
            this.addAnimObjBtn.Name = "addAnimObjBtn";
            this.addAnimObjBtn.Size = new System.Drawing.Size(113, 39);
            this.addAnimObjBtn.TabIndex = 7;
            this.addAnimObjBtn.Text = "Add Animated Obj";
            this.addAnimObjBtn.UseVisualStyleBackColor = true;
            this.addAnimObjBtn.Click += new System.EventHandler(this.addAnimObjBtn_Click);
            // 
            // addOtherObjBtn
            // 
            this.addOtherObjBtn.Location = new System.Drawing.Point(11, 586);
            this.addOtherObjBtn.Name = "addOtherObjBtn";
            this.addOtherObjBtn.Size = new System.Drawing.Size(113, 39);
            this.addOtherObjBtn.TabIndex = 8;
            this.addOtherObjBtn.Text = "Add Other Obj";
            this.addOtherObjBtn.UseVisualStyleBackColor = true;
            this.addOtherObjBtn.Click += new System.EventHandler(this.addOtherObjBtn_Click);
            // 
            // duplicateGroupBtn
            // 
            this.duplicateGroupBtn.Location = new System.Drawing.Point(246, 541);
            this.duplicateGroupBtn.Name = "duplicateGroupBtn";
            this.duplicateGroupBtn.Size = new System.Drawing.Size(113, 39);
            this.duplicateGroupBtn.TabIndex = 9;
            this.duplicateGroupBtn.Text = "Duplicate Group";
            this.duplicateGroupBtn.UseVisualStyleBackColor = true;
            this.duplicateGroupBtn.Click += new System.EventHandler(this.duplicateGroupBtn_Click);
            // 
            // editGroupBtn
            // 
            this.editGroupBtn.Location = new System.Drawing.Point(246, 586);
            this.editGroupBtn.Name = "editGroupBtn";
            this.editGroupBtn.Size = new System.Drawing.Size(113, 39);
            this.editGroupBtn.TabIndex = 10;
            this.editGroupBtn.Text = "Edit Group";
            this.editGroupBtn.UseVisualStyleBackColor = true;
            this.editGroupBtn.Click += new System.EventHandler(this.editGroupBtn_Click);
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(12, 434);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 11;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // GameObjEditorForm
            // 
            this.AcceptButton = this.addNewObjTxtBox;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(489, 648);
            this.ControlBox = false;
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.editGroupBtn);
            this.Controls.Add(this.duplicateGroupBtn);
            this.Controls.Add(this.addOtherObjBtn);
            this.Controls.Add(this.addAnimObjBtn);
            this.Controls.Add(this.deleteSelectedObjTxtBox);
            this.Controls.Add(this.addNewObjTxtBox);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.objModifierPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.objsListBox);
            this.Name = "GameObjEditorForm";
            this.Text = "Game Object Editor";
            this.positionGroupBox.ResumeLayout(false);
            this.positionGroupBox.PerformLayout();
            this.objModifierPanel.ResumeLayout(false);
            this.objModifierPanel.PerformLayout();
            this.gamePanel.ResumeLayout(false);
            this.gamePanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.matEditPanel.ResumeLayout(false);
            this.matEditPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox objsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox positionGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox zPosTxtBox;
        private System.Windows.Forms.TextBox yPosTxtBox;
        private System.Windows.Forms.TextBox xPosTxtBox;
        private System.Windows.Forms.Panel objModifierPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox scaleTxtBox;
        private System.Windows.Forms.Button closeBtn;
		private System.Windows.Forms.Button addNewObjTxtBox;
		private System.Windows.Forms.Button deleteSelectedObjTxtBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox idTxtBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button editMatBtn;
        private System.Windows.Forms.Panel matEditPanel;
        protected System.Windows.Forms.ComboBox selectedMatComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rotXTxtBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox rotYTxtBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox rotZTxtBox;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Button addAnimObjBtn;
        private System.Windows.Forms.Button duplicateBtn;
        private System.Windows.Forms.Button addOtherObjBtn;
        private System.Windows.Forms.Button editPhysBtn;
        private System.Windows.Forms.Button duplicateGroupBtn;
        private System.Windows.Forms.Button editGroupBtn;
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox tagComboBox;
        private System.Windows.Forms.Button editTypeBtn;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Button setOriginBtn;
        private System.Windows.Forms.Label originLbl;
        private System.Windows.Forms.Button resetOriginBtn;
    }
}