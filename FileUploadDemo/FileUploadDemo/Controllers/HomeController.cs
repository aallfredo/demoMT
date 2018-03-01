using MassTransit;
using Messages.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUploadDemo.Controllers
{
    public class HomeController : Controller
    {
        private IBusControl _bus;

        public HomeController()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });               
            });
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var newFileName = fileName + Guid.NewGuid().ToString().Replace("-", "");
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), newFileName);
                //saving this for logging purposes.
                file.SaveAs(path);
                // convert stream to string
                StreamReader reader = new StreamReader(file.InputStream);
                string text = reader.ReadToEnd();
                if (String.IsNullOrWhiteSpace(text))
                {
                    throw new HttpException(500, "Invalid File");
                }
                else
                {
                    var rows = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    int countOfPublishes = 0;
                    int numberOfRows = 0;
                    foreach (var unprocessedRow in rows)
                    {
                        if (!String.IsNullOrWhiteSpace(unprocessedRow) && numberOfRows != 0)
                        {
                            // We Publish a message saying to process the row.
                            _bus.Publish<IUnProcessedFileRowReceived>(new UnProcessedFileRowReceived(newFileName, unprocessedRow));
                            countOfPublishes++;                            
                        }
                        numberOfRows++;
                    }
                    _bus.Publish<IProjectRowsPublished>(new ProjectRowsPublished(newFileName, countOfPublishes));
                }

                return Json(new { processingFilename = newFileName }); ;
            }
            throw new HttpException(500, "Invalid File");
        }

      
    }
}