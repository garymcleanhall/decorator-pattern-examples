using System;
using System.Collections.Generic;

namespace DecoratorPattern.Client
{
    public class CustomerController
    {
        public CustomerController(ICustomerService customerService)
        {
            if (customerService == null) throw new ArgumentNullException(nameof(customerService));
            _customerService = customerService;
        }

        public IEnumerable<string> GetAllCustomerNames()
        {
            var allCustomerNames = _customerService.GetCustomerNames();
            return allCustomerNames;            
        }

        private readonly ICustomerService _customerService;
    }
}
