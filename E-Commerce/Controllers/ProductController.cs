using E_Commerce.DTO;
using E_Commerce.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Context Context;
        public ProductController(Context _context) 
        {
            Context = _context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Product>products = Context.Products.ToList();
            List<ProductDTO> productDTOs = products.Select(product => new ProductDTO
            {
                ProductID = product.ID,
                ProductName = product.Name,
                ProductPrice = product.Price,
            }).ToList();
            return Ok(productDTOs);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) 
        {
            
            if(ModelState.IsValid)
            {
                Product product = Context.Products.FirstOrDefault(p => p.ID == id);
                ProductDTO productDTO = new ProductDTO();
                productDTO.ProductID = product.ID;
                productDTO.ProductName = product.Name;
                productDTO.ProductPrice=product.Price;

                return Ok(productDTO);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]

        public ActionResult NewProduct(Product Newproduct)
        {
            if (ModelState.IsValid) 
            {
                Context.Products.Add(Newproduct);
                Context.SaveChanges();
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public IActionResult Edit (int id,Product newproduct) 
        {
            if(ModelState.IsValid)
            {
                Product orgProduct = Context.Products.FirstOrDefault(p=>p.ID == id);
                orgProduct.Name = newproduct.Name;
                orgProduct.Price = newproduct.Price;

                return new StatusCodeResult(StatusCodes.Status205ResetContent);

            }
            return BadRequest(ModelState);
        }

        [HttpDelete("id")]

        public IActionResult DeleteProduct(int id)
        {


            try
            {
                if(ModelState.IsValid)
                {
                    Product product = Context.Products.FirstOrDefault(p => p.ID == id);
                    Context.Products.Remove(product);
                    Context.SaveChanges();
                }
                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }






        }




    }
}
