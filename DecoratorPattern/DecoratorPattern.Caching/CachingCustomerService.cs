using System;
using System.Collections.Generic;

using DecoratorPattern.Client;

namespace DecoratorPattern.Caching
{
    public class CachingCustomerService : ICustomerService
    {
        public CachingCustomerService(ICustomerService customerService)
        {
            if (customerService == null) throw new ArgumentNullException(nameof(customerService));

            _customerService = customerService;
            _customerNames = new Lazy<IEnumerable<string>>(GetCustomerNamesInternal);
        }

        public IEnumerable<string> GetCustomerNames()
        {
            return _customerNames.Value;
        }

        private IEnumerable<string> GetCustomerNamesInternal()
        {
            return _customerService.GetCustomerNames();
        }

        private readonly ICustomerService _customerService;
        private readonly Lazy<IEnumerable<string>> _customerNames;
    }
}
