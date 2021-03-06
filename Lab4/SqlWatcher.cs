﻿using System.Data;
using System.Data.SqlClient;

namespace Lab4
{
    class SqlWatcher
    {
        private static string ConnectionString { get; set; }

        public SqlWatcher(string sqlServer, string dbName, bool integratedSecurity)
        {
            ConnectionString = @"Data Source=" + sqlServer + ";Initial Catalog=" + dbName + ";Integrated Security=" + integratedSecurity;
        }

        public object[][] GetLastUserInfo(System.DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[GetLastUserInfo]", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@datetime", date);
                    DataTable dt = new DataTable();
                    using (SqlDataReader dataReader1 = command.ExecuteReader())
                    {
                        dt.Load(dataReader1);
                    }
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            object[][] values = new object[dt.Rows.Count][];
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                values[j] = new object[dataReader.FieldCount];
                            }
                            int i = 0;
                            while (dataReader.Read())
                            {
                                dataReader.GetValues(values[i]);
                                i++;
                            }
                            return values;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}
