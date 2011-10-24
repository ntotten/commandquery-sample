using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.Samples.WindowsAzure.Storage;
using Common;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("WorkerRole1 entry point called", "Information");

            CloudStorageAccount.SetConfigurationSettingPublisher(
              (name, publisher) =>
              {
                  var connectionString = RoleEnvironment.GetConfigurationSettingValue(name);
                  publisher(connectionString);
              }
            );

            var account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            AzureQueue<UserRegistrationMessage> queue = new AzureQueue<UserRegistrationMessage>(account);
            queue.EnsureExist();

            AzureBlobContainer<RegistrationTokenEntity> registrationBlob = new AzureBlobContainer<RegistrationTokenEntity>(account, true);
            registrationBlob.EnsureExist();

            while (true)
            {
                try
                {
                    var message = queue.GetMessage();
                    if (message != null)
                    {
                        var entity = new RegistrationTokenEntity();
                        entity.RegistrationToken = (new Random()).Next().ToString();
                        registrationBlob.Save(message.ContainerId, entity);
                        queue.DeleteMessage(message);
                    }
                }
                catch { }
                Thread.Sleep(5000);
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
