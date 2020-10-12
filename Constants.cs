using System;
using System.Collections.Generic;
using System.Text;

namespace DapperEFCorePerformanceBenchmarks
{
    public static class Constants
    {
        /// <summary>
        /// The connection string is not included in the sample project as, in my setup, it was too unique.
        /// Consequently you will need to set up your own connection to a SQL database and use your own connection string.
        /// </summary>
        //public static readonly string SportsConnectionString = "server=10.0.6.14;port=3306;user=root;password=12345678@Abc;database=dapper_efcore";
        public static readonly string SportsConnectionString = "Server=localhost\\MSSQLSERVER01;Database=TEST;Trusted_Connection=True"; 
    }
}
