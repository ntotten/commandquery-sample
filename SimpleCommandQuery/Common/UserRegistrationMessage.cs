// -----------------------------------------------------------------------
// <copyright file="UserRegistrationCommand.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Samples.WindowsAzure.Storage;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserRegistrationMessage : AzureQueueMessage
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContainerId { get; set; }

    }
}
