﻿using LagoVista.IoT.DeviceAdmin.Interfaces.Managers;
using LagoVista.IoT.DeviceAdmin.Interfaces.Repos;
using LagoVista.IoT.DeviceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LagoVista.Core.Models;
using LagoVista.Core;
using LagoVista.Core.Validation;

namespace LagoVista.IoT.DeviceAdmin.Managers
{
    public class DeviceAdminManager : IDeviceAdminManager
    {
        IDeviceConfigurationRepo _deviceConfigRepo;
        ISharedActionRepo _sharedActionRepo;
        ISharedAtributeRepo _sharedAttributeRepo;
        IAttributeUnitSetRepo _unitSetRepo;
        IStateMachineRepo _stateMachineRepo;

        public DeviceAdminManager(IDeviceConfigurationRepo deviceConfigRepo, ISharedActionRepo sharedActionRepo, ISharedAtributeRepo sharedAttributeRepo,
            IAttributeUnitSetRepo unitSetRepo, IStateMachineRepo stateMachineRepo)
        {
            _deviceConfigRepo = deviceConfigRepo;
            _sharedActionRepo = sharedActionRepo;
            _sharedAttributeRepo = sharedAttributeRepo;
            _unitSetRepo = unitSetRepo;
            _stateMachineRepo = stateMachineRepo;
        }

        public async Task<InvokeResult> AddStateMachineAsync(StateMachine stateMachine, EntityHeader org, EntityHeader user)
        {
            var result = Validator.Validate(stateMachine, Actions.Create);
            if (result.IsValid)
            {
                await _stateMachineRepo.AddStateMachineAsync(stateMachine);
            }

            return result.ToActionResult();

        }

        public async Task<InvokeResult> AddSharedActionAsync(SharedAction sharedAction, EntityHeader org, EntityHeader user)
        {
            var result = Validator.Validate(sharedAction, Actions.Create);
            if (result.IsValid)
            {
                await _sharedActionRepo.AddSharedActionAsync(sharedAction);
            }

            return result.ToActionResult();
        }

        public async Task<InvokeResult> AddSharedAttributeAsync(SharedAttribute sharedAttribute, EntityHeader org, EntityHeader user)
        {
            var result = Validator.Validate(sharedAttribute, Actions.Create);
            if (result.IsValid)
            {
                await _sharedAttributeRepo.AddSharedAttributeAsync(sharedAttribute);
            }

            return result.ToActionResult();
        }

        public async Task<InvokeResult> AddUnitSetAsync(AttributeUnitSet unitSet, EntityHeader org, EntityHeader user)
        {
            var result = Validator.Validate(unitSet, Actions.Create);
            if (result.IsValid)
            {

                await _unitSetRepo.AddUnitSetAsync(unitSet);
            }

            return result.ToActionResult();

        }
        public async Task<InvokeResult> AddDeviceConfigurationAsync(DeviceConfiguration deviceConfiguration, EntityHeader org, EntityHeader user)
        {
            var result = Validator.Validate(deviceConfiguration, Actions.Create);

            if (result.IsValid)
            {
                await _deviceConfigRepo.AddDeviceConfigurationAsync(deviceConfiguration);
            }

            return result.ToActionResult();
        }

        public async Task<InvokeResult> UpdateStateMachineAsync(StateMachine stateMachine, EntityHeader user)
        {
            var result = Validator.Validate(stateMachine, Actions.Create);

            if (result.IsValid)
            {
                stateMachine.LastUpdatedBy = user;
                stateMachine.LastUpdatedDate = DateTime.Now.ToJSONString();

                await _stateMachineRepo.UpdateStateMachineAsync(stateMachine);
            }

            return result.ToActionResult();
        }

        public async Task<InvokeResult> UpdateSharedActionAsync(SharedAction sharedAction, EntityHeader user)
        {
            var result = Validator.Validate(sharedAction, Actions.Create);

            if (result.IsValid)
            {
                sharedAction.LastUpdatedBy = user;
                sharedAction.LastUpdatedDate = DateTime.Now.ToJSONString();
                await _sharedActionRepo.UpdateSharedActionAsync(sharedAction);
            }

            return result.ToActionResult();

        }

        public async Task<InvokeResult> UpdateSharedAttributeAsync(SharedAttribute sharedAttribute, EntityHeader user)
        {
            var result = Validator.Validate(sharedAttribute, Actions.Create);

            if (result.IsValid)
            {
                sharedAttribute.LastUpdatedBy = user;
                sharedAttribute.LastUpdatedDate = DateTime.Now.ToJSONString();

                await _sharedAttributeRepo.UpdateSharedAttributeAsync(sharedAttribute);
            }

            return result.ToActionResult();
        }

