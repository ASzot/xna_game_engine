namespace BaseLogic.Editor.Forms
{
    partial class MaterialEditorForm
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
            this.acceptBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.diffuseMapTxtBox = new System.Windows.Forms.TextBox();
            this.normalMapTxtBox = new System.Windows.Forms.TextBox();
            this.specularMapTxtBox = new System.Windows.Forms.TextBox();
            this.translXTxtBox = new System.Windows.Forms.TextBox();
            this.translZTxtBox = new System.Windows.Forms.TextBox();
            this.translYTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rotXTxtBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rotZTxtBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rotYTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.scaleXTxtBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.scaleZTxtBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.scaleYTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.selectColorBtn = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.diffuseRTxtBox = new System.Windows.Forms.TextBox();
            this.diffuseATxtBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.diffuseBTxtBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.diffuseGTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.selectColorBtn2 = new System.Windows.Forms.Button();
            this.specularRTxtBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.specularBTxtBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.specularGTxtBox = new System.Windows.Forms.TextBox();
            this.useDiffuseMatCheckBox = new System.Windows.Forms.CheckBox();
            this.useSpecularMatCheckBox = new System.Windows.Forms.CheckBox();
            this.alphaClippingCheckBox = new System.Windows.Forms.CheckBox();
            this.alphaRefTxtBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.copyBtn = new System.Windows.Forms.Button();
            this.pasteBtn = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // acceptBtn
            // 
            this.acceptBtn.Location = new System.Drawing.Point(185, 463);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(75, 34);
            this.acceptBtn.TabIndex = 13;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(347, 463);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 34);
            this.cancelBtn.TabIndex = 14;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Diffuse Map:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Normal Map:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Specular Map:";
            // 
            // diffuseMapTxtBox
            // 
            this.diffuseMapTxtBox.Location = new System.Drawing.Point(112, 12);
            this.diffuseMapTxtBox.Name = "diffuseMapTxtBox";
            this.diffuseMapTxtBox.Size = new System.Drawing.Size(280, 20);
            this.diffuseMapTxtBox.TabIndex = 1;
            // 
            // normalMapTxtBox
            // 
            this.normalMapTxtBox.Location = new System.Drawing.Point(112, 38);
            this.normalMapTxtBox.Name = "normalMapTxtBox";
            this.normalMapTxtBox.Size = new System.Drawing.Size(280, 20);
            this.normalMapTxtBox.TabIndex = 2;
            // 
            // specularMapTxtBox
            // 
            this.specularMapTxtBox.Location = new System.Drawing.Point(112, 64);
            this.specularMapTxtBox.Name = "specularMapTxtBox";
            this.specularMapTxtBox.Size = new System.Drawing.Size(280, 20);
            this.specularMapTxtBox.TabIndex = 3;
            // 
            // translXTxtBox
            // 
            this.translXTxtBox.Location = new System.Drawing.Point(34, 19);
            this.translXTxtBox.Name = "translXTxtBox";
            this.translXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.translXTxtBox.TabIndex = 4;
            // 
            // translZTxtBox
            // 
            this.translZTxtBox.Location = new System.Drawing.Point(34, 71);
            this.translZTxtBox.Name = "translZTxtBox";
            this.translZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.translZTxtBox.TabIndex = 6;
            // 
            // translYTxtBox
            // 
            this.translYTxtBox.Location = new System.Drawing.Point(34, 45);
            this.translYTxtBox.Name = "translYTxtBox";
            this.translYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.translYTxtBox.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.translXTxtBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.translZTxtBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.translYTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(41, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 100);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Texture Translation";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Z:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "X:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.rotXTxtBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.rotZTxtBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.rotYTxtBox);
            this.groupBox2.Location = new System.Drawing.Point(41, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(148, 100);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Texture Rotation";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Z:";
            // 
            // rotXTxtBox
            // 
            this.rotXTxtBox.Location = new System.Drawing.Point(34, 19);
            this.rotXTxtBox.Name = "rotXTxtBox";
            this.rotXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotXTxtBox.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Y:";
            // 
            // rotZTxtBox
            // 
            this.rotZTxtBox.Location = new System.Drawing.Point(34, 71);
            this.rotZTxtBox.Name = "rotZTxtBox";
            this.rotZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotZTxtBox.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "X:";
            // 
            // rotYTxtBox
            // 
            this.rotYTxtBox.Location = new System.Drawing.Point(34, 45);
            this.rotYTxtBox.Name = "rotYTxtBox";
            this.rotYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotYTxtBox.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.scaleXTxtBox);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.scaleZTxtBox);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.scaleYTxtBox);
            this.groupBox3.Location = new System.Drawing.Point(195, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(148, 100);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Texture Scale";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Z:";
            // 
            // scaleXTxtBox
            // 
            this.scaleXTxtBox.Location = new System.Drawing.Point(34, 19);
            this.scaleXTxtBox.Name = "scaleXTxtBox";
            this.scaleXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.scaleXTxtBox.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Y:";
            // 
            // scaleZTxtBox
            // 
            this.scaleZTxtBox.Location = new System.Drawing.Point(34, 71);
            this.scaleZTxtBox.Name = "scaleZTxtBox";
            this.scaleZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.scaleZTxtBox.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "X:";
            // 
            // scaleYTxtBox
            // 
            this.scaleYTxtBox.Location = new System.Drawing.Point(34, 45);
            this.scaleYTxtBox.Name = "scaleYTxtBox";
            this.scaleYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.scaleYTxtBox.TabIndex = 11;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.selectColorBtn);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.diffuseRTxtBox);
            this.groupBox4.Controls.Add(this.diffuseATxtBox);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.diffuseBTxtBox);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.diffuseGTxtBox);
            this.groupBox4.Location = new System.Drawing.Point(41, 350);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(142, 143);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Diffuse Material";
            // 
            // selectColorBtn
            // 
            this.selectColorBtn.Location = new System.Drawing.Point(30, 120);
            this.selectColorBtn.Name = "selectColorBtn";
            this.selectColorBtn.Size = new System.Drawing.Size(75, 23);
            this.selectColorBtn.TabIndex = 21;
            this.selectColorBtn.Text = "Select Color";
            this.selectColorBtn.UseVisualStyleBackColor = true;
            this.selectColorBtn.Click += new System.EventHandler(this.selectColorBtn_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 100);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 13);
            this.label16.TabIndex = 28;
            this.label16.Text = "A:";
            // 
            // diffuseRTxtBox
            // 
            this.diffuseRTxtBox.Location = new System.Drawing.Point(30, 19);
            this.diffuseRTxtBox.Name = "diffuseRTxtBox";
            this.diffuseRTxtBox.Size = new System.Drawing.Size(100, 20);
            this.diffuseRTxtBox.TabIndex = 24;
            // 
            // diffuseATxtBox
            // 
            this.diffuseATxtBox.Location = new System.Drawing.Point(30, 97);
            this.diffuseATxtBox.Name = "diffuseATxtBox";
            this.diffuseATxtBox.Size = new System.Drawing.Size(100, 20);
            this.diffuseATxtBox.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "R:";
            // 
            // diffuseBTxtBox
            // 
            this.diffuseBTxtBox.Location = new System.Drawing.Point(30, 71);
            this.diffuseBTxtBox.Name = "diffuseBTxtBox";
            this.diffuseBTxtBox.Size = new System.Drawing.Size(100, 20);
            this.diffuseBTxtBox.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "G:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 13);
            this.label15.TabIndex = 23;
            this.label15.Text = "B:";
            // 
            // diffuseGTxtBox
            // 
            this.diffuseGTxtBox.Location = new System.Drawing.Point(30, 45);
            this.diffuseGTxtBox.Name = "diffuseGTxtBox";
            this.diffuseGTxtBox.Size = new System.Drawing.Size(100, 20);
            this.diffuseGTxtBox.TabIndex = 26;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.selectColorBtn2);
            this.groupBox5.Controls.Add(this.specularRTxtBox);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.specularBTxtBox);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.specularGTxtBox);
            this.groupBox5.Location = new System.Drawing.Point(201, 234);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(142, 132);
            this.groupBox5.TabIndex = 29;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Specular Material";
            // 
            // selectColorBtn2
            // 
            this.selectColorBtn2.Location = new System.Drawing.Point(30, 97);
            this.selectColorBtn2.Name = "selectColorBtn2";
            this.selectColorBtn2.Size = new System.Drawing.Size(75, 23);
            this.selectColorBtn2.TabIndex = 21;
            this.selectColorBtn2.Text = "Select Color";
            this.selectColorBtn2.UseVisualStyleBackColor = true;
            this.selectColorBtn2.Click += new System.EventHandler(this.selectColorBtn2_Click);
            // 
            // specularRTxtBox
            // 
            this.specularRTxtBox.Location = new System.Drawing.Point(30, 19);
            this.specularRTxtBox.Name = "specularRTxtBox";
            this.specularRTxtBox.Size = new System.Drawing.Size(100, 20);
            this.specularRTxtBox.TabIndex = 24;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(18, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "R:";
            // 
            // specularBTxtBox
            // 
            this.specularBTxtBox.Location = new System.Drawing.Point(30, 71);
            this.specularBTxtBox.Name = "specularBTxtBox";
            this.specularBTxtBox.Size = new System.Drawing.Size(100, 20);
            this.specularBTxtBox.TabIndex = 25;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 48);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(18, 13);
            this.label19.TabIndex = 22;
            this.label19.Text = "G:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(9, 73);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 13);
            this.label20.TabIndex = 23;
            this.label20.Text = "B:";
            // 
            // specularGTxtBox
            // 
            this.specularGTxtBox.Location = new System.Drawing.Point(30, 45);
            this.specularGTxtBox.Name = "specularGTxtBox";
            this.specularGTxtBox.Size = new System.Drawing.Size(100, 20);
            this.specularGTxtBox.TabIndex = 26;
            // 
            // useDiffuseMatCheckBox
            // 
            this.useDiffuseMatCheckBox.AutoSize = true;
            this.useDiffuseMatCheckBox.Location = new System.Drawing.Point(189, 398);
            this.useDiffuseMatCheckBox.Name = "useDiffuseMatCheckBox";
            this.useDiffuseMatCheckBox.Size = new System.Drawing.Size(121, 17);
            this.useDiffuseMatCheckBox.TabIndex = 30;
            this.useDiffuseMatCheckBox.Text = "Use Diffuse Material";
            this.useDiffuseMatCheckBox.UseVisualStyleBackColor = true;
            // 
            // useSpecularMatCheckBox
            // 
            this.useSpecularMatCheckBox.AutoSize = true;
            this.useSpecularMatCheckBox.Location = new System.Drawing.Point(189, 420);
            this.useSpecularMatCheckBox.Name = "useSpecularMatCheckBox";
            this.useSpecularMatCheckBox.Size = new System.Drawing.Size(130, 17);
            this.useSpecularMatCheckBox.TabIndex = 31;
            this.useSpecularMatCheckBox.Text = "Use Specular Material";
            this.useSpecularMatCheckBox.UseVisualStyleBackColor = true;
            // 
            // alphaClippingCheckBox
            // 
            this.alphaClippingCheckBox.AutoSize = true;
            this.alphaClippingCheckBox.Location = new System.Drawing.Point(189, 443);
            this.alphaClippingCheckBox.Name = "alphaClippingCheckBox";
            this.alphaClippingCheckBox.Size = new System.Drawing.Size(115, 17);
            this.alphaClippingCheckBox.TabIndex = 32;
            this.alphaClippingCheckBox.Text = "Use Alpha Clipping";
            this.alphaClippingCheckBox.UseVisualStyleBackColor = true;
            // 
            // alphaRefTxtBox
            // 
            this.alphaRefTxtBox.Location = new System.Drawing.Point(284, 372);
            this.alphaRefTxtBox.Name = "alphaRefTxtBox";
            this.alphaRefTxtBox.Size = new System.Drawing.Size(100, 20);
            this.alphaRefTxtBox.TabIndex = 33;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(188, 374);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 13);
            this.label17.TabIndex = 34;
            this.label17.Text = "Alpha Reference:";
            // 
            // copyBtn
            // 
            this.copyBtn.Location = new System.Drawing.Point(149, 87);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(75, 34);
            this.copyBtn.TabIndex = 35;
            this.copyBtn.Text = "Copy Material";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // pasteBtn
            // 
            this.pasteBtn.Location = new System.Drawing.Point(229, 87);
            this.pasteBtn.Name = "pasteBtn";
            this.pasteBtn.Size = new System.Drawing.Size(75, 34);
            this.pasteBtn.TabIndex = 36;
            this.pasteBtn.Text = "Paste Material";
            this.pasteBtn.UseVisualStyleBackColor = true;
            this.pasteBtn.Click += new System.EventHandler(this.pasteBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.applyBtn.Location = new System.Drawing.Point(266, 463);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 34);
            this.applyBtn.TabIndex = 37;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // MaterialEditorForm
            // 
            this.AcceptButton = this.acceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(428, 510);
            this.ControlBox = false;
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.pasteBtn);
            this.Controls.Add(this.copyBtn);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.alphaRefTxtBox);
            this.Controls.Add(this.alphaClippingCheckBox);
            this.Controls.Add(this.useSpecularMatCheckBox);
            this.Controls.Add(this.useDiffuseMatCheckBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.specularMapTxtBox);
            this.Controls.Add(this.normalMapTxtBox);
            this.Controls.Add(this.diffuseMapTxtBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.acceptBtn);
            this.Name = "MaterialEditorForm";
            this.Text = "Material Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox diffuseMapTxtBox;
        private System.Windows.Forms.TextBox normalMapTxtBox;
        private System.Windows.Forms.TextBox specularMapTxtBox;
        private System.Windows.Forms.TextBox translXTxtBox;
        private System.Windows.Forms.TextBox translZTxtBox;
        private System.Windows.Forms.TextBox translYTxtBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox rotXTxtBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rotZTxtBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox rotYTxtBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox scaleXTxtBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox scaleZTxtBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox scaleYTxtBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button selectColorBtn;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox diffuseRTxtBox;
        private System.Windows.Forms.TextBox diffuseATxtBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox diffuseBTxtBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox diffuseGTxtBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button selectColorBtn2;
        private System.Windows.Forms.TextBox specularRTxtBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox specularBTxtBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox specularGTxtBox;
        private System.Windows.Forms.CheckBox useDiffuseMatCheckBox;
        private System.Windows.Forms.CheckBox useSpecularMatCheckBox;
        private System.Windows.Forms.CheckBox alphaClippingCheckBox;
        private System.Windows.Forms.TextBox alphaRefTxtBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button copyBtn;
        private System.Windows.Forms.Button pasteBtn;
        private System.Windows.Forms.Button applyBtn;
    }
}