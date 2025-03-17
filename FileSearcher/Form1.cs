using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSearcher
{
    public partial class Form1: Form
    {

        private Searcher searcher;
        public Form1()
        {
            InitializeComponent();
            this.searcher = new Searcher(null, null);
            this.searcher.OnFileFound += FileFound;
            bgWorker.DoWork += WorkinBackground;
            bgWorker.RunWorkerCompleted += WokrCompleted;
        }

        private void FileFound(string path)
        {

            lbFileFound.BeginInvoke((Action)delegate ()
            {
                lbFileFound.Items.Add(path);
            });
        }

        private void WokrCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            MessageBox.Show("Completed");
        }

        private void WorkinBackground(object sender, DoWorkEventArgs args)
        {
            searcher.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            this.searcher.Term = tbTerm.Text;
            this.searcher.Dir = tbSearchDir.Text;
            bgWorker.RunWorkerAsync();

        }
    }
}
