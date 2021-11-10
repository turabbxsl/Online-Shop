using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Data;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {

        private ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var values = _db.SpecialTags.ToList();

            return View(values);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTags);
                await _db.SaveChangesAsync();
                TempData["save"] = "Special Tag Başarıyla Kaydedildi";
                return RedirectToAction("Index");
            }
            return View(specialTags);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _specialTag = _db.SpecialTags.Find(id);

            if (_specialTag == null)
            {
                return NotFound();
            }

            return View(_specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTags specialTags)
        {
            if (ModelState.IsValid)
            {
                _db.Update(specialTags);
                await _db.SaveChangesAsync();
                TempData["update"] = "Special Tag Başarıyla Guncellendi";
                return RedirectToAction("Index");
            }
            return View(specialTags);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _specialTag = _db.SpecialTags.Find(id);

            if (_specialTag == null)
            {
                return NotFound();
            }

            return View(_specialTag);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTags specialTags)
        {
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _specialTag = _db.SpecialTags.Find(id);

            if (_specialTag == null)
            {
                return NotFound();
            }

            return View(_specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTags specialTags)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != specialTags.ID)
            {
                return NotFound();
            }

            var _specialTags = _db.SpecialTags.Find(id);
            if (_specialTags == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(_specialTags);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Special Tag Başarıyla Silindi";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }



    }
}
