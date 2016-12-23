﻿using LagoVista.IoT.DeviceAdmin.CloudRepos.Repos;
using LagoVista.IoT.DeviceAdmin.Interfaces.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace LagoVista.IoT.DeviceAdmin.CloudRepos
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAttributeUnitSetRepo, AttributeUnitSetRepo>();
            services.AddTransient<ISharedAtributeRepo, SharedAttributeRepo>();
            services.AddTransient<ISharedActionRepo, SharedActionRepo>();
            services.AddTransient<IStateMachineRepo, StateMachineRepo>();
            services.AddTransient<IDeviceConfigurationRepo, DeviceConfigurationRepo>();
        }
    }
}
