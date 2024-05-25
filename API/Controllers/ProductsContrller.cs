using Infrastructures.Data;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using core;
using core.Interfaces;
using API.Dtos;
using AutoMapper;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepo , IGenericRepository<ProductType> productTypeRepo , IGenericRepository<ProductBrand> productBrandRepo ,IMapper mapper)
    {
        _productRepo = productRepo;
        _productTypeRepo = productTypeRepo;
        _productBrandRepo = productBrandRepo;
        _mapper = mapper;
    }
 [HttpGet]
 public async Task <ActionResult<IReadOnlyList<ProductToReturnDto>>> Getproducts(){
     var spec = new ProductsWithTypesAndBrandsSpecification();
     var products =await  _productRepo.ListAsync(spec);
    //  return products.Select(Product => new ProductToReturnDto{
    //     Id= Product.Id,
    //     Name = Product.Name,
    //     Description = Product.Description,
    //     PictureUrl = Product.PictureUrl,
    //     Price = Product.Price,
    //     ProductBrand = Product.ProductBrand.Name,
    //     ProductType = Product.ProductType.Name
    //  }).ToList();
   return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
 }

[HttpGet("{id}")]
public async Task <ActionResult<ProductToReturnDto>> GetProduct(int id){
    var spec = new ProductsWithTypesAndBrandsSpecification(id);
    var product=  await _productRepo.GetEntityWithSpec(spec);
    // return new ProductToReturnDto{
    //     Id = product.Id,
    //     Name = product.Name,
    //     Description = product.Description,
    //     PictureUrl = product.PictureUrl,
    //     Price = product.Price,
    //     ProductBrand = product.ProductBrand.Name,
    //     ProductType = product.ProductType.Name
    // };
return _mapper.Map<Product, ProductToReturnDto>(product);
}
[HttpGet("brands")]
public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands(){
    return Ok(await _productBrandRepo.ListAllAsync());
}
[HttpGet("types")]
public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(){
    return Ok(await _productTypeRepo.ListAllAsync());
}
}
