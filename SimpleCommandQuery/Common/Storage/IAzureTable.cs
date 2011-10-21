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
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.WindowsAzure.StorageClient;

    public interface IAzureTable<TEntity> where TEntity : TableServiceEntity
    {
        IQueryable<TEntity> Query { get; }

        bool CreateIfNotExist();

        bool DeleteIfExist();

        void AddEntity(TEntity obj);

        void AddEntity(IEnumerable<TEntity> objs);

        void AddOrUpdateEntity(TEntity obj);

        void AddOrUpdateEntity(IEnumerable<TEntity> objs);

        void DeleteEntity(TEntity obj);

        void DeleteEntity(IEnumerable<TEntity> objs);
    }
}