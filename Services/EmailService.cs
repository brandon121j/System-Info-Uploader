using System;
using Microsoft.Exchange.WebServices.Data;

namespace SystemInfoGrabber.Services;

public class EmailService
{
    private readonly ExchangeService _service;

    public EmailService()
    {
        _service = new ExchangeService(ExchangeVersion.Exchange2013_SP1)
        {
            UseDefaultCredentials = true
        }; // or your Exchange version
        _service.AutodiscoverUrl(Environment.UserName + "@email.com", RedirectionUrlValidationCallback);
    }

    private bool RedirectionUrlValidationCallback(string redirectionUrl)
    {
        var result = false;
        var redirectionUri = new Uri(redirectionUrl);
        if (redirectionUri.Scheme == "https") result = true;
        return result;
    }

    public void SendEmail(string toEmail, string subject, string body)
    {
        var email = new EmailMessage(_service);
        email.ToRecipients.Add(toEmail);
        email.Subject = subject;
        email.Body = new MessageBody(BodyType.HTML, body); // Set the body type to HTML here
        email.Send();
    }
}
