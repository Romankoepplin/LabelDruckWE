using System;
using System.Drawing;
using System.Windows.Forms;

namespace LabelDruck
{
    public partial class GfsfNF : Form
    {
        public GfsfNF()
        {
            InitializeComponent();
        }

        private void GfsfNF_Load(object sender, EventArgs e)
        {
            labelX1.Visible = true;
        }

        private void painting(object sender, PaintEventArgs e)
        {
            
        }

        private void rePaint()
        {
            for (int i = 0; i < 20; i++)
            {
                if (i % 2 == 0)
                {
                    labelX1.ForeColor = Color.Green;
                    GfsfNF.ActiveForm.Refresh();
                }
                else
                {
                    labelX1.ForeColor = Color.Red;
                    GfsfNF.ActiveForm.Refresh();
                }
            }
        }

        private void formClose(object sender, EventArgs e)
        {
            GfsfNF.ActiveForm.Hide();
        }

    }
}
