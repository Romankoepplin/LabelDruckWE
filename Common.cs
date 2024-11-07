using System;
using System.Globalization;

namespace LabelDruck
{
    public class Common
    {
        public Common()
        {
        }

        public static string ConvertNumber(Double dValue)
        {
            // Den aktuell eingestellten Formatprovider kopieren
            NumberFormatInfo nif = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();

            // Dezimaltrenner ist '.', Tausendertrenner gibt es keine
            nif.NumberDecimalSeparator = ".";
            nif.NumberGroupSeparator = "";

            // Rueckgabe numerisch mit 2 Nachkommastellen
            return dValue.ToString("N2", nif);
        }

        public static string ConvertNumberRegional(Double dValue)
        {
            // Den aktuell eingestellten Formatprovider kopieren
            NumberFormatInfo nif = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();

            // Tausendertrenner gibt es keine
            nif.NumberGroupSeparator = "";

            // Rueckgabe numerisch mit 2 Nachkommastellen
            return dValue.ToString("N2", nif);
        }
    }
}