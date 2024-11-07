using System;
using System.Data;

namespace LabelDruck
{
    // In der Klasse wird geprüft ob wichtige Felder "richtig" beschrieben sind
    // Wenn das der Fall ist wird der Druckvorgang freigegeben
    // Wenn das nicht der Fall ist wird eine entsprechende Fehlermeldung zurückgegeben
    class DruckPrüfung
    {
        TlNrDB modWE = new TlNrDB();
        string retMess;
        int anzI;

        // Prüfung für den Bereich Rückstellmuster, hier muss die Anzahl geprüft werden
        // Außerdem muss der User richtig eingeben sein
        public string pruefRueckstell(string anz, int user, string rev, string teilNo) 
        {
            DataTable dtX = new DataTable();
            retMess = "Okay";
            dtX = modWE.myDataTableDB2(modWE.revisionen(teilNo));                   // Abfragen der Datenbank nach der Revision
            if ( dtX == null)                                                       // Wenn die Tabelle leer ist wird auf die 
            {                                                                       // MS-SQL Datenbank zurückgegriffen um die
               dtX = modWE.myDataTableMS(modWE.revisionenMS(teilNo));               // Revision abzufragen
            }
            for (int i = 0; i < dtX.Rows.Count; i++)                                // Jeden Eintrag in der Tabelle mit der übergebenen
            {                                                                       // Revision überprüfen und vergleichen
                if (rev == dtX.Rows[i]["Revision"].ToString())
                {                                                                   // Wenn sie gleich sind ist die Revision vorhanden
                }                                                                   
                else 
                {                                                                   // Falls das nicht der Fall ist wird überprüft ob 
                    if ( i == dtX.Rows.Count -1)                                    // die Tabelle schon durchscannt wurde
                    {
                        retMess = "Die Revision ist nicht vorhanden";               // Wenn ja wird die Fehlermeldung in retMess 
                        return retMess;                                             // eingetragen und zurück gegeben, die Methode wird
                    }                                                               // unterbrochen
                }
            }
            
            try
            {
                anzI = Convert.ToInt32(anz);                                        // Überprüfung ob in die TextBox für die Anzahl
            }                                                                       // eine Zahl eingegeben wurde oder beim Konvertieren
            catch                                                                   // ein Fehler auftritt der gecatcht wird
            {
                retMess = "FEHLERHAFTE EINGABE!!!";                                 // Rückgabe der Fehlermeldung
                return retMess; 
            }
            if (anzI < 1)                                                           // Wenn die Anzahl kleiner als 1 ist wird ebenfalls
            {                                                                       // ein Fehler gemeldet, denn 0 Labels dürfen nicht 
                retMess = "Achtung! Keine Anzahl eingegeben"; retMess += "\n Drucken nicht gestartet"; // gedruckt werden
            }
            if (user <= 0) { retMess = "FEHLERHAFTE EINGABE \nIM FELD BENUTZER"; return retMess; }
            return retMess; 
        }

