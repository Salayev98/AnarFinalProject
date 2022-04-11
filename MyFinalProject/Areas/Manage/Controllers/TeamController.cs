using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Dal;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TeamController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(ProjectContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            return View(PaginatedList<OurTeam>.Create(_context.OurTeams.AsQueryable(), 4, page));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OurTeam team)
        {
            if (team.ImageFile == null)
                ModelState.AddModelError("ImageFile", "Image file is required!");

            if (!ModelState.IsValid)
                return View();

            if (team.ImageFile.ContentType != "image/jpeg" && team.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "incorrect file type");
                return View();
            }

            if (team.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "file size must be less than 2mb");
                return View();
            }
            string b = Guid.NewGuid().ToString() + team.ImageFile.FileName;
            if (b.Length > 99)
            {
                b.Substring(64, team.ImageFile.FileName.Length - 64);
            }

            team.Image = b;

            string path = Path.Combine(_env.WebRootPath, "upload/teams", team.Image);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                team.ImageFile.CopyTo(stream);
            }


            _context.OurTeams.Add(team);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            OurTeam team = _context.OurTeams.FirstOrDefault(x => x.Id == id);
            if (team == null) return NotFound();



            return View(team);
        }
        [HttpPost]
        public IActionResult Delete(OurTeam team)
        {


            OurTeam existteam = _context.OurTeams.FirstOrDefault(x => x.Id == team.Id);

            if (existteam == null) return NotFound();

            if (existteam.Image != null)
            {
                string path = Path.Combine(_env.WebRootPath, "upload/teams", existteam.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }


            _context.OurTeams.Remove(existteam);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            OurTeam team = _context.OurTeams.FirstOrDefault(x => x.Id == id);

            if (team == null) return NotFound();

            return View(team);
        }

        [HttpPost]
        public IActionResult Edit(OurTeam team)
        {
            if (!ModelState.IsValid)
                return View();

            OurTeam exsteam = _context.OurTeams.FirstOrDefault(x => x.Id == team.Id);
            if (exsteam == null) return NotFound();

            if (team.ImageFile != null)
            {
                if (team.ImageFile.ContentType != "image/jpeg" && team.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be image/jpeg or image/png");
                    return View();
                }

                if (team.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 2mb");
                    return View();
                }

                team.Image = Guid.NewGuid().ToString() + team.ImageFile.FileName;

                string path = Path.Combine(_env.WebRootPath, "upload/teams", team.Image);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    team.ImageFile.CopyTo(stream);
                }

                if (exsteam.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "upload/teams", exsteam.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);
                }

                exsteam.Image = team.Image;
            }
            else
            {
                if (team.Image == null && exsteam.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "upload/teams", exsteam.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);

                    exsteam.Image = null;
                }
            }


            exsteam.Name = team.Name;
            exsteam.Position = team.Position;
            exsteam.FacebookUrl = team.FacebookUrl;
            exsteam.TwitterUrl = team.TwitterUrl;
            exsteam.YoutubeUrl = team.YoutubeUrl;
            exsteam.GoogleUrl = team.GoogleUrl;



            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
