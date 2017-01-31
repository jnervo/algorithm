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
        }

        private void inputFileSelectBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = inputFileOpenDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                inputFileTb.Text = inputFileOpenDialog.FileName;
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
                CrfProcessor.Pos2Crf(posFileTb.Text);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Catched exception: " + ex.Message);
            }

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            segBtn_Click(null, null);
            posBtn_Click(null, null);
            pos2CrfBtn_Click(null, null);
        }
    }
}
