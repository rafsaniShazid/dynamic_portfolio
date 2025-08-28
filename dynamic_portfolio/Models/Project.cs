using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dynamic_portfolio.Models
{
    [Serializable]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(256)]
        public string Title { get; set; }

        [Required, MaxLength(512)]
        public string ImagePath { get; set; }   // e.g., "~/assets/project-1.png"

        [MaxLength(256)]
        public string ImageCss { get; set; }    // e.g., "project-img project-img1"

        [MaxLength(256)]
        public string Alt { get; set; }

        [MaxLength(512)]
        public string GithubUrl { get; set; }

        [MaxLength(512)]
        public string LiveUrl { get; set; }

        public string Description { get; set; }
    }
}