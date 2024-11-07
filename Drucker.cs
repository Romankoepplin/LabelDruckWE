using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;


namespace LabelDruck
{
    class Drucker
    {
        System.Drawing.Printing.PrintDocument printDocument;

        int index;
        DataSet ds;
        String[] strArr;
        //DataRow Row;
        TlNrDB modWE = new TlNrDB();
        int menge;
        int pagecount;
        bool AllowToPrint;

        // Einstellen der Werte für den Druck des Wareneingangs-Labels
        // Die Werte die der Methode übergeben werden werden dann in ein Array umgespeichert um 
        // sie Klassenintern an die Lbelmethode weiterzuleiten
        // Dieser Bereich ist für den Druck ohne Gefahrenstofffelder
        public void wareneinDruck(string teilNr, string rev, string name, string teilBesch, string teilBesch2, string lot, string datum, string anz, string WEDate)
        {
            strArr = new String[7];
            strArr[0] = teilNr + rev;
            strArr[1] = name;
            strArr[2] = teilBesch;
            strArr[3] = teilBesch2;
            strArr[4] = lot;
            datum = datum.Substring(0, 10);
            strArr[5] = datum;
            strArr[6] = WEDate;
            menge = 1;
            int.TryParse(anz, out menge);
            index = 1;
            pagecount = 1;

            //for (int i = 0; i < menge; i++)
            {
                printDocument = new PrintDocument();

                //Druckername
                printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterWhite;

                //printDocument.PrinterSettings.PrinterName = "StockNQC";
                //Prüfung ob Drucker bereit ist
                try
                {
                    //Querformat einstellen
                    printDocument.DefaultPageSettings.Landscape = false;

                    //Ränder einstellen
                    printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

                    //Printeventhändler erstellen. Das Document wird mit Inhalt gefüllt, wenn dieses Erreignis ausgelöst wird.
                    printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

                    //if (AllowToPrint != false)
                    //{
                        //Drucken
                        printDocument.Print();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Print a label is not allowed for scraped parts", "Print not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu" + System.Environment.NewLine + ex.Message);
                }
                //}
                //else
                //{
                //    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu");
                //}
            }
        }

        // Einstellen der Werte für den Druck des Wareneingangs-Labels
        // Die Werte die der Methode übergeben werden werden dann in ein Array umgespeichert um 
        // sie Klassenintern an die Labelmethode weiterzuleiten
        // Dieser Bereich ist für den Druck mit den Gefahrenstofffeldern
        public void wareneinDruck(string teilNr, string rev, string name, string teilBesch, string teilBesch2, string lot, string anz, string gebindeGr, string verfDat, string WEDate, Hashtable ingefahrstoffInfo)
        {
            strArr = new String[15];
            strArr[0] = teilNr + rev;//+ dtY.Rows[0]["Kleber_ID"].ToString();
            strArr[1] = name;
            strArr[2] = teilBesch;
            strArr[3] = teilBesch2;
            strArr[4] = lot;
            strArr[5] = gebindeGr;
            strArr[6] = verfDat;
            if (ingefahrstoffInfo["Schutzstufe_GHS"] != null) { strArr[7] = ingefahrstoffInfo["Schutzstufe_GHS"].ToString(); }
            if (ingefahrstoffInfo["Plan"] != null) { strArr[8] = ingefahrstoffInfo["Plan"].ToString(); }
            if (ingefahrstoffInfo["PlanVP"] != null) { strArr[9] = ingefahrstoffInfo["PlanVP"].ToString(); }
            if (ingefahrstoffInfo["KleberId"] != null) { strArr[11] = ingefahrstoffInfo["KleberId"].ToString(); }
            if (ingefahrstoffInfo["EntsorgungsTXTKurz_D"] != null) { strArr[12] = ingefahrstoffInfo["EntsorgungsTXTKurz_D"].ToString(); }
            if (ingefahrstoffInfo["EntsorgungsTXTKurz_D_VP"] != null) { strArr[13] = ingefahrstoffInfo["EntsorgungsTXTKurz_D_VP"].ToString(); }
            if (ingefahrstoffInfo["Storage_Temperature"] != null) { strArr[14] = ingefahrstoffInfo["Storage_Temperature"].ToString(); }

            strArr[10] = WEDate;
            menge = 1;
            int.TryParse(anz, out menge);
            index = 5;
            pagecount = 1;

            //for (int i = 0; i < menge; i++)
            {
                printDocument = new PrintDocument();

                //Druckername
                printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterGreen;
                // \\10.176.250.108\Stockroom-Label-green
                //printDocument.PrinterSettings.PrinterName = "PDFCreator";

                //Prüfung ob Drucker bereit ist
                //if (printDocument.PrinterSettings.IsValid)
                //{
                try
                {
                    //Querformat einstellen
                    printDocument.DefaultPageSettings.Landscape = false;

                    //Ränder einstellen
                    printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

                    //Printeventhändler erstellen. Das Document wird mit Inhalt gefüllt, wenn dieses Erreignis ausgelöst wird.
                    printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

                    //Drucken
                    printDocument.Print();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu" + System.Environment.NewLine + ex.Message);

                }
                //}
                //else
                //{
                //    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu");
                //}
            }
        }


