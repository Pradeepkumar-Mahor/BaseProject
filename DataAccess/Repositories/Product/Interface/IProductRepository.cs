using Domain.DataClass;

namespace CMR.DataAccess.Repositories
{
    public interface IProductRepository
    {
        public Task<bool> AddAsync(AddProduct product);

        public Task<bool> UpdateAsync(UpdateProduct product);

        public Task<bool> DeleteAsync(int id);

        public Task<ProductDetails?> GetByIdAsync(int id);

        public Task<IEnumerable<ProductDataTableList>> GetAllProductAsync(int pageNumber = 1, int rowsOfPage = 1);
    }
}