using System.Linq;
using System.Collections.Generic;
using global::System.Web.Http;
using System.Net;
using System.Net.Http;
#pragma warning disable 1591 //stop warning about not having xml description for all fields

namespace SelfHostedWebApi.Controllers
{
    public class ProductsController : ApiController
    {
        private static List<Product> Products;

        private static int idCounter = 4; // counter for adding products to give them a unique id

        static ProductsController()
        {
            Products = new List<Product>();
            Products.Add(new Product() { Id = 1, Name = "iPad", Price=500 });
            Products.Add(new Product() { Id = 2, Name = "iPhone X", Price=900 });
            Products.Add(new Product() { Id = 3, Name = "Google Tablet", Price=400 });
        }

        // GET api/products
        public IEnumerable<Product> GetProducts()
        {
            return Products;
        }

        // GET api/products/2
        public Product GetProduct(int id)
        {
            //get the first one, as there can be duplicate product ids
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound); //404
            }
            return product;
        }

        // POST api/products
        public HttpResponseMessage PostProduct([FromBody()] Product product)
        {
            try
            {
                product.Id = idCounter;
                Products.Add(product);
                idCounter++;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, product); // 201
                return response;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); // 400
            }
        }

        // PUT api/products/5
        public HttpResponseMessage PutValue(int id, [FromBody()] Product product)
        {
            try
            {
                Products.Single(p => p.Id == id).Name = product.Name;
                Products.Single(p => p.Id == id).Price = product.Price;
                product.Id = id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, product); // 200
                return response;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); // 400
            }
        }

        // DELETE api/products/2
        public HttpResponseMessage DeleteValue(int id)
        {
            try
            {
                Products.Remove(Products.Single(p => p.Id == id));
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, id); // 200
                return response;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest); // 400
            }
        }
    }
}