        public void iqcRedLabelDruck(string iqc, string lot, string WEDate, string anz, string Ablaufdatum)
        {
            menge = 1;
            int.TryParse(anz, out menge);

            ds = new DataSet();
            ds = DataCon(SQLstring(iqc));

            strArr = new String[4];
            strArr[0] = lot;
            strArr[1] = WEDate;
            strArr[2] = modWE.myValueMS(SQLstring2(iqc));
            strArr[1] = Ablaufdatum;

            index = 6;
            pagecount = 1;
            //for (int i = 0; i < menge; i++)
            {
                printDocument = new PrintDocument();

                //Druckername
                if (System.Environment.UserName.Length > 2)
                {
                    switch (System.Environment.UserName.ToLower().Substring(0, 2))
                    {
                        case "gv":
                            printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterDE_RedLabel;
                            break;
                    }
                }

                //printDocument.PrinterSettings.PrinterName = "\\\\localhost\\FreePDF";
                //printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterSK_KE;
                //Prüfung ob Drucker bereit ist
                //if (printDocument.PrinterSettings.IsValid)
                //{

                try
                {
                    //Querformat einstellen
                    printDocument.DefaultPageSettings.Landscape = false;

                    //Ränder einstellen
                    printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

                    //printDocument.DefaultPageSettings.PrinterSettings.Copies = menge;

                    //printDocument.PrinterSettings.Copies = menge;
                    //Printeventhändler erstellen. Das Document wird mit Inhalt gefüllt, wenn dieses Erreignis ausgelöst wird.
                    //printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
                    printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

                    printDocument.DocumentName = "IQC-RedLabel-" + iqc + " LOT-" + lot;

                    //PrintPreviewDialog pd = new PrintPreviewDialog();
                    //pd.Document = printDocument;
                    //pd.ShowDialog();

                    //Drucken

                    printDocument.Print();

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu" + System.Environment.NewLine + ex.Message);
                }

                //PrintDialog dlg = new PrintDialog();
                //dlg.PrinterSettings.PrinterName = printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterIQC;
                //dlg.Document = printDocument;
                //dlg.PrinterSettings.Copies = 2;
                //printDocument.Print();

                //}
                //else
                //{
                //    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu");
                //}
            }
        }

