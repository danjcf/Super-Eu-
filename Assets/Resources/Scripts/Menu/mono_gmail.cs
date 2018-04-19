using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class mono_gmail : MonoBehaviour {

	public void SendMail (string mailTo)
	{
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("danjcf@gmail.com");
		mail.To.Add(mailTo);
		mail.Subject = "Super Eu! - Conta Criada";
		mail.Body = "Conta Criada com sucesso no Super Eu!";

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("danjcf@gmail.com", "daniel01f") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
		smtpServer.Send(mail);
		Debug.Log("success");

	}
}