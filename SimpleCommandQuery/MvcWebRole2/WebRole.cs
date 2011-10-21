using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.Samples.WindowsAzure.Storage;
using Common;

namespace MvcWebRole2
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

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

            AzureBlobContainer<RegistrationTokenEntity> registrationBlob = new AzureBlobContainer<RegistrationTokenEntity>(account);
            registrationBlob.EnsureExist();

            return base.OnStart();
        }
    }
}
