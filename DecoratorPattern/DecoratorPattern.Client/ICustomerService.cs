using System.Collections.Generic;

namespace DecoratorPattern.Client
{
    public interface ICustomerService
    {
        IEnumerable<string> GetCustomerNames();
    }
}
