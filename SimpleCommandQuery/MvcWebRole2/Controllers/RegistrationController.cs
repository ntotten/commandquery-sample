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
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            AzureQueue<UserRegistrationMessage> queue = new AzureQueue<UserRegistrationMessage>(account);
            AzureBlobContainer<RegistrationTokenEntity> registrationBlob = new AzureBlobContainer<RegistrationTokenEntity>(account, true);

            var containerId = Guid.NewGuid().ToString();
            registrationBlob.Save(containerId, new RegistrationTokenEntity
            {
                RegistrationToken = null,
            });
            var blobContainer = registrationBlob.GetSharedAccessSignature(containerId, DateTime.Now.AddHours(1));

            queue.AddMessage(new UserRegistrationMessage
            {
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ContainerId = containerId,
            });

            return Json(new { container = blobContainer });
        }

    }
}
