using ENTITY1.Models;
using ENTITY1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ENTITY1.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{

    private readonly ILogger<CategoryController> _logger;

    private readonly ICategoryService _categoryService;
    public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var entities = await _categoryService.GetAllAsync();
        var result = from item in entities
                     select new CategoryViewModel
                     {
                         Id = item.Id,
                         Name = item.Name,
                         Products = from p in item.Products
                         select new ProductViewModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Manufacture = p.Manufacture

                            }
                     };
        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneAsync(int id)
    {
        // var entity = await _studentService.GetOneAsync(id);
        // if (entity == null) return NotFound();

        // return new JsonResult(new StudentViewModel
        // {
        //     Id = entity.Id,
        //     FullName = $"{entity.LastName} {entity.FirstName}",
        //     City = entity.City
        // });
        throw new NotImplementedException();
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync(CategoryCreateModel model)
    {
        try
        {

            var entity = new Data.Entities.Category
            {
                Name = model.Name,
                Products = (from p in model.Products
                            select new Data.Entities.Product
                            {
                                Name = p.Name,
                                Manufacture = p.Manufacture
                            }).ToList()
      
            };
            var result = await _categoryService.AddAsync(entity);
            return new JsonResult(new CategoryViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Products = from p in result.Products
                            select new ProductViewModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Manufacture = p.Manufacture
                            }

            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, CategoryCreateModel model)
    {

        // try
        // {
        //     var student = await _studentService.GetOneAsync(id);

        //     student.FirstName = model.FirstName;
        //     student.LastName = model.LastName;
        //     student.City = model.City;
        //     student.State = model.State;

        //     _studentService.EditAsync(student);

        //     return new JsonResult(student);
        // }
        // catch (Exception ex)
        // {
        //     // return NotFound(ex);
        //     _logger.LogError(ex, ex.Message);
        //     return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        // }
        throw new NotImplementedException();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {

        // try
        // {
        //     await _studentService.RemoveAsync(id); 
        //     return Ok();
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, ex.Message);
        //     return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        // }
        throw new NotImplementedException();


    }
}
