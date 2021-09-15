using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;


namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET Checkout actioin method

        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult form()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View (_db.Order.Include(c => c.Id).Include(c => c.Name).Include(c => c.Email).Include(c => c.PhoneNo).Include(c => c.Address).Include(c => c.OrderDetails).ToList());
        }
        [HttpPost]
        public IActionResult Index(int? lowAmount, int? largeAmount)
        {
            var orders = _db.Order.Include(c => c.Id).Include(c => c.Name).Include(c => c.Email).Include(c => c.PhoneNo).Include(c => c.Address).Include(c => c.OrderDetails).ToList();

            return View(orders);
        }

      
        public IActionResult Create()
        {
            
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order orders)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.Order.FirstOrDefault(c => c.Name == orders.Name );
                if (searchProduct != null)
                {
                   
       
                    return View(orders);
                }
                _db.Order.Add(orders);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }
       
        //POST Index action method
       
        //POST Checkout action method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Checkout(Order anOrder)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.PorductId = product.Id;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }

            _db.Order.Add(anOrder);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View(products);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Order.Include(c => c.OrderDetails).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST Delete Action Method

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Order.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Order.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

    }

}
