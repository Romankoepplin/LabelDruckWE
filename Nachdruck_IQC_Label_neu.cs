using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.DirectoryServices;

// Programm Labeldruck
// Software für den Nachdruck der Labels für den Wareneingang, IQC, gelbe Etikette und Rückstellmuster

namespace LabelDruck
{
    // Bekanntmachung der anderen Klassen
    // initialisierung der globalen Variablen

    public partial class Form1 : Form
    {
        KlebAbmeld fKlebAb = new KlebAbmeld();
        LadeForm loadScr = new LadeForm();
        TlNrDB modWE;
        BestandsListeKleber bestKleb;
        DruckPrüfung druckPrüf = new DruckPrüfung();
        Drucker druckJob = new Drucker();
        DataTable dt;
        DataTable dtRev;

        bool init;
        bool paint;
        //delegate void StringParameterDelegate(DataTable dtXY);
        //int valThread = 0;
        GfsfNF gefahr = new GfsfNF();

        // Initialisierung der Hauptform
        // Hier werden die Objekte erzeugt und für den Paintvorgang vorbereitet
        // außerdem werden hier schon einige Datenbankdaten abgerufen

        public Form1()
        {


            InitializeComponent();

            this.Cursor = Cursors.WaitCursor;
            this.Refresh();
            init = true;
            paint = false;

            modWE = new TlNrDB();


            this.Text = "Stock - Labelprint Ver.: " + Application.ProductVersion.ToString();

            if (!IsInGroup("SK-all"))
            {
                loadScr.Show();
                loadScr.Refresh();
                
                loadScr.ladeVorgang();                      //20
               
                dt = new DataTable();
                dtRev = new DataTable();
                loadScr.initialisiere(1);
                dt = modWE.myDataTableMS(modWE.SQLrueckTn());
                loadScr.ladeVorgang();                      //40
                loadScr.Refresh();
                cbWETlnr.DataSource = dt;
                cbWETlnr.DisplayMember = "Number";
                cbWETlnr.ValueMember = "Number";
                loadScr.ladeVorgang();                      //60
                loadScr.Refresh();
            }
            this.Cursor = Cursors.Default;
            this.Refresh();


        }

        // Ladevorgang der Hauptform
        // Hier werden die restlichen Datenbankdaten der hauptcomboboxen geladen

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            cbRueckTeilNr.DataSource = modWE.myDataTableMS(modWE.SQLrueckTn());
            cbRueckTeilNr.DisplayMember = "Number";
            cbRueckTeilNr.ValueMember = "Number";
            cbRueckTeilNr.SelectedIndex = -1;

            if (!IsInGroup("SK-all"))
            {
                loadScr.ladeVorgang();                                  //80
                loadScr.Refresh();
                
                cbRueckUser.DataSource = modWE.myDataTableMS(modWE.SQLrueckUser());
                cbRueckUser.DisplayMember = "Name";
                cbRueckUser.ValueMember = "Name";
                cbRueckUser.SelectedIndex = -1;
                cbWETlnr.SelectedIndex = -1;
                loadScr.ladeVorgang();                                  //100
                loadScr.Refresh();
                loadScr.Hide();
            }
            init = false;
            
            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        public bool IsInGroup(string strGroup)
        {
            DirectorySearcher search = new DirectorySearcher();
            search.Filter = "(sAMAccountName=" + System.Environment.UserName + ")";
            //search.Filter = "(sAMAccountName=BS6127)";
            search.PropertiesToLoad.Add("memberOf");
            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (equalsIndex != -1)
                        if (dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1) == strGroup)
                            return true;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        private void layoutPnWE_Paint(object sender, PaintEventArgs e)
        {

        }

        private void layoutRueck_paint(object sender, PaintEventArgs e)
        {
            //if (init == false)
            //{
            //aktiviereThread();
            paint = true;
            //}
        }

        // Wenn Tab gewechselt wird soll kurzzeitig der Waitcursor angezeigt werden
        private void selTabChange(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Cursor = Cursors.Default;
        }

        private void btnSchliess_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Öffnen und laden der Kleber Abmelden Form
        private void btnKlebAbmeld_Click(object sender, EventArgs e)
        {

            fKlebAb.Show();
        }

        // Verstecken der Zusatzfelder für Gefahrenstoffe/Kleber
        private void weHide()
        {

            lbWEGebindeGr.Hide();
            cbWEGebGr.Hide();
            lbWEEingangsDat.Hide();
            dtEingang.Hide();
            lbWEVerfDat.Hide();
            dtVerfall.Hide();
            lbWEAnzGeb.Text = "Anzahl Etiketten :";
        }

        // Anzeigen der Zusatzfelder für Gefahrenstoffe/Kleber
        private void weShow()
        {

            lbWEGebindeGr.Show();
            cbWEGebGr.Show();
            lbWEEingangsDat.Show();
            dtEingang.Show();
            lbWEVerfDat.Show();
            dtVerfall.Show();
            lbWEAnzGeb.Text = "Anzahl Gebinde :";

        }

        // Diese Methode sorgt dafür das der DruckenButton des jeweiligen Tabs erst
        // aktiviert wird wenn die benötigten Felder ausgefüllt sind

