using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using DecoratorPattern.Client;

namespace DecoratorPattern.Service
{
    public class CustomerService : ICustomerService
    {
        public IEnumerable<string> GetCustomerNames()
        {
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
    }
}
