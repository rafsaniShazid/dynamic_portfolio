using System;
using System.Linq;
using dynamic_portfolio.Data;
using dynamic_portfolio.Models;

namespace dynamic_portfolio.Services
{
    public static class ProfileRepository
    {
        public static Profile GetProfile()
        {
            using (var db = new PortfolioContext())
            {
                db.Database.CreateIfNotExists();

                var profile = db.Profiles.FirstOrDefault();
                if (profile == null)
                {
                    // Seed default profile data
                    profile = new Profile
                    {
                        Name = "Md. Rafsani Shazid",
                        Role = "Android Developer",
                        ProfileImagePath = "~/assets/profile-pic.png",
                        AboutImagePath = "~/assets/about-pic.png",
                        ResumePath = "~/assets/resume.pdf",
                        AboutDescription = "I'm a CS student from Bangladesh with a passion for technology, problem-solving, and lifelong learning. I specialize in C++ and have experience in mobile app and web development.\n\nCurrently, I'm diving into AI and Machine Learning, aiming to contribute to impactful research. Outside of tech, I enjoy football, staying active, watching movies and connecting with friends. I'm always eager to learn, collaborate, and take on new challenges.",
                        ExperienceYears = "2+ years in Development\n1+ year in SEO",
                        Education = "Bsc in computer science and engineering",
                        LinkedInUrl = "https://www.linkedin.com/in/md-rafsani-shazid-393230289/",
                        GithubUrl = "https://github.com/rafsaniShazid",
                        Email = "rafsanishazid@gmail.com"
                    };
                    db.Profiles.Add(profile);
                    db.SaveChanges();
                }
                return profile;
            }
        }

        public static void UpdateProfile(Profile profile)
        {
            using (var db = new PortfolioContext())
            {
                var existing = db.Profiles.FirstOrDefault();
                if (existing == null)
                {
                    db.Profiles.Add(profile);
                }
                else
                {
                    existing.Name = profile.Name;
                    existing.Role = profile.Role;
                    existing.ProfileImagePath = profile.ProfileImagePath;
                    existing.AboutImagePath = profile.AboutImagePath;
                    existing.ResumePath = profile.ResumePath;
                    existing.AboutDescription = profile.AboutDescription;
                    existing.ExperienceYears = profile.ExperienceYears;
                    existing.Education = profile.Education;
                    existing.LinkedInUrl = profile.LinkedInUrl;
                    existing.GithubUrl = profile.GithubUrl;
                    existing.Email = profile.Email;
                }
                db.SaveChanges();
            }
        }
    }
}