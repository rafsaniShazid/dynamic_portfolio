using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using dynamic_portfolio.Models;

namespace dynamic_portfolio.Services
{
    public static class ProjectRepository
    {
        private static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["PortfolioDb"].ConnectionString;

        static ProjectRepository()
        {
            InitializeDatabase();
        }

        private static void InitializeDatabase()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                // Create Projects table if not exists
                var createProjectsTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Projects' AND xtype='U')
                    CREATE TABLE Projects (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Title NVARCHAR(256) NOT NULL,
                        ImagePath NVARCHAR(512),
                        ImageCss NVARCHAR(256),
                        Alt NVARCHAR(256),
                        GithubUrl NVARCHAR(512),
                        LiveUrl NVARCHAR(512),
                        Description NVARCHAR(MAX)
                    )";

                using (var cmd = new SqlCommand(createProjectsTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Seed default data if empty
                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Projects", conn))
                {
                    var count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        SeedProjects(conn);
                    }
                }
            }
        }

        private static void SeedProjects(SqlConnection conn)
        {
            var seedSql = @"
                INSERT INTO Projects (Title, ImagePath, ImageCss, Alt, GithubUrl, LiveUrl, Description)
                VALUES 
                (@Title1, @ImagePath1, @ImageCss1, @Alt1, @GithubUrl1, @LiveUrl1, @Description1),
                (@Title2, @ImagePath2, @ImageCss2, @Alt2, @GithubUrl2, @LiveUrl2, @Description2),
                (@Title3, @ImagePath3, @ImageCss3, @Alt3, @GithubUrl3, @LiveUrl3, @Description3)";

            using (var cmd = new SqlCommand(seedSql, conn))
            {
                cmd.Parameters.AddWithValue("@Title1", "MP3 Player");
                cmd.Parameters.AddWithValue("@ImagePath1", "~/assets/project-1.png");
                cmd.Parameters.AddWithValue("@ImageCss1", "project-img project-img1");
                cmd.Parameters.AddWithValue("@Alt1", "project-1");
                cmd.Parameters.AddWithValue("@GithubUrl1", "https://github.com/rafsaniShazid/MP3_player");
                cmd.Parameters.AddWithValue("@LiveUrl1", "https://drive.google.com/file/d/1jpmlTABruHj-Je9OEF88NuiQo1Ffq8Es/view?usp=drivesdk");
                cmd.Parameters.AddWithValue("@Description1", DBNull.Value);

                cmd.Parameters.AddWithValue("@Title2", "Portfolio Website");
                cmd.Parameters.AddWithValue("@ImagePath2", "~/assets/project-2.png");
                cmd.Parameters.AddWithValue("@ImageCss2", "project-img");
                cmd.Parameters.AddWithValue("@Alt2", "project-2");
                cmd.Parameters.AddWithValue("@GithubUrl2", "https://github.com/rafsaniShazid/MP3_player");
                cmd.Parameters.AddWithValue("@LiveUrl2", DBNull.Value);
                cmd.Parameters.AddWithValue("@Description2", "This website is the project itself");

                cmd.Parameters.AddWithValue("@Title3", "Snake Game");
                cmd.Parameters.AddWithValue("@ImagePath3", "~/assets/project-3.png");
                cmd.Parameters.AddWithValue("@ImageCss3", "project-img project-img3");
                cmd.Parameters.AddWithValue("@Alt3", "project-3");
                cmd.Parameters.AddWithValue("@GithubUrl3", "https://github.com/rafsaniShazid/Snake_game.");
                cmd.Parameters.AddWithValue("@LiveUrl3", "https://drive.google.com/file/d/1k1AM-JSObbuRZOK_7zHOOE0GQHrapf0p/view?usp=drivesdk");
                cmd.Parameters.AddWithValue("@Description3", DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public static List<Project> GetAll()
        {
            var projects = new List<Project>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Id, Title, ImagePath, ImageCss, Alt, GithubUrl, LiveUrl, Description FROM Projects ORDER BY Id", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            Id = reader.GetInt32(0),        // Id column index 0
                            Title = reader.GetString(1),    // Title column index 1
                            ImagePath = reader.IsDBNull(2) ? null : reader.GetString(2),     // ImagePath index 2
                            ImageCss = reader.IsDBNull(3) ? null : reader.GetString(3),      // ImageCss index 3
                            Alt = reader.IsDBNull(4) ? null : reader.GetString(4),           // Alt index 4
                            GithubUrl = reader.IsDBNull(5) ? null : reader.GetString(5),     // GithubUrl index 5
                            LiveUrl = reader.IsDBNull(6) ? null : reader.GetString(6),       // LiveUrl index 6
                            Description = reader.IsDBNull(7) ? null : reader.GetString(7)    // Description index 7
                        });
                    }
                }
            }

            return projects;
        }

        public static Project GetById(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Id, Title, ImagePath, ImageCss, Alt, GithubUrl, LiveUrl, Description FROM Projects WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Project
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                ImagePath = reader.IsDBNull(2) ? null : reader.GetString(2),
                                ImageCss = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Alt = reader.IsDBNull(4) ? null : reader.GetString(4),
                                GithubUrl = reader.IsDBNull(5) ? null : reader.GetString(5),
                                LiveUrl = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Description = reader.IsDBNull(7) ? null : reader.GetString(7)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static void Add(Project project)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var sql = @"
                    INSERT INTO Projects (Title, ImagePath, ImageCss, Alt, GithubUrl, LiveUrl, Description)
                    VALUES (@Title, @ImagePath, @ImageCss, @Alt, @GithubUrl, @LiveUrl, @Description)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", project.Title ?? "");
                    cmd.Parameters.AddWithValue("@ImagePath", (object)project.ImagePath ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ImageCss", (object)project.ImageCss ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Alt", (object)project.Alt ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GithubUrl", (object)project.GithubUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LiveUrl", (object)project.LiveUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)project.Description ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Update(Project project)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var sql = @"
                    UPDATE Projects SET 
                        Title = @Title, ImagePath = @ImagePath, ImageCss = @ImageCss, 
                        Alt = @Alt, GithubUrl = @GithubUrl, LiveUrl = @LiveUrl, 
                        Description = @Description
                    WHERE Id = @Id";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", project.Id);
                    cmd.Parameters.AddWithValue("@Title", project.Title ?? "");
                    cmd.Parameters.AddWithValue("@ImagePath", (object)project.ImagePath ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ImageCss", (object)project.ImageCss ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Alt", (object)project.Alt ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GithubUrl", (object)project.GithubUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LiveUrl", (object)project.LiveUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)project.Description ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM Projects WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}