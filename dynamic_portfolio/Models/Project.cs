using System;

namespace dynamic_portfolio.Models
{
    [Serializable]
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string ImageCss { get; set; }
        public string Alt { get; set; }
        public string GithubUrl { get; set; }
        public string LiveUrl { get; set; }
        public string Description { get; set; }
    }
}