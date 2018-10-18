using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CST465_Armadillo.Repositories
{
    public class TreaterRepository
    {
        public List<string> GetCandyTypes()
        {
            List<string> candy = new List<string>();
            using (SqlConnection connection = new SqlConnection("Server=otk-dbdev-01;database=Treaters;User Id=treater;Password=trick or treat;"))
            {
                using (SqlCommand command = new SqlCommand("select * from candy", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            candy.Add(reader["ProductName"].ToString());
                        }
                    }
                }
            }
            return candy;
        }
    }
}
