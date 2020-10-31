using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using despensa.Models;
using Microsoft.AspNetCore.Http;
using despensa.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace despensa.Controllers
{
    public class CartController : Controller
    {
        [Route("index")]
        [Authorize(Roles = "3,2,1")]
        public IActionResult Index()
        {
            try
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.PrecioVenta * item.Quantity);
                return View();
            }
            catch
            {
                List<Item> cart = new List<Item>();
                ViewBag.cart = cart;
                ViewBag.total = 0;
                return View();
            }
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
        [Authorize(Roles = "3,2,1")]
        //   public async Task<IActionResult> DeleteConfirmed(int id)
        public async Task compra(int id)
        {
            despensa1Context productModel = new despensa1Context();
            var productos = await productModel.Producto.FindAsync(id);
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = productos, Quantity = 1, precio_total_articulo = productos.PrecioVenta });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                    cart[index].precio_total_articulo = cart[index].precio_total_articulo + productos.PrecioVenta;
                }
                else
                {
                    cart.Add(new Item { Product = productos, Quantity = 1, precio_total_articulo = productos.PrecioVenta });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }

        [HttpGet]
        [Authorize(Roles = "3,2,1")]
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
            if (cantidad<0)
            {
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                foreach (var item in cart)
                {
                    if (item.Product.CodProducto.Equals(id))
                    {
                        item.Quantity = cantidad;
                        item.precio_total_articulo = item.Product.PrecioVenta * cantidad;
                    }
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
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