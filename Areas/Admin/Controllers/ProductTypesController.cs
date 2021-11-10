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
    public class ProductTypesController : Controller
    {

        private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Type Başarıyla Kaydedildi";
                return RedirectToAction("Index");
            }
            return View(productTypes);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _productType = _db.ProductTypes.Find(id);

            if (_productType == null)
            {
                return NotFound();
            }

            return View(_productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                TempData["update"] = "Product Type Başarıyla Guncellendi";
                return RedirectToAction("Index");
            }
            return View(productTypes);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _productType = _db.ProductTypes.Find(id);

            if (_productType == null)
            {
                return NotFound();
            }

            return View(_productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
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

            var _productType = _db.ProductTypes.Find(id);

            if (_productType == null)
            {
                return NotFound();
            }

            return View(_productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id , ProductTypes productTypes)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != productTypes.ID)
            {
                return NotFound();
            }

            var _productType = _db.ProductTypes.Find(id);
            if (_productType == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(_productType);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Product Type Başarıyla Silindi";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


    }
}
