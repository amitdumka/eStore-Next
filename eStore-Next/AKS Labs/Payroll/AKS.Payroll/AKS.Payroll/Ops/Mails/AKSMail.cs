using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Payroll.Ops.Mails
{
    public static class MailConfig{
        public const string API = "";
        public static string From = "thearvindstoredumka@gmail.com";
        public static string User = "Store Manager";
    }

    //***********HNgFM6
    // L1tDhX5BkxsNfYZ9
    //SMTP Serversmtp-relay.sendinblue.com
    //Port587
    //Loginaprajitaretailsdumka @gmail.com
    //SMTP KEY NAME SMTP KEY VALUE  CREATED
    //Master passwordL1tDhX5BkxsNfYZ9


    public class Msg
    {
        public string Subject { get; set; }
        public string ToAddresses { get; set; }
        public string Names { get; set; }
        public string PlainContent { get; set; }
        public string HTMLContext { get; set; }
    }
    public class AKSMail
    {

        public async void SendSingle(Msg msg)
        {
            var sendGridClient = new SendGridClient(MailConfig.API);
            var from = new EmailAddress(MailConfig.From, MailConfig.User);
            var to = new EmailAddress(msg.ToAddresses, msg.Names);
            var mailMessage = MailHelper.CreateSingleEmail(from, to, msg.Subject, msg.PlainContent, msg.HTMLContext);
            await sendGridClient.SendEmailAsync(mailMessage);

        }
        public void SendBlue(Msg msg)
        {

        }
    }
}
