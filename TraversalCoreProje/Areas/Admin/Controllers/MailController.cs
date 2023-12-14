using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "traversalcore2@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
            mimeMessage.From.Add(mailboxAddressTo);

            mimeMessage.Subject = mailRequest.Subject;
            //mimeMessage.Body = mailRequest.Body; bu şekilde hata verir alttaki şekilde kullanılmali bir entity olmalı string değil
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            //Gmail in güvenlik sçeeneğine gel 2 adımlı dogrulamayı açman gerek google un yeni mail gönderme işlemelrine göre bu yapılmalı tekrardan myaccount google.com a gel uygulama şifreleri kısmına gel bir uygulama ve cihazı seç isim ver ve verdiği şifre yi uygulama şifresini yaz 3. parti yazılımlardan mail gönderme işlemi bu şekilde yapılır gmail de
            client.Authenticate("traversalcore2@gmail.com", "gmail_uygulama_şifresi");
            client.Send(mimeMessage);
            client.Disconnect(true);

            return View();
        }
    }
}
