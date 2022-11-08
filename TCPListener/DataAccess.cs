using Dapper;
using System.Data.SqlClient;
using Newtonsoft;
using Newtonsoft.Json;

namespace TCPListener
{
    public class DataAccess
    {
        private static readonly string _connectionString = @"Data Source=ABHEELENOVO\SQLEXPRESS;Initial Catalog = SQLContactsDB; Integrated Security = True;";



        public static string RetrieveItemInfo(string barcode)
        {
            string output = "Not Found";
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new { Barcode = barcode };
                var sql = "SELECT [Barcode] ,[Processed] FROM [SQLContactsDB].[dbo].[TCPTester] WHERE Barcode = @barcode;";
                var ItemModel = connection.QuerySingleOrDefault(sql, parameters);

                if(ItemModel != null)
                {
                    output = JsonConvert.SerializeObject(ItemModel);
                }
            }

            return output;
        }
    }
}