        public async Task<InvokeResult> UpdateUnitSetAsync(AttributeUnitSet unitSet, EntityHeader user)
        {
            var result = Validator.Validate(unitSet, Actions.Create);

            if (result.IsValid)
            {
                unitSet.LastUpdatedBy = user;
                unitSet.LastUpdatedDate = DateTime.Now.ToJSONString();
                await _unitSetRepo.UpdateUnitSetAsync(unitSet);
            }

            return result.ToActionResult();
        }

        public async Task<InvokeResult> UpdateDeviceConfigurationAsync(DeviceConfiguration deviceConfig, EntityHeader user)
        {
            var result = Validator.Validate(deviceConfig, Actions.Create);

            if (result.IsValid)
            {
                deviceConfig.LastUpdatedBy = user;
                deviceConfig.LastUpdatedDate = DateTime.Now.ToJSONString();
                await _deviceConfigRepo.UpdateDeviceConfigurationAsync(deviceConfig);
            }

            return result.ToActionResult();
        }

        public async Task<StateMachine> GetStateMachineAsync(String id, EntityHeader org)
        {
            var stateMachine = await _stateMachineRepo.GetStateMachineAsync(id);
            if (!stateMachine.IsPublic && stateMachine.OwnerOrganization != org)
            {
                throw new Exception();
            }

            return stateMachine;
        }

        public async Task<SharedAction> GetSharedActionAsync(String id, EntityHeader org)
        {
            var sharedAction = await _sharedActionRepo.GetSharedActionAsync(id);
            if (!sharedAction.IsPublic && sharedAction.OwnerOrganization != org)
            {
                throw new Exception();
            }

            return  sharedAction ;
        }

        public async Task<SharedAttribute> GetSharedAttributeAsync(String id, EntityHeader org)
        {
            var sharedAttribute = await _sharedAttributeRepo.GetSharedAttributeAsync(id);
            if (!sharedAttribute.IsPublic && sharedAttribute.OwnerOrganization.Id != org.Id)
            {
                throw new Exception();
            }

            return  sharedAttribute ;
        }

        public async Task<AttributeUnitSet> GetAttributeUnitSetAsync(String id, EntityHeader org)
        {
            var unitSet = await _unitSetRepo.GetUnitSetAsync(id);
            if (!unitSet.IsPublic && (unitSet.OwnerOrganization.Id != org.Id))
            {
                throw new Exception();
            }

            return unitSet;
        }

        public async Task<DeviceConfiguration> GetDeviceConfigurationAsync(String id, EntityHeader org)
        {
            var deviceConfig = await _deviceConfigRepo.GetDeviceConfigurationAsync(id);
            if (!deviceConfig.IsPublic && deviceConfig.OwnerOrganization.Id != org.Id)
            {
                throw new Exception();
            }

            return deviceConfig;
        }

        public Task<IEnumerable<StateMachineSummary>> GetStateMachinesForOrgAsync(String orgId)
        {
            return _stateMachineRepo.GetStateMachinesForOrgAsync(orgId);
        }

        public Task<IEnumerable<AttributeUnitSetSummary>> GetUnitSetsForOrgAsync(String orgId)
        {
            return _unitSetRepo.GetUnitSetsForOrgAsync(orgId);
        }

        public Task<IEnumerable<SharedAttributeSummary>> GetSharedAttributesForOrgAsync(String orgId)
        {
            return _sharedAttributeRepo.GetSharedAttributesForOrgAsync(orgId);
        }

        public Task<IEnumerable<SharedActionSummary>> GetSharedActionsForOrgAsync(String orgId)
        {
            return _sharedActionRepo.GetSharedActionsForOrgAsync(orgId);
        }

        public Task<IEnumerable<DeviceConfigurationSummary>> GetDeviceConfigurationsForOrgsAsync(String orgId)
        {
            return _deviceConfigRepo.GetDeviceConfigurationsForOrgAsync(orgId);
        }

        public Task<bool> QueryDeviceConfigurationKeyInUseAsync(String key, String orgId)
        {
            return _deviceConfigRepo.QueryKeyInUseAsync(key, orgId);
        }

        public Task<bool> QueryStateMachineKeyInUseAsync(String key, String orgId)
        {
            return _stateMachineRepo.QueryKeyInUseAsync(key, orgId);
        }

        public Task<bool> QueryAttributeUnitSetKeyInUseAsync(String key, String orgId)
        {
            return _unitSetRepo.QueryKeyInUseAsync(key, orgId);
        }

        public Task<bool> QuerySharedActionKeyInUseAsync(String key, String orgId)
        {
            return _sharedActionRepo.QueryKeyInUseAsync(key, orgId);
        }

        public Task<bool> QuerySharedAttributeKeyInUseAsync(String key, String orgId)
        {
            return _sharedAttributeRepo.QueryKeyInUseAsync(key, orgId);
        }
    }
}
