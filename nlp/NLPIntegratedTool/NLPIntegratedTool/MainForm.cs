using NLPIntegratedTool.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NLPIntegratedTool.LstmProcessor;

namespace NLPIntegratedTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            LogHelper.Init(resultTb);

            InitModelType(LstmProcessor.ModelType.Mobile);
        }

        private void InitModelType(LstmProcessor.ModelType modelType)
        {
            switch (modelType)
            {
                case ModelType.Mobile:
                    mobileModelBtn.BackColor = System.Drawing.Color.LightSkyBlue;
                    hotelModelBtn.BackColor = System.Drawing.SystemColors.Control;
                    hotelModelBtn.UseVisualStyleBackColor = true;
                    LstmProcessor.SetModelType(modelType);
                    break;
                case ModelType.Hotel:
                    mobileModelBtn.BackColor = System.Drawing.SystemColors.Control;
                    mobileModelBtn.UseVisualStyleBackColor = true;
                    hotelModelBtn.BackColor = System.Drawing.Color.LightSkyBlue;
                    LstmProcessor.SetModelType(modelType);
                    break;
            }

            LoadDefaultData();
        }

        private void LoadDefaultData()
        {
            this.inputTextBox.Text = File.ReadAllText(Processor.DefaultDataFilePath);
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            var text = this.inputTextBox.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("输入为空。请重新输入。");
            }

            try
            {
                var result = Processor.ProcessText(text);

                DisplayProcessResult(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常：" + ex.Message + " \r\n StackTrace:" + ex.StackTrace);
            }
        }

        private void DisplayProcessResult(ProcessResult result)
        {
            resultTree.Nodes.Clear();
            TreeNode rootNode = new TreeNode("处理结果");

            foreach (var sentence in result.Sentences)
            {
                TreeNode sentenceNode = new TreeNode();
                sentenceNode.Text = sentence.SentenceStr;

                TreeNode attributeNode = new TreeNode("属性");
                attributeNode.Nodes.Add(new TreeNode(string.Join(";", sentence.Attributes)));

                TreeNode evaluationNode = new TreeNode("评价");
                evaluationNode.Nodes.Add(new TreeNode(string.Join(";", sentence.Evaluations)));

                TreeNode expressionNode = new TreeNode("意见解释");
                expressionNode.Nodes.Add(new TreeNode(string.Join(";", sentence.Expressions)));

                sentenceNode.Nodes.Add(attributeNode);
                sentenceNode.Nodes.Add(evaluationNode);
                sentenceNode.Nodes.Add(expressionNode);

                rootNode.Nodes.Add(sentenceNode);
            }
            resultTree.Nodes.Add(rootNode);
        }

        private void propertyCb_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mobileModelBtn_Click(object sender, EventArgs e)
        {
            InitModelType(LstmProcessor.ModelType.Mobile);
        }

        private void hotelModelBtn_Click(object sender, EventArgs e)
        {
            InitModelType(LstmProcessor.ModelType.Hotel);
        }

        /*
        private void inputFileSelectBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = inputFileOpenDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                inputFileTb.Text = inputFileOpenDialog.FileName;
            }
        }

        private void modelFileSeletBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = modelFileOpenDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                modelFileTb.Text = modelFileOpenDialog.FileName;
            }
        }
        private void segBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.Log("Start to seg: " + inputFileTb.Text);
                segFileTb.Text = NlpProcessor.Seg(inputFileTb.Text);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Catched exception: " + ex.Message);
            }
        }

        private void posBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.Log("Start to pos: " + segFileTb.Text);
                posFileTb.Text = NlpProcessor.Pos(segFileTb.Text);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Catched exception: " + ex.Message);
            }
        }

        private void pos2CrfBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.Log("Start to convert POS to CRF: " + posFileTb.Text);
                crfFileTb.Text = CrfProcessor.Pos2Crf(posFileTb.Text);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Catched exception: " + ex.Message);
            }

        }

        private void crf2LstmBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.Log("Start to convert CRF to LSTM: " + crfFileTb.Text);
                lstmFileTb.Text = CrfProcessor.Crf2Lstm(crfFileTb.Text);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Catched exception: " + ex.Message);
            }
        }

        private void lstm2OutputBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LogHelper.Log("Start to convert LSTM to final output: " + lstmFileTb.Text);
                finalOutputFileTb.Text = LstmProcessor.Lstm2FinalOutput(lstmFileTb.Text, modelFileTb.Text);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Catched exception: " + ex.Message);
            }

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            LogHelper.Log("#### Step 1 : input => seg ####");
            segBtn_Click(null, null);
            LogHelper.Log("#### Step 2 : seg => pos ####");
            posBtn_Click(null, null);
            LogHelper.Log("#### Step 3 : skip ####");
            LogHelper.Enter();
            LogHelper.Log("#### Step 4 : pos => crf ####");
            pos2CrfBtn_Click(null, null);
            LogHelper.Log("#### Step 5 : skip ####");
            LogHelper.Log("#### Step 6 : skip ####");
            LogHelper.Enter();
            LogHelper.Log("#### Step 7 : crf => lstm ####");
            crf2LstmBtn_Click(null, null);
            LogHelper.Log("#### Step 8 : lstm => final output ####");
            lstm2OutputBtn_Click(null, null);
            LogHelper.Log("#### Finish!!! ####");
        }
        */
    }
}