        // Einstellen der Druckparameter für den Druck der IQC Labels
        // Hier wird die ICQ ID umgespeichert, anschliessend werden die Daten mittels
        // SQL String in ein DataSet geladen und von dort aus an die Druckmethode
        // weitergeleitet und gedruckt
        public void iqcDruck(string iqc, string lot, string WEDate, string anz, string Ablaufdatum, string printer)
        {
            menge = 1;
            int.TryParse(anz, out menge);

            ds = new DataSet();
            ds = DataCon(SQLstring(iqc));

            if (ds.Tables[0].Rows[0]["Q"].ToString() != "71" && ds.Tables[0].Rows[0]["Q"].ToString() != "70")
            {

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("This number is not listed in DOPAS, please make an entry in DOPAS first");
                    return;


                }
                //ds.Merge(DataCon(SQLstring2(iqc)));

                strArr = new String[4];
                strArr[0] = lot;
                strArr[1] = WEDate;
                strArr[2] = modWE.myValueMS(SQLstring2(iqc));
                strArr[1] = Ablaufdatum;

                index = 2;
                pagecount = 1;
                //for (int i = 0; i < menge; i++)
                {
                    printDocument = new PrintDocument();

                    //Druckername
                    if (System.Environment.UserName.Length > 2)
                    {
                        switch (System.Environment.UserName.ToLower().Substring(0, 2))
                        {
                            case "gv":
                                printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.ConPrinterDE_Zebra;
                                index = 7;
                                break;
                            case "bp":
                                printDocument.PrinterSettings.PrinterName =
                                    global::LabelDruck.Properties.Settings.Default.conPrinterThai;
                                break;
                            case "bs":
                                printDocument.PrinterSettings.PrinterName =
                                    global::LabelDruck.Properties.Settings.Default.conPrinterSK;
                                break;
                            case "ke":
                                if (printer != "")
                                {
                                    printDocument.PrinterSettings.PrinterName = printer;
                                }

                                //printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterSK_KE;
                                break;
                        }

                        if (System.Environment.UserName.ToLower() == "bs6062" ||
                            System.Environment.UserName.ToLower() == "gv9182")
                        {
                            if (printer != "")
                            {
                               // printDocument.PrinterSettings.PrinterName = "\\\\10.176.251.72\\Stockroom - GreenLabel";
                            }
                        }

                    }

                    //printDocument.PrinterSettings.PrinterName = "\\\\localhost\\FreePDF";
                    //printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterSK_KE;
                    //Prüfung ob Drucker bereit ist
                    //if (printDocument.PrinterSettings.IsValid)
                    //{

                    try
                    {

                        if (index != 7)
                        {


                            //Querformat einstellen
                            printDocument.DefaultPageSettings.Landscape = false;

                            //Ränder einstellen
                            printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

                            //printDocument.DefaultPageSettings.PrinterSettings.Copies = menge;

                            //printDocument.PrinterSettings.Copies = menge;
                            //Printeventhändler erstellen. Das Document wird mit Inhalt gefüllt, wenn dieses Erreignis ausgelöst wird.
                            //printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
                            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

                            printDocument.DocumentName = "IQC-Label-" + iqc + " LOT-" + lot;

                            //printDocument.PrinterSettings.PrinterName = "\\\\10.176.251.72\\Stockroom-GreenLabel";
                            //PrintPreviewDialog pd = new PrintPreviewDialog();
                            //pd.Document = printDocument;
                            //pd.ShowDialog();

                            //Drucken

                            try
                            {
                                int Status = 0;
                                TlNrDB hlp = new TlNrDB();
                                Status = hlp.myIntValueMS(hlp.getArticleStatus(ds.Tables[0].Rows[0]["Teilenummer"].ToString()));
                                if (Status == 0)
                                {
                                    Status = hlp.myIntValueMS(hlp.getArticleStatus2(ds.Tables[0].Rows[0]["Teilenummer"].ToString()));
                                }

                                if (Status > 0)
                                {


                                    printDocument.Print();
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            iqcLabel3();
                        }

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu" +
                            System.Environment.NewLine + ex.Message);
                    }
                }
              
            }
            else
            {
                MessageBox.Show("Print a label is not allowed for scraped parts", "Print not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Einstellen der Druckparameter für den Druck der gelben Etiketten
        // Hier wird lediglich die Anzal umgespeichert und dann wieder das 
        // Label zum Druckvorgang geschikt
        public void gelbEtDruck(string anz)
        {
            menge = 1;
            int.TryParse(anz, out menge);
            pagecount = 1;

            //for (int i = 0; i < menge; i++)
            {

                printDocument = new PrintDocument();

                //Druckername
                printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterGreen;
                index = 3;
                //Prüfung ob Drucker bereit ist
                //if (printDocument.PrinterSettings.IsValid)
                //{
                try
                {
                    //Querformat einstellen
                    printDocument.DefaultPageSettings.Landscape = false;

                    //Ränder einstellen
                    printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

                    //Printeventhändler erstellen. Das Document wird mit Inhalt gefüllt, wenn dieses Erreignis ausgelöst wird.
                    printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

                    //Drucken
                    printDocument.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu" + System.Environment.NewLine + ex.Message);
                }
                //}
                //else
                //{
                //    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu");
                //}
            }
        }

        // Hier wird der Druck des Rückstell Labels vorbereitet
        // Die hier übergebenen Daten werden ebenfalls in ein Array umgespeichert und
        // dann für den Druck Klassenintern weitergegeben 
        public void rueckstellDruck(string user, string datum, string teilNr, string teilName, string anz, string Rev)
        {
            strArr = new String[5];
            strArr[0] = user;
            strArr[1] = datum;
            strArr[2] = teilNr;
            strArr[3] = teilName;
            strArr[4] = Rev;
            menge = 1;
            int.TryParse(anz, out menge);
            datum = datum.Substring(0, 10);
            index = 4;
            pagecount = 1;

            //for (int i = 0; i < menge; i++)
            {
                printDocument = new PrintDocument();

                //Druckername
                printDocument.PrinterSettings.PrinterName = global::LabelDruck.Properties.Settings.Default.conPrinterGreen;

                //Prüfung ob Drucker bereit ist
                //if (printDocument.PrinterSettings.IsValid)
                //{
                try
                {
                    //Querformat einstellen
                    printDocument.DefaultPageSettings.Landscape = false;

                    //Ränder einstellen
                    printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

                    //Printeventhändler erstellen. Das Document wird mit Inhalt gefüllt, wenn dieses Erreignis ausgelöst wird.
                    printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

                    //Drucken
                    printDocument.PrinterSettings.Copies = (short)menge;
                    printDocument.Print();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu" + System.Environment.NewLine + ex.Message);
                }
                //}
                //else
                //{
                //    MessageBox.Show("Der Drucker ist gerade nicht verfügbar\n Bitte überprüfen sie den Drucker und starten sie das Gerät gegebenenfalls neu");
                //}
            }
        }

        // Aufruf der Methode printPage in der entschieden wird welches Label gedruckt wird 
        // mit der switch-case wird festgestellt welches Label gedrückt werden muss und wird dann
        // gestartet
        private void printDocument_PrintPage2(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Eigentliche Druck des Dokuments
            Graphics g = e.Graphics;

            g.SetClip(e.MarginBounds);

            g.TranslateTransform(e.MarginBounds.Left, e.MarginBounds.Top);

            g.PageUnit = GraphicsUnit.Millimeter;

            

            switch (index)
            {
                case (1):

                    warenEinLabel(g);
                    break;

                case (2):

                    iqcLabel(g);
                    break;

                case (3):

                    gelbEtLabel(g);
                    break;

                case (4):

                    rueckstellLabel(g);
                    break;

                case (5):
                    kleberLabel(g);
                    break;

            }
        }

        // Aufruf der Methode printPage in der entschieden wird welches Label gedruckt wird 
        // mit der switch-case wird festgestellt welches Label gedrückt werden muss und wird dann
        // gestartet
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            if (pagecount < menge)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

            pagecount++;

            /*
            if (dtY.Rows.Count > 0)
            {
                strArr[7] = dtY.Rows[0]["Schutzstufe_GefSTV"].ToString();
                strArr[8] = dtY.Rows[0]["Abf_SCHL_NR"].ToString();
                strArr[9] = dtY.Rows[0]["Abf_SCHL_NR_VP"].ToString();
                strArr[11] = dtY.Rows[0]["Kleber_ID"].ToString();
            }
             */

            // Eigentliche Druck des Dokuments
            Graphics g = e.Graphics;

            g.SetClip(e.MarginBounds);

            g.TranslateTransform(e.MarginBounds.Left, e.MarginBounds.Top);

            g.PageUnit = GraphicsUnit.Millimeter;

            switch (index)
            {
                case (1):

                    warenEinLabel(g);
                    break;

                case (2):

                    iqcLabel2(g);
                    break;

                case (3):

                    gelbEtLabel(g);
                    break;

                case (4):

                    rueckstellLabel(g);
                    break;

                case (5):
                    kleberLabel(g);
                    break;

                case (6):
                    redLabel(g);
                    break;

                case (7):
                    iqcLabel3();
                    break;

            }
        }

        // Zeichnen der Labels 
        // Die folgenden Methoden enthalten den Code für das Labeldesign
        // Sie sind in die jeweiligen Bereiche gegliedert

        private void warenEinLabel(Graphics g)
        {
            AllowToPrint = true;
            double penWidth = 0.5;
            String sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Pen p = new Pen(Color.Black, (float)penWidth);
            Brush b = Brushes.Black;
            Font font8r = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);
            Font font9b = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            Font font18b = new Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Point);

            //Teilnummer
            g.DrawString("" + strArr[0], font18b, b, 1, 2);

            //Labelkörper
            g.DrawString("" + strArr[1], font9b, b, 2, 9);
            g.DrawString("Druck:  " + strArr[5], font9b, b, 57, 9);
            g.DrawString("" + strArr[2], font9b, b, 2, 14);
            g.DrawString("" + strArr[3], font9b, b, 2, 19);

            //Labelboden
            g.DrawString("LOT-Nr:  " + strArr[4], font9b, b, 2, 24);
            g.DrawString("WE:  " + strArr[6], font9b, b, 2, 29);

        }