        private void btPrintActivate(object sender, EventArgs e)
        {

            String[] strArr = new String[5];
            int[] intArr = new int[5];
            int x;

            // Abfragen welcher Tab gerade selektiert ist
            switch (tabControl.SelectedTab.Name)
            {
                case "tabWareneingang":
                    if (cbWETlnr.SelectedIndex > -1 && cbWERev.SelectedIndex > -1 && tbWEAnz.Text.Length > 0)
                    {
                        btnWEPrint.Enabled = true;
                    }
                    else
                    {
                        btnWEPrint.Enabled = false;
                    }
                    break;

                case "tabNachdrIQC":

                    if (checkIQCID(tbIQCqcID.Text))
                    {
                        string DRW = modWE.myValueMS(modWE.msGet_DRW(tbIQCqcID.Text));
                        cbIQCLot.DataSource = modWE.myDataTableDB2(modWE.SQLLotNR(DRW));
                        cbIQCLot.DisplayMember = "Number";
                        cbIQCLot.ValueMember = "Number";

                        string haltbarkeit = modWE.myValueMS(modWE.getHaltbarkeit(DRW));
                        int Mon = 0;
                        int.TryParse(haltbarkeit, out Mon);
                        if (Mon > 0)
                        {
                            dtExpiryDate.Value = DateTime.Today.AddMonths(Mon);
                            dtExpiryDate.Visible = true;
                            lbExpiryDate.Visible = true;
                        }
                        else
                        {
                            dtExpiryDate.Visible = false;
                            lbExpiryDate.Visible = false;
                        }

                        btnIQCPrint.Enabled = true;
                        btnRedLabel.Enabled = true;
                    }
                    else
                    {
                        cbIQCLot.DataSource = null;
                        btnIQCPrint.Enabled = false;
                    }
                    break;

                case "tabGelbEt":
                    if (tbGelbEtAnz.Text == "")
                    {
                        btnGelbEtPrint.Enabled = false;
                    }
                    else { btnGelbEtPrint.Enabled = true; }
                    break;

                case "tabRueckstell":
                    if (paint == true)
                    {
                        x = 1;
                        intArr[0] = cbRueckUser.SelectedIndex;
                        intArr[1] = cbRueckTeilNr.SelectedIndex;
                        if (intArr[0] > -1 && intArr[1] > -1) { btnRueckPrint.Enabled = true; } else { btnRueckPrint.Enabled = false; }
                    }
                    break;
            }
        }

        private bool checkIQCID(string IQC_ID)
        {
            bool exists = false;
            if (IQC_ID.Length > 0)
            {
                int i = modWE.myIntValueMS(modWE.msIQC_IDexists(IQC_ID));
                if (i > 0)
                {
                    exists = true;
                }
            }
            return exists;
        }


        // Konvertiert die Eingaben in den Textboxen um zu überprüfen ob sie korrekt sind 
        private string converter(String[] strArr, int x)
        {
            String str = "";
            for (int i = 0; i <= x; i++)
            {
                if (Convert.ToInt32(strArr[i]) > 0) { str += 1; } else { str += 0; }
            }

            return str;
        }

