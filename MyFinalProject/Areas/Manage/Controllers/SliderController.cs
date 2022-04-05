
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Dal;
using MyFinalProject.Helpers;
using MyFinalProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFinal.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SliderController:Controller
    {
        private readonly ProjectContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(ProjectContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        
        public IActionResult Index(int page = 1)
        {
            var slider = _context.Sliders.AsQueryable();
            return View(PaginatedList<Slider>.Create(slider, 2, page));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            var existslider = _context.Sliders.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Invalid Content Type");
                }

                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Invalid Image Length");
                }
            }

            slider.Image = FileManager.Save("upload/slider", slider.ImageFile, _env.WebRootPath);
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var existslider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (existslider == null)
            {
                return View();
            }
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Invalid Content Type");
                }

                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Invalid Image Length");
                }

                var newimage = FileManager.Save("upload/slider", slider.ImageFile, _env.WebRootPath);
                if (existslider.Image != null)
                {
                    FileManager.Delete("upload/slider", existslider.Image, _env.WebRootPath);

                }
                existslider.Image = newimage;
            }
            else
            {
                if (slider.Image == null && existslider.Image != null)
                {
                    FileManager.Delete("upload/slider", existslider.Image, _env.WebRootPath);
                    existslider.Image = null;
                }

            }
            existslider.BtnText = slider.BtnText;
            existslider.BtnUrl = slider.BtnUrl;
            existslider.Desc1 = slider.Desc1;
            existslider.Desc2 = slider.Desc2;
            existslider.Title1 = slider.Title1;
            existslider.Title2 = slider.Title2;
            existslider.Order = slider.Order;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            var existslider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (existslider.Image!=null)
            {
            FileManager.Delete("upload/slider", existslider.Image, _env.WebRootPath);

            }
            _context.Sliders.Remove(existslider);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
