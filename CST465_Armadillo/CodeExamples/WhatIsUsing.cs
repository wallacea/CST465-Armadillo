using System;
using System.Data.SqlClient;

namespace CST465_Armadillo
{
    //This class isn't actually useful, it just demonstrates what a
    //using block is
    public class WhatIsUsing
    {
        public void UsingExample()
        {
            using (SqlConnection connection = new SqlConnection("connection string"))
            {
            }
            //The above is equivalent to
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection("connection string");
                }
                finally
                {
                    connection?.Dispose();
                }
            }
        }
    }
}