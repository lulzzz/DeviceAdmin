﻿using LagoVista.CloudStorage.DocumentDB;
using LagoVista.Core.PlatformSupport;
using LagoVista.IoT.DeviceAdmin.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using LagoVista.IoT.DeviceAdmin.Interfaces.Repos;
using LagoVista.IoT.Logging.Loggers;

namespace LagoVista.IoT.DeviceAdmin.CloudRepos.Repos
{
    public class DeviceWorkflowRepo : DocumentDBRepoBase<DeviceWorkflow>, IDeviceWorkflowRepo
    {
        private bool _shouldConsolidateCollections;
        public DeviceWorkflowRepo(IDeviceRepoSettings repoSettings, IAdminLogger logger) : base(repoSettings.DeviceDocDbStorage.Uri, repoSettings.DeviceDocDbStorage.AccessKey, repoSettings.DeviceDocDbStorage.ResourceName, logger)
        {
            _shouldConsolidateCollections = repoSettings.ShouldConsolidateCollections;
        }

        protected override bool ShouldConsolidateCollections
        {
            get
            {
                return _shouldConsolidateCollections;
            }
        }

        public Task AddDeviceWorkflowAsync(DeviceWorkflow workflow)
        {
            return base.CreateDocumentAsync(workflow);
        }

        public Task UpdateDeviceWorkflowAsync(DeviceWorkflow workflow)
        {
            return base.UpsertDocumentAsync(workflow);
        }

        public Task<DeviceWorkflow> GetDeviceWorkflowAsync(String id)
        {
            return GetDocumentAsync(id);
        }

        public async Task<IEnumerable<DeviceWorkflowSummary>> GetDeviceWorkflowsForOrgAsync(string orgId)
        {
            var items = await base.QueryAsync(qry => qry.IsPublic == true || qry.OwnerOrganization.Id == orgId);

            return from item in items
                   select item.CreateSummary();
        }

        public async Task<bool> QueryKeyInUseAsync(string key, string orgId)
        {
            var items = await base.QueryAsync(attr => (attr.OwnerOrganization.Id == orgId || attr.IsPublic == true) && attr.Key == key);
            return items.Any();
        }

        public Task DeleteDeviceWorkflowAsync(string id)
        {
            return DeleteDocumentAsync(id);
        }
    }
}
