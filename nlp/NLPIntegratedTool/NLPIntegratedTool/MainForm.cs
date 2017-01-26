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
    }
}
