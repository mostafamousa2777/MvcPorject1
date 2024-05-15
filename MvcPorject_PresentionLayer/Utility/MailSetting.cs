using DataAccess_Layer.models;
using System.Net;
using System.Net.Mail;

namespace MvcPorject_PresentionLayer.Utility
{
	public  static class MailSetting
	{
		public static void SendEmail(Email email) {
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mostafamousa287@gmail.com", "vhjbcrktzilxpkwt");
			client.Send("mostafamousa287@gmail.com",email.Recipient,email.Subject,email.Body);


		}
	}
}