        // Aktualisierung der Info Boxen und der LotNr Combobox, sowie auch der Revision Combobox
        // Hier wird zuerst die Revision abgerufen und gewählt und im Anschluss zur Teilnummer
        // und Revision die passenden Infodaten und Lot Nummern heraus gesucht.
        private void wETlInfo(object sender, EventArgs e)
        {
            if (cbWETlnr.SelectedIndex > -1)
            {
                if (init == false)
                {
                    init = true;
                    this.Cursor = Cursors.WaitCursor;
                    this.Refresh();

                    // Überprüfen ob es sich um einen Gefahrenstoff/ Kleber handelt
                    // für den andere Felder angezeigt werden müssen als für normale Teile
                    if (cbWETlnr.SelectedValue.ToString().StartsWith("9962") || cbWETlnr.SelectedValue.ToString().StartsWith("9961") || cbWETlnr.SelectedValue.ToString().StartsWith("9960") || cbWETlnr.SelectedValue.ToString().StartsWith("9963") || cbWETlnr.SelectedValue.ToString().StartsWith("9964"))
                    {
                        weShow();
                    }
                    else { weHide(); }

                    dtRev = modWE.myDataTableDB2(modWE.revisionen(cbWETlnr.SelectedValue.ToString()));
                    if (dtRev.Rows.Count > 0)
                    {
                        cbWERev.DataSource = dtRev;
                    }
                    else
                    {
                        cbWERev.DataSource = modWE.myDataTableMS(modWE.revisionenMS(cbWETlnr.SelectedValue.ToString()));
                    }
                    if (dtRev.Rows.Count >= 1) { cbWERev.SelectedIndex = dtRev.Rows.Count - 1; }
                    else { cbWERev.SelectedIndex = 0; }
                    cbWERev.DisplayMember = "Revision";
                    cbWERev.ValueMember = "Revision";

                    // Abfragen der LOT-Nr
                    cbWELotNr.DataSource = modWE.myDataTableDB2(modWE.SQLLotNR(cbWETlnr.SelectedValue.ToString() + cbWERev.SelectedValue.ToString()));
                    cbWELotNr.DisplayMember = "Number";
                    cbWELotNr.ValueMember = "Number";

                    // Absicherung für den Fall das die Descriptions für die Teilnummer nicht in der
                    // AS400 DB2 enthalten sind. Ist dies der Fall werden aus der MSDB die benötigten
                    // Daten für die tb 1-3 ausgelesen.
                    string val = modWE.myValueDB2(modWE.SQLInfo(cbWETlnr.SelectedValue.ToString() + cbWERev.SelectedValue.ToString()));
                    if (val != null) { tbWE1.Text = val; }
                    else { tbWE1.Text = modWE.myValueMS(modWE.SQLMSInfo(cbWETlnr.SelectedValue.ToString() + cbWERev.SelectedValue.ToString())); }
                    val = modWE.myValueDB2(modWE.SQLInfo2(cbWETlnr.SelectedValue.ToString() + cbWERev.SelectedValue.ToString()));
                    if (val != null) { tbWE2.Text = val; }
                    else { tbWE2.Text = modWE.myValueMS(modWE.SQLMSInfo2(cbWETlnr.SelectedValue.ToString() + cbWERev.SelectedValue.ToString())); }
                    val = modWE.myValueDB2(modWE.SQLInfo3(cbWETlnr.SelectedValue.ToString() + cbWERev.SelectedValue.ToString()));
                    if (val != null) { tbWE3.Text = val; }
                    else { tbWE3.Text = modWE.myValueMS(modWE.SQLMSInfo3(cbWETlnr.SelectedValue.ToString() + cbWERev.SelectedValue.ToString())); }

                    // Für die Klebstoffe mit dem Anfang 9962 werden hier die zusätzlichen Informationen wie
                    // Gebindegröße, Eingangsdatum und Ablaufdatum abgerufen und in die entsprechenden
                    // Zusatzfelder eingetragen.
                    if (cbWETlnr.SelectedValue.ToString().Contains("9962001") == true
                        || cbWETlnr.SelectedValue.ToString().Contains("9962002") == true
                        || cbWETlnr.SelectedValue.ToString().Contains("9962003") == true
                        || cbWETlnr.SelectedValue.ToString().Contains("9963100") == true
                        || cbWETlnr.SelectedValue.ToString().Contains("9964001") == true)
                    {
                        // Prüfung ob Gefahrenstoff/Kleber freigegeben ist
                        string valFreig = modWE.myValueMS(modWE.gefahrstoffFreigabe(cbWETlnr.Text));
                        if (Convert.ToInt32(valFreig) != 1)
                        {
                            // ALARM
                            // E-Mail versand
                            String message = "Achtung, " + System.Environment.NewLine;
                            message += "am Wareneingang wurde ein Gefahrenstoff angeliefert der nicht freigegeben ist!" + System.Environment.NewLine;
                            message += "Setzten sie sich bitte mit den Kollegen am Wareneingang in Verbindung.";
                            message += "" + System.Environment.NewLine + System.Environment.NewLine;
                            message += "Es handelt sich um den Stoff: ";
                            message += "" + System.Environment.NewLine + "" + tbWE1.Text;
                            message += "" + System.Environment.NewLine + "" + tbWE2.Text;
                            message += "" + System.Environment.NewLine + "" + tbWE3.Text;
                            EMail email = new EMail();
                            email.Sendmail(message, "Label-Druck: Gefahrenstoff  ", "Label-Druck-WE@pmdm.de", global::LabelDruck.Properties.Settings.Default.EmailGefahr.ToString(), "", "", false);

                            // Gefahrfenster
                            gefahr.Show();
                            btnWEPrint.Enabled = false;

                            
                        }

                        try
                        {
                            // Hier wird das Datum noch abgefragt und zugeschnitten, falls eine Uhrzeit dahinter hängt
                            DateTime datum = DateTime.Today;
                            DateTime.TryParse(modWE.myValueMS(modWE.weKleberEinDat(cbWETlnr.SelectedValue.ToString())), out datum);
                            dtEingang.Value = datum;

                            DateTime datum2 = DateTime.Today;
                            DateTime.TryParse(modWE.myValueMS(modWE.weKleberAblDat(cbWETlnr.SelectedValue.ToString())), out datum2);
                            dtVerfall.Value = datum2;

                            if (dtEingang.Value == DateTime.MinValue)
                            {
                                dtEingang.Value = DateTime.Today;
                            }

                            cbWEGebGr.DataSource = modWE.myDataTableMS(modWE.weKleberGeb(cbWETlnr.SelectedValue.ToString()));
                            cbWEGebGr.DisplayMember = "Menge";
                            cbWEGebGr.ValueMember = "Menge";
                        }
                        catch
                        {
                            //dtEingang.Text = "ABGELAUFEN!";
                        }
                    }

                    // Falls kein Text enthalten ist wird das Feld mit einem - Markiert und gilt als leer
                    if (tbWE1.Text == "") { tbWE1.Text = " - "; }
                    if (tbWE2.Text == "") { tbWE2.Text = " - "; }
                    if (tbWE3.Text == "") { tbWE3.Text = " - "; }

                    btPrintActivate(sender, e);
                    this.Cursor = Cursors.Default;
                    this.Refresh();
                    init = false;
                }
            }
        }

        // Methode für das automatische Ausfüllen der Informationsbox im Rückstellmuster Bereich

