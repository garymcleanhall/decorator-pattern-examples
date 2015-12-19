using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using DecoratorPattern.Client;

namespace DecoratorPattern.Service
{
    public class CustomerService : ICustomerService
    {
        public CustomerService(Action<string> logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _logger = logger;
            _customerNames = new Lazy<IEnumerable<string>>(GetCustomerNamesInternal);
            _profiler = new Stopwatch();
        }

        public IEnumerable<string> GetCustomerNames()
        {
            _profiler.Restart();
            var customerNames = _customerNames.Value;
            _profiler.Stop();
            _logger($"Took {_profiler.Elapsed.TotalMilliseconds}ms");
            return customerNames;
        } 

        public IEnumerable<string> GetCustomerNamesInternal()
        {
            _logger("Getting customer names...");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CustomerDatabase"].ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "[dbo].[GetCustomerNames]";
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader.GetString(reader.GetOrdinal("Name"));
                    }
                }

                connection.Close();
            }
        }

        private readonly Action<string> _logger;
        private readonly Lazy<IEnumerable<string>> _customerNames;
        private readonly Stopwatch _profiler;
    }
}
