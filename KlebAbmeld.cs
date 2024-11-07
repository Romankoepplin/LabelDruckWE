using System;
using System.Data;
using System.Windows.Forms;

namespace LabelDruck
{

    // Klasse Kleber Abmelden
    // Bekanntmachen des Haupfensters und der Klasse Tooltip
    public partial class KlebAbmeld : Form
    {
        TlNrDB modWE = new TlNrDB();
        ToolTip tip2 = new ToolTip();

        // Initialisierung der Komponenten und Tooltips
        public KlebAbmeld()
        {
            InitializeComponent();
            tip2.InitialDelay = 1000;
            tip2.AutoPopDelay = 5000;
            tip2.SetToolTip(tbKlebAbm, "Bitte die KLEBER ID eingeben");
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        // Methode die den KLebstoff abmeldet und die datenbank updatet
        private void kleberAbmelden(object sender, EventArgs e)
        {
            if (tbKlebAbm.Text == "")                                                           // Überprüfung ob die KleberID Textbox leer ist
            {                                                                                   // Wenn ja dann wird eine Fehlermeldung ausgegeben
                MessageBox.Show("Fehlerhafte Eingabe\nBitte Nummer eingeben", "ERROR 05", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Überprüfung ob die Kleber ID überhaupt vorhanden ist, wenn nicht gibt es eine Fehlermeldung
                string kleber = tbKlebAbm.Text;
                DataTable kleberDB = modWE.myDataTableMS(modWE.kleberAbm());
                for (int i = 0; i < kleberDB.Rows.Count; i++)
                {
                    if (kleber == kleberDB.Rows[i]["Kleber_ID"].ToString())
                    {
                        modWE.myVoidMS(modWE.kleberAbmelden(kleber));
                        MessageBox.Show("Der Kleber wurde erfolgreich abgemeldet");
                        break;
                    }
                    else
                    {
                        if (kleberDB.Rows.Count-1 <= i)
                        {
                            MessageBox.Show("Diese Kleber ID ist nicht vorhanden\nBitte existierende Kleber ID eingeben", "ERROR 06", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }
    }
}
