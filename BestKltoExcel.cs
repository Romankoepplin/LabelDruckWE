using System;
using System.Data;
using System.Windows.Forms;

namespace LabelDruck
{
    class BestKltoExcel
    {
        private DataGridView dgv;
        private DataSet source;
        private string strTitle;

        public BestKltoExcel(DataGridView d)
        {
            dgv = d;
            strTitle = dgv.Name;
        }

        public BestKltoExcel(DataGridView d, string sTitle)
        {
            dgv = d;
            strTitle = sTitle;
        }

        public BestKltoExcel(DataSet ds)
        {
            source = ds;
            strTitle = "";
        }

        public BestKltoExcel(DataSet ds, string sTitle)
        {
            source = ds;
            strTitle = sTitle;
        }

        public void ExportTable(string fileName, string strTableName)
        {
            int iTbl = 0;

            // Die entsprechende Table im Dataset suchen
            while ((iTbl < source.Tables.Count) && (source.Tables[iTbl].TableName != strTableName))
                iTbl++;

            // Gibt's die Table ueberhaupt ?
            if (iTbl >= source.Tables.Count)
                throw (new Exception("Table " + strTableName + " not found !"));

            try
            {
                System.IO.StreamWriter excelDoc;

                Cursor.Current = Cursors.WaitCursor;
                excelDoc = new System.IO.StreamWriter(fileName);
                const string startExcelXML = "<xml version>\r\n<Workbook " +
                      "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                      " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                      "xmlns:x=\"urn:schemas-microsoft-com:office:" +
                      "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                      "office:spreadsheet\">\r\n <Styles>\r\n " +
                      "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                      "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                      "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                      "\r\n <Protection/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                      "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                      "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                      " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                      "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                      "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                      "ss:Format=\"dd/mm/yyyy;@\"/>\r\n </Style>\r\n " +
                      "</Styles>\r\n ";
                const string endExcelXML = "</Workbook>";

                int rowCount = 0;
                int sheetCount = 1;
                excelDoc.Write(startExcelXML);
                excelDoc.Write("<Worksheet ss:Name=\"" + source.Tables[iTbl].TableName + "\">\r\n");
                excelDoc.Write("    <Table>\r\n");
                excelDoc.Write("        <Row>\r\n");
                for (int x = 0; x < source.Tables[iTbl].Columns.Count; x++)
                {
                    excelDoc.Write("            <Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    excelDoc.Write(source.Tables[iTbl].Columns[x].ColumnName);
                    excelDoc.Write("</Data></Cell>\r\n");
                }
                excelDoc.Write("        </Row>\r\n");
                foreach (DataRow x in source.Tables[iTbl].Rows)
                {
                    rowCount++;
                    //if the number of rows is > 64000 create a new page to continue output
                    if (rowCount == 64000)
                    {
                        rowCount = 0;
                        sheetCount++;
                        excelDoc.Write("    </Table>\r\n");
                        excelDoc.Write("</Worksheet>\r\n");
                        excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">\r\n");
                        excelDoc.Write("    <Table>\r\n");
                    }
                    excelDoc.Write("        <Row>\r\n"); //ID=" + rowCount + "
                    for (int y = 0; y < source.Tables[iTbl].Columns.Count; y++)
                    {
                        System.Type rowType;
                        rowType = x[y].GetType();
                        switch (rowType.ToString())
                        {
                            case "System.String":
                                string XMLstring = x[y].ToString();
                                XMLstring = XMLstring.Trim();
                                XMLstring = XMLstring.Replace("&", "&amp;");
                                XMLstring = XMLstring.Replace(">", "&gt;");
                                XMLstring = XMLstring.Replace("<", "&lt;");
                                excelDoc.Write("            <Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(XMLstring);
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.DateTime":
                                //Excel has a specific Date Format of YYYY-MM-DD followed by  
                                //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                                //The Following Code puts the date stored in XMLDate 
                                //to the format above
                                DateTime XMLDate = (DateTime)x[y];
                                string XMLDatetoString = ""; //Excel Converted Date
                                XMLDatetoString = XMLDate.Year.ToString() +
                                     "-" +
                                     (XMLDate.Month < 10 ? "0" +
                                     XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                     "-" +
                                     (XMLDate.Day < 10 ? "0" +
                                     XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                     "T" +
                                     (XMLDate.Hour < 10 ? "0" +
                                     XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                     ":" +
                                     (XMLDate.Minute < 10 ? "0" +
                                     XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                     ":" +
                                     (XMLDate.Second < 10 ? "0" +
                                     XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                     ".000";
                                excelDoc.Write("            <Cell ss:StyleID=\"DateLiteral\">" +
                                             "<Data ss:Type=\"DateTime\">");
                                excelDoc.Write(XMLDatetoString);
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.Boolean":
                                excelDoc.Write("            <Cell ss:StyleID=\"StringLiteral\">" +
                                            "<Data ss:Type=\"String\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                excelDoc.Write("            <Cell ss:StyleID=\"Integer\">" +
                                        "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.Decimal":
                            case "System.Double":
                            case "System.Single":
                                excelDoc.Write("            <Cell ss:StyleID=\"Decimal\">" +
                                      "<Data ss:Type=\"Number\">");
                                excelDoc.Write(Common.ConvertNumber(Double.Parse(x[y].ToString())));
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.DBNull":
                                excelDoc.Write("            <Cell ss:StyleID=\"StringLiteral\">" +
                                      "<Data ss:Type=\"String\">");
                                excelDoc.Write("");
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            default:
                                throw (new Exception(rowType.ToString() + " not handled."));
                        }
                    }
                    excelDoc.Write("        </Row>\r\n");
                }
                excelDoc.Write("    </Table>\r\n");
                excelDoc.Write("</Worksheet>\r\n");
                excelDoc.Write(endExcelXML);
                excelDoc.Close();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                throw ex;
            }

        }

        public void ExportDataGridView(string fileName)
        {
            // Gibt's ein DataGridView ?
            if (dgv == null)
                throw (new Exception("No DataGridView to export !"));

            try
            {
                System.IO.StreamWriter excelDoc;

                Cursor.Current = Cursors.WaitCursor;
                excelDoc = new System.IO.StreamWriter(fileName);
                const string startExcelXML = "<xml version>\r\n<Workbook " +
                      "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                      " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                      "xmlns:x=\"urn:schemas-microsoft-com:office:" +
                      "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                      "office:spreadsheet\">\r\n <Styles>\r\n " +
                      "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                      "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                      "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                      "\r\n <Protection/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                      "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                      "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                      " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                      "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                      "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                      "ss:Format=\"dd/mm/yyyy;@\"/>\r\n </Style>\r\n " +
                      "</Styles>\r\n ";
                const string endExcelXML = "</Workbook>";

                int rowCount = 0;
                int sheetCount = 1;
                excelDoc.Write(startExcelXML);
                excelDoc.Write("<Worksheet ss:Name=\"" + strTitle + "\">\r\n");
                excelDoc.Write("    <Table>\r\n");
                excelDoc.Write("        <Row>\r\n");
                for (int x = 0; x < dgv.Columns.Count; x++)
                {
                    excelDoc.Write("            <Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    excelDoc.Write(dgv.Columns[x].HeaderText);
                    excelDoc.Write("</Data></Cell>\r\n");
                }
                excelDoc.Write("        </Row>\r\n");

                // Alle Datensaetze im DataGridView durchgehen
                foreach (DataGridViewRow x in dgv.Rows)
                {
                    rowCount++;
                    // If the number of rows is > 64000 create a new page to continue output
                    if (rowCount == 64000)
                    {
                        rowCount = 0;
                        sheetCount++;
                        excelDoc.Write("    </Table>\r\n");
                        excelDoc.Write("</Worksheet>\r\n");
                        excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">\r\n");
                        excelDoc.Write("    <Table>\r\n");
                    }
                    excelDoc.Write("        <Row>\r\n"); //ID=" + rowCount + "
                    for (int y = 0; y < dgv.Columns.Count; y++)
                    {
                        System.Type rowType;
                        rowType = x.Cells[y].GetType();
                        switch (rowType.ToString())
                        {
                            case "System.String":
                            case "System.Windows.Forms.DataGridViewTextBoxCell":
                            case "System.Windows.Forms.DataGridViewCheckBoxCell":
                                string XMLstring = x.Cells[y].FormattedValue.ToString();
                                XMLstring = XMLstring.Trim();
                                XMLstring = XMLstring.Replace("&", "&amp;");
                                XMLstring = XMLstring.Replace(">", "&gt;");
                                XMLstring = XMLstring.Replace("<", "&lt;");
                                excelDoc.Write("            <Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(XMLstring);
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.DateTime":
                                //Excel has a specific Date Format of YYYY-MM-DD followed by  
                                //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                                //The Following Code puts the date stored in XMLDate 
                                //to the format above
                                DateTime XMLDate = (DateTime)x.Cells[y].FormattedValue;
                                string XMLDatetoString = ""; //Excel Converted Date
                                XMLDatetoString = XMLDate.Year.ToString() +
                                     "-" +
                                     (XMLDate.Month < 10 ? "0" +
                                     XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                     "-" +
                                     (XMLDate.Day < 10 ? "0" +
                                     XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                     "T" +
                                     (XMLDate.Hour < 10 ? "0" +
                                     XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                     ":" +
                                     (XMLDate.Minute < 10 ? "0" +
                                     XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                     ":" +
                                     (XMLDate.Second < 10 ? "0" +
                                     XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                     ".000";
                                excelDoc.Write("            <Cell ss:StyleID=\"DateLiteral\">" +
                                             "<Data ss:Type=\"DateTime\">");
                                excelDoc.Write(XMLDatetoString);
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.Boolean":
                                excelDoc.Write("            <Cell ss:StyleID=\"StringLiteral\">" +
                                            "<Data ss:Type=\"String\">");
                                excelDoc.Write(x.Cells[y].FormattedValue.ToString());
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                excelDoc.Write("            <Cell ss:StyleID=\"Integer\">" +
                                        "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x.Cells[y].FormattedValue.ToString());
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.Decimal":
                            case "System.Double":
                            case "System.Single":
                                excelDoc.Write("            <Cell ss:StyleID=\"Decimal\">" +
                                      "<Data ss:Type=\"Number\">");
                                excelDoc.Write(Common.ConvertNumber((Double)x.Cells[y].Value));
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            case "System.DBNull":
                                excelDoc.Write("            <Cell ss:StyleID=\"StringLiteral\">" +
                                      "<Data ss:Type=\"String\">");
                                excelDoc.Write("");
                                excelDoc.Write("</Data></Cell>\r\n");
                                break;
                            default:
                                throw (new Exception(rowType.ToString() + " not handled."));
                        }
                    }
                    excelDoc.Write("        </Row>\r\n");
                }
                excelDoc.Write("    </Table>\r\n");
                excelDoc.Write("</Worksheet>\r\n");
                excelDoc.Write(endExcelXML);
                excelDoc.Close();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                throw ex;
            }

        }
    }
}
