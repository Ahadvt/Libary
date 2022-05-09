using JuanBackFinal.DAL;
using JuanBackFinal.Models;
using JuanBackFinal.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanBackFinal.Controllers
{
    public class BasketController : Controller
    {

        private readonly JuanAppDbContext _context;
        public BasketController(JuanAppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>  Index()
        {
            string cookieBasket = HttpContext.Request.Cookies["basket"];
            List<BasketVM> basketVMs = null;
            if (cookieBasket != null)
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }
            foreach (BasketVM basketVM in basketVMs)
            {
                Product dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
                basketVM.Image = dbProduct.MainImage;
                basketVM.Price = dbProduct.DiscountPrice > 0 ? dbProduct.DiscountPrice : dbProduct.SalePrice;
                basketVM.Name = dbProduct.Name;
            }
            return View(basketVMs);
        }
        //public async Task<IActionResult> Update(int? id, int? count)
        //{
        //    if (id == null) return BadRequest();
        //    Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    string cookieBasket = HttpContext.Request.Cookies["basket"];

        //    List<BasketVM> basketVMs = null;


        //    if (cookieBasket != null)
        //    {
        //        basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);

        //        if (!basketVMs.Any(b => b.ProductId == id))
        //        {
        //            return NotFound();
        //        }
        //        basketVMs.Find(b => b.ProductId == id).Count = (int)count;

        //    }
        //    else
        //    {
        //        return BadRequest(); 
        //    }


        //    cookieBasket = JsonConvert.SerializeObject(basketVMs);
        //    HttpContext.Response.Cookies.Append("basket", cookieBasket);

        //    foreach (BasketVM basketVM in basketVMs)
        //    {
        //        Product dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
        //        basketVM.Image = dbProduct.MainImage;
        //        basketVM.Price = dbProduct.DiscountPrice > 0 ? dbProduct.DiscountPrice : dbProduct.SalePrice;
        //        basketVM.Name = dbProduct.Name;
        //    }


        //    return PartialView("_BasketIndexPartial",basketVMs);
        //}



        public async Task<IActionResult> Update(int? id, int? count)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            string cookieBasket = HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (cookieBasket != null)
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);

                if (!basketVMs.Any(b => b.ProductId == id))
                {
                    return NotFound();
                }

                basketVMs.Find(b => b.ProductId == id).Count = (int)count;
            }
            else
            {
                return BadRequest();
            }

            cookieBasket = JsonConvert.SerializeObject(basketVMs);
            HttpContext.Response.Cookies.Append("basket", cookieBasket);

            foreach (BasketVM basketVM in basketVMs)
            {
                Product dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
                basketVM.Image = dbProduct.MainImage;
                basketVM.Price = dbProduct.DiscountPrice > 0 ? dbProduct.DiscountPrice : dbProduct.SalePrice;
                //basketVM.ExTax = dbProduct.ExTax;
                basketVM.Name = dbProduct.Name;
               
            }

            return PartialView("_BasketIndexPartial", basketVMs);
        }


        public async Task<IActionResult> Delete(int id)
        {
      

            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            string cookieBasket = HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (cookieBasket != null)
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);

                if (!basketVMs.Any(b => b.ProductId == id))
                {
                    return NotFound();
                }
                BasketVM RemoveItem = basketVMs.Find(b => b.ProductId == id);
                basketVMs.Remove(RemoveItem);
                //basketVMs.Find(b => b.ProductId == id).Count = (int)count;
            }
            else
            {
                return BadRequest();
            }

            cookieBasket = JsonConvert.SerializeObject(basketVMs);
            HttpContext.Response.Cookies.Append("basket", cookieBasket);

            foreach (BasketVM basketVM in basketVMs)
            {
                Product dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
                basketVM.Image = dbProduct.MainImage;
                basketVM.Price = dbProduct.DiscountPrice > 0 ? dbProduct.DiscountPrice : dbProduct.SalePrice;
                //basketVM.ExTax = dbProduct.ExTax;
                basketVM.Name = dbProduct.Name;

            }

            return PartialView("_BasketIndexPartial", basketVMs);
        }

        public async Task<IActionResult> DeleteItem(int id)
        {


            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            string cookieBasket = HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (cookieBasket != null)
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);

                if (!basketVMs.Any(b => b.ProductId == id))
                {
                    return NotFound();
                }
                BasketVM RemoveItem = basketVMs.Find(b => b.ProductId == id);
                basketVMs.Remove(RemoveItem);
                //basketVMs.Find(b => b.ProductId == id).Count = (int)count;
            }
            else
            {
                return BadRequest();
            }

            cookieBasket = JsonConvert.SerializeObject(basketVMs);
            HttpContext.Response.Cookies.Append("basket", cookieBasket);

            foreach (BasketVM basketVM in basketVMs)
            {
                Product dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
                basketVM.Image = dbProduct.MainImage;
                basketVM.Price = dbProduct.DiscountPrice > 0 ? dbProduct.DiscountPrice : dbProduct.SalePrice;
                //basketVM.ExTax = dbProduct.ExTax;
                basketVM.Name = dbProduct.Name;

            }

            return PartialView("_BasketPartial", basketVMs);
        }
    }
}
