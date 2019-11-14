using System.Web.Mvc;
using SAT.UI.MVC.Models;
using System.Net.Mail;
using System.Net;
using System;

namespace SAT.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact( ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                string message = $"{cvm.Name} has sent the following message from SAT: <br/>" + $"{cvm.Message} <strong>from the email address:</strong> {cvm.Email}";

                MailMessage m = new MailMessage("admin@kendra-allen.com", "kendradallen@outlook.com", cvm.Subject, message);
         

                m.IsBodyHtml = true;
                m.Priority = MailPriority.High;
                m.ReplyToList.Add(cvm.Email);

                SmtpClient client = new SmtpClient("mail.kendra-allen.com");
 

                client.Credentials = new NetworkCredential("admin@kendra-allen.com", "Who*Done*It64");

           

                try
                {
                    client.Send(m);
                }
                catch (Exception ex)
                {
                    ViewBag.CustomerMessage = "We are sorry. Your request could not be completed at this time. Please try again later. Error Message: <br/> " + ex.StackTrace;
                    return View(cvm);
                } 
            }
            return View("EmailConfirmation", cvm);

        }
    }
}
