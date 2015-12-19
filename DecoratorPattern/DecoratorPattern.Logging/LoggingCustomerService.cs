using System;
using System.Collections.Generic;

using DecoratorPattern.Client;

namespace DecoratorPattern.Logging
{
    public class LoggingCustomerService : ICustomerService
    {
        public LoggingCustomerService(ICustomerService customerService, Action<string> logger)
        {
            if (customerService == null) throw new ArgumentNullException(nameof(customerService));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _customerService = customerService;
            _logger = logger;
        }

        public IEnumerable<string> GetCustomerNames()
        {
            _logger("Getting customer names...");
            return _customerService.GetCustomerNames();
        }

        private readonly ICustomerService _customerService;
        private readonly Action<string> _logger;
    }    
}
