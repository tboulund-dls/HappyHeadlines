// See https://aka.ms/new-console-template for more information

using System.Net.Mail;

Console.WriteLine("Hello, World!");

var smtp = new SmtpClient("mailpit", 1025);
var mail = new MailMessage("test@hello.dk", "hello@world.com");
mail.Subject = "Hello, World!";
mail.IsBodyHtml = true;
mail.Body = "Hello, <b>World</b>!";
smtp.Send(mail);