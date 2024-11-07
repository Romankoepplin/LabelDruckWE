using System;
using System.Windows.Forms;

namespace LabelDruck
{
    public partial class LadeForm : Form
    {
        // Initialisierung 
        public LadeForm()
        {
            LadeForm.ActiveForm.Text = "L A D E N - Programmstart";
            InitializeComponent();
            pgrbLoad.Minimum = 0;
            pgrbLoad.Show();
            lbLoad.Text = "Programmstart, bitte warten";
        }

        // Manuelle Initialisierung der Spezifikationen
        public void initialisiere(int x) 
        {
            switch (x)
            {
                case (1):
                    lbLoad.Text = "Datenbankdaten werden abgerufen \n";
                    lbLoad.Text += "Bitte warten ";
                    pgrbLoad.Maximum = 100;
                    pgrbLoad.Step = 20;
                    break;

                case (2):
                    lbLoad.Text = "Datenbankdaten werden abgerufen \n";
                    lbLoad.Text += "             Bitte warten ";
                    pgrbLoad.ResetText();
                    pgrbLoad.Refresh();
                    pgrbLoad.Maximum = 100;
                    pgrbLoad.Step = 25;
                    break;
            }
        }

        // Ladebalken füllen
        public void ladeVorgang()
        {
            pgrbLoad.PerformStep();
            lbLoad.Text += ".";
        }

        private void LadeForm_Load(object sender, EventArgs e)
        {
            
        } 
    }
}
