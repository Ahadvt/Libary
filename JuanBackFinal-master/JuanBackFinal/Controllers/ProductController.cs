using JuanBackFinal.DAL;
using JuanBackFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using JuanBackFinal.ViewModels.Basket;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace JuanBackFinal.Controllers
{
    public class ProductController : Controller
    {
        private readonly JuanAppDbContext _context;

        public ProductController(JuanAppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DetailModal(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null) return NotFound();

            return PartialView("_ProductDetailPartial", product);
        }

        public async Task<IActionResult> AddBasket(int?id,int count=1)
        {

            if(id==null)return BadRequest();
            Product product = await _context.Products.FirstOrDefaultAsync(p=>p.Id==id);
            if (product==null)
            {
                return NotFound();
            }

            string cookieBasket = HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;


            if (cookieBasket!=null)
            {
             basketVMs=JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);

                if (basketVMs.Any(b=>b.ProductId==id))
                {
                    basketVMs.Find(b => b.ProductId == id).Count += count;
                }
                else
                {
                

                    basketVMs.Add(new BasketVM
                    {
                        ProductId = (int)id,
                        Count = count
                    });
                }
            }
            else
            {
                basketVMs = new List<BasketVM>();


                basketVMs.Add(new BasketVM
                {
                    ProductId = (int)id,
                    Count = count
                });
            }

            cookieBasket=JsonConvert.SerializeObject(basketVMs);
            HttpContext.Response.Cookies.Append("basket", cookieBasket);

            foreach (BasketVM basketVM in basketVMs)
            {
                Product dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.ProductId);
                basketVM.Image = dbProduct.MainImage;
                basketVM.Price = dbProduct.DiscountPrice > 0 ? dbProduct.DiscountPrice : dbProduct.SalePrice;
                basketVM.Name = dbProduct.Name; 
            }

            return PartialView("_BasketPartial", basketVMs);
        }

        [HttpGet]
        public async Task<int> GetBasket()
        {
           
            string cookieBasket =HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(cookieBasket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookieBasket);
            }
     
            return basketVMs.Count();
        }

    }
}
