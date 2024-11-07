using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LabelDruck
{
    public partial class BestandsListeKleber : Form 
    {
        protected DataSet ds;
        TlNrDB modWE;
        DataTable dt = new DataTable();
        DataTable dtF = new DataTable();
        DataView dv = new DataView();
        protected bool filterInit;
        ToolTip filterTip = new ToolTip();
        
        
        // Initialisierung
        public BestandsListeKleber()
        {
            InitializeComponent();                                                                  // Init
            modWE = new TlNrDB();                                                                   // bekanntmachen der TlNrDB
            dt.Merge(modWE.myDataTableMS(modWE.kleber()));                                          // Abrufen der Teilenummern der Kleber und einfügen in die DataTable
            dv = dt.AsDataView();                                                                   // DataTable in DataView umwandeln
            dtGvBestKl.DataSource = dv;                                                             // DataView an GridView übergeben und als Datenquelle eintragen
            filterTip.AutoPopDelay = 5000;                                                          // ToolTip Popupläge setzen
            filterTip.InitialDelay = 100;                                                           // ToolTip delay bevor Popup auftaucht setzen
            filterTip.SetToolTip(btnFilterAus, "Bei Klicken wird der Filter zurückgesetzt");        // ToolTip an Objekt (Button) befestigen
            dataViewInit();                                                                         // Die eigene Methode dataViewInit aufrufen
            rBtnTeilNr.Checked = true;                                                              // Radiobutton rBtnTeilNr auf checked(geklickt) setzen
            filterSource();                                                                         // Die eigene Methode filterSource aufrufen
            dtGvBestKl.Select();                                                                    // Die GridView auswählen,
            dtGvBestKl.Focus();                                                                     // und sie Fokussieren um versehntliches Mausradscrolling in der
                                                                                                    // filterTextBox zu verhindern
            btnAchtung.Visible = false;
            

        }

        // Dataview einrichten und Columnheadernamen festlegen
        private void dataViewInit()
        {
            dtGvBestKl.Columns["Kleber_ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtGvBestKl.Columns["Kleber_ID"].CellTemplate.Style.BackColor = Color.LightBlue;
            dtGvBestKl.Columns["Menge"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  
            dtGvBestKl.Columns["Eingangsdatum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtGvBestKl.Columns["Verfallsdatum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtGvBestKl.Columns["Verfallsdatum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dtGvBestKl.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtGvBestKl.DefaultCellStyle.NullValue = "no entry";
            dtGvBestKl.Columns["Number"].HeaderText = "Teilnummer :";
            dtGvBestKl.Columns["Kleber_ID"].HeaderText = "Kleber ID :";
            dtGvBestKl.Columns["Description"].HeaderText = "Bezeichnung :";
            dtGvBestKl.Columns["Menge"].HeaderText = "Menge :";
            dtGvBestKl.Columns["Eingangsdatum"].HeaderText = "Eingangsdatum :";
            dtGvBestKl.Columns["Verfallsdatum"].HeaderText = "Verfallsdatum :";
            
        }

        // Ändert die Hintergrundfarbe der Reihen je nach dem ob sie Kleber enthalten die Gültig, fast Abgelaufen oder 
        // Abgelaufen sind. Die Methode wird beim Zeichnen (Paint) der DataGridView aufgerufen
        private void colourChange(object sender, PaintEventArgs e)
        {
            for (int i = 0; dtGvBestKl.Rows.Count > i; i++)
            {
                int dateScan = DateTime.Compare(Convert.ToDateTime(dt.Rows[i]["Verfallsdatum"]), DateTime.Now.Date);
                int yellowRed = 0;

                for (int j = 0; j < dtGvBestKl.Columns.Count; j++)
                {
                    if (dateScan == 1 && j != 1)
                    {
                        dateScan = DateTime.Compare(Convert.ToDateTime(dt.Rows[i]["Verfallsdatum"]), DateTime.Now.Date.AddMonths(+1));

                        if (dateScan == 1 && j != 1)
                        {
                            dtGvBestKl[j, i].Style.BackColor = Color.LimeGreen;
                        }
                        if (dateScan == -1 && j != 1)
                        {
                            dtGvBestKl[j, i].Style.BackColor = Color.Yellow;
                            yellowRed = 1;
                        }
                    }
                    if (dateScan == -1 && j != 1 && yellowRed == 1)
                    {
                        dtGvBestKl[j, i].Style.BackColor = Color.Yellow; 
                    }

                    if (dateScan == -1 && j != 1 && yellowRed == 0)
                    {
                            dtGvBestKl[j, i].Style.BackColor = Color.Red;
                    }
                }
            }
            prüfAblaufDat();
        }

        // Die Methode filterSource legt fest welche Daten in der Filter CheckBox stehen, dass ist abhängig
        // davon welcher RadioButton gedrückt ist
        private void filterSource()
        {
            filterInit = true;
            if (rBtnTeilNr.Checked == true)
            {
                dtF = modWE.myDataTableMS(modWE.filterTLNO());
                cbBestKl.DataSource = dtF;
                cbBestKl.DisplayMember = "Number";
                cbBestKl.ValueMember = "Number";
                cbBestKl.SelectedIndex = -1;
            }
            if (rbtnTeilBez.Checked == true)
            {
                dtF = modWE.myDataTableMS(modWE.filterBez());
                int counter = dtF.Rows.Count;
                for (int i = 0; counter > i; i++)
                {
                    string cutof = dtF.Rows[i]["Description"].ToString();
                    for (int j = 0; j < 10; j++)
                    {
                        int index = cutof.IndexOf("" + j);
                        
                        if (dtF.Rows[i]["Description"].ToString().Contains("Araldit") == true)
                        {
                            index = 7;
                        }
                        
                        if (dtF.Rows[i]["Description"].ToString().Contains("Loctite") == true)
                        {
                            index = 7;
                        }
                        try
                        {
                            cutof = cutof.Substring(0, index);
                        }
                        catch { }
                    }
                    dtF.Rows[i]["Description"] = cutof;
                    if (i + 1 < counter)
                    {
                        if (dtF.Rows[i+1]["Description"].ToString().Contains(cutof) == true)
                        {
                            dtF.Rows[i].Delete();
                        }
                    }
                }
                cbBestKl.DataSource = dtF;
                cbBestKl.DisplayMember = "Description";
                cbBestKl.ValueMember = "Description";
                cbBestKl.SelectedIndex = -1;
            }
            filterInit = false;
        }

        // Methode die die Filterung der DataViewGrid übernimmt und überprüft nach welcher Methode gefilter wird 
        private void filterNach(object sender, EventArgs e)
        {
            if (sender == rbtnTeilBez || sender == rBtnTeilNr)
            { filterSource(); }

            if (filterInit == false)
            {

                int newIndex;
                for (int i = 0; dtGvBestKl.Rows.Count > i; i++)
                {
                    if ( rBtnTeilNr.Checked == true)
                    {
                        try
                        {
                            if (dtGvBestKl[0, i].Value.ToString() == cbBestKl.SelectedValue.ToString())
                            {
                                newIndex = dtGvBestKl.Rows[i].Index;
                                dtGvBestKl.Rows[newIndex].Visible = true;
                                (dtGvBestKl.BindingContext[dtGvBestKl.DataSource] as CurrencyManager).Position = dtGvBestKl.Rows[newIndex].Index;
                                for (int j = 0; dtGvBestKl.Rows.Count > j; j++)
                                {
                                    if (dtGvBestKl[0, j].Value.ToString() == cbBestKl.SelectedValue.ToString())
                                    { dtGvBestKl.Rows[j].Visible = true; }
                                    else { dtGvBestKl.Rows[j].Visible = false; }
                                }
                            }
                        }

                        catch
                        {
                            if (i == 19)
                            { }  
                        }
                    }
                    if (rbtnTeilBez.Checked == true)
                    {
                        try
                        {
                            string bez = cbBestKl.SelectedValue.ToString();
                            if ( dtGvBestKl["Description", i].Value.ToString().StartsWith(bez) == true)
                            {
                                newIndex = dtGvBestKl.Rows[i].Index;
                                dtGvBestKl.Rows[newIndex].Visible = true;
                                (dtGvBestKl.BindingContext[dtGvBestKl.DataSource] as CurrencyManager).Position = dtGvBestKl.Rows[newIndex].Index;
                                for (int j = 0; dtGvBestKl.Rows.Count > j; j++)
                                {
                                    if (dtGvBestKl[2, j].Value.ToString().Contains(bez) == true)
                                    { dtGvBestKl.Rows[j].Visible = true; }
                                    else { dtGvBestKl.Rows[j].Visible = false; }
                                }
                            }
                        }
                        catch
                        {
                            if (i == 19)
                            { }
                        }

                    }
                }
                dtGvBestKl.Refresh();
            }
        }

        // Wird aufgerufen wenn der Benutzer eine Taste drückt und sich im Textfeld der CheckBox befindet.
        // Wird allerdings nur ausgeführt wenn kein Text enthalten ist (alternative zum filterAus)
        private void keyPressEvent(object sender, KeyPressEventArgs e)
        {
            if (cbBestKl.Text == "")
            {
                for (int i = 0; dtGvBestKl.Rows.Count > i; i++)
                {
                    dtGvBestKl.Rows[i].Visible = true;
                    (dtGvBestKl.BindingContext[dtGvBestKl.DataSource] as CurrencyManager).Position = dtGvBestKl.Rows[0].Index;
                    dtGvBestKl.Refresh();
                }
            }
        }

        // Wird aufgerufen wenn der Button Filter Aus agedrückt wird 
        // Dann wird wieder die gesamte Tabelle angezeigt
        private void filterAus(object sender, EventArgs e)
        {
            for (int i = 0; dtGvBestKl.Rows.Count > i; i++)
            {
                dtGvBestKl.Rows[i].Visible = true;
                (dtGvBestKl.BindingContext[dtGvBestKl.DataSource] as CurrencyManager).Position = dtGvBestKl.Rows[0].Index;
                dtGvBestKl.Refresh();
            }
        }

        // Diese Methode prüft ob ein Kleber bereits länger abgelaufen ist
        // Wenn der Kleber länger als einen Monat abgelaufen ist wird der Achtung Button aktiviert
        public void prüfAblaufDat()
        {
            int dateScan;
            for (int i = 0; dtGvBestKl.Rows.Count > i; i++)
            {
                dateScan = DateTime.Compare(Convert.ToDateTime(dt.Rows[i]["Verfallsdatum"]), DateTime.Now.Date.AddMonths(-1));
                if (dateScan == -1)
                {
                    btnAchtung.Visible = true;
                    
                }
            }
            ToolTip totiAchtung = new ToolTip();
            totiAchtung.SetToolTip(btnAchtung, "KLEBER ÄLTER ALS 1 MONAT!");
        }

        // 
        private void alteKleber(object sender, EventArgs e)
        {
            MessageBox.Show("Es gibt Kleber die seit mehr als einem Monat Verfallen sind.\nSie sollten sie so schnell wie möglich ersetzen!!!");
            
        }

        // Methode die beim drücken auf den Print Button die DataGridView in ein Exceldokument speichert
        // Dann kann die Tabelle ausgedruckt werden
        private void printToExcel(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                dlg.DefaultExt = "xls";
                dlg.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        BestKltoExcel exp = new BestKltoExcel(dtGvBestKl);
                        exp.ExportDataGridView(dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BestandsListeKleber_Load(object sender, EventArgs e)
        {

        }   
    }
}
