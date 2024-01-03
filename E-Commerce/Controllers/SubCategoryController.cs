using E_Commerce.DTO;
using E_Commerce.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly Context context;
        public SubCategoryController(Context _context)
        {
            context = _context;

        }
        [HttpGet]
        public ActionResult GetAll() 
        {
            List<SubCategory>subCategories = context.SubCategories.Include(p=>p.Product).ToList();
            List<SubCategoryDTO> subCategoryDTOs = subCategories.Select(subcategory => new SubCategoryDTO
            {
                SubCategoryID = subcategory.ID,
                SubCategoryName=subcategory.Name,
                SubCategoryProducts = subcategory.Product.Select(product=>new ProductDTO
                {
                    ProductID = product.ID,
                    ProductName= product.Name,
                }).ToList()
            }).ToList();
            

            return Ok(subCategoryDTOs);
           
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            SubCategory subCategory = context.SubCategories.Include(p=>p.Product).FirstOrDefault(s=>s.ID == id);
            SubCategoryDTO subCategoryDTO = new SubCategoryDTO();
            subCategoryDTO.SubCategoryID = subCategory.ID;
            subCategoryDTO.SubCategoryName = subCategory.Name;
            subCategoryDTO.SubCategoryProducts = subCategory.Product.Select(product => new ProductDTO
            {
                ProductID = product.ID,
                ProductName = product.Name,
            }).ToList();

            return Ok(subCategoryDTO);

        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            SubCategory subCategory = context.SubCategories.Include(p => p.Product).FirstOrDefault(s => s.Name == name);

            SubCategoryDTO subCategoryDTO = new SubCategoryDTO();
            subCategoryDTO.SubCategoryID = subCategory.ID;
            subCategoryDTO.SubCategoryName= subCategory.Name;
            subCategoryDTO.SubCategoryProducts = subCategory.Product.Select(product => new ProductDTO
            {
                ProductID = product.ID,
                ProductName = product.Name,
            }).ToList();

            return Ok(subCategoryDTO);

        }


        [HttpPost]
        public IActionResult NewSubCategory (SubCategory newsubCategory)
        {
            if(ModelState.IsValid)
            {
                context.SubCategories.Add(newsubCategory);
                context.SaveChanges();
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Edit (int id,SubCategory newsubCategory)
        {
            if(ModelState.IsValid) 
            {
                SubCategory orgsubCategory = context.SubCategories.FirstOrDefault(s=>s.ID==id);
                if(orgsubCategory != null) 
                {
                    orgsubCategory.Name = newsubCategory.Name;
                }
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSubCategory(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SubCategory OrgsubCategory = context.SubCategories.FirstOrDefault(s => s.ID == id);
                    if (OrgsubCategory == null)
                    {
                        return new StatusCodeResult(StatusCodes.Status404NotFound);
                    }
                    context.Remove(OrgsubCategory);
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
