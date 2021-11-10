using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Shop.Data;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private ApplicationDbContext _db;
        private IHostingEnvironment _he;

        public ProductController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTags).ToList());
        }

        [HttpPost]       
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _db.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTags).Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();

            if (lowAmount == null || largeAmount == null)
            {
                products = _db.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTags).ToList();
            }

            return View(products);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["productTypeID"] = new SelectList(_db.ProductTypes.ToList(), "ID", "ProductType");
            ViewData["TagID"] = new SelectList(_db.SpecialTags.ToList(), "ID", "SpecialTag");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {

                var searchProduct = _db.Products.FirstOrDefault(c => c.Name == products.Name);

                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeID"] = new SelectList(_db.ProductTypes.ToList(), "ID", "ProductType");
                    ViewData["TagID"] = new SelectList(_db.SpecialTags.ToList(), "ID", "SpecialTag");
                    return View(products);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "images/noimage.jpg";
                }

                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Başarıyla Kaydedildi";
                return RedirectToAction("Index");
            }
            return View(products);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            ViewData["productTypeID"] = new SelectList(_db.ProductTypes.ToList(), "ID", "ProductType");
            ViewData["TagID"] = new SelectList(_db.SpecialTags.ToList(), "ID", "SpecialTag");

            if (id == null)
            {
                return NotFound();
            }

            var _products = _db.Products.Include(c => c.ProductTypes).Include(x => x.SpecialTags).FirstOrDefault(p => p.ID == id);

            if (_products == null)
            {
                return NotFound();
            }

            return View(_products);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "images/noimage.jpg";
                }

                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product Başarıyla Kaydedildi";
                return RedirectToAction("Index");
            }
            return View(products);
        }

        [Authorize]
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(x => x.ProductTypes).Include(c => c.SpecialTags).FirstOrDefault(c => c.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(x => x.ProductTypes).Include(z => z.SpecialTags).Where(x => x.ID == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id,string sr)
        {
            if (id == null)
            {
                return NotFound();
            }


            var _productType = _db.Products.Find(id);
            if (_productType == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(_productType);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Product Başarıyla Silindi";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }




    }
}