        private void RueckUpdate(object sender, EventArgs e)
        {
            if (init == false)
            {
                if (cbRueckTeilNr.SelectedIndex > -1)
                {
                    lbRueckTeilName.Text = "";

                    if (paint == true)
                    {
                        init = true;

                        string teilNO = cbRueckTeilNr.SelectedValue.ToString();
                        int dbVal = rueckRevDetect(teilNO);
                        // Wenn die Daten nicht in der AS400 enthalten sind wird auf die MS DB umgestiegen
                        // dann werden die Daten von dort abgerufen
                        switch (dbVal)
                        {
                            case (0):

                                lbRueckTeilName.Text = modWE.myValueDB2(modWE.SQLInfo(teilNO + cbRueckRev.SelectedValue.ToString())) + "\n";
                                lbRueckTeilName.Text += modWE.myValueDB2(modWE.SQLInfo2(teilNO + cbRueckRev.SelectedValue.ToString())) + "\n";
                                lbRueckTeilName.Text += modWE.myValueDB2(modWE.SQLInfo3(teilNO + cbRueckRev.SelectedValue.ToString()));
                                break;

                            case (1):

                                lbRueckTeilName.Text = modWE.myValueMS(modWE.SQLMSInfo(cbRueckTeilNr.SelectedValue.ToString() + cbRueckRev.SelectedValue.ToString())) + "\n";
                                lbRueckTeilName.Text += modWE.myValueMS(modWE.SQLMSInfo2(cbRueckTeilNr.SelectedValue.ToString() + cbRueckRev.SelectedValue.ToString())) + "\n";
                                lbRueckTeilName.Text += modWE.myValueMS(modWE.SQLMSInfo3(cbRueckTeilNr.SelectedValue.ToString() + cbRueckRev.SelectedValue.ToString()));
                                break;
                        }
                        btPrintActivate(sender, e);
                        init = false;
                    }

                }
            }
        }

        // Methode die erkennt welche Revisionen das Teil hat

        private int rueckRevDetect(string teilNO)
        {
            int dbVal;
            dtRev = modWE.myDataTableDB2(modWE.revisionen(teilNO));

            if (dtRev.Rows.Count > 0)
            {
                dbVal = 0;
            }
            else
            {
                dtRev = modWE.myDataTableMS(modWE.revisionenMS(teilNO));
                dbVal = 1;
            }
            cbRueckRev.DataSource = dtRev;
            if (dtRev.Rows.Count >= 1) { cbRueckRev.SelectedIndex = dtRev.Rows.Count - 1; }
            else { cbRueckRev.SelectedIndex = 0; }
            cbRueckRev.DisplayMember = "Revision";
            cbRueckRev.ValueMember = "Revision";
            return dbVal;
        }

        private Hashtable getGefahrStoffInfo(string teilNo)
        {
            string drw_id = "";
            Hashtable erg = new Hashtable();
            string sql = "select DRW_ID from Drawings where Number like '" + teilNo + "%'";
            DataTable dummy = new DataTable();
            dummy = modWE.myDataTableMS(sql);
            if (dummy.Rows.Count > 0)
            {
                drw_id = System.Convert.ToString(dummy.Rows[0]["DRW_ID"].ToString());
                erg.Add("DRW_ID", dummy.Rows[0]["DRW_ID"].ToString());
            }
            
            if (drw_id!="")
            {
                sql = "SELECT Gefstv_ID,Schutzstufe_GHS,Abf_SCHL_NR,Abf_SCHL_NR_VP FROM [Umwelt].[dbo].[Gefahrstoffe] where DRW_ID = '" + drw_id + "'";
                dummy = new DataTable();dummy = modWE.myDataTableMS(sql);
                if (dummy.Rows.Count > 0)
                {
                    erg.Add("Gefstv_ID", dummy.Rows[0]["Gefstv_ID"].ToString());
                    erg.Add("Schutzstufe_GHS", dummy.Rows[0]["Schutzstufe_GHS"].ToString());
                    erg.Add("Abf_SCHL_NR_VP", dummy.Rows[0]["Abf_SCHL_NR_VP"].ToString());
                    erg.Add("Abf_SCHL_NR", dummy.Rows[0]["Abf_SCHL_NR"].ToString());
                }   
            }
            if (erg["Abf_SCHL_NR"] != null)
            {
                sql = "SELECT Entsorgungs_ID FROM [Umwelt].[dbo].[Abfallschlüssel] where Abfall_ID = '" + erg["Abf_SCHL_NR"] + "'";
                dummy = new DataTable(); dummy = modWE.myDataTableMS(sql);
                if (dummy.Rows.Count > 0)
                {
                    erg.Add("Entsorgungs_ID", dummy.Rows[0]["Entsorgungs_ID"].ToString());
                }   
            }
            if (erg["Abf_SCHL_NR_VP"] != null)
            {
                sql = "SELECT Entsorgungs_ID FROM [Umwelt].[dbo].[Abfallschlüssel] where Abfall_ID = '" + erg["Abf_SCHL_NR_VP"] + "'";
                dummy = new DataTable(); dummy = modWE.myDataTableMS(sql);
                if (dummy.Rows.Count > 0)
                {
                    erg.Add("Entsorgungs_ID_VP", dummy.Rows[0]["Entsorgungs_ID"].ToString());
                }
            }


            if (erg["Entsorgungs_ID"] != null)
            {
                sql = "SELECT EntsorgungsTXTKurz_D,\"Plan\" FROM [Umwelt].[dbo].[Entsorgungsmittel] where Entsorg_ID = '" + erg["Entsorgungs_ID"] + "'";
                dummy = new DataTable(); dummy = modWE.myDataTableMS(sql);
                if (dummy.Rows.Count > 0)
                {
                    erg.Add("EntsorgungsTXTKurz_D", dummy.Rows[0]["EntsorgungsTXTKurz_D"].ToString());
                    erg.Add("Plan", dummy.Rows[0]["Plan"].ToString());
                }
            }

            if (erg["Entsorgungs_ID_VP"] != null)
            {
                sql = "SELECT EntsorgungsTXTKurz_D,\"Plan\" FROM [Umwelt].[dbo].[Entsorgungsmittel] where Entsorg_ID = '" + erg["Entsorgungs_ID_VP"] + "'";
                dummy = new DataTable(); dummy = modWE.myDataTableMS(sql);
                if (dummy.Rows.Count > 0)
                {
                    erg.Add("EntsorgungsTXTKurz_D_VP", dummy.Rows[0]["EntsorgungsTXTKurz_D"].ToString());
                    erg.Add("PlanVP", dummy.Rows[0]["Plan"].ToString());
                }
            }
            
            sql = "select Storage_Temperature from Drawings where Number like '" + teilNo + "%'";
            dummy = new DataTable();
            dummy = modWE.myDataTableMS(sql);
            if (dummy.Rows.Count > 0)
            {
                erg.Add("Storage_Temperature", dummy.Rows[0]["Storage_Temperature"].ToString());
            }

            return erg;
        }

