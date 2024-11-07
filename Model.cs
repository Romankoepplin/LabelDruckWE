using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LabelDruck
{
    public class TlNrDB
    {

        public TlNrDB()
        {
            
        }

        // Connectionaufbau für DataTables aus der DB2
        public DataTable myDataTableDB2(String sqlBefA)
        {
            DataTable dt = new DataTable();
            
            try
            {
                OleDbConnection con = new OleDbConnection(global::LabelDruck.Properties.Settings.Default.constrAS400);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(sqlBefA, con);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                dt = null;
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        // Connectionaufbau für einzelne Werte aus der DB2
        public String myValueDB2(String sqlBefA)
        {
            String val = "";

            try
            {
                OleDbConnection con = new OleDbConnection(global::LabelDruck.Properties.Settings.Default.constrAS400);
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sqlBefA, con);
                val = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch
            {
                val = null;
            }
            return val;
        }

        // Connectionaufbau für DataTables aus der MSDB
        public DataTable myDataTableMS(String sqlBefA)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection con = new SqlConnection(global::LabelDruck.Properties.Settings.Default.constrMSsql);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlBefA, con);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                //dt = null;
            }
            return dt;
        }

        // Connectionaufbau für einzelne Werte aus der MSDB
        public String myValueMS(String sqlBefA)
        {
            String val = "";

            try
            {
                SqlConnection con = new SqlConnection(global::LabelDruck.Properties.Settings.Default.constrMSsql);
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlBefA, con);
                val = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch
            {
                val = null;
            }
            return val;
        }

        public int myIntValueMS(String sqlBefA)
        {
            int val = 0;

            try
            {
                SqlConnection con = new SqlConnection(global::LabelDruck.Properties.Settings.Default.constrMSsql);
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlBefA, con);
                //val = (int)cmd.ExecuteScalar();
                int.TryParse(cmd.ExecuteScalar().ToString(), out val);
                con.Close();
            }
            catch
            {
                val = 0;
            }
            return val;
        }


        public void myVoidMS(String sqlBefA)
        {
            try 
            {
                SqlConnection con = new SqlConnection(global::LabelDruck.Properties.Settings.Default.constrMSsql);
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlBefA, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verarbeitungsfehler, Kleber konnte nicht abgemeldet werden!" +"\n" + ex.ToString());
            }
        }
        
        // Beginn der verschiedenen SQL-Strings die für die unterschiedlichen Abfragen gebraucht werden
        //
        //

        public string SQLLotNR(String TeilNr)
        {
            String LotNr = "";
            LotNr += "SELECT TAMLTN AS Number ";
            LotNr += "FROM PBA1TD.ZET010P ";
            LotNr += "INNER JOIN PBA1TD.ZGT010P ON T1DIVI = TADIVI AND T1RCNO = TARCNO AND T1OPEC = TAOPEC ";
            LotNr += "WHERE T1DIVI = '8A' AND T1ITNO LIKE '" + TeilNr + "%' ";
            LotNr += "AND (T1TRCD='RM' OR T1TRCD='RX' OR T1TRCD='RW' OR T1TRCD='RI') AND T1SQTY > 0 ";
            //LotNr += "GROUP BY TAMLTN, TALUYY, TALUMM, TALUDD ";
            //LotNr += "ORDER BY  TALUYY DESC, TALUMM DESC, TALUDD DESC ";
            LotNr += "UNION ALL ";
            LotNr += "SELECT DISTINCT T1MLOT from sapgwd.ico110P ";
            LotNr += "where F3ITDS like '" + TeilNr + "%' ";
 return LotNr;
        }

        public string SQLLotWEDate(String LotNr)
        {
            String WeDate = "";
            WeDate += "SELECT CONCAT(TALUDD, CONCAT('.', CONCAT(TALUMM, CONCAT('.',TALUYY)))) AS WEDATE ";
            WeDate += "FROM PBA1TD.ZET010P ";
            WeDate += "INNER JOIN PBA1TD.ZGT010P ON T1DIVI = TADIVI AND T1RCNO = TARCNO AND T1OPEC = TAOPEC ";
            WeDate += "WHERE T1DIVI = '8A' AND TAMLTN LIKE '" + LotNr + "%' ";
            WeDate += "AND (T1TRCD='RM' OR T1TRCD='RX' OR T1TRCD='RW' OR T1TRCD='RI') AND T1SQTY > 0 ";
            //LotNr += "GROUP BY TAMLTN, TALUYY, TALUMM, TALUDD ";
            //WeDate += "ORDER BY  TALUYY DESC, TALUMM DESC, TALUDD DESC ";
            return WeDate;
        }

        public string SQLInfo(String TeilNr)
        {
            String SQLInfo = "";
            SQLInfo += "SELECT N4ITNM ";
            SQLInfo += "FROM PBA1MD.KEN040P ";
            SQLInfo += "WHERE N4DIVI = '8A' AND N4ITNO LIKE '" + TeilNr + "%'";
            return SQLInfo;
        }
        public string SQLInfo2(String TeilNr)
        {
            String SQLInfo = "";
            SQLInfo += "SELECT N4SPEC ";
            SQLInfo += "FROM PBA1MD.KEN040P ";
            SQLInfo += "WHERE N4DIVI = '8A' AND N4ITNO LIKE '" + TeilNr + "%'";
            return SQLInfo;
        }
        public string SQLInfo3(String TeilNr)
        {
            String SQLInfo = "";
            SQLInfo += "SELECT DISTINCT T1MLOT ";
            SQLInfo += "FROM PBA1TD.ZET010P ";
            SQLInfo += "WHERE T1DIVI = '8A' AND T1ITNO LIKE '" + TeilNr + "%'";
            return SQLInfo;
        }

        public string SQLMSInfo(String TeilNr)
        {
            String SQLInfo = "";
            SQLInfo += "SELECT [Description] ";
            SQLInfo += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] ";
            SQLInfo += "WHERE [Number] LIKE '" + TeilNr + "%'";
            return SQLInfo;
        }
        public string SQLMSInfo2(String TeilNr)
        {
            String SQLInfo = "";
            SQLInfo += "SELECT [Description2] ";
            SQLInfo += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] ";
            SQLInfo += "WHERE [Number] LIKE '" + TeilNr + "%'";
            return SQLInfo;
        }
        public string SQLMSInfo3(String TeilNr)
        {
            String SQLInfo = "";
            SQLInfo += "SELECT DISTINCT [Remark] ";
            SQLInfo += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] ";
            SQLInfo += "WHERE [Number] LIKE '" + TeilNr + "%'";
            return SQLInfo;
        }

        public string SQLrueckTn()
        {
            String SQLms = "";
            SQLms += "SELECT Number ";
            SQLms += "FROM [_Teilestamm_PMDM].[dbo].[vwDrawingsNrList] ";
            return SQLms;
        }
        public string SQLrueckUser()
        {
            String SQLms = "";
            SQLms += "SELECT [VollständigerName] AS Name ";
            SQLms += "FROM [__AdminDB_PMDM].[dbo].[User] ";
            SQLms += "WHERE [aktuell] = 'TRUE' AND [LoginName] LIKE 'GV%' ";
            SQLms += "ORDER BY [VollständigerName]";
            return SQLms;
        }
        
        public string revisionen(string TeilNr)
        {
            String SQLms = "";
            SQLms += "SELECT DISTINCT SUBSTR(T1ITNO, 13, 1) AS Revision ";
            SQLms += "FROM PBA1TD.ZET010P ";
            SQLms += "WHERE T1ITNO LIKE '" + TeilNr + "%' ";
            SQLms += "ORDER BY SUBSTR(T1ITNO, 13, 1)";
            return SQLms;
        }
        //public string revisionenX(string TeilNr)
        //{
        //    String SQLms = "";
        //    SQLms += "SELECT DISTINCT SUBSTR(T1ITNO, 13, 2) AS Revision ";
        //    SQLms += "FROM PBA1TD.ZET010P ";
        //    SQLms += "WHERE T1ITNO LIKE '" + TeilNr + "%' ";
        //    SQLms += "ORDER BY SUBSTR(T1ITNO, 13, 1)";
        //    return SQLms;
        //}
        public string revisionenMS(string TeilNr)
        {
            String SQLms = "";
            SQLms += "SELECT DISTINCT SUBSTRING([Number], 13, 1) AS Revision ";
            SQLms += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] ";
            SQLms += "WHERE [Number] LIKE '" + TeilNr + "%' ";
            SQLms += "ORDER BY SUBSTRING([Number], 13, 1)";
            return SQLms;
        }
        //public string revisionenMSX(string TeilNr)
        //{
        //    String SQLms = "";
        //    SQLms += "SELECT DISTINCT SUBSTRING([Number], 13, 2) AS Revision ";
        //    SQLms += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] ";
        //    SQLms += "WHERE [Number] LIKE '" + TeilNr + "%' ";
        //    SQLms += "ORDER BY SUBSTRING([Number], 13, 1)";
        //    return SQLms;
        //}
        public string kleber()
        {
            String SQL = "";
            SQL += "SELECT [Number],[Kleber_ID],[Description],([Gebindegr] + ' ' + [Einheit]) AS Menge";
            SQL += ",[Eingangsdatum],[Verfallsdatum] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] AS A ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] AS B ON A.[DRW_ID] = B.[DRW_ID] ";
            SQL += "WHERE [Number] LIKE '9962%' AND B.[ImBestand] = 'TRUE' ";
            SQL += "ORDER BY [Number]";
            return SQL;
        }
        public string filterTLNO()
        {
            String SQL = "";
            SQL += "SELECT DISTINCT [Number] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] AS A ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] AS B ON A.[DRW_ID] = B.[DRW_ID] ";
            SQL += "WHERE [Number] LIKE '9962%' AND B.[ImBestand] = 'TRUE' ";
            SQL += "ORDER BY [Number]";
            return SQL;
        }
        public string filterBez()
        {
            String SQL = "";
            SQL += "SELECT DISTINCT [Description] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] AS A ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] AS B ON A.[DRW_ID] = B.[DRW_ID] ";
            SQL += "WHERE [Number] LIKE '9962%' AND B.[ImBestand] = 'TRUE' ";
            SQL += "ORDER BY [Description]";
            return SQL;
        }
        public string weKleberEinDat(string TeilNr)
        {
            String SQL = "";
            SQL += "SELECT DISTINCT [Eingangsdatum] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] AS A ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] AS B ON A.[DRW_ID] = B.[DRW_ID] ";
            SQL += "WHERE [Number] LIKE '" + TeilNr + "%' AND B.[ImBestand] = 'TRUE' ";
            return SQL;
        }
        public string weKleberAblDat(string TeilNr)
        {
            String SQL = "";
            SQL += "SELECT DISTINCT [Verfallsdatum] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] AS A ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] AS B ON A.[DRW_ID] = B.[DRW_ID] ";
            SQL += "WHERE [Number] LIKE '" + TeilNr + "%' AND B.[ImBestand] = 'TRUE' ";
            return SQL;
        }
        public string weKleberGeb(string TeilNr)
        {
            String SQL = "";
            SQL += "SELECT ([Gebindegr] + ' ' + [Einheit]) AS Menge ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] AS A ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] AS B ON A.[DRW_ID] = B.[DRW_ID] ";
            SQL += "WHERE [Number] LIKE '" + TeilNr + "%' AND B.[ImBestand] = 'TRUE' ";
            return SQL;
        }
        public string kleberAbm()
        {
            String SQL = "";
            SQL += "SELECT [Kleber_ID] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] AS A ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] AS B ON A.[DRW_ID] = B.[DRW_ID] ";
            SQL += "WHERE [Number] LIKE '9962%' AND B.[ImBestand] = 'TRUE' ";
            return SQL;
        }
        public string msQCID(string qcid)
        {
            String SQL = "";
            SQL += "SELECT [QC_ID] ";
            SQL += "FROM [_Datenstamm_PMDM].[dbo].[IQC_Lieferantenbewertung_QC] AS A ";
            SQL += "INNER JOIN [_Datenstamm_PMDM].[dbo].[EK_Lieferantendaten] AS B ON A.[LID] = B.[LID] ";
            SQL += "INNER JOIN [__AdminDB_PMDM].[dbo].[User] AS C ON A.[Prüfer] = C.[UID] ";
            SQL += "WHERE [QC_ID] LIKE '" + qcid + "%'";
            return SQL;
        }

        public string msIQC_IDexists(string qcid)
        {
            String SQL = "";
            SQL += "SELECT COUNT(*) ";
            SQL += "FROM [_Datenstamm_PMDM].[dbo].[IQC_Lieferantenbewertung_QC] ";
            SQL += "WHERE [QC_ID]=" + qcid + "";
            return SQL;
        }

        public string msGet_DRW(string qcid)
        {
            String SQL = "";
            SQL += "SELECT [Teilenummer] ";
            SQL += "FROM [_Datenstamm_PMDM].[dbo].[IQC_Lieferantenbewertung_QC] ";
            SQL += "WHERE [QC_ID] LIKE '" + qcid + "%'";
            return SQL;
        }

        public string kleberAbmelden(string klebID)
        {
            String SQL = "";
            SQL += "UPDATE [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] ";
            SQL += "SET [ImBestand] = 0 , [Ausgabedatum] = '" + System.DateTime.Now.Date.ToString().Substring(0,10) + "' ";
            SQL += "WHERE [ImBestand] <> 0 AND [Kleber_ID] = " + klebID + " ";
            return SQL;
        }
        public string gefahrstoffInfo(string teilNo)
        {
            
            
            String SQL = "";
            SQL += "SELECT [Schutzstufe],E.[Kleber_ID], A.[GefSTV_ID], A.[DRW_ID], [Abf_SCHL_NR], [Abf_SCHL_NR_VP], [Schutzstufe_GefSTV] ";
            SQL += "FROM [Umwelt].[dbo].[Gefahrstoffe] AS A ";
            SQL += "INNER JOIN [Umwelt].[dbo].[VK_Gefahrstoff_R-Sätze] AS B ON A.[GefSTV_ID] = B.[Gefstv_ID] ";
            SQL += "INNER JOIN [Umwelt].[dbo].[R-Sätze] AS C ON B.[R_ID] = C.[R_ID] ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[Drawings] AS D ON A.[DRW_ID] = D.[DRW_ID] ";
            SQL += "INNER JOIN [_Teilestamm_PMDM].[dbo].[VK_Drawing_Kleber] AS E ON A.[DRW_ID] = E.[DRW_ID] ";
            SQL += "WHERE D.[Number] LIKE '" + teilNo + "%' ";

            

            //SQL += "AND [ImBestand] = 1 ";

            // Änderung 
            // SELECT E.Kleber_ID, A.GefSTV_ID, A.DRW_ID, Abf_SCHL_NR, Abf_SCHL_NR_VP, Schutzstufe_GefSTV 
            //FROM Umwelt.dbo.Gefahrstoffe AS A INNER JOIN _Teilestamm_PMDM.dbo.Drawings AS D ON A.DRW_ID = D.DRW_ID INNER JOIN _Teilestamm_PMDM.dbo.VK_Drawing_Kleber AS E ON A.DRW_ID = E.DRW_ID WHERE D.Number LIKE '996045500078%' 
            



            return SQL;
        }
        
        public string gefahrstoffFreigabe(string teilNO)
        {
            String SQL = "";
            SQL += "SELECT [GFreigegeben] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] ";
            if (teilNO.Length > 12)
            { SQL += "WHERE [Number] LIKE '" + teilNO.Substring(1, 12) + "%' "; }
            else
            { SQL += "WHERE [Number] LIKE '" + teilNO + "%' "; }
            return SQL;
        }

        public string getArticleStatus(string teilNO)
        {
            String SQL = "";
            SQL += "SELECT [NFT_ZUST] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[vwDrawings_Profile] ";
            SQL += "WHERE [NFT_STL9] = '" + teilNO + "' ";
            return SQL;
        }

        public string getArticleStatus2(string teilNO)
        {
            
            string sql = "";
            sql += "SELECT ProE_Release FROM  ";
            sql += "[dbo].[Drawings] ";
            //sql += "WHERE [number] = 'A19-322816-2C' ";
            sql += "WHERE [number] = '" + teilNO + "' ";
            return sql;
        }




        public string getHaltbarkeit(string teilNO)
        {
            String SQL = "";
            SQL += "SELECT DISTINCT [Haltbarkeit_Monate] ";
            SQL += "FROM [_Teilestamm_PMDM].[dbo].[Drawings] ";
            SQL += "WHERE [Number] LIKE '" + teilNO + "%' ";
            return SQL;
        }
    }
}