        private void iqcLabel2(Graphics g)
        {

            AllowToPrint = true;
            string zpl_code = "^XA";
           


            //^XA
            //    ^ FO100,100
            //    ^ BXN,10,200
            //    ^ FDZEBRA TECHNOLOGIES CORPORATION
            //333 CORPORATE WOODS PARKWAY
            //VERNON HILLS, IL
            //60061 - 3109 ^ FS
            //    ^ XZ

            //^XA
            //    ^ FT101,59 ^ A0N,31,30 ^ FH\^CI28 ^ FDGS1 - 128 : ^FS ^ CI27
            //    ^ FT101,174 ^ A0N,31,30 ^ FH\^CI28 ^ FDGS1 - DataMatrix : ^FS ^ CI27
            //    ^ FT548,174 ^ A0N,31,30 ^ FH\^CI28 ^ FDGS1 - QR: ^FS ^ CI27
            //    ^ BY2,3,54 ^ FT110,134 ^ BCN ,,N,N
            //    ^ FH\^FD >;> 8011234567890123121123456 ^ FS
            //    ^ FT185,324 ^ BXN,6,200,0,0,1,_,1
            //    ^ FH\^FD_1011234567890123121123456 ^ FS
            //    ^ FO642,206 ^ BQN ,2,5
            //    ^ FH\^FD >;> 8011234567890123121123456 ^ FS
            //    ^ PQ1,0,1,Y
            //    ^ XZ

            string barcode_text = "@##";
            double penWidth = 0.5;
            String sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Pen p = new Pen(Color.Black, (float)penWidth);
            Brush b = Brushes.Black;
            Font font8r = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);
            Font font9b = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            Font font12b = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            String datum = System.DateTime.Now.Date.ToString();
            datum = datum.Substring(0, 10);

            TlNrDB hlp = new TlNrDB();

            int Status = 0;
            try
            {
                string no = ds.Tables[0].Rows[0]["Teilenummer"].ToString();
                no.ToString();
                Status = hlp.myIntValueMS(hlp.getArticleStatus(ds.Tables[0].Rows[0]["Teilenummer"].ToString()));
                if (Status == 0)
                {
                    Status = hlp.myIntValueMS(hlp.getArticleStatus2(ds.Tables[0].Rows[0]["Teilenummer"].ToString()));
                }
            }
            catch
            {
            }

