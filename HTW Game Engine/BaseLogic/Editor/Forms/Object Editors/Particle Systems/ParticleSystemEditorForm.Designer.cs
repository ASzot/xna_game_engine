namespace BaseLogic.Editor.Forms
{
    partial class ParticleSystemEditorForm
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
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.posXTxtBox = new System.Windows.Forms.TextBox();
            this.posZTxtBox = new System.Windows.Forms.TextBox();
            this.posYTxtBox = new System.Windows.Forms.TextBox();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.gravityXTxtBox = new System.Windows.Forms.TextBox();
            this.gravityZTxtBox = new System.Windows.Forms.TextBox();
            this.gravityYTxtBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.maxEndSizeTxtBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.minEndSizeTxtBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.maxStartSizeTxtBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maxColorComboBox = new System.Windows.Forms.ComboBox();
            this.blendStateComboBox = new System.Windows.Forms.ComboBox();
            this.minColorComboBox = new System.Windows.Forms.ComboBox();
            this.minStartSizeTxtBox = new System.Windows.Forms.TextBox();
            this.maxRotSpeedTxtBox = new System.Windows.Forms.TextBox();
            this.minRotSpeedTxtBox = new System.Windows.Forms.TextBox();
            this.endVelTxtBox = new System.Windows.Forms.TextBox();
            this.maxVertVelTxtBox = new System.Windows.Forms.TextBox();
            this.minVertVelTxtBox = new System.Windows.Forms.TextBox();
            this.maxHorizTxtBox = new System.Windows.Forms.TextBox();
            this.minHorizVelTxtBox = new System.Windows.Forms.TextBox();
            this.emitVelSensTxtBox = new System.Windows.Forms.TextBox();
            this.durationRandTxtBox = new System.Windows.Forms.TextBox();
            this.durationTxtBox = new System.Windows.Forms.TextBox();
            this.maxParticlesTxtBox = new System.Windows.Forms.TextBox();
            this.textureNameTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.applyBtn = new System.Windows.Forms.Button();
            this.idTxtBox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.emitRateTxtBox = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(208, 609);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(116, 55);
            this.cancelBtn.TabIndex = 89;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.posXTxtBox);
            this.groupBox2.Controls.Add(this.posZTxtBox);
            this.groupBox2.Controls.Add(this.posYTxtBox);
            this.groupBox2.Location = new System.Drawing.Point(359, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 107);
            this.groupBox2.TabIndex = 82;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Position";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(16, 74);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 13);
            this.label24.TabIndex = 14;
            this.label24.Text = "Z:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(16, 48);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(17, 13);
            this.label25.TabIndex = 13;
            this.label25.Text = "Y:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(16, 22);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(17, 13);
            this.label26.TabIndex = 12;
            this.label26.Text = "X:";
            // 
            // posXTxtBox
            // 
            this.posXTxtBox.Location = new System.Drawing.Point(39, 19);
            this.posXTxtBox.Name = "posXTxtBox";
            this.posXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posXTxtBox.TabIndex = 9;
            // 
            // posZTxtBox
            // 
            this.posZTxtBox.Location = new System.Drawing.Point(39, 71);
            this.posZTxtBox.Name = "posZTxtBox";
            this.posZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posZTxtBox.TabIndex = 10;
            // 
            // posYTxtBox
            // 
            this.posYTxtBox.Location = new System.Drawing.Point(39, 45);
            this.posYTxtBox.Name = "posYTxtBox";
            this.posYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posYTxtBox.TabIndex = 11;
            // 
            // acceptBtn
            // 
            this.acceptBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptBtn.Location = new System.Drawing.Point(186, 502);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(168, 54);
            this.acceptBtn.TabIndex = 86;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(359, 372);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(62, 13);
            this.label22.TabIndex = 85;
            this.label22.Text = "Blend State";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(360, 301);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 13);
            this.label21.TabIndex = 84;
            this.label21.Text = "Max Color";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(359, 237);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 13);
            this.label20.TabIndex = 83;
            this.label20.Text = "Min Color";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.gravityXTxtBox);
            this.groupBox1.Controls.Add(this.gravityZTxtBox);
            this.groupBox1.Controls.Add(this.gravityYTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(359, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 107);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gravity";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 74);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 13);
            this.label19.TabIndex = 14;
            this.label19.Text = "Z:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 13);
            this.label18.TabIndex = 13;
            this.label18.Text = "Y:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 13);
            this.label17.TabIndex = 12;
            this.label17.Text = "X:";
            // 
            // gravityXTxtBox
            // 
            this.gravityXTxtBox.Location = new System.Drawing.Point(39, 19);
            this.gravityXTxtBox.Name = "gravityXTxtBox";
            this.gravityXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.gravityXTxtBox.TabIndex = 9;
            // 
            // gravityZTxtBox
            // 
            this.gravityZTxtBox.Location = new System.Drawing.Point(39, 71);
            this.gravityZTxtBox.Name = "gravityZTxtBox";
            this.gravityZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.gravityZTxtBox.TabIndex = 10;
            // 
            // gravityYTxtBox
            // 
            this.gravityYTxtBox.Location = new System.Drawing.Point(39, 45);
            this.gravityYTxtBox.Name = "gravityYTxtBox";
            this.gravityYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.gravityYTxtBox.TabIndex = 11;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 410);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 13);
            this.label16.TabIndex = 80;
            this.label16.Text = "Max Ending Size";
            // 
            // maxEndSizeTxtBox
            // 
            this.maxEndSizeTxtBox.Location = new System.Drawing.Point(151, 403);
            this.maxEndSizeTxtBox.Name = "maxEndSizeTxtBox";
            this.maxEndSizeTxtBox.Size = new System.Drawing.Size(168, 20);
            this.maxEndSizeTxtBox.TabIndex = 79;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 384);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 13);
            this.label15.TabIndex = 78;
            this.label15.Text = "Min Ending Size";
            // 
            // minEndSizeTxtBox
            // 
            this.minEndSizeTxtBox.Location = new System.Drawing.Point(151, 377);
            this.minEndSizeTxtBox.Name = "minEndSizeTxtBox";
            this.minEndSizeTxtBox.Size = new System.Drawing.Size(168, 20);
            this.minEndSizeTxtBox.TabIndex = 77;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 358);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 13);
            this.label14.TabIndex = 76;
            this.label14.Text = "Max Starting Size";
            // 
            // maxStartSizeTxtBox
            // 
            this.maxStartSizeTxtBox.Location = new System.Drawing.Point(151, 351);
            this.maxStartSizeTxtBox.Name = "maxStartSizeTxtBox";
            this.maxStartSizeTxtBox.Size = new System.Drawing.Size(168, 20);
            this.maxStartSizeTxtBox.TabIndex = 75;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 332);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 74;
            this.label13.Text = "Min Starting Size";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 308);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 13);
            this.label12.TabIndex = 73;
            this.label12.Text = "Max Rotate Speed";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 280);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 72;
            this.label11.Text = "Min Rotate Speed";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 71;
            this.label10.Text = "End Velocity";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 70;
            this.label9.Text = "Max Vertical Velocity";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 69;
            this.label8.Text = "Min Vertical Velocity";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 13);
            this.label7.TabIndex = 68;
            this.label7.Text = "Max Horizontal Velocity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 67;
            this.label6.Text = "Min Horizontal Velocity";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Emit Velocity Sensity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "Duration Randomness";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Duration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Max Particles";
            // 
            // maxColorComboBox
            // 
            this.maxColorComboBox.FormattingEnabled = true;
            this.maxColorComboBox.Location = new System.Drawing.Point(359, 317);
            this.maxColorComboBox.Name = "maxColorComboBox";
            this.maxColorComboBox.Size = new System.Drawing.Size(121, 21);
            this.maxColorComboBox.TabIndex = 62;
            // 
            // blendStateComboBox
            // 
            this.blendStateComboBox.FormattingEnabled = true;
            this.blendStateComboBox.Location = new System.Drawing.Point(359, 389);
            this.blendStateComboBox.Name = "blendStateComboBox";
            this.blendStateComboBox.Size = new System.Drawing.Size(121, 21);
            this.blendStateComboBox.TabIndex = 61;
            // 
            // minColorComboBox
            // 
            this.minColorComboBox.FormattingEnabled = true;
            this.minColorComboBox.Location = new System.Drawing.Point(359, 254);
            this.minColorComboBox.Name = "minColorComboBox";
            this.minColorComboBox.Size = new System.Drawing.Size(121, 21);
            this.minColorComboBox.TabIndex = 60;
            // 
            // minStartSizeTxtBox
            // 
            this.minStartSizeTxtBox.Location = new System.Drawing.Point(151, 325);
            this.minStartSizeTxtBox.Name = "minStartSizeTxtBox";
            this.minStartSizeTxtBox.Size = new System.Drawing.Size(168, 20);
            this.minStartSizeTxtBox.TabIndex = 59;
            // 
            // maxRotSpeedTxtBox
            // 
            this.maxRotSpeedTxtBox.Location = new System.Drawing.Point(151, 299);
            this.maxRotSpeedTxtBox.Name = "maxRotSpeedTxtBox";
            this.maxRotSpeedTxtBox.Size = new System.Drawing.Size(168, 20);
            this.maxRotSpeedTxtBox.TabIndex = 58;
            // 
            // minRotSpeedTxtBox
            // 
            this.minRotSpeedTxtBox.Location = new System.Drawing.Point(151, 273);
            this.minRotSpeedTxtBox.Name = "minRotSpeedTxtBox";
            this.minRotSpeedTxtBox.Size = new System.Drawing.Size(168, 20);
            this.minRotSpeedTxtBox.TabIndex = 57;
            // 
            // endVelTxtBox
            // 
            this.endVelTxtBox.Location = new System.Drawing.Point(151, 247);
            this.endVelTxtBox.Name = "endVelTxtBox";
            this.endVelTxtBox.Size = new System.Drawing.Size(168, 20);
            this.endVelTxtBox.TabIndex = 56;
            // 
            // maxVertVelTxtBox
            // 
            this.maxVertVelTxtBox.Location = new System.Drawing.Point(151, 221);
            this.maxVertVelTxtBox.Name = "maxVertVelTxtBox";
            this.maxVertVelTxtBox.Size = new System.Drawing.Size(168, 20);
            this.maxVertVelTxtBox.TabIndex = 55;
            // 
            // minVertVelTxtBox
            // 
            this.minVertVelTxtBox.Location = new System.Drawing.Point(151, 194);
            this.minVertVelTxtBox.Name = "minVertVelTxtBox";
            this.minVertVelTxtBox.Size = new System.Drawing.Size(168, 20);
            this.minVertVelTxtBox.TabIndex = 54;
            // 
            // maxHorizTxtBox
            // 
            this.maxHorizTxtBox.Location = new System.Drawing.Point(151, 168);
            this.maxHorizTxtBox.Name = "maxHorizTxtBox";
            this.maxHorizTxtBox.Size = new System.Drawing.Size(168, 20);
            this.maxHorizTxtBox.TabIndex = 53;
            // 
            // minHorizVelTxtBox
            // 
            this.minHorizVelTxtBox.Location = new System.Drawing.Point(151, 142);
            this.minHorizVelTxtBox.Name = "minHorizVelTxtBox";
            this.minHorizVelTxtBox.Size = new System.Drawing.Size(168, 20);
            this.minHorizVelTxtBox.TabIndex = 52;
            // 
            // emitVelSensTxtBox
            // 
            this.emitVelSensTxtBox.Location = new System.Drawing.Point(151, 116);
            this.emitVelSensTxtBox.Name = "emitVelSensTxtBox";
            this.emitVelSensTxtBox.Size = new System.Drawing.Size(168, 20);
            this.emitVelSensTxtBox.TabIndex = 51;
            // 
            // durationRandTxtBox
            // 
            this.durationRandTxtBox.Location = new System.Drawing.Point(151, 90);
            this.durationRandTxtBox.Name = "durationRandTxtBox";
            this.durationRandTxtBox.Size = new System.Drawing.Size(168, 20);
            this.durationRandTxtBox.TabIndex = 50;
            // 
            // durationTxtBox
            // 
            this.durationTxtBox.Location = new System.Drawing.Point(151, 64);
            this.durationTxtBox.Name = "durationTxtBox";
            this.durationTxtBox.Size = new System.Drawing.Size(168, 20);
            this.durationTxtBox.TabIndex = 49;
            // 
            // maxParticlesTxtBox
            // 
            this.maxParticlesTxtBox.Location = new System.Drawing.Point(151, 38);
            this.maxParticlesTxtBox.Name = "maxParticlesTxtBox";
            this.maxParticlesTxtBox.Size = new System.Drawing.Size(168, 20);
            this.maxParticlesTxtBox.TabIndex = 48;
            // 
            // textureNameTxtBox
            // 
            this.textureNameTxtBox.Location = new System.Drawing.Point(151, 12);
            this.textureNameTxtBox.Name = "textureNameTxtBox";
            this.textureNameTxtBox.Size = new System.Drawing.Size(168, 20);
            this.textureNameTxtBox.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Texture Name";
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(218, 562);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(97, 41);
            this.applyBtn.TabIndex = 90;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // idTxtBox
            // 
            this.idTxtBox.Location = new System.Drawing.Point(151, 455);
            this.idTxtBox.Name = "idTxtBox";
            this.idTxtBox.Size = new System.Drawing.Size(100, 20);
            this.idTxtBox.TabIndex = 91;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(18, 458);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(18, 13);
            this.label23.TabIndex = 92;
            this.label23.Text = "ID";
            // 
            // emitRateTxtBox
            // 
            this.emitRateTxtBox.Location = new System.Drawing.Point(151, 429);
            this.emitRateTxtBox.Name = "emitRateTxtBox";
            this.emitRateTxtBox.Size = new System.Drawing.Size(168, 20);
            this.emitRateTxtBox.TabIndex = 93;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(18, 432);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 13);
            this.label27.TabIndex = 94;
            this.label27.Text = "Emit Rate";
            // 
            // ParticleSystemEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 677);
            this.ControlBox = false;
            this.Controls.Add(this.label27);
            this.Controls.Add(this.emitRateTxtBox);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.idTxtBox);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.maxEndSizeTxtBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.minEndSizeTxtBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.maxStartSizeTxtBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxColorComboBox);
            this.Controls.Add(this.blendStateComboBox);
            this.Controls.Add(this.minColorComboBox);
            this.Controls.Add(this.minStartSizeTxtBox);
            this.Controls.Add(this.maxRotSpeedTxtBox);
            this.Controls.Add(this.minRotSpeedTxtBox);
            this.Controls.Add(this.endVelTxtBox);
            this.Controls.Add(this.maxVertVelTxtBox);
            this.Controls.Add(this.minVertVelTxtBox);
            this.Controls.Add(this.maxHorizTxtBox);
            this.Controls.Add(this.minHorizVelTxtBox);
            this.Controls.Add(this.emitVelSensTxtBox);
            this.Controls.Add(this.durationRandTxtBox);
            this.Controls.Add(this.durationTxtBox);
            this.Controls.Add(this.maxParticlesTxtBox);
            this.Controls.Add(this.textureNameTxtBox);
            this.Controls.Add(this.label1);
            this.Name = "ParticleSystemEditorForm";
            this.Text = "Particle System Editor";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox posXTxtBox;
        private System.Windows.Forms.TextBox posZTxtBox;
        private System.Windows.Forms.TextBox posYTxtBox;
        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox gravityXTxtBox;
        private System.Windows.Forms.TextBox gravityZTxtBox;
        private System.Windows.Forms.TextBox gravityYTxtBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox maxEndSizeTxtBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox minEndSizeTxtBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox maxStartSizeTxtBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox maxColorComboBox;
        private System.Windows.Forms.ComboBox blendStateComboBox;
        private System.Windows.Forms.ComboBox minColorComboBox;
        private System.Windows.Forms.TextBox minStartSizeTxtBox;
        private System.Windows.Forms.TextBox maxRotSpeedTxtBox;
        private System.Windows.Forms.TextBox minRotSpeedTxtBox;
        private System.Windows.Forms.TextBox endVelTxtBox;
        private System.Windows.Forms.TextBox maxVertVelTxtBox;
        private System.Windows.Forms.TextBox minVertVelTxtBox;
        private System.Windows.Forms.TextBox maxHorizTxtBox;
        private System.Windows.Forms.TextBox minHorizVelTxtBox;
        private System.Windows.Forms.TextBox emitVelSensTxtBox;
        private System.Windows.Forms.TextBox durationRandTxtBox;
        private System.Windows.Forms.TextBox durationTxtBox;
        private System.Windows.Forms.TextBox maxParticlesTxtBox;
        private System.Windows.Forms.TextBox textureNameTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.TextBox idTxtBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox emitRateTxtBox;
        private System.Windows.Forms.Label label27;
    }
}