using NLPIntegratedTool.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NLPIntegratedTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            LogHelper.Init(logTb);

            modelFileTb.Text = LstmProcessor.DefaultModelFile;
        }

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

    }
}