        private long saveKleber(string teilNo,string gebindeGr,string Einheit,DateTime eingangsDatum,DateTime verfallsDatum)
        {
            long erg = -1;
            int lc_err = 0;
            string drw_id = "";
            string sql = "select DRW_ID from Drawings where Number like '" + teilNo + "%'";
            DataTable dummy = new DataTable();dummy = modWE.myDataTableMS(sql);
            if (dummy.Rows.Count > 0)
            {
                drw_id = System.Convert.ToString(dummy.Rows[0]["DRW_ID"].ToString());
            }
            else { lc_err = 1; }

            if (lc_err==0)
            {
                sql = "insert into [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] (DRW_ID,Gebindegr,Einheit,Eingangsdatum,Verfallsdatum,ImBestand)";
                sql += "values ('" + drw_id + "','" + gebindeGr + "','" + Einheit + "','" + eingangsDatum.ToString("yyyy/dd/MM") + "','" + verfallsDatum.ToString("yyyy/dd/MM") + "',1)";
                try
                {
                    modWE.myVoidMS(sql);
                }
                catch { lc_err = 2; }
            }

            if (lc_err == 0)
            {
                sql = "Select max(Kleber_ID) as maxi from  [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] where DRW_ID = '"+drw_id+"'";
                dummy = new DataTable(); dummy = modWE.myDataTableMS(sql);
                if (dummy.Rows.Count > 0)
                {
                    erg = System.Convert.ToInt64(dummy.Rows[0]["maxi"].ToString());
                }
            }
            if (lc_err != 0) { erg = lc_err * -1; }
            return erg;

        }

