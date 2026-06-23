using ArcTestsServices.DTOs;
using ArcTestsServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArcTestsApis.Controllers;
//API layer - only depends on ArcTestsServices interfaces
//Arch rule: this controller must never reference to ArcTestsData.Repositories directly 
//NetArchTest enforces this via the dataLayerAccessPolicy 

[ApiController]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductAsync(id);
        return product is null ? NotFound() : Ok(product);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        await _productService.CreateProductAsync(request);
        return Created();
    }
}