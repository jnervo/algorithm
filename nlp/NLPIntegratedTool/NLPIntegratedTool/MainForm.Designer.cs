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
            this.logTb = new System.Windows.Forms.TextBox();
            this.inputFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lstmFileTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.crfFileTb = new System.Windows.Forms.TextBox();
            this.crf2LstmBtn = new System.Windows.Forms.Button();
            this.pos2CrfBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.posFileTb = new System.Windows.Forms.TextBox();
            this.posBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.segFileTb = new System.Windows.Forms.TextBox();
            this.segBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.inputFileSelectBtn = new System.Windows.Forms.Button();
            this.inputFileTb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.modelFileSeletBtn = new System.Windows.Forms.Button();
            this.modelFileTb = new System.Windows.Forms.TextBox();
            this.modelFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.lstm2OutputBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.finalOutputFileTb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // logTb
            // 
            this.logTb.Location = new System.Drawing.Point(12, 314);
            this.logTb.Multiline = true;
            this.logTb.Name = "logTb";
            this.logTb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTb.Size = new System.Drawing.Size(816, 212);
            this.logTb.TabIndex = 1;
            // 
            // inputFileOpenDialog
            // 
            this.inputFileOpenDialog.FileName = "Input.txt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "执行日志:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "LSTM文件:";
            // 
            // lstmFileTb
            // 
            this.lstmFileTb.Location = new System.Drawing.Point(85, 178);
            this.lstmFileTb.Name = "lstmFileTb";
            this.lstmFileTb.Size = new System.Drawing.Size(626, 20);
            this.lstmFileTb.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "CRF文件:";
            // 
            // crfFileTb
            // 
            this.crfFileTb.Location = new System.Drawing.Point(85, 147);
            this.crfFileTb.Name = "crfFileTb";
            this.crfFileTb.Size = new System.Drawing.Size(626, 20);
            this.crfFileTb.TabIndex = 36;
            // 
            // crf2LstmBtn
            // 
            this.crf2LstmBtn.Location = new System.Drawing.Point(321, 253);
            this.crf2LstmBtn.Name = "crf2LstmBtn";
            this.crf2LstmBtn.Size = new System.Drawing.Size(83, 23);
            this.crf2LstmBtn.TabIndex = 34;
            this.crf2LstmBtn.Text = "CRF=>LSTM";
            this.crf2LstmBtn.UseVisualStyleBackColor = true;
            // 
            // pos2CrfBtn
            // 
            this.pos2CrfBtn.Location = new System.Drawing.Point(226, 253);
            this.pos2CrfBtn.Name = "pos2CrfBtn";
            this.pos2CrfBtn.Size = new System.Drawing.Size(75, 23);
            this.pos2CrfBtn.TabIndex = 33;
            this.pos2CrfBtn.Text = "POS=>CRF";
            this.pos2CrfBtn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "词性标注:";
            // 
            // posFileTb
            // 
            this.posFileTb.Location = new System.Drawing.Point(85, 114);
            this.posFileTb.Name = "posFileTb";
            this.posFileTb.Size = new System.Drawing.Size(626, 20);
            this.posFileTb.TabIndex = 32;
            // 
            // posBtn
            // 
            this.posBtn.Location = new System.Drawing.Point(125, 253);
            this.posBtn.Name = "posBtn";
            this.posBtn.Size = new System.Drawing.Size(75, 23);
            this.posBtn.TabIndex = 30;
            this.posBtn.Text = "词性标注";
            this.posBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "分词结果:";
            // 
            // segFileTb
            // 
            this.segFileTb.Location = new System.Drawing.Point(85, 83);
            this.segFileTb.Name = "segFileTb";
            this.segFileTb.Size = new System.Drawing.Size(626, 20);
            this.segFileTb.TabIndex = 29;
            // 
            // segBtn
            // 
            this.segBtn.Location = new System.Drawing.Point(24, 253);
            this.segBtn.Name = "segBtn";
            this.segBtn.Size = new System.Drawing.Size(75, 23);
            this.segBtn.TabIndex = 27;
            this.segBtn.Text = "分词";
            this.segBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "语料文件:";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(538, 253);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 23;
            this.startBtn.Text = "所有步骤";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // inputFileSelectBtn
            // 
            this.inputFileSelectBtn.Location = new System.Drawing.Point(717, 50);
            this.inputFileSelectBtn.Name = "inputFileSelectBtn";
            this.inputFileSelectBtn.Size = new System.Drawing.Size(30, 23);
            this.inputFileSelectBtn.TabIndex = 26;
            this.inputFileSelectBtn.Text = "...";
            this.inputFileSelectBtn.UseVisualStyleBackColor = true;
            this.inputFileSelectBtn.Click += new System.EventHandler(this.inputFileSelectBtn_Click);
            // 
            // inputFileTb
            // 
            this.inputFileTb.Location = new System.Drawing.Point(85, 52);
            this.inputFileTb.Name = "inputFileTb";
            this.inputFileTb.Size = new System.Drawing.Size(626, 20);
            this.inputFileTb.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Model 文件";
            // 
            // modelFileSeletBtn
            // 
            this.modelFileSeletBtn.Location = new System.Drawing.Point(717, 19);
            this.modelFileSeletBtn.Name = "modelFileSeletBtn";
            this.modelFileSeletBtn.Size = new System.Drawing.Size(30, 23);
            this.modelFileSeletBtn.TabIndex = 41;
            this.modelFileSeletBtn.Text = "...";
            this.modelFileSeletBtn.UseVisualStyleBackColor = true;
            this.modelFileSeletBtn.Click += new System.EventHandler(this.modelFileSeletBtn_Click);
            // 
            // modelFileTb
            // 
            this.modelFileTb.Location = new System.Drawing.Point(85, 21);
            this.modelFileTb.Name = "modelFileTb";
            this.modelFileTb.Size = new System.Drawing.Size(626, 20);
            this.modelFileTb.TabIndex = 40;
            // 
            // modelFileOpenDialog
            // 
            this.modelFileOpenDialog.FileName = "Input.txt";
            // 
            // lstm2OutputBtn
            // 
            this.lstm2OutputBtn.Location = new System.Drawing.Point(427, 253);
            this.lstm2OutputBtn.Name = "lstm2OutputBtn";
            this.lstm2OutputBtn.Size = new System.Drawing.Size(105, 23);
            this.lstm2OutputBtn.TabIndex = 42;
            this.lstm2OutputBtn.Text = "LSTM=>OUTPUT";
            this.lstm2OutputBtn.UseVisualStyleBackColor = true;
            this.lstm2OutputBtn.Click += new System.EventHandler(this.lstm2OutputBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "最终输出:";
            // 
            // finalOutputFileTb
            // 
            this.finalOutputFileTb.Location = new System.Drawing.Point(85, 209);
            this.finalOutputFileTb.Name = "finalOutputFileTb";
            this.finalOutputFileTb.Size = new System.Drawing.Size(626, 20);
            this.finalOutputFileTb.TabIndex = 44;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 538);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.finalOutputFileTb);
            this.Controls.Add(this.lstm2OutputBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.modelFileSeletBtn);
            this.Controls.Add(this.modelFileTb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lstmFileTb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.crfFileTb);
            this.Controls.Add(this.crf2LstmBtn);
            this.Controls.Add(this.pos2CrfBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.posFileTb);
            this.Controls.Add(this.posBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.segFileTb);
            this.Controls.Add(this.segBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.inputFileSelectBtn);
            this.Controls.Add(this.inputFileTb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logTb);
            this.Name = "MainForm";
            this.Text = "NLP Integrated Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox logTb;
        private System.Windows.Forms.OpenFileDialog inputFileOpenDialog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lstmFileTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox crfFileTb;
        private System.Windows.Forms.Button crf2LstmBtn;
        private System.Windows.Forms.Button pos2CrfBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox posFileTb;
        private System.Windows.Forms.Button posBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox segFileTb;
        private System.Windows.Forms.Button segBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button inputFileSelectBtn;
        private System.Windows.Forms.TextBox inputFileTb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button modelFileSeletBtn;
        private System.Windows.Forms.TextBox modelFileTb;
        private System.Windows.Forms.OpenFileDialog modelFileOpenDialog;
        private System.Windows.Forms.Button lstm2OutputBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox finalOutputFileTb;
    }
}

