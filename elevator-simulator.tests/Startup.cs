using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.core.v1.Handlers;
using elevator_simulator.core.v1.Repo;
using elevator_simulator.tests.Fixture;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace elevator_simulator.tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFloorRequestHandler, FloorRequestHandler>();

            services.AddTransient<IQueueHandler, QueueHandler>();

            services.AddTransient<IElevatorRepository, ElevatorRepository>();

        }
    }
}
