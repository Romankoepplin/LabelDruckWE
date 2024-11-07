using System;
using System.Net.Mail;

namespace LabelDruck
{
    class EMail
    {
        public void Sendmail(string Emailtext, string Header, string SenderFrom, string To, string ToCC, string ToBCC, bool HtmlBody)
        {

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                MailAddress fromAddress = new MailAddress(Check_Umlaute(SenderFrom));

                // You can specify the host name or ipaddress of your server
                smtpClient.Host = global::LabelDruck.Properties.Settings.Default.EmailHost;
                //Default port will be 25
                smtpClient.Port = 25;

                //From address will be given as a MailAddress Object
                message.From = fromAddress;

                // To address collection of MailAddress
                message.To.Add(Check_Umlaute(To));
                message.Subject = Header;

                // CC and BCC optional
                // MailAddressCollection class is used to send the email to various users
                // You can specify Address as new MailAddress("admin1@yoursite.com")
                if (ToCC != "")
                {
                    message.CC.Add(Check_Umlaute(ToCC));
                }
                if (ToBCC != "")
                {
                    message.Bcc.Add(Check_Umlaute(ToBCC));
                }



                //Body can be Html or text format
                //Specify true if it  is html message
                message.IsBodyHtml = HtmlBody;

                // Message body content
                message.Body = Emailtext;

                // Send SMTP mail
                smtpClient.Send(message);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + System.Environment.NewLine + Emailtext + System.Environment.NewLine + Header + System.Environment.NewLine + To + System.Environment.NewLine + System.Environment.NewLine);
            }
        }

        private string Check_Umlaute(string Text)
        {
            string text = Text;

            text = text.ToLower().Replace("ö", "oe").Replace("ä", "ae").Replace("ü", "ue").Replace("ß", "ss").Replace(" ", "");

            return text;

        }
    }
}
