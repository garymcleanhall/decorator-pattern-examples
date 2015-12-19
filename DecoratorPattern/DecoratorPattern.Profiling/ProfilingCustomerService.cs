using System;
using System.Collections.Generic;
using System.Diagnostics;

using DecoratorPattern.Client;

namespace DecoratorPattern.Profiling
{
    public class ProfilingCustomerService : ICustomerService
    {
        public ProfilingCustomerService(ICustomerService customerService, Action<string> logger)
        {
            if (customerService == null) throw new ArgumentNullException(nameof(customerService));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _customerService = customerService;
            _logger = logger;
            _profiler = new Stopwatch();
        }

        public IEnumerable<string> GetCustomerNames()
        {
            _profiler.Restart();
            var customerNames = _customerService.GetCustomerNames();
            _profiler.Stop();
            _logger($"Took {_profiler.Elapsed.TotalMilliseconds}ms");
            return customerNames;            
        }

        private readonly ICustomerService _customerService;
        private readonly Action<string> _logger;
        private readonly Stopwatch _profiler;
    }
}