        // Prüft ob in den Felder vom Tab Wareneingang fehleingaben sind, erkennt diese und
        // passt die Fehlermeldung dementsprechend an
        public string pruefWarenein(string name, string anz, string lotNr, string rev, string teilNr)
        {
            retMess = "Okay";
            if (name == "")                                                         // Überprüfung ob der Teilname eingetragen ist
            {                                                                       // Wenn das nicht der Fall ist wird sofort abgebrochen
                retMess = "Fehlerhafte Teilnummer\nBitte vorhandene Teilnummer eingeben"; // und eine Fehlermeldung ausgegeben
                return retMess;                                                     // Da der Teilname im direkten zusammenhang mit der
            }                                                                       // Teilnummer ist wird die Teilnummer als falsch gemeldet
            try { anzI = Convert.ToInt32(anz); }
            catch { retMess = "Anzahl muss Zahl sein!"; return retMess; }           // Konvertierung der Zahl, Fehlerausgabe bei Buchstabe
            if (anzI < 1)
            {
                retMess = "Achtung! Keine Anzahl eingegeben";                       // Wenn die Zahl kleiner als 1 ist wird gemeldet das
                return retMess;                                                     // keine Anzahl angegeben wurde
            }
            if ( rev == "")                                                         // Prüfung ob die Revisionsbox leer ist, wenn ja
            {                                                                       // dann wird eine Fehlermeldung ausgegeben
                retMess = "Revision fehlt!\nBitte Revision auswählen";              
                return retMess;
            }
            DataTable dtX = modWE.myDataTableDB2(modWE.revisionen(teilNr));         // abrufen der Revisionen aus der Datenbank
            if (dtX.Rows.Count < 1)                                                 // Wenn in der DataTable keine Werte enthalten sind
            {                                                                       // wird aus der MS-SQL Datenbank ausgelesen
                dtX = modWE.myDataTableMS(modWE.revisionenMS(teilNr));
            }
            if (dtX.Rows.Count < 1)                                                 // Wenn der Reihencount kleiner als 1 ist dann
            {                                                                       // ist die Teilnummer nicht vorhanden und es wird
                retMess = "Teilnummer ist nicht vorhanden\nBitte vorhandene Teilnummer auswählen!"; // ein Fehler gemeldet
                return retMess; 
            }
            for (int i = 0; i < dtX.Rows.Count; i++)                                // Für jeden Eintrag in der Table wird nun überprüft
            {                                                                       // ob die Revision vorhanden ist, wenn das nicht der
                if (rev != dtX.Rows[i]["Revision"].ToString())                      // Fall ist wird die entsprechende Fehlermeldung
                {                                                                   // ausgegeben
                    retMess = "Revision ist nicht vorhanden!\nBitte existierende Revision eingeben";
                    if (i + 1 == dtX.Rows.Count)
                    { return retMess; }
                }
                else { i = dtX.Rows.Count; }                                        // Beenden der Schleife wenn die Revision gefunden
                                                                                    // wurde
            }
            retMess = "Okay";

            if (lotNr == "")                                                        // Überprüfen ob das Feld für die LOT-Nummer leer ist
            {                                                                       // Sofern das Feld leer ist wird überprüft ob es 
                if (modWE.myDataTableDB2(modWE.SQLLotNR(teilNr)) == null)           // eine LOT-Nummer in der Datenbank gibt
                {                                                                   // Wenn es eine gibt wird der Fehler gemeldet, dass
                    retMess = "Teilnummer oder Lotnummer sind fehlerhaft\nBitte Werte auswählen die existieren";// der User eine existierende
                    return retMess;                                                 // LOT-Nummer auswählen soll
                }
                return retMess;
            }
            return retMess;
        }

        // Prüft ob in den Feldern vom Tab IQC fehleingaben sind, erkennt diese und 
        // passt die Fehlermeldung dementsprechend an
        public string pruefIQC(string id, string anz)
        {
            retMess = "Okay";
            try
            {
                anzI = Convert.ToInt32(id);                                         // Überprüfen ob die ID korrekt ist
                
            }
            catch
            {                                                                       // Wenn die ID nicht konvertiert werden kann
                retMess = "Fehlerhafte Eingabe!!!\nDie ID muss eine Zahl und vorhanden sein!";// gibt es einen Fehler
                return retMess;
            }
            if (modWE.myValueMS(modWE.msQCID(id)) == null)                          // Ist die ID korrekt wird überprüft ob die ID
            {                                                                       // vorhanden ist, wenn sie nicht vorhanden ist wird eine
                retMess = "Fehlerhafte Eingabe!\nDie ID ist nicht vorhanden, bitte neu eingeben";// Fehlermeldung ausgegeben
                return retMess;
            }
            try
            {                                                                       // Überprüfen der Anzahl
                anzI = Convert.ToInt32(anz);
            }
            catch
            {
                retMess = "Fehlerhafte Eingabe!!!\nDie Anzahl an drucken muss eine Zahl sein!";
                return retMess;
            }
            return retMess;
        }
    }
}
