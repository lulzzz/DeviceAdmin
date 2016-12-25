﻿using LagoVista.Core.Models;
using LagoVista.IoT.DeviceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = LagoVista.IoT.DeviceAdmin.Models;

namespace LagoVista.IoT.DeviceAdmin.Interfaces.Managers
{
    public interface IDeviceAdminManager
    {
        Task AddDeviceConfigurationAsync(DeviceConfiguration deviceConfiguration, EntityHeader org, EntityHeader user);
        Task AddSharedActionAsync(SharedAction sharedAction, EntityHeader org, EntityHeader user);
        Task AddSharedAttributeAsync(SharedAttribute sharedAttribute, EntityHeader org, EntityHeader user);
        Task AddStateMachineAsync(StateMachine attribute, EntityHeader org, EntityHeader user);
        Task AddUnitSetAsync(AttributeUnitSet unitSet, EntityHeader org, EntityHeader user);

        Task<IEnumerable<DeviceConfigurationSummary>> GetDeviceConfigurationsForOrgsAsync(string orgId);
        Task<IEnumerable<SharedActionSummary>> GetSharedActionsForOrgAsync(string orgId);
        Task<IEnumerable<SharedAttributeSummary>> GetSharedAttributesForOrgAsync(string orgId);
        Task<IEnumerable<StateMachineSummary>> GetStateMachinesForOrgAsync(string orgId);
        Task<IEnumerable<AttributeUnitSetSummary>> GetUnitSetsForOrgAsync(string orgId);

        Task<bool> QuerySharedActionKeyInUseAsync(string key, string orgId);
        Task<bool> QueryAttributeUnitSetKeyInUseAsync(string key, string orgId);
        Task<bool> QuerySharedAttributeKeyInUseAsync(string key, string orgId);
        Task<bool> QueryStateMachineKeyInUseAsync(string key, string orgId);

        Task<StateMachine> GetStateMachineAsync(string id, EntityHeader org);
        Task<SharedAction> GetSharedActionAsync(string id, EntityHeader org);
        Task<SharedAttribute> GetSharedAttributeAsync(string id, EntityHeader org);
        Task<AttributeUnitSet> GetAttributeUnitSetAsync(string id, EntityHeader org);
        Task<DeviceConfiguration> GetDeviceConfigurationAsync(string id, EntityHeader org);

        Task UpdateDeviceConfigurationAsync(DeviceConfiguration deviceConfig, EntityHeader user);
        Task UpdateSharedActionAsync(SharedAction sharedAction, EntityHeader user);
        Task UpdateSharedAttributeAsync(SharedAttribute sharedAttribute, EntityHeader user);
        Task UpdateStateMachineAsync(StateMachine attribute, EntityHeader user);
        Task UpdateUnitSetAsync(AttributeUnitSet unitSet, EntityHeader user);
    }
}