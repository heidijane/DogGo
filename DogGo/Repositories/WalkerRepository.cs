﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkerRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkerRepository(IConfiguration config)
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

        public List<Walker> GetAllWalkers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walker.Id, Walker.[Name], Walker.ImageUrl, Walker.NeighborhoodId, Neighborhood.[Name] AS NeighborhoodName
                        FROM Walker
                        JOIN Neighborhood ON Neighborhood.Id = Walker.NeighborhoodId
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                        };

                        Neighborhood neighborhood = new Neighborhood()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };

                        walker.Neighborhood = neighborhood;

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }

        public Walker GetWalkerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walker.Id, Walker.[Name], Walker.ImageUrl, Walker.NeighborhoodId, Neighborhood.[Name] AS NeighborhoodName
                        FROM Walker
                        JOIN Neighborhood ON Neighborhood.Id = Walker.NeighborhoodId
                        WHERE Walker.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                        };

                        Neighborhood neighborhood = new Neighborhood()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };

                        walker.Neighborhood = neighborhood;

                        reader.Close();
                        return walker;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        public List<Walker> GetWalkersInNeighborhood(int neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walker.Id, Walker.[Name], Walker.ImageUrl, Walker.NeighborhoodId, Neighborhood.[Name] AS NeighborhoodName
                        FROM Walker
                        JOIN Neighborhood ON Neighborhood.Id = Walker.NeighborhoodId
                        WHERE Walker.NeighborhoodId = @neighborhoodId
            ";

                    cmd.Parameters.AddWithValue("@neighborhoodId", neighborhoodId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                        };

                        Neighborhood neighborhood = new Neighborhood()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };

                        walker.Neighborhood = neighborhood;

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }
    }
}