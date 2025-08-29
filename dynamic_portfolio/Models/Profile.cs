using System;
using System.ComponentModel.DataAnnotations;

namespace dynamic_portfolio.Models
{
    [Serializable]
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(256)]
        public string Role { get; set; }

        [MaxLength(512)]
        public string ProfileImagePath { get; set; }

        [MaxLength(512)]
        public string AboutImagePath { get; set; }

        [MaxLength(512)]
        public string ResumePath { get; set; }

        public string AboutDescription { get; set; }

        [MaxLength(256)]
        public string ExperienceYears { get; set; }

        [MaxLength(256)]
        public string Education { get; set; }

        [MaxLength(512)]
        public string LinkedInUrl { get; set; }

        [MaxLength(512)]
        public string GithubUrl { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }
    }
}