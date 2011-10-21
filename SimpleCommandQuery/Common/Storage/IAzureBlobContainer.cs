// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace Microsoft.Samples.WindowsAzure.Storage
{
    using System;
    using System.IO;

    public interface IAzureBlobContainer<T>
    {
        void EnsureExist();

        void EnsureExist(bool publicContainer);

        void Save(string objId, T obj);

        string SaveFile(string objId, byte[] content, string contentType);

        string SaveFile(string objId, byte[] content, string contentType, TimeSpan timeOut);

        T Get(string objId);

        Stream GetFile(string objId);

        void Delete(string objId);

        string GetSharedAccessSignature(string objId, DateTime expiryTime);

        void DeleteContainer();
    }
}