        // Druckauftrag einleiten und dann ausführen. Erst wird aber geprüft welcher Print Button
        // gedrückt wurde. Jeder Tabreiter hat seinen eigenen und anhand des Tabnamen wird festgemacht
        // welcher Button gedrückt wurde. Dadurch kann der individuelle Druckauftrag gestartet werden.
        private void btnPrintPressed(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int cbVal;
            string LotNr = "";
            string tmpWedate = "";

            switch (tabControl.SelectedTab.Name)
            {
                case "tabWareneingang":

                    // Überprüfung ob sich in den wichtigen Feldern auch vernünftige Werte befinden.
                    // Es dürfen beispielsweise keine Buchstaben in der Textbox für die Anzahl an Etiketten stehen. 
                    DialogResult result = new DialogResult();
                  
                    // Überprüfen ob das Feld der LOT-Nr leer ist,
                    // wenn ja dann wird geprüft ob es einen Wert haben müsste
                    // wenn dieser Wert dann nicht eingetragen ist wird der Druckvorgang abgebrochen
                    try
                    {
                        // Prüft ob die Checkbox mit der LOT-Nummer leer ist
                        if (cbWELotNr.SelectedValue.ToString() == null)
                        {
                            // Wenn die CheckBox Werte beinhaltet kommt eine Fehlermeldung
                            if (cbWELotNr.Items.Count > 0)
                            {
                                MessageBox.Show("Fehler beim bearbeiten\nSie müssen vor dem Drucken eine LOT-NR auswählen", "ERROR 09");
                            }
                            else { cbWELotNr.Text = "-"; }
                        }
                    }
                    catch
                    {

                        if (cbWELotNr.Items.Count > 0)
                        {
                            MessageBox.Show("Fehler beim bearbeiten\nSie müssen vor dem Drucken eine LOT-NR auswählen", "ERROR 09");
                        }
                        else { LotNr = "-----"; }
                    }

                    if (cbWELotNr.Items.Count > 0)
                    {
                        LotNr = cbWELotNr.SelectedValue.ToString();
                        tmpWedate = modWE.myValueDB2(modWE.SQLLotWEDate(LotNr));
                    }

                    // Sicherheitsabfrage um vor versehentlichem vieldrucken zu schützen
                    bool do_print = true;
                    if (Convert.ToInt32(tbWEAnz.Text) >= 20)
                    {
                        result = MessageBox.Show("Sie wollen " + tbWEAnz.Text + " Etiketten drucken\nSind Sie sicher?", "Sind Sie sicher?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result != DialogResult.Yes)
                        {
                            do_print = false;
                        }
                    }

                    if (cbWETlnr.SelectedValue.ToString().StartsWith("9962") && dtVerfall.Value == DateTime.MinValue)
                    {
                        try
                        {
                            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(global::LabelDruck.Properties.Settings.Default.constrMSsql);
                            con.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT [Haltbarkeit_Monate] FROM [_Teilestamm_PMDM].[dbo].[Drawings] WHERE [Number] LIKE '" + cbWETlnr.SelectedValue.ToString().Substring(0, 12) + "%' ", con);
                            //val = (int)cmd.ExecuteScalar();
                            int val = 0;

                            int.TryParse(cmd.ExecuteScalar().ToString(), out val);
                            con.Close();

                            //int iMonate = modWE.myIntValueMS();
                            if (val > 0)
                            {
                                MessageBox.Show("Bitte Verfallsdatum eintragen!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                        return;
                    }

                    if (do_print)
                    {
                        if (dtEingang.Visible == false && dtVerfall.Visible == false && cbWEGebGr.Visible == false)
                        {
                            druckJob.wareneinDruck(cbWETlnr.SelectedValue.ToString(), cbWERev.SelectedValue.ToString(), tbWE1.Text, tbWE2.Text, tbWE3.Text, LotNr, System.DateTime.Now.Date.ToString(), tbWEAnz.Text, tmpWedate);
                        }
                        else
                        {
                            DataTable dummy = new DataTable();
                            dummy = modWE.myDataTableMS(modWE.gefahrstoffInfo(cbWETlnr.Text));
                            Hashtable gefahrstoffinfo = this.getGefahrStoffInfo(cbWETlnr.Text);
                            string gebGroesse = "Stk";

                            //if (cbWEGebGr.SelectedIndex > -1)
                            //{
                            //    gebGroesse = cbWEGebGr.Items[cbWEGebGr.SelectedIndex].ToString();
                            //}
                            //else
                            //{
                                gebGroesse = cbWEGebGr.Text.ToString();
                            //}

                            if (cbWETlnr.SelectedValue.ToString().StartsWith("9962"))
                            {
                               //string einh = ""; 
                                double wert = 0;
                                string geb = gebGroesse; 
                                int inum = 0;

                                try
                                {
                                    while ((double.TryParse(geb.Substring(0, inum + 1), out wert)) && (inum < geb.Length))
                                    {
                                        inum++;
                                    }
                                }
                                catch 
                                { 
                                }

                                if (inum > 0)
                                {
                                    bool bo1 = double.TryParse(geb.Substring(0, inum), out wert);
                                    gebGroesse = geb.Substring(inum, geb.Length - inum).Trim();
                                }
                                else
                                {
                                    gebGroesse = geb; wert = -1;
                                }

                                long id = this.saveKleber(cbWETlnr.Text, wert.ToString(), gebGroesse, dtEingang.Value, dtVerfall.Value);
                                if (id >= 0) { gefahrstoffinfo.Add("KleberId", id.ToString()); }
                            }
                            druckJob.wareneinDruck(cbWETlnr.SelectedValue.ToString(), cbWERev.SelectedValue.ToString(), tbWE1.Text, tbWE2.Text, tbWE3.Text, LotNr, tbWEAnz.Text, gebGroesse, dtVerfall.Value.ToString("dd.MM.yyyy"),  tmpWedate,gefahrstoffinfo);
                        }
        
                    }
                    
                    Cursor.Current = Cursors.Default;
                    break;

                case "tabNachdrIQC":

                    string tmpLot = "";

                    if (cbIQCLot.SelectedIndex > -1)
                    {
                        tmpLot = cbIQCLot.SelectedValue.ToString();
                        tmpWedate = modWE.myValueDB2(modWE.SQLLotWEDate(cbIQCLot.SelectedValue.ToString()));
                    }

                    if (tbSAPLot.Visible == true && tbSAPLot.Text.Length > 0)
                    {
                        tmpLot = tbSAPLot.Text;
                    }


                    string printer = "";
                    if (cbPrinterSK.Visible == true)
                    {
                        printer = cbPrinterSK.SelectedValue.ToString();
                    }
                    druckJob.iqcDruck(tbIQCqcID.Text, tmpLot, tmpWedate, tbIQCanz.Text, dtExpiryDate.Value.ToString("dd.MM.yyyy"),printer);

                    if (cbMTCEZebra.Checked == true)
                    {
                        druckJob.iqcDruck(tbIQCqcID.Text, tmpLot, tmpWedate, tbIQCanz.Text, dtExpiryDate.Value.ToString("dd.MM.yyyy"), "Zebra");
                    }


                    Cursor.Current = Cursors.Default;
                    break;

                case "tabGelbEt":

                    result = MessageBox.Show("Anzahl gelber Etikette " + tbGelbEtAnz.Text + " ist okay?", "Sind Sie sicher?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        try { Convert.ToInt32(tbGelbEtAnz.Text); }
                        catch { MessageBox.Show("Fehlerhafte Anzahl, Buchstaben/Sonderzeichen sind keine Zahlen", "ERROR 02", MessageBoxButtons.OK, MessageBoxIcon.Error); break; }
                        druckJob.gelbEtDruck(tbGelbEtAnz.Text);
                    }
                    Cursor.Current = Cursors.Default;
                    break;

                case "tabRueckstell":

                    cbVal = 1 + cbRueckUser.FindString(cbRueckUser.SelectedValue.ToString());
                    //fehlerMess = druckPrüf.pruefRueckstell(tbRueckAnz.Text, cbVal, cbRueckRev.Text, cbRueckTeilNr.Text);

                    //if (fehlerMess != "Okay")
                    //{
                    //    MessageBox.Show(fehlerMess, "ERROR 03");
                    //}
                    //else
                    //{
                    druckJob.rueckstellDruck(cbRueckUser.SelectedValue.ToString(), dateTPickerRueck.Value.ToString("dd.MM.yyyy"), cbRueckTeilNr.SelectedValue.ToString(), lbRueckTeilName.Text, tbRueckAnz.Text, cbRueckRev.SelectedValue.ToString());
                    //}
                    Cursor.Current = Cursors.Default;
                    break;
            }
        }

        // Methode die aufgerufen wird wenn eine andere Revision im Tab Rüeckstellmuster ausgewählt wird
        // Dann werden die Informationen zum Teil neu geladen
        private void revValUpdate(object sender, EventArgs e)
        {
            if (init == false)
            {
                string teilNo;
                teilNo = cbRueckTeilNr.Text;
                int teilNoLen = teilNo.Length;
                if (teilNoLen == 12) { teilNo += cbRueckRev.SelectedValue.ToString(); }
                else { teilNo.Insert(12, cbRueckRev.SelectedValue.ToString()); }

                lbRueckTeilName.Text = modWE.myValueDB2(modWE.SQLInfo(teilNo)) + "\n";
                lbRueckTeilName.Text += modWE.myValueDB2(modWE.SQLInfo2(teilNo)) + "\n";
                lbRueckTeilName.Text += modWE.myValueDB2(modWE.SQLInfo3(teilNo));
            }
        }

        // Aktualisiert die Infofelder passend zur Revision. Die Teilenummer bleibt gleich
        private void weRevUpdate(object sender, EventArgs e)
        {
            if (init == false)
            {
                string teilNO;
                teilNO = cbWETlnr.Text;
                int teilNoLen = teilNO.Length;
                if (teilNoLen == 12) { teilNO += cbWERev.SelectedValue.ToString(); }
                else { teilNO.Insert(12, cbWERev.SelectedValue.ToString()); }

                cbWELotNr.DataSource = modWE.myDataTableDB2(modWE.SQLLotNR(cbWETlnr.SelectedValue.ToString() + cbWERev.SelectedValue.ToString()));
                tbWE1.Text = modWE.myValueDB2(modWE.SQLInfo(teilNO));
                tbWE2.Text = modWE.myValueDB2(modWE.SQLInfo2(teilNO));
                tbWE3.Text = modWE.myValueDB2(modWE.SQLInfo3(teilNO));
                btPrintActivate(sender, e);
            }

        }

        // Zeigt das Fenster für die Bestandsliste der Kleber an
        private void bestKlebOpen(object sender, EventArgs e)
        {
            bestKleb = new BestandsListeKleber();
            bestKleb.Show();
        }

        // Gibt bei unbehandeltem Fehler eine Fehlermeldung und sendet den Vorfall an den Entwickler
        // als Ticket, sodass dieser sich mit dem Bug befassen kann
        static void LastChanceHandler(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                if (Application.ExecutablePath.Contains("dbman") || Application.ExecutablePath.Contains("DBMAN"))
                {
                    Exception ex = (Exception)args.ExceptionObject;

                    String message = "Fatal Error occurred in: ";
                    message += System.Environment.NewLine;
                    message += "User: " + System.Environment.UserName.ToString();
                    message += System.Environment.NewLine;
                    message += System.Environment.NewLine + ex.ToString() + System.Environment.NewLine;
                    message += System.Environment.NewLine;
                    message += Application.ExecutablePath.ToString();
                    message += System.Environment.NewLine;
                    message += "PC Name: " + System.Environment.MachineName.ToString() + System.Environment.NewLine; ;
                    message += "Version: " + Application.ProductVersion.ToString() + System.Environment.NewLine;
                    message += System.Environment.NewLine;
                    message += System.Environment.NewLine;

                    message += "Handles: " + System.Environment.NewLine;

                    message += System.Environment.NewLine;
                    message += System.Environment.NewLine;

                    message += System.Environment.NewLine;
                    if (args.IsTerminating)
                    {
                        message += "The Program is Terminating!";
                    }
                    else
                    {
                        message += "The Program is not Terminating!";
                    }

                    // Senden der EMail an den Entwickler, als Ticket sodass der Fehler nicht
                    // unter geht
                    EMail email = new EMail();
                    email.Sendmail(message, "Label-Druck: Exceptions", "Label-Druck-WE@pmdm.de", global::LabelDruck.Properties.Settings.Default.SendEMailTo.ToString(), "", "", false);
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal Error: Unhandled Exception! " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            this.tabControl.SelectedTabIndex = 1;

            if (IsInGroup("QS-Pruefplaner"))
            {
                tabGelbEt.Visible = false;
                tabNachdrIQC.Visible = true;
                tabRueckstell.Visible = false;
                tabWareneingang.Visible = false;
                btnMenue.Visible = false;
                btnRedLabel.Visible = true;
            }

            if (IsInGroup("Material") || System.Environment.UserName.ToLower() == "gv9182")
            {
                tabGelbEt.Visible = true;
                tabNachdrIQC.Visible = true;
                tabRueckstell.Visible = true;
                tabWareneingang.Visible = true;
                btnMenue.Visible = true;
            }

            if (IsInGroup("SK-all") )
            {
                tabGelbEt.Visible = false;
                tabNachdrIQC.Visible = true;
                tabRueckstell.Visible = false;
                tabWareneingang.Visible = false;
                btnMenue.Visible = false;

                //this.Text = "Label";
                lbMenulabel.Text = "";
                btnMenue.Visible = false;
                tabNachdrIQC.Text = "IQC";
                lbIQCqcID.Text = "QC-ID";
                lbIQCanzahl.Text = "Count";
                label2.Text = "LOT-Number";
                btnIQCPrint.Text = "Print";

                if (System.Environment.UserName.ToLower().Substring(0, 2) == "ke" || System.Environment.UserName.ToLower() == "bs6062" || System.Environment.UserName.ToLower() == "gv9182")
                {
                    cbPrinterSK.Visible = true;


                    cbPrinterSK.DisplayMember = "Text";
                    cbPrinterSK.ValueMember = "Value";

                    var items = new[] { 
                        new { Text = "IQC", Value = global::LabelDruck.Properties.Settings.Default.conPrinterSK_KE }, 
                        new { Text = "Production", Value = global::LabelDruck.Properties.Settings.Default.conPrinterSK_KE2 }
                        
                    };

                    cbPrinterSK.DataSource = items;

                    //cbPrinterSK.Items.Add(new { Text = "IQC", Value = global::LabelDruck.Properties.Settings.Default.conPrinterSK_KE });
                    //cbPrinterSK.Items.Add(new { Text = "Production", Value = global::LabelDruck.Properties.Settings.Default.conPrinterSK_KE2 });

                    cbPrinterSK.SelectedIndex = 0;

                }

                //btnRedLabel.Visible = true;
            }
            
          

            string text = "";

            if (Environment.GetCommandLineArgs().Length > 2)
            {
                String[] arg = Environment.GetCommandLineArgs();


                foreach (string item in arg)
                {
                    text += item + ";";
                }
                //MessageBox.Show(text);

                tbIQCqcID.Text = arg[1].ToString();
                tbIQCanz.Text = arg[2].ToString();
               
            }

            //tbIQCqcID.Text = "34673";
            //tbIQCanz.Text = "1";
            

            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        private void dtEingang_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(global::LabelDruck.Properties.Settings.Default.constrMSsql);
                con.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT [Haltbarkeit_Monate] FROM [_Teilestamm_PMDM].[dbo].[Drawings] WHERE [Number] LIKE '" + cbWETlnr.SelectedValue.ToString().Substring(0, 12) + "%' ", con);
                //val = (int)cmd.ExecuteScalar();
                int val = 0;

                int.TryParse(cmd.ExecuteScalar().ToString(), out val);
                con.Close();


                //int iMonate = modWE.myIntValueMS();
                if (val > 0)
                {
                    
                    DateTime dt = dtEingang.Value.AddMonths(val);
                    DateTime.TryParse(DateTime.DaysInMonth(dt.Year, dt.Month).ToString() + "." + dt.Month.ToString() + "." + dt.Year.ToString(), out dt);
                    dtVerfall.Value = dt;
                    
                }
               
            }
            catch
            { }
        }

        private void btnRedLabel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string tmpLot = "";
            string tmpWedate = "";

            if (cbIQCLot.SelectedIndex > -1)
            {
                tmpLot = cbIQCLot.SelectedValue.ToString();
                tmpWedate = modWE.myValueDB2(modWE.SQLLotWEDate(cbIQCLot.SelectedValue.ToString()));
            }

            druckJob.iqcRedLabelDruck(tbIQCqcID.Text, tmpLot, tmpWedate, tbIQCanz.Text, dtExpiryDate.Value.ToString("dd.MM.yyyy"));

            Cursor.Current = Cursors.Default;
          
            
        }

        private void cbSAP_CheckedChanged(object sender, EventArgs e)
        {
            tbSAPLot.Visible = true;
        }

        private void tbSAPLot_TextChanged(object sender, EventArgs e)
        {
            btnIQCPrint.Enabled = true;
        }
    }
}

//Struktur für Benennung :
//Art/Ort/Funktion    bsp. : lbWETlNr
//                           LabelWarenEingangTeilNummer