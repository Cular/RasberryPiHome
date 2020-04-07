using HomeWatcher.Sensors.MagneticSwitch;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;

namespace HomeWatcher.Sensors
{
    public static class DIExtensions
    {
        public static void RegisterSensors(this IServiceCollection services)
        {
            services.AddTransient<GpioController>();
            services.AddHostedService<MagneticHost>();
        }
    }
}
