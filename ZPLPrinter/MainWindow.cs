using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace ZPLPrinter
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
           
        }

        private void printByFileName(string filename)
        {
            try
            {
                // Allow the user to select a printer.
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                pd.UseEXDialog = true;
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    // Send a printer-specific to the printer.
                    RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, filename);
                    MessageBox.Show("Data sent to printer.");
                }
                else
                {
                    MessageBox.Show("Data not sent to printer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2 && args[1].Length > 0)
            {
                printByFileName(args[1]);
            }
            else
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "ZPL Files (*.zpl)|*.zpl|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Multiselect = false;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    printByFileName(openFileDialog1.FileName);
                }
            }
            this.Close();
        }
    }
}
