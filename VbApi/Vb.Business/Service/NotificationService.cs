using System.Net.Mail;

namespace Vb.Business.Service;

public interface INotificationService
{
    public void SendEmail(string Subject, string Email, String Content);
}

public class NotificationService : INotificationService
{
    public void SendEmail(string Subject, string Email, string Content)
    {
        SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");

        mySmtpClient.UseDefaultCredentials = false;
        System.Net.NetworkCredential basicAuthenticationInfo = new
            System.Net.NetworkCredential("username", "password");
        mySmtpClient.Credentials = basicAuthenticationInfo;

        MailAddress from = new MailAddress("test@example.com", "TestFromName");
        MailAddress to = new MailAddress("test2@example.com", "TestToName");
        MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
        MailAddress replyTo = new MailAddress("reply@example.com");
        myMail.ReplyToList.Add(replyTo);

        myMail.Subject = "Test message";
        myMail.SubjectEncoding = System.Text.Encoding.UTF8;

        myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
        myMail.BodyEncoding = System.Text.Encoding.UTF8;
        myMail.IsBodyHtml = true;

        mySmtpClient.Send(myMail);
    }
}