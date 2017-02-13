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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.resultTb = new System.Windows.Forms.TextBox();
            this.inputFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.startBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.modelFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.mobileModelBtn = new System.Windows.Forms.Button();
            this.hotelModelBtn = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.attributeCb = new System.Windows.Forms.CheckBox();
            this.expressionCb = new System.Windows.Forms.CheckBox();
            this.evaluateCb = new System.Windows.Forms.CheckBox();
            this.resultTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // resultTb
            // 
            this.resultTb.Location = new System.Drawing.Point(369, 276);
            this.resultTb.Multiline = true;
            this.resultTb.Name = "resultTb";
            this.resultTb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTb.Size = new System.Drawing.Size(195, 250);
            this.resultTb.TabIndex = 1;
            // 
            // inputFileOpenDialog
            // 
            this.inputFileOpenDialog.FileName = "Input.txt";
            // 
            // startBtn
            // 
            this.startBtn.BackColor = System.Drawing.SystemColors.Control;
            this.startBtn.Location = new System.Drawing.Point(485, 174);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 44);
            this.startBtn.TabIndex = 23;
            this.startBtn.Text = "识别";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "请选择识别领域：";
            // 
            // modelFileOpenDialog
            // 
            this.modelFileOpenDialog.FileName = "Input.txt";
            // 
            // mobileModelBtn
            // 
            this.mobileModelBtn.BackColor = System.Drawing.Color.LightSkyBlue;
            this.mobileModelBtn.Location = new System.Drawing.Point(116, 19);
            this.mobileModelBtn.Name = "mobileModelBtn";
            this.mobileModelBtn.Size = new System.Drawing.Size(75, 23);
            this.mobileModelBtn.TabIndex = 45;
            this.mobileModelBtn.Text = "手机";
            this.mobileModelBtn.UseVisualStyleBackColor = false;
            this.mobileModelBtn.Click += new System.EventHandler(this.mobileModelBtn_Click);
            // 
            // hotelModelBtn
            // 
            this.hotelModelBtn.BackColor = System.Drawing.SystemColors.Control;
            this.hotelModelBtn.Location = new System.Drawing.Point(197, 19);
            this.hotelModelBtn.Name = "hotelModelBtn";
            this.hotelModelBtn.Size = new System.Drawing.Size(75, 23);
            this.hotelModelBtn.TabIndex = 46;
            this.hotelModelBtn.Text = "酒店";
            this.hotelModelBtn.UseVisualStyleBackColor = true;
            this.hotelModelBtn.Click += new System.EventHandler(this.hotelModelBtn_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(16, 50);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputTextBox.Size = new System.Drawing.Size(450, 168);
            this.inputTextBox.TabIndex = 47;
            // 
            // attributeCb
            // 
            this.attributeCb.AutoSize = true;
            this.attributeCb.Checked = true;
            this.attributeCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.attributeCb.Location = new System.Drawing.Point(16, 253);
            this.attributeCb.Name = "attributeCb";
            this.attributeCb.Size = new System.Drawing.Size(50, 17);
            this.attributeCb.TabIndex = 48;
            this.attributeCb.Text = "属性";
            this.attributeCb.UseVisualStyleBackColor = true;
            this.attributeCb.CheckedChanged += new System.EventHandler(this.propertyCb_CheckedChanged);
            // 
            // expressionCb
            // 
            this.expressionCb.AutoSize = true;
            this.expressionCb.Checked = true;
            this.expressionCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.expressionCb.Location = new System.Drawing.Point(140, 253);
            this.expressionCb.Name = "expressionCb";
            this.expressionCb.Size = new System.Drawing.Size(74, 17);
            this.expressionCb.TabIndex = 49;
            this.expressionCb.Text = "意见解释";
            this.expressionCb.UseVisualStyleBackColor = true;
            this.expressionCb.CheckedChanged += new System.EventHandler(this.propertyCb_CheckedChanged);
            // 
            // evaluateCb
            // 
            this.evaluateCb.AutoSize = true;
            this.evaluateCb.Checked = true;
            this.evaluateCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.evaluateCb.Location = new System.Drawing.Point(78, 253);
            this.evaluateCb.Name = "evaluateCb";
            this.evaluateCb.Size = new System.Drawing.Size(50, 17);
            this.evaluateCb.TabIndex = 50;
            this.evaluateCb.Text = "评价";
            this.evaluateCb.UseVisualStyleBackColor = true;
            this.evaluateCb.CheckedChanged += new System.EventHandler(this.propertyCb_CheckedChanged);
            // 
            // resultTree
            // 
            this.resultTree.Location = new System.Drawing.Point(12, 276);
            this.resultTree.Name = "resultTree";
            this.resultTree.Size = new System.Drawing.Size(320, 250);
            this.resultTree.TabIndex = 51;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 538);
            this.Controls.Add(this.resultTree);
            this.Controls.Add(this.evaluateCb);
            this.Controls.Add(this.expressionCb);
            this.Controls.Add(this.attributeCb);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.hotelModelBtn);
            this.Controls.Add(this.mobileModelBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.resultTb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "解释性意见要素识别系统";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox resultTb;
        private System.Windows.Forms.OpenFileDialog inputFileOpenDialog;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog modelFileOpenDialog;
        private System.Windows.Forms.Button mobileModelBtn;
        private System.Windows.Forms.Button hotelModelBtn;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.CheckBox attributeCb;
        private System.Windows.Forms.CheckBox expressionCb;
        private System.Windows.Forms.CheckBox evaluateCb;
        private System.Windows.Forms.TreeView resultTree;
    }
}

