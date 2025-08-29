using System;

namespace dynamic_portfolio.Models
{
    [Serializable]
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string ProfileImagePath { get; set; }
        public string AboutImagePath { get; set; }
        public string ResumePath { get; set; }
        public string AboutDescription { get; set; }
        public string ExperienceYears { get; set; }
        public string Education { get; set; }
        public string LinkedInUrl { get; set; }
        public string GithubUrl { get; set; }
        public string Email { get; set; }
    }
}