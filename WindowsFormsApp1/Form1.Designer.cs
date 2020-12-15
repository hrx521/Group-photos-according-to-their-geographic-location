
namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonSource = new System.Windows.Forms.Button();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDest = new System.Windows.Forms.Button();
            this.textBoxDest = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonExcuteGroup = new System.Windows.Forms.Button();
            this.textBoxTolerence = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelProcess = new System.Windows.Forms.Label();
            this.pictureBoxProcess = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxGpsDataSource = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSource
            // 
            this.buttonSource.Location = new System.Drawing.Point(366, 60);
            this.buttonSource.Name = "buttonSource";
            this.buttonSource.Size = new System.Drawing.Size(85, 24);
            this.buttonSource.TabIndex = 0;
            this.buttonSource.Text = "选择";
            this.buttonSource.UseVisualStyleBackColor = true;
            this.buttonSource.Click += new System.EventHandler(this.buttonSource_Click);
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(118, 61);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.Size = new System.Drawing.Size(242, 23);
            this.textBoxSource.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "源路径";
            // 
            // buttonDest
            // 
            this.buttonDest.Location = new System.Drawing.Point(366, 102);
            this.buttonDest.Name = "buttonDest";
            this.buttonDest.Size = new System.Drawing.Size(85, 24);
            this.buttonDest.TabIndex = 0;
            this.buttonDest.Text = "选择";
            this.buttonDest.UseVisualStyleBackColor = true;
            this.buttonDest.Click += new System.EventHandler(this.buttonTarg_Click);
            // 
            // textBoxDest
            // 
            this.textBoxDest.Location = new System.Drawing.Point(118, 103);
            this.textBoxDest.Name = "textBoxDest";
            this.textBoxDest.Size = new System.Drawing.Size(242, 23);
            this.textBoxDest.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "目标路径";
            // 
            // buttonExcuteGroup
            // 
            this.buttonExcuteGroup.Location = new System.Drawing.Point(118, 194);
            this.buttonExcuteGroup.Name = "buttonExcuteGroup";
            this.buttonExcuteGroup.Size = new System.Drawing.Size(85, 24);
            this.buttonExcuteGroup.TabIndex = 0;
            this.buttonExcuteGroup.Text = "开始分组";
            this.buttonExcuteGroup.UseVisualStyleBackColor = true;
            this.buttonExcuteGroup.Click += new System.EventHandler(this.buttonExcuteGroup_Click);
            // 
            // textBoxTolerence
            // 
            this.textBoxTolerence.Location = new System.Drawing.Point(118, 145);
            this.textBoxTolerence.Name = "textBoxTolerence";
            this.textBoxTolerence.Size = new System.Drawing.Size(110, 23);
            this.textBoxTolerence.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "距离容差小于(米)";
            // 
            // labelProcess
            // 
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(299, 201);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(0, 17);
            this.labelProcess.TabIndex = 3;
            // 
            // pictureBoxProcess
            // 
            this.pictureBoxProcess.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxProcess.Image")));
            this.pictureBoxProcess.Location = new System.Drawing.Point(256, 193);
            this.pictureBoxProcess.Name = "pictureBoxProcess";
            this.pictureBoxProcess.Size = new System.Drawing.Size(37, 29);
            this.pictureBoxProcess.TabIndex = 4;
            this.pictureBoxProcess.TabStop = false;
            this.pictureBoxProcess.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "复制",
            "剪切"});
            this.comboBox1.Location = new System.Drawing.Point(369, 143);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(82, 25);
            this.comboBox1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(256, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "目标文件创建方式";
            // 
            // comboBoxGpsDataSource
            // 
            this.comboBoxGpsDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGpsDataSource.FormattingEnabled = true;
            this.comboBoxGpsDataSource.Items.AddRange(new object[] {
            "Pos文件",
            "图片文件"});
            this.comboBoxGpsDataSource.Location = new System.Drawing.Point(121, 21);
            this.comboBoxGpsDataSource.Name = "comboBoxGpsDataSource";
            this.comboBoxGpsDataSource.Size = new System.Drawing.Size(107, 25);
            this.comboBoxGpsDataSource.TabIndex = 7;
            this.comboBoxGpsDataSource.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Gps数据来源";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 243);
            this.Controls.Add(this.comboBoxGpsDataSource);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBoxProcess);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxTolerence);
            this.Controls.Add(this.textBoxDest);
            this.Controls.Add(this.textBoxSource);
            this.Controls.Add(this.buttonExcuteGroup);
            this.Controls.Add(this.buttonDest);
            this.Controls.Add(this.buttonSource);
            this.Name = "Form1";
            this.Text = "GroupImages";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProcess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSource;
        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDest;
        private System.Windows.Forms.TextBox textBoxDest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonExcuteGroup;
        private System.Windows.Forms.TextBox textBoxTolerence;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.PictureBox pictureBoxProcess;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxGpsDataSource;
        private System.Windows.Forms.Label label5;
    }
}

