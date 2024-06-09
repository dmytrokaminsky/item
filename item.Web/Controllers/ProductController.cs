using AutoMapper;
using item.Domain;
using item.Persistence;
using item.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace item.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private readonly ShopDatabaseContext dbContext;
    private readonly IMapper mapper;

    public ProductController(ShopDatabaseContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    [HttpGet("list")]
    public async Task<IActionResult> Get()
    {
        var products = await dbContext.Products.ToListAsync();

        return Ok(products);
    }

    [HttpGet("byTitle")]
    public async Task<IActionResult> GetByTitle(string title)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(c => c.Title == title);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductViewModel createProductViewModel)
    {
        var product = this.mapper.Map<Product>(createProductViewModel);

        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();

        return Created("test", product.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateProductViewModel updateProductViewModel)
    {


        var product = await dbContext.Products.FindAsync(updateProductViewModel.id);
        if (product == null)
        {
            return NotFound();
        }

        product.Title = updateProductViewModel.Title;
        product.Price = updateProductViewModel.Price;
        product.CategoryId = updateProductViewModel.CategoryId;
       

        await dbContext.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var cproduct = await dbContext.Products.FindAsync(id);
        if (cproduct == null)
        {
            return NotFound();
        }

        dbContext.Remove(cproduct);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }


}