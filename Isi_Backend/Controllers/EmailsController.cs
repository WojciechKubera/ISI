using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Isi_Backend.Data;
using Isi_Backend.Models;
using System.Net;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Office.CustomUI;
using System.IO;
using Newtonsoft.Json.Linq;
using CsvHelper;
using System.Globalization;
using System.Data;
using CsvHelper.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.DependencyInjection;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;
using Google.Apis.Util;
using MailKit.Security;
using MailKit.Net.Imap;

namespace Isi_Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : Controller
    {
        private readonly Isi_BackendContext _context;
       
        public EmailsController()
        {
            var serviceProvider = (ServiceProvider)ServicesHolder.serviceProvider;
            _context = (Isi_BackendContext)serviceProvider.GetService(typeof(Isi_BackendContext));

        }

        public async void Send()
        {
           /* const string GMailAccount = "isitest211@gmail.com";

            var clientSecrets = new ClientSecrets
            {
                ClientId = "153693562140-aa9d9ci93q1abisiea3hgdb9nc17l8bj.apps.googleusercontent.com",
                ClientSecret = "_c_97tEjJQ3tQ_g3GLyCMewz"
            };

            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore("CredentialCacheFolder", false),
                Scopes = new[] { "https://mail.google.com/" },
                ClientSecrets = clientSecrets
            });

            // Note: For a web app, you'll want to use AuthorizationCodeWebApp instead.
            var codeReceiver = new LocalServerCodeReceiver();
            var authCode = new AuthorizationCodeInstalledApp(codeFlow, codeReceiver);

            var credential = await authCode.AuthorizeAsync(GMailAccount, CancellationToken.None);

            if (credential.Token.IsExpired(SystemClock.Default))
                await credential.RefreshTokenAsync(CancellationToken.None);

            var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);
           */
           
            var Emails = _context.Emails;
            var Statistics = _context.Statistics;
            var rec = Emails.Select(m => m.email).First().ToString();

            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Admin",
            "lexsiuo@gmail.com");
            message.From.Add(from);
                              

           MailboxAddress to = new MailboxAddress("Subscribent",
           rec);
           message.To.Add(to);

           message.Subject = "Daily Messege";


            if (CheckCondtion(int.Parse(Emails.Select(m => m.condition).FirstOrDefault().ToString()), Emails.Select(m => m.countryCondition).FirstOrDefault().ToString()))
            {

                message.Body = new TextPart("plain")
                {
                    Text = $"{Emails.Select(m => m.countryCondition).FirstOrDefault().ToString()}, new cases  = {Statistics.Select(m => m.NewConfirmed).FirstOrDefault().ToString()}"
                };

                /*using (var client = new ImapClient())
                {
                    await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(oauth2);
                    client.
                    await client.DisconnectAsync(true);
                }*/
                using (var client = new SmtpClient())
                {
                    
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("isitest211@gmail.com", "Testtest1!");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }

        }

        public bool CheckCondtion(int con, string contruCond)
        {
            var Emails = _context.Emails;
            var Statistics = _context.Statistics;
            var y = Statistics.Where(m => m.CountryCode == contruCond);
            var t = y.Select(m => m.NewConfirmed).FirstOrDefault().ToString();
            if (con < int.Parse(t) ){
                return true;
            }return false;
        }
       
    }
}
