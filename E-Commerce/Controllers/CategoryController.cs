using E_Commerce.DTO;
using E_Commerce.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Context context;
        public CategoryController(Context _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categoties = context.Categories.Include(s=>s.SubCategories).ThenInclude(p=>p.Product).ToList();
            List<CategoryDTO> categoryDTOs = categoties.Select(category => new CategoryDTO
            {
                CategoryID= category.ID,
                CategoryName= category.Name,
                SubCategoreisName=category.SubCategories.Select(subcategory=>new SubCategoryDTO
                {
                    SubCategoryID = subcategory.ID,
                    SubCategoryName = subcategory.Name,
                    SubCategoryProducts = subcategory.Product.Select(product => new ProductDTO
                    {
                        ProductID = product.ID,
                        ProductName = product.Name
                    }).ToList()

                }).ToList(),
            }).ToList();

            
            return Ok(categoryDTOs);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            Category category = context.Categories.Include(s=>s.SubCategories).ThenInclude(p=>p.Product).FirstOrDefault(c => c.ID == id);
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.CategoryID = category.ID;
            categoryDTO.CategoryName = category.Name;
            categoryDTO.SubCategoreisName = category.SubCategories.Select(subcategory => new SubCategoryDTO
            {
                SubCategoryName = subcategory.Name,
                SubCategoryProducts = subcategory.Product.Select(Product => new ProductDTO 
                {
                    ProductID = Product.ID,
                    ProductName=Product.Name,
                }).ToList()
             
            }).ToList();
            return Ok(categoryDTO);

        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Category category = context.Categories.Include(s=>s.SubCategories).ThenInclude(p=>p.Product).FirstOrDefault(c => c.Name == name);
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.CategoryID = category.ID;
            categoryDTO.CategoryName = category.Name;
            categoryDTO.SubCategoreisName = category.SubCategories.Select(subcategory => new SubCategoryDTO
            {
                SubCategoryName = subcategory.Name,
                SubCategoryProducts = subcategory.Product.Select(Product => new ProductDTO
                {
                    ProductID = Product.ID,
                    ProductName = Product.Name,
                }).ToList()



            }).ToList();
            return Ok(categoryDTO);
            

        }
        [HttpPost]
        public IActionResult NewCategory(Category newcategory)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(newcategory);
                return new StatusCodeResult(StatusCodes.Status201Created);


            }
            return Ok(newcategory);
        }
        [HttpPut("{id}")]
        public IActionResult EditCategory(int id,Category Newcategory)
        {
            if (ModelState.IsValid)
            {
                Category orgcategory = context.Categories.FirstOrDefault(c => c.ID == id);
                orgcategory.Name = orgcategory.Name;
                context.SaveChanges();
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category category = context.Categories.FirstOrDefault(c => c.ID == id);
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
