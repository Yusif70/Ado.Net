using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net.Abstractions
{
    public static class Connection
    {
        private static readonly string connectionString = "Server=DESKTOP-NIJAT;Database=SMS;Integrated Security = true";
        public static readonly SqlConnection connection = new(Connection.connectionString);
    }
}
