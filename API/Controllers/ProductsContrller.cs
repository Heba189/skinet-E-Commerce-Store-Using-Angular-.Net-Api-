using Infrastructures.Data;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using core;
using core.Interfaces;
using API.Dtos;
using AutoMapper;
using API.Errors;
using core.Specification;
using API.Helpers;

namespace API.Controllers;

public class ProductsController : BaseApiController
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
 public async Task <ActionResult<Pagination<ProductToReturnDto>>> Getproducts(
   [FromQuery] ProductSpecParams productParams){
     var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
     var countSpec= new ProductWithFiltersForCountSpecification(productParams);
     var totalItems = await _productRepo.CountAsync(countSpec);        
     var products =await  _productRepo.ListAsync(spec);
     var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
     return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItems,data));
 }

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]

public async Task <ActionResult<ProductToReturnDto>> GetProduct(int id){
    var spec = new ProductsWithTypesAndBrandsSpecification(id);
    var product=  await _productRepo.GetEntityWithSpec(spec);
    if(product == null) return NotFound(new ApiResponse(404));
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
