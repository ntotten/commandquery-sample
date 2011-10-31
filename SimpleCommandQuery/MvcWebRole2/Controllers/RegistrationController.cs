using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebRole2.Models;
using Microsoft.WindowsAzure;
using Microsoft.Samples.WindowsAzure.Storage;
using Common;

namespace MvcWebRole2.Controllers
{
    public class RegistrationController : Controller
    {
        CloudStorageAccount account;

        public RegistrationController()
        {
            account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
        }


        //
        // GET: /Registration/

        public ActionResult Index()
        {
            ViewBag.RegistrationId = Guid.NewGuid().ToString();
            var url = account.BlobEndpoint.ToString();
            if (!url.EndsWith("/"))
            {
                url = url + "/";
            }
            ViewBag.TokenUrl = url + "registrationtoken/";
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            AzureQueue<UserRegistrationMessage> queue = new AzureQueue<UserRegistrationMessage>(account);
            queue.AddMessage(new UserRegistrationMessage
            {
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RegistratoinId = model.RegistrationId,
            });

            return new EmptyResult();
        }

    }
}