            if (Status == 0)
            {
                //MessageBox.Show("this number not found in DOPAS, printing is not psssible","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                g.Dispose();
                MessageBox.Show("this number not found in DOPAS, printing is not psssible","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            string Release = "";

            if (Status > 0)
            {
                if (Status == 220)
                {
                    Release = " (sample released)";
                }

                if (Status == 230)
                {
                    Release = " (first article)";
                }

                if (Status == 240)
                {
                    Release = " (production rel.)";
                }

                if (Status == 2)
                {
                    Release = " (sample released)";
                }

                if (Status == 3)
                {
                    Release = " (production rel.)";
                }

            }

            g.DrawString(ds.Tables[0].Rows[0]["Teilenummer"].ToString() + Release, font9b, b, 35, 2);
            barcode_text += ds.Tables[0].Rows[0]["Teilenummer"].ToString() + "##";
            barcode_text += Release + "##";
            zpl_code += "^FT450,60 ^A0N,40,40 ^FD" + ds.Tables[0].Rows[0]["Teilenummer"].ToString() + Release + " ^FS";

            g.DrawString("LOT: " + strArr[0], font9b, b, 35, 8);
            barcode_text += strArr[0] + "##";
            zpl_code += "^FT450,110 ^A0N,40,40 ^FD" + "LOT: " + strArr[0] + " ^FS";

            g.DrawString("" + ds.Tables[0].Rows[0]["Teilebezeichnung"].ToString(), font9b, b, 35, 12);
            barcode_text += ds.Tables[0].Rows[0]["Teilebezeichnung"].ToString() + "##";
            zpl_code += "^FT450,160 ^A0N,40,40 ^FD" + ds.Tables[0].Rows[0]["Teilebezeichnung"].ToString() + " ^FS";

            //g.DrawLine(p, 35, 14, 100, 14);
            g.DrawString("...................................................................", font9b, b, 35, 14);
            zpl_code += "^FT450,200 ^A0N,40,40 ^FD" + "..................................................................." + " ^FS";
            DateTime anlageAm = new DateTime();

            DateTime.TryParse(ds.Tables[0].Rows[0]["AnlageAm"].ToString(), out anlageAm);

            g.DrawString("QC-ID:  " + ds.Tables[0].Rows[0]["QC_ID"].ToString() + "  /  " + anlageAm.ToString("dd.MM.yyyy"), font9b, b, 35, 17);
            zpl_code += "^FT450,250 ^A0N,40,40 ^FD" + "QC-ID:  " + ds.Tables[0].Rows[0]["QC_ID"].ToString() + "  /  " + anlageAm.ToString("dd.MM.yyyy") + " ^FS";
            barcode_text += ds.Tables[0].Rows[0]["QC_ID"].ToString() + "##";
            barcode_text += ds.Tables[0].Rows[0]["AnlageAm"].ToString().Substring(0, 10) + "##";

            g.DrawString("Tester:  " + ds.Tables[0].Rows[0]["VollständigerName"].ToString(), font9b, b, 35, 21);
            zpl_code += "^FT450,300 ^A0N,40,40 ^FD" + "Tester:  " + ds.Tables[0].Rows[0]["VollständigerName"].ToString() + " ^FS";
            barcode_text += ds.Tables[0].Rows[0]["VollständigerName"].ToString() + "##";

            g.DrawString("ORD-NR: " + ds.Tables[0].Rows[0]["WE_NR"].ToString(), font9b, b, 35, 25);
            zpl_code += "^FT450,350 ^A0N,40,40 ^FD" + "ORD-NR: " + ds.Tables[0].Rows[0]["WE_NR"].ToString() + " ^FS";
            barcode_text += ds.Tables[0].Rows[0]["WE_NR"].ToString() + "##";

            g.DrawString("Vendor:  " + ds.Tables[0].Rows[0]["Nr"].ToString(), font9b, b, 35, 29);
            zpl_code += "^FT450,400 ^A0N,40,40 ^FD" + "Vendor:  " + ds.Tables[0].Rows[0]["Nr"].ToString() + " ^FS";
            barcode_text += ds.Tables[0].Rows[0]["Nr"].ToString() + "##";

            //g.DrawLine(p, 35, 31, 100, 31);

            g.DrawString("...................................................................", font9b, b, 35, 31);
            zpl_code += "^FT450,450 ^A0N,40,40 ^FD" + "..................................................................." + " ^FS";
            string Zusatztext = "";

            switch (ds.Tables[0].Rows[0]["Q"].ToString())
            {
                case "100":
                    Zusatztext = "APPROVED";
                    break;
                case "90":
                    Zusatztext = "SPECIAL APPROVAL";
                    break;
                case "80":
                    Zusatztext = "REWORK / SORTING";
                    break;
                case "71":
                    Zusatztext = "SCRAP";
                    AllowToPrint = false;
                    //Fehlermeldung + darf nicht gedruckt werden

                    break;
                case "70":
                    Zusatztext = "RETURN TO VENDOR";
                    AllowToPrint = false;
                    //Fehlermeldung + darf nicht gedruckt werden
                    break;
            }

            g.DrawString(Zusatztext, font9b, b, 35, 35);
            zpl_code += "^FT450,500 ^A0N,40,40 ^FD" + Zusatztext + " ^FS";
            barcode_text += Zusatztext + "##";

            //Durabillity
            int Durabillity = 0;
            int.TryParse(ds.Tables[0].Rows[0]["Haltbarkeit_Monate"].ToString(), out Durabillity);
            DateTime WE_Datum = DateTime.Today;
            DateTime.TryParse(ds.Tables[0].Rows[0]["AnlageAm"].ToString(), out WE_Datum);
            if (Durabillity > 0)
            {
                //g.DrawString("Expiration date:  " + Durabillity.ToString() + " months, ( " + WE_Datum.AddMonths(Durabillity).ToShortDateString() + " )", font9b, b, 35, 43);
                g.DrawString("Expiration date:  " + strArr[3], font9b, b, 35, 43);
                zpl_code += "^FT450,550 ^A0N,40,40 ^FD" + "Expiration date:  " + strArr[3] + " ^FS";
                barcode_text += Durabillity.ToString() + "##";
                barcode_text += WE_Datum.AddMonths(Durabillity).ToShortDateString() + "##";
            }
            zpl_code += "^FO20,30 ^BXN,10,200 ^FD" + barcode_text + " ^FS";

            //barcode_text = "12345678901234567890123456789012345678901234567890" + "##";
            //barcode_text += "12345678901234567890123456789012345678901234567890" + "##";
            //barcode_text += "12345678901234567890123456789012345678901234567890" + "##";
            //barcode_text += "12345678901234567890123456789012345678901234567890" + "##";
            //barcode_text += "12345678901234567890123456789012345678901234567890" + "##";
            //barcode_text += "12345678901234567890123456789012345678901234567890" + "##";

       
            zpl_code += "^XZ";

            DataMatrix.NetCore.DmtxImageEncoderOptions opt = new DataMatrix.NetCore.DmtxImageEncoderOptions();
            DataMatrix.NetCore.DmtxImageEncoder enc = new DataMatrix.NetCore.DmtxImageEncoder();
            
            Bitmap Img = new Bitmap(600, 600);

            Img = enc.EncodeImage(barcode_text);

            Bitmap resized = new Bitmap(Img, new Size(Img.Width / 2, Img.Height / 2));
            g.DrawImage(resized, 2, 2);

        
        }

        private void iqcLabel3()
        {

            AllowToPrint = true;
            string zpl_code = "^XA";
            string barcode_text = "@##";
            double penWidth = 0.5;
            String sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Pen p = new Pen(Color.Black, (float)penWidth);
            Brush b = Brushes.Black;
            Font font8r = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);
            Font font9b = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            Font font12b = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            String datum = System.DateTime.Now.Date.ToString();
            datum = datum.Substring(0, 10);

            TlNrDB hlp = new TlNrDB();

            int Status = 0;
            try
            {
                string no = ds.Tables[0].Rows[0]["Teilenummer"].ToString();
                no.ToString();
                Status = hlp.myIntValueMS(hlp.getArticleStatus(ds.Tables[0].Rows[0]["Teilenummer"].ToString()));
                if (Status == 0)
                {
                    Status = hlp.myIntValueMS(hlp.getArticleStatus2(ds.Tables[0].Rows[0]["Teilenummer"].ToString()));
                }
            }
            catch
            {
            }

            if (Status == 0)
            {
                //MessageBox.Show("this number not found in DOPAS, printing is not psssible","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
               
                MessageBox.Show("this number not found in DOPAS, printing is not psssible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string Release = "";

            if (Status > 0)
            {
                if (Status == 220)
                {
                    Release = " (sample released)";
                }

                if (Status == 230)
                {
                    Release = " (first article)";
                }

                if (Status == 240)
                {
                    Release = " (production rel.)";
                }

                if (Status == 2)
                {
                    Release = " (sample released)";
                }

                if (Status == 3)
                {
                    Release = " (production rel.)";
                }

            }

            barcode_text += ds.Tables[0].Rows[0]["Teilenummer"].ToString() + "##";
            barcode_text += Release + "##";
            zpl_code += "^FT470,60 ^A0N,40,40 ^FD" + ds.Tables[0].Rows[0]["Teilenummer"].ToString() + Release + " ^FS";

          
            barcode_text += strArr[0] + "##";
            zpl_code += "^FT470,110 ^A0N,40,40 ^FD" + "LOT: " + strArr[0] + " ^FS";

          
            barcode_text += ds.Tables[0].Rows[0]["Teilebezeichnung"].ToString() + "##";
            zpl_code += "^FT470,160 ^A0N,40,40 ^FD" + ds.Tables[0].Rows[0]["Teilebezeichnung"].ToString() + " ^FS";

            //g.DrawLine(p, 35, 14, 100, 14);
         
            zpl_code += "^FT470,200 ^A0N,40,40 ^FD" + "......" + " ^FS";
            DateTime anlageAm = new DateTime();

            DateTime.TryParse(ds.Tables[0].Rows[0]["AnlageAm"].ToString(), out anlageAm);

          
            zpl_code += "^FT470,250 ^A0N,40,40 ^FD" + "QC-ID:  " + ds.Tables[0].Rows[0]["QC_ID"].ToString() + "  /  " + anlageAm.ToString("dd.MM.yyyy") + " ^FS";
            barcode_text += ds.Tables[0].Rows[0]["QC_ID"].ToString() + "##";
            barcode_text += ds.Tables[0].Rows[0]["AnlageAm"].ToString().Substring(0, 10) + "##";

          
            zpl_code += "^FT470,300 ^A0N,40,40 ^FD" + "Tester:  " + ds.Tables[0].Rows[0]["VollständigerName"].ToString() + " ^FS";
            barcode_text += ds.Tables[0].Rows[0]["VollständigerName"].ToString() + "##";

           
            zpl_code += "^FT470,350 ^A0N,40,40 ^FD" + "ORD-NR: " + ds.Tables[0].Rows[0]["WE_NR"].ToString() + " ^FS";
            barcode_text += ds.Tables[0].Rows[0]["WE_NR"].ToString() + "##";

         
            zpl_code += "^FT470,400 ^A0N,40,40 ^FD" + "Vendor:  " + ds.Tables[0].Rows[0]["Nr"].ToString() + " ^FS";
            barcode_text += ds.Tables[0].Rows[0]["Nr"].ToString() + "##";

            //g.DrawLine(p, 35, 31, 100, 31);

           
            zpl_code += "^FT470,450 ^A0N,40,40 ^FD" + "......" + " ^FS";
            string Zusatztext = "";

            switch (ds.Tables[0].Rows[0]["Q"].ToString())
            {
                case "100":
                    Zusatztext = "APPROVED";
                    break;
                case "90":
                    Zusatztext = "SPECIAL APPROVAL";
                    break;
                case "80":
                    Zusatztext = "REWORK / SORTING";
                    break;
                case "71":
                    Zusatztext = "SCRAP";
                    AllowToPrint = false;
                    //Fehlermeldung + darf nicht gedruckt werden

                    break;
                case "70":
                    Zusatztext = "RETURN TO VENDOR";
                    AllowToPrint = false;
                    //Fehlermeldung + darf nicht gedruckt werden
                    break;
            }

           
            zpl_code += "^FT470,500 ^A0N,40,40 ^FD" + Zusatztext + " ^FS";
            barcode_text += Zusatztext + "##";

            //Durabillity
            int Durabillity = 0;
            int.TryParse(ds.Tables[0].Rows[0]["Haltbarkeit_Monate"].ToString(), out Durabillity);
            DateTime WE_Datum = DateTime.Today;
            DateTime.TryParse(ds.Tables[0].Rows[0]["AnlageAm"].ToString(), out WE_Datum);
            if (Durabillity > 0)
            {
                //g.DrawString("Expiration date:  " + Durabillity.ToString() + " months, ( " + WE_Datum.AddMonths(Durabillity).ToShortDateString() + " )", font9b, b, 35, 43);
              
                zpl_code += "^FT470,550 ^A0N,40,40 ^FD" + "Expiration date:  " + strArr[3] + " ^FS";
                barcode_text += Durabillity.ToString() + "##";
                barcode_text += WE_Datum.AddMonths(Durabillity).ToShortDateString() + "##";
            }
            //zpl_code += "^FO20,30 ^BXN,10,200 ^FD" + barcode_text + " ^FS";
            zpl_code += "^FO5,30 ^BXN,10,200 ^FD" + barcode_text + " ^FS";

            zpl_code += "^XZ";

            var printerConn = new Zebra.Sdk.Comm.TcpConnection("10.176.5.230", 9100);

            printerConn.Open();

            var printerInstance = Zebra.Sdk.Printer.ZebraPrinterFactory.GetInstance(printerConn);
            
            for (int i = 1; i <= menge; i++)
            {
                printerInstance.SendCommand(zpl_code);
            }
            printerConn.Close();

        }

        // Konstruktion des iqc Labels 
        private void iqcLabel(Graphics g)
        {
            double penWidth = 0.5;
            String sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Pen p = new Pen(Color.Black, (float)penWidth);
            Brush b = Brushes.Black;
            Font font8r = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);
            Font font9b = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            Font font14b = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);
            String datum = System.DateTime.Now.Date.ToString();
            datum = datum.Substring(0, 10);

            //Labelkopf Zeichnen
            g.DrawString("FREIGEGEBEN / APPROVED", font9b, b, 18, 0);
            g.DrawLine(p, 1, 4, 82, 4);

            //Labelkörper Zeichnen
            //1. Zeile
            g.DrawString("QC-ID:  " + ds.Tables[0].Rows[0]["QC_ID"].ToString(), font9b, b, 2, 5);
            //g.DrawString("printed by Stockroom", font9b, b, 52, 5);

            g.DrawString("Test-date:  " + ds.Tables[0].Rows[0]["AnlageAm"].ToString().Substring(0, 10), font9b, b, 52, 5);
            //string test = ds.Tables[0].Rows[0]["AnlageAm"].ToString().Substring(0, 10);

            //2. Zeile
            g.DrawString("Print-date:  " + datum, font9b, b, 52, 9);
            g.DrawString("Tester:  " + ds.Tables[0].Rows[0]["VollständigerName"].ToString(), font9b, b, 2, 9);

            //3. + 4.Zeile
            g.DrawString("Lieferant:  " + ds.Tables[0].Rows[0]["Nr"].ToString(), font9b, b, 52, 13);
            g.DrawString("Part:    " + ds.Tables[0].Rows[0]["Teilenummer"].ToString(), font9b, b, 2, 13);
            g.DrawString("" + ds.Tables[0].Rows[0]["Teilebezeichnung"].ToString(), font9b, b, 13, 17);
            g.DrawString("LOT:     " + strArr[0], font9b, b, 2, 21);
            g.DrawString("WE-Datum: " + strArr[1], font9b, b, 52, 21);

            TlNrDB hlp = new TlNrDB();

            int Status = 0;
            try
            {
                Status = hlp.myIntValueMS(hlp.getArticleStatus(ds.Tables[0].Rows[0]["Teilenummer"].ToString()));
            }
            catch
            {
            }

            if (Status > 0)
            {
                if (Status == 220)
                {
                    g.DrawString("Sample / Muster", font9b, b, 8, 28);

                    Font font8w = new Font("Wingdings", 12, FontStyle.Regular, GraphicsUnit.Point);//Häckle
                    g.DrawString("þ", font8w, b, 2, 28);//Häckle

                }
                else
                {
                    //g.DrawString("Sample / Muster", font9b, b, 8, 28);
                    //Font font8w = new Font("Wingdings", 12, FontStyle.Regular, GraphicsUnit.Point);//Häckle
                    //g.DrawString("o", font8w, b, 2, 28);//Häckle
                }

            }

            //Bemerkung + Abschluss

            //Dim ZusatzInfo As String
            //If rs!Mängelbericht = True Then
            //If rs!Q = 90 Then ZusatzInfo = "Annahme mit Sonderfreigabe"
            //If rs!Q = 80 Then ZusatzInfo = "Annahme mit Nacharbeit/Aussortieren"
            //If rs!Q = 71 Then ZusatzInfo = "VERSCHROTTEN siehe Begleitschein"
            //If rs!Q = 70 Then ZusatzInfo = "Annahme verweigert - Ruecksendung"
            //End If

            string Zusatztext = "";

            switch (ds.Tables[0].Rows[0]["Q"].ToString())
            {
                case "90":
                    Zusatztext = "Annahme mit Sonderfreigabe";
                    break;
                case "80":
                    Zusatztext = "Annahme mit Nacharbeit/Aussortieren";
                    break;
                case "71":
                    Zusatztext = "VERSCHROTTEN siehe Begleitschein";
                    break;
                case "70":
                    Zusatztext = "Annahme verweigert - Ruecksendung";
                    break;
            }

            g.DrawString(Zusatztext, font9b, b, 2, 30);
            //g.DrawString("printed by Stockroom", font9b, b, 50, 35);


            //Dim ZusatzInfo As String
            //If rs!Mängelbericht = True Then
            //If rs!Q = 90 Then ZusatzInfo = "Annahme mit Sonderfreigabe"
            //If rs!Q = 80 Then ZusatzInfo = "Annahme mit Nacharbeit/Aussortieren"
            //If rs!Q = 71 Then ZusatzInfo = "VERSCHROTTEN siehe Begleitschein"
            //If rs!Q = 70 Then ZusatzInfo = "Annahme verweigert - Ruecksendung"
            //End If

            Image barcode1 = Image.FromFile(@"C:\Source\Barcode\barcode_5.gif");

            g.DrawImage(barcode1, 8, 32);

            Image barcode2 = Image.FromFile(@"C:\Source\Barcode\barcode_7.gif");

            g.DrawImage(barcode2, 8, 100);

            Image barcode3 = Image.FromFile(@"C:\Source\Barcode\barcode_10.gif");

            g.DrawImage(barcode3, 8, 200);

        }

