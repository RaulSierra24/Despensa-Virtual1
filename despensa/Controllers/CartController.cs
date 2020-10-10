using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using despensa.Models;
using Microsoft.AspNetCore.Http;
using despensa.Helpers;



namespace despensa.Controllers
{
    public class CartController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.PrecioVenta * item.Quantity);
            return View();
        }

        [HttpGet]
        public JsonResult Get()
        {
            try{
                var entradas = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                return Json(entradas.ToList());
            }
            catch
            {
                List<Item> cart = new List<Item>();
                return Json(cart.ToList());
            }

        }


        [HttpGet]
        //   public async Task<IActionResult> DeleteConfirmed(int id)
        public async Task compra(int id)
        {
            despensaContext productModel = new despensaContext();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = await productModel.Producto.FindAsync(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = await productModel.Producto.FindAsync(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }

        [HttpGet]
        public void Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart.RemoveAt(index);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        }

        [HttpGet]
        public void Editar(int id, int cantidad)
        {
            Console.WriteLine("entre a editar la cantidad");
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            foreach (var item in cart)
            {
                if (item.Product.CodProducto.Equals(id))
                {
                    item.Quantity= cantidad;

                }
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int i = 0;
            foreach (var item in cart)
            {

                if (item.Product.CodProducto.Equals(id))
                {
                    Console.WriteLine("id " + i);
                    return i;

                }
                i++;
            }

            return -1;
        }

    }
}