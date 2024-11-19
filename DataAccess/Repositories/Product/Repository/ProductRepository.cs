using Domain.DataClass;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlDataAccess _dataAccess;
        private readonly IConfiguration _configuration;

        public ProductRepository(ISqlDataAccess db, IConfiguration configuration)
        {
            _dataAccess = db;
            _configuration = configuration;
        }

        public async Task<bool> AddAsync(AddProduct product)
        {
            try
            {
                _ = await _dataAccess.SaveData("sp_add_Product", new
                {
                    product.ProductName,
                    product.ProductDescription,
                    product.ImgUrl,
                    product.ProductIsActive
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UpdateProduct product)
        {
            try
            {
                _ = await _dataAccess.SaveData("sp_update_Product", new
                {
                    product.Id,
                    product.ProductName,
                    product.ProductDescription,
                    product.ImgUrl,
                    product.ProductIsActive
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _ = await _dataAccess.SaveData("sp_delete_Product", new { Id = id });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDetails?> GetByIdAsync(int id)
        {
            IEnumerable<ProductDetails> result = await _dataAccess.GetData<ProductDetails, dynamic>
                ("sp_get_Product", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ProductDataTableList>> GetAllProductAsync(int pageNumber, int rowsOfPage)
        {
            string query = "sp_get_AllProduct";
            return await _dataAccess.GetData<ProductDataTableList, dynamic>(query, new { PageNumber = pageNumber, RowsOfPage = rowsOfPage });
        }
    }
}