        private void redLabel(Graphics g)
        {
            double penWidth = 0.5;
            String sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Pen p = new Pen(Color.Black, (float)penWidth);
            Brush b = Brushes.Black;
            Font font8r = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);
            Font font9b = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            Font font14b = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);
            String datum = System.DateTime.Now.Date.ToString();
            datum = datum.Substring(0, 10);

            //Labelkopf Zeichnen
            g.DrawString("GESPERRT / BLOCKED", font9b, b, 18, 0);
            g.DrawLine(p, 1, 4, 82, 4);

            //Labelkörper Zeichnen
            //1. Zeile
            g.DrawString("QC-ID:  " + ds.Tables[0].Rows[0]["QC_ID"].ToString(), font9b, b, 2, 5);
            //g.DrawString("printed by Stockroom", font9b, b, 52, 5);

            g.DrawString("Test-date:  " + ds.Tables[0].Rows[0]["AnlageAm"].ToString().Substring(0, 10), font9b, b, 52, 5);
            //string test = ds.Tables[0].Rows[0]["AnlageAm"].ToString().Substring(0, 10);

            //2. Zeile
            g.DrawString("Print-date:  " + datum, font9b, b, 52, 9);
            g.DrawString("Tester:  " + ds.Tables[0].Rows[0]["VollständigerName"].ToString(), font9b, b, 2, 9);

