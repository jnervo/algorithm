namespace NLPIntegratedTool
{
    partial class MainForm
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
            this.startBtn = new System.Windows.Forms.Button();
            this.logTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inputFileTb = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.inputFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.inputFileSelectBtn = new System.Windows.Forms.Button();
            this.segBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.segFileTb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.posBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.posFileTb = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pos2CrfBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(312, 123);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "所有步骤";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // logTb
            // 
            this.logTb.Location = new System.Drawing.Point(12, 218);
            this.logTb.Multiline = true;
            this.logTb.Name = "logTb";
            this.logTb.Size = new System.Drawing.Size(816, 308);
            this.logTb.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "语料文件:";
            // 
            // inputFileTb
            // 
            this.inputFileTb.Location = new System.Drawing.Point(70, 17);
            this.inputFileTb.Name = "inputFileTb";
            this.inputFileTb.Size = new System.Drawing.Size(626, 20);
            this.inputFileTb.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(816, 187);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pos2CrfBtn);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.posFileTb);
            this.tabPage1.Controls.Add(this.posBtn);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.segFileTb);
            this.tabPage1.Controls.Add(this.segBtn);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.startBtn);
            this.tabPage1.Controls.Add(this.inputFileSelectBtn);
            this.tabPage1.Controls.Add(this.inputFileTb);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(808, 161);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "训练";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(378, 353);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "处理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // inputFileOpenDialog
            // 
            this.inputFileOpenDialog.FileName = "Input.txt";
            // 
            // inputFileSelectBtn
            // 
            this.inputFileSelectBtn.Location = new System.Drawing.Point(702, 15);
            this.inputFileSelectBtn.Name = "inputFileSelectBtn";
            this.inputFileSelectBtn.Size = new System.Drawing.Size(30, 23);
            this.inputFileSelectBtn.TabIndex = 7;
            this.inputFileSelectBtn.Text = "...";
            this.inputFileSelectBtn.UseVisualStyleBackColor = true;
            this.inputFileSelectBtn.Click += new System.EventHandler(this.inputFileSelectBtn_Click);
            // 
            // segBtn
            // 
            this.segBtn.Location = new System.Drawing.Point(9, 123);
            this.segBtn.Name = "segBtn";
            this.segBtn.Size = new System.Drawing.Size(75, 23);
            this.segBtn.TabIndex = 8;
            this.segBtn.Text = "分词";
            this.segBtn.UseVisualStyleBackColor = true;
            this.segBtn.Click += new System.EventHandler(this.segBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "分词结果:";
            // 
            // segFileTb
            // 
            this.segFileTb.Location = new System.Drawing.Point(70, 48);
            this.segFileTb.Name = "segFileTb";
            this.segFileTb.Size = new System.Drawing.Size(626, 20);
            this.segFileTb.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "执行日志:";
            // 
            // posBtn
            // 
            this.posBtn.Location = new System.Drawing.Point(110, 123);
            this.posBtn.Name = "posBtn";
            this.posBtn.Size = new System.Drawing.Size(75, 23);
            this.posBtn.TabIndex = 11;
            this.posBtn.Text = "词性标注";
            this.posBtn.UseVisualStyleBackColor = true;
            this.posBtn.Click += new System.EventHandler(this.posBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "词性标注:";
            // 
            // posFileTb
            // 
            this.posFileTb.Location = new System.Drawing.Point(70, 79);
            this.posFileTb.Name = "posFileTb";
            this.posFileTb.Size = new System.Drawing.Size(626, 20);
            this.posFileTb.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(702, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(702, 77);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pos2CrfBtn
            // 
            this.pos2CrfBtn.Location = new System.Drawing.Point(211, 123);
            this.pos2CrfBtn.Name = "pos2CrfBtn";
            this.pos2CrfBtn.Size = new System.Drawing.Size(75, 23);
            this.pos2CrfBtn.TabIndex = 16;
            this.pos2CrfBtn.Text = "POS=>CRF";
            this.pos2CrfBtn.UseVisualStyleBackColor = true;
            this.pos2CrfBtn.Click += new System.EventHandler(this.pos2CrfBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 538);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.logTb);
            this.Name = "MainForm";
            this.Text = "NLP Integrated Tool";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox logTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputFileTb;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.OpenFileDialog inputFileOpenDialog;
        private System.Windows.Forms.Button inputFileSelectBtn;
        private System.Windows.Forms.Button segBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox segFileTb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button posBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox posFileTb;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button pos2CrfBtn;
    }
}

