using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArmadilloLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CST465_Armadillo.Repositories
{
    public class ArmadilloDBRepository : IArmadilloRepository
    {
        private ArmadilloSettings _Settings;
        public ArmadilloDBRepository(IOptions<ArmadilloSettings> armadilloConfig)
        {
            _Settings = armadilloConfig.Value;
        }
        public string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(_Settings.DatabaseConfigFile, optional: false, reloadOnChange: true)
            .AddJsonFile("farmsettings.json", optional: false, reloadOnChange: true);


            var configuration = builder.Build();

            return configuration.GetConnectionString("DB_TheFarm");

        }
        public Armadillo Get(int id)
        {
            Armadillo armadillo = null;
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Armadillo_Get", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            armadillo = new Armadillo();
                            armadillo.ID = (int) reader["ID"];
                            armadillo.Name = reader["Name"].ToString();
                            armadillo.Age = (int) reader["Age"];
                            armadillo.ShellHardness = (int) reader["ShellHardness"];
                            armadillo.IsPainted = (bool) reader["IsPainted"];
                            armadillo.Homeland = reader["Homeland"].ToString();
                            armadillo.Description = reader["Description"].ToString();
                        }
                    }
                }

            }
            return armadillo;
        }


        public List<Armadillo> SearchList(string searchText)
        {
            List<Armadillo> armadilloList = GetList().Where(a => a.Name.ToLower().Contains(searchText.ToLower())).ToList();
            return armadilloList;
        }
        public List<Armadillo> GetList()
        {
            List<Armadillo> armadilloList = new List<Armadillo>();
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Armadillo_GetList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Armadillo armadillo = new Armadillo();
                            armadillo.ID = (int) reader["ID"];
                            armadillo.Name = reader["Name"].ToString();
                            armadillo.Age = (int) reader["Age"];
                            armadillo.ShellHardness = (int) reader["ShellHardness"];
                            armadillo.IsPainted = (bool) reader["IsPainted"];
                            armadillo.Homeland = reader["Homeland"].ToString();
                            armadillo.Description = reader["Description"].ToString();
                            armadilloList.Add(armadillo);
                        }
                    }
                }

            }
            return armadilloList;
        }


        public void Save(Armadillo armadillo)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Armadillo_InsertUpdate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    if (armadillo.ID != 0)
                    {
                        command.Parameters.AddWithValue("@ID", armadillo.ID);
                    }
                    command.Parameters.AddWithValue("@Name", armadillo.Name);
                    command.Parameters.AddWithValue("@Age", armadillo.Age);
                    command.Parameters.AddWithValue("@ShellHardness", armadillo.ShellHardness);
                    command.Parameters.AddWithValue("@IsPainted", armadillo.IsPainted);
                    command.Parameters.AddWithValue("@Homeland", armadillo.Homeland ?? "USA");
                    command.Parameters.AddWithValue("@Description", armadillo.Description);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("Armadillo_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    
                    command.Parameters.AddWithValue("@ID", id);
                    
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