            //3. + 4.Zeile
            g.DrawString("Lieferant:  " + ds.Tables[0].Rows[0]["Nr"].ToString(), font9b, b, 52, 13);
            g.DrawString("Part:    " + ds.Tables[0].Rows[0]["Teilenummer"].ToString(), font9b, b, 2, 13);
            g.DrawString("" + ds.Tables[0].Rows[0]["Teilebezeichnung"].ToString(), font9b, b, 13, 17);
            g.DrawString("LOT:     " + strArr[0], font9b, b, 2, 21);
            g.DrawString("WE-Datum: " + strArr[1], font9b, b, 52, 21);

        }


        // Konstruktion der gelben Etiketten
        private void gelbEtLabel(Graphics g)
        {
            double penWidth = 0.5;
            String sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Pen p = new Pen(Color.Black, (float)penWidth);
            Brush b = Brushes.Black;
            Font font8r = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);
            Font font9b = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            Font font14b = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);

            //Labelkopf
            g.DrawLine(p, 2, 2, 6, 2);                      //Kasten1 top Wand
            g.DrawLine(p, 17, 2, 21, 2);                    //Kasten2 top Wand

            g.DrawLine(p, 2, 2, 2, 6);                      //Kasten1 linke Wand
            g.DrawLine(p, 6, 2, 6, 6);                      //Kasten1 rechte Wand
            g.DrawString("NQC", font9b, b, 7, 2);           //KastenArt

            g.DrawLine(p, 17, 2, 17, 6);                    //Kasten2 linke Wand
            g.DrawLine(p, 21, 2, 21, 6);                    //Kasten2 rechte Wand
            g.DrawString("FuMu", font9b, b, 23, 2);         //KastenArt

            g.DrawLine(p, 2, 6, 6, 6);                      //Kasten1 Bodenwand
            g.DrawLine(p, 17, 6, 21, 6);                    //Kasten2 Bodenwand

            //1. Zeile
            g.DrawString("aus KST: ..................", font9b, b, 2, 8);
            g.DrawString("durch: ...............................................", font9b, b, 36, 8);

            //2. Zeile
            g.DrawString("WE-Nr.: .....................", font9b, b, 2, 13);
            g.DrawString("WE-Dat.: ....................", font9b, b, 36, 13);
            g.DrawString("STK: .........", font9b, b, 70, 13);

            //3. + 4 Zeile
            g.DrawString("Art.-Nr.: ...................................................................", font9b, b, 2, 18);
            g.DrawString("Lieferant: ................................................................", font9b, b, 2, 23);

        }

        // Konstruktion der Rückstell Labels
        private void rueckstellLabel(Graphics g)
        {
            double penWidth = 0.5;
            String sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Pen p = new Pen(Color.Black, (float)penWidth);
            Brush b = Brushes.Black;
            Font font8b = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);
            Font font10b = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
            Font font12b = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            String datum = System.DateTime.Now.Date.ToString();
            datum = datum.Substring(0, 10);

            //Labelkopf
            g.DrawString("R Ü C K S T E L L M U S T E R", font12b, b, 12, 2);

            //1. Zeile
            g.DrawString("Anlieferung durch: ", font8b, b, 3, 6);
            g.DrawString("am: ", font8b, b, 59, 6);
            g.DrawLine(p, 3, 9, 31, 9);
            g.DrawLine(p, 58, 9, 65, 9);

            //2. Zeile
            g.DrawString("" + strArr[0], font10b, b, 3, 10);
            g.DrawString("" + strArr[1], font10b, b, 59, 10);

            //3. Zeile
            g.DrawString("Artikel: ", font8b, b, 3, 14);
            g.DrawLine(p, 3, 17, 14, 17);

            //Abschluss
            g.DrawString("" + strArr[2] + strArr[4], font10b, b, 3, 18);
            g.DrawString("" + strArr[3], font10b, b, 3, 23);
        }

        // Konstruktion des Kleber/Gefahrenstoff Labels
        private void kleberLabel(Graphics g)
        {
            double penWidth = 0.5;
            String sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Pen p = new Pen(Color.Black, (float)penWidth);
            Brush b = Brushes.Black;
            Font font11b = new Font("Arial", 11, FontStyle.Bold, GraphicsUnit.Point);
            Font font9b = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
            Font font18b = new Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Point);

            //Teilnummer//+ strArr[0].Substring(13) +
            string KlebstoffNR = "";
            if (strArr[11] != null)
            {
                KlebstoffNR = strArr[11].ToString();
            }
            g.DrawString("Klebstoff-Nr.: " + KlebstoffNR + " / Teile-Nr.: " + strArr[0].Substring(0, 12), font9b, b, 1, 2);

            //Labelkörper
            int y = 7; int height = 4; int x2 = 54;
            g.DrawString("" + strArr[1], font9b, b, 2, y);
            g.DrawString("Verfallsdatum:  ", font9b, b, x2, y);

            y = y + height;
            g.DrawString("" + strArr[6], font11b, b, x2, y);
            g.DrawString("" + strArr[2], font9b, b, 2, y);

            y = y + height;
            g.DrawString("" + strArr[3], font9b, b, 2, y);
            g.DrawString("Schutzstufe: " + strArr[7], font9b, b, x2, y);

            //Labelboden
            y = y + height;
            g.DrawString("LOT-Nr: " + strArr[4], font9b, b, 2, y);
            g.DrawString("WE:  " + strArr[10], font9b, b, x2, y);

            y = y + height;
            g.DrawString("Entsorgung Stoff: " + strArr[8], font9b, b, 2, y);
            g.DrawString("Verpackung: " + strArr[9], font9b, b, x2, y);

            y = y + height;
            g.DrawString(strArr[12], font9b, b, 2, y); // Text-Stoff
            g.DrawString(strArr[13], font9b, b, x2, y); //Text-Verpackung

            y = y + height;
            g.DrawString("Lagertemperatur: " + strArr[14], font9b, b, 2, y); // Storage
        }

        // Spezieller Verbindungsaufbau für das QC-Label
        // Hier wird der SQL-String verarbeitet und liefert die gewünschten daten in einer DataTable zurück
        private DataSet DataCon(string sqlBef)
        {
            DataSet ds = new DataSet();

            try
            {
                SqlConnection con = new SqlConnection(global::LabelDruck.Properties.Settings.Default.constrMssql2);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlBef, con);
                da.Fill(ds);
                con.Close();
            }
            catch
            {
                ds = null;
            }
            return ds;
        }

        // Spezieller SQL-String für das QC-Label 
        // In diesem String werden die wichtigen Daten für das Label abgerufen
        private string SQLstring(string qcID)
        {
            string SQL = "";
            SQL += "SELECT [QC_ID],B.[Nr],[Teilenummer],[Teilebezeichnung],";
            SQL += "C.[VollständigerName],A.[Bemerkungen],[Nr],[Q],[WE_NR],[AnlageAm], ";
            SQL += "D.[Haltbarkeit_Monate] ";
            SQL += "FROM [_Datenstamm_PMDM].[dbo].[IQC_Lieferantenbewertung_QC] AS A ";
            SQL += "INNER JOIN [_Datenstamm_PMDM].[dbo].[EK_Lieferantendaten] AS B ON A.[LID] = B.[LID] ";
            SQL += "LEFT JOIN [__AdminDB_PMDM].[dbo].[User] AS C ON A.[Prüfer] = C.[UID] ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[Drawings] AS D ON A.DRW_ID = D.DRW_ID ";
            SQL += "WHERE [QC_ID]=" + qcID + "";
            return SQL;
        }

        private string SQLstring2(string qcID)
        {
            string SQL = "SELECT ";
            SQL += "MAX([PDatum]) AS 'PDatum' ";
            SQL += "FROM [_Datenstamm_PMDM].[dbo].[PP_Prüfdaten] ";
            SQL += "WHERE [QC_ID]=" + qcID + " ";
            //SQL += "ORDER BY [PDatum]";
            return SQL;
        }
    }
}
