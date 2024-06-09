using item.Domain;
using item.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace item.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{

    private readonly ShopDatabaseContext dbContext;

    public CategoryController(ShopDatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("list")]
    public async Task<IActionResult> Get()
    {
        var categories = await dbContext.Categories.ToListAsync();

        return Ok(categories);
    }

    [HttpGet("byTitle")]
    public async Task<IActionResult> GetByTitle(string title)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Title == title);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();

        return Ok(category);
    }

    [HttpPut]
    public async Task<IActionResult> Put(Category category)
    {
        var dbCategory = await dbContext.Categories.FindAsync(category.Id);
        if (category == null)
        {
            return NotFound();
        }
        dbCategory.Title = category.Title;

        await dbContext.SaveChangesAsync();
        return Ok(category);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        dbContext.Remove(category);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }


}