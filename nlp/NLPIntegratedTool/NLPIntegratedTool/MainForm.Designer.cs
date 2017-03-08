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
            this.logTb = new System.Windows.Forms.TextBox();
            this.inputFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.modelFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.mobileModelBtn = new System.Windows.Forms.Button();
            this.hotelModelBtn = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.attributeCb = new System.Windows.Forms.CheckBox();
            this.expressionCb = new System.Windows.Forms.CheckBox();
            this.evaluateCb = new System.Windows.Forms.CheckBox();
            this.resultTree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.clearBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logTb
            // 
            this.logTb.Location = new System.Drawing.Point(344, 248);
            this.logTb.Multiline = true;
            this.logTb.Name = "logTb";
            this.logTb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTb.Size = new System.Drawing.Size(320, 278);
            this.logTb.TabIndex = 1;
            // 
            // inputFileOpenDialog
            // 
            this.inputFileOpenDialog.FileName = "Input.txt";
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
            this.inputTextBox.Size = new System.Drawing.Size(567, 168);
            this.inputTextBox.TabIndex = 47;
            // 
            // attributeCb
            // 
            this.attributeCb.AutoSize = true;
            this.attributeCb.Checked = true;
            this.attributeCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.attributeCb.Location = new System.Drawing.Point(18, 225);
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
            this.expressionCb.Location = new System.Drawing.Point(142, 225);
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
            this.evaluateCb.Location = new System.Drawing.Point(80, 225);
            this.evaluateCb.Name = "evaluateCb";
            this.evaluateCb.Size = new System.Drawing.Size(50, 17);
            this.evaluateCb.TabIndex = 50;
            this.evaluateCb.Text = "评价";
            this.evaluateCb.UseVisualStyleBackColor = true;
            this.evaluateCb.CheckedChanged += new System.EventHandler(this.propertyCb_CheckedChanged);
            // 
            // resultTree
            // 
            this.resultTree.Location = new System.Drawing.Point(18, 248);
            this.resultTree.Name = "resultTree";
            this.resultTree.Size = new System.Drawing.Size(320, 278);
            this.resultTree.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "处理日志：";
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.SystemColors.Control;
            this.clearBtn.Location = new System.Drawing.Point(589, 181);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 37);
            this.clearBtn.TabIndex = 53;
            this.clearBtn.Text = "重置";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.BackColor = System.Drawing.SystemColors.Control;
            this.startBtn.Location = new System.Drawing.Point(589, 50);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 125);
            this.startBtn.TabIndex = 54;
            this.startBtn.Text = "识别";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 538);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resultTree);
            this.Controls.Add(this.evaluateCb);
            this.Controls.Add(this.expressionCb);
            this.Controls.Add(this.attributeCb);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.hotelModelBtn);
            this.Controls.Add(this.mobileModelBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.logTb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "汉语解释意见句识别系统";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox logTb;
        private System.Windows.Forms.OpenFileDialog inputFileOpenDialog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog modelFileOpenDialog;
        private System.Windows.Forms.Button mobileModelBtn;
        private System.Windows.Forms.Button hotelModelBtn;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.CheckBox attributeCb;
        private System.Windows.Forms.CheckBox expressionCb;
        private System.Windows.Forms.CheckBox evaluateCb;
        private System.Windows.Forms.TreeView resultTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button startBtn;
    }
}

