using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using dynamic_portfolio.Data;
using dynamic_portfolio.Models;

namespace dynamic_portfolio.Services
{
    public static class ProjectRepository
    {
        private static readonly object Sync = new object();
        private static string FilePath => HostingEnvironment.MapPath("~/App_Data/projects.json");

        public static List<Project> Load()
        {
            try
            {
                var path = FilePath;
                if (string.IsNullOrEmpty(path) || !File.Exists(path)) return new List<Project>();
                var json = File.ReadAllText(path);
                var serializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
                var list = serializer.Deserialize<List<Project>>(json);
                return list ?? new List<Project>();
            }
            catch
            {
                return new List<Project>();
            }
        }

        public static void Save(List<Project> projects)
        {
            var list = projects ?? new List<Project>();
            var serializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
            var json = serializer.Serialize(list);

            var path = FilePath;
            if (string.IsNullOrEmpty(path)) return;

            lock (Sync)
            {
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.WriteAllText(path, json);
            }
        }

        public static List<Project> GetAll()
        {
            using (var db = new PortfolioContext())
            {
                db.Database.CreateIfNotExists();

                if (!db.Projects.Any())
                {
                    db.Projects.Add(new Project
                    {
                        Title = "MP3 Player",
                        ImagePath = "~/assets/project-1.png",
                        ImageCss = "project-img project-img1",
                        Alt = "project-1",
                        GithubUrl = "https://github.com/rafsaniShazid/MP3_player",
                        LiveUrl = "https://drive.google.com/file/d/1jpmlTABruHj-Je9OEF88NuiQo1Ffq8Es/view?usp=drivesdk"
                    });
                    db.Projects.Add(new Project
                    {
                        Title = "Portfolio Website",
                        ImagePath = "~/assets/project-2.png",
                        ImageCss = "project-img",
                        Alt = "project-2",
                        GithubUrl = "https://github.com/rafsaniShazid/MP3_player",
                        Description = "This website is the project itself"
                    });
                    db.Projects.Add(new Project
                    {
                        Title = "Snake Game",
                        ImagePath = "~/assets/project-3.png",
                        ImageCss = "project-img project-img3",
                        Alt = "project-3",
                        GithubUrl = "https://github.com/rafsaniShazid/Snake_game.",
                        LiveUrl = "https://drive.google.com/file/d/1k1AM-JSObbuRZOK_7zHOOE0GQHrapf0p/view?usp=drivesdk"
                    });
                    db.SaveChanges();
                }

                return db.Projects.OrderBy(p => p.Id).ToList();
            }
        }

        public static Project GetById(int id)
        {
            using (var db = new PortfolioContext())
            {
                return db.Projects.FirstOrDefault(p => p.Id == id);
            }
        }

        public static void Add(Project project)
        {
            using (var db = new PortfolioContext())
            {
                db.Projects.Add(project);
                db.SaveChanges();
            }
        }

        public static void Update(Project project)
        {
            using (var db = new PortfolioContext())
            {
                var existing = db.Projects.FirstOrDefault(p => p.Id == project.Id);
                if (existing == null) return;

                existing.Title = project.Title;
                existing.ImagePath = project.ImagePath;
                existing.ImageCss = project.ImageCss;
                existing.Alt = project.Alt;
                existing.GithubUrl = project.GithubUrl;
                existing.LiveUrl = project.LiveUrl;
                existing.Description = project.Description;

                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (var db = new PortfolioContext())
            {
                var p = db.Projects.FirstOrDefault(x => x.Id == id);
                if (p == null) return;
                db.Projects.Remove(p);
                db.SaveChanges();
            }
        }
    }
}