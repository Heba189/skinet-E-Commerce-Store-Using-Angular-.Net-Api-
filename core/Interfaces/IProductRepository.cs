﻿using core.Entities;

namespace core;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
    Task<IReadOnlyList<ProductType>> GetProductTypeAsync();
}
