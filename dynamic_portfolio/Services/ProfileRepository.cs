using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using dynamic_portfolio.Models;

namespace dynamic_portfolio.Services
{
    public static class ProfileRepository
    {
        private static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["PortfolioDb"].ConnectionString;

        static ProfileRepository()
        {
            InitializeDatabase();
        }

        private static void InitializeDatabase()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                // Create Profiles table if not exists
                var createProfilesTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Profiles' AND xtype='U')
                    CREATE TABLE Profiles (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(256) NOT NULL,
                        Role NVARCHAR(256) NOT NULL,
                        ProfileImagePath NVARCHAR(512),
                        AboutImagePath NVARCHAR(512),
                        ResumePath NVARCHAR(512),
                        AboutDescription NVARCHAR(MAX),
                        ExperienceYears NVARCHAR(256),
                        Education NVARCHAR(256),
                        LinkedInUrl NVARCHAR(512),
                        GithubUrl NVARCHAR(512),
                        Email NVARCHAR(256)
                    )";

                using (var cmd = new SqlCommand(createProfilesTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Seed default profile if empty
                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Profiles", conn))
                {
                    var count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        SeedProfile(conn);
                    }
                }
            }
        }

        private static void SeedProfile(SqlConnection conn)
        {
            var seedSql = @"
                INSERT INTO Profiles (Name, Role, ProfileImagePath, AboutImagePath, ResumePath, 
                                    AboutDescription, ExperienceYears, Education, LinkedInUrl, GithubUrl, Email)
                VALUES (@Name, @Role, @ProfileImagePath, @AboutImagePath, @ResumePath, 
                        @AboutDescription, @ExperienceYears, @Education, @LinkedInUrl, @GithubUrl, @Email)";

            using (var cmd = new SqlCommand(seedSql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", "Md. Rafsani Shazid");
                cmd.Parameters.AddWithValue("@Role", "Android Developer");
                cmd.Parameters.AddWithValue("@ProfileImagePath", "~/assets/profile-pic.png");
                cmd.Parameters.AddWithValue("@AboutImagePath", "~/assets/about-pic.png");
                cmd.Parameters.AddWithValue("@ResumePath", "~/assets/resume.pdf");
                cmd.Parameters.AddWithValue("@AboutDescription", "I'm a CS student from Bangladesh with a passion for technology, problem-solving, and lifelong learning. I specialize in C++ and have experience in mobile app and web development.\n\nCurrently, I'm diving into AI and Machine Learning, aiming to contribute to impactful research. Outside of tech, I enjoy football, staying active, watching movies and connecting with friends. I'm always eager to learn, collaborate, and take on new challenges.");
                cmd.Parameters.AddWithValue("@ExperienceYears", "2+ years in Development\n1+ year in SEO");
                cmd.Parameters.AddWithValue("@Education", "Bsc in computer science and engineering");
                cmd.Parameters.AddWithValue("@LinkedInUrl", "https://www.linkedin.com/in/md-rafsani-shazid-393230289/");
                cmd.Parameters.AddWithValue("@GithubUrl", "https://github.com/rafsaniShazid");
                cmd.Parameters.AddWithValue("@Email", "rafsanishazid@gmail.com");

                cmd.ExecuteNonQuery();
            }
        }

        public static Profile GetProfile()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT TOP 1 * FROM Profiles", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Profile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Role = reader.GetString(reader.GetOrdinal("Role")),
                            ProfileImagePath = reader.IsDBNull(reader.GetOrdinal("ProfileImagePath")) ? null : reader.GetString(reader.GetOrdinal("ProfileImagePath")),
                            AboutImagePath = reader.IsDBNull(reader.GetOrdinal("AboutImagePath")) ? null : reader.GetString(reader.GetOrdinal("AboutImagePath")),
                            ResumePath = reader.IsDBNull(reader.GetOrdinal("ResumePath")) ? null : reader.GetString(reader.GetOrdinal("ResumePath")),
                            AboutDescription = reader.IsDBNull(reader.GetOrdinal("AboutDescription")) ? null : reader.GetString(reader.GetOrdinal("AboutDescription")),
                            ExperienceYears = reader.IsDBNull(reader.GetOrdinal("ExperienceYears")) ? null : reader.GetString(reader.GetOrdinal("ExperienceYears")),
                            Education = reader.IsDBNull(reader.GetOrdinal("Education")) ? null : reader.GetString(reader.GetOrdinal("Education")),
                            LinkedInUrl = reader.IsDBNull(reader.GetOrdinal("LinkedInUrl")) ? null : reader.GetString(reader.GetOrdinal("LinkedInUrl")),
                            GithubUrl = reader.IsDBNull(reader.GetOrdinal("GithubUrl")) ? null : reader.GetString(reader.GetOrdinal("GithubUrl")),
                            Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email"))
                        };
                    }
                }
            }
            return null;
        }

        public static void UpdateProfile(Profile profile)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                // Check if profile exists
                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Profiles WHERE Id = @Id", conn))
                {
                    checkCmd.Parameters.AddWithValue("@Id", profile.Id);
                    var exists = (int)checkCmd.ExecuteScalar() > 0;

                    if (exists)
                    {
                        // Update existing
                        var updateSql = @"
                            UPDATE Profiles SET 
                                Name = @Name, Role = @Role, ProfileImagePath = @ProfileImagePath,
                                AboutImagePath = @AboutImagePath, ResumePath = @ResumePath,
                                AboutDescription = @AboutDescription, ExperienceYears = @ExperienceYears,
                                Education = @Education, LinkedInUrl = @LinkedInUrl, 
                                GithubUrl = @GithubUrl, Email = @Email
                            WHERE Id = @Id";

                        using (var cmd = new SqlCommand(updateSql, conn))
                        {
                            AddProfileParameters(cmd, profile);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert new
                        var insertSql = @"
                            INSERT INTO Profiles (Name, Role, ProfileImagePath, AboutImagePath, ResumePath,
                                                AboutDescription, ExperienceYears, Education, LinkedInUrl, GithubUrl, Email)
                            VALUES (@Name, @Role, @ProfileImagePath, @AboutImagePath, @ResumePath,
                                    @AboutDescription, @ExperienceYears, @Education, @LinkedInUrl, @GithubUrl, @Email)";

                        using (var cmd = new SqlCommand(insertSql, conn))
                        {
                            AddProfileParameters(cmd, profile);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private static void AddProfileParameters(SqlCommand cmd, Profile profile)
        {
            cmd.Parameters.AddWithValue("@Id", profile.Id);
            cmd.Parameters.AddWithValue("@Name", profile.Name ?? "");
            cmd.Parameters.AddWithValue("@Role", profile.Role ?? "");
            cmd.Parameters.AddWithValue("@ProfileImagePath", (object)profile.ProfileImagePath ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AboutImagePath", (object)profile.AboutImagePath ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ResumePath", (object)profile.ResumePath ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AboutDescription", (object)profile.AboutDescription ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ExperienceYears", (object)profile.ExperienceYears ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Education", (object)profile.Education ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@LinkedInUrl", (object)profile.LinkedInUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GithubUrl", (object)profile.GithubUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)profile.Email ?? DBNull.Value);
        }
    }
}