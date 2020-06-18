using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkRepository
    {
        private readonly IConfiguration _config;

        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetWalksByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Walks.Id,
                                               Walks.Date,
                                               Walks.Duration, 
                                               Walks.WalkerId,
                                               Walks.DogId,
                                               Dog.Name,
                                               Dog.OwnerId,
                                               Dog.Breed,
                                               Owner.Name AS OwnerName
                                        FROM Walks 
                                        JOIN Dog ON Dog.Id = Walks.DogId
                                        JOIN Owner ON Dog.OwnerId = Owner.Id
                                        WHERE Walks.WalkerId = @walkerId";

                    cmd.Parameters.AddWithValue("@walkerId", walkerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                        };
                        Dog dog = new Dog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed"))
                        };

                        walk.Dog = dog;

                        Owner owner = new Owner()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Name = reader.GetString(reader.GetOrdinal("OwnerName"))
                        };
                        walk.Owner = owner;

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }
    }
}