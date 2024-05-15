using core;
using core.Entities;
using Infrastructures.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructures;

public class ProductRepository : IProductRepository
{
        private readonly StoreContext _context;
    public ProductRepository(StoreContext context)
    {
            _context = context;
        
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
                            // .FindAsync(id);
                            .Include(p => p.ProductType)
                            .Include(p => p.ProductBrand)
                            .FirstOrDefaultAsync(p=> p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products
                              .Include(p=>p.ProductType)
                              .Include(p=>p.ProductType)  
                              .ToListAsync();
    }
     public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
    {
        return await _context.ProductBrands.ToListAsync();
    }
     public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
    {
        return await _context.ProductTypes.ToListAsync();
    }
}
