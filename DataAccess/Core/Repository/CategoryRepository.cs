using DataAccess.Context;
using DataAccess.Core.Interface;
using Domain.Core.Tables;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Category> GetCoolestCategory()
        {
            return await GetAll()
                .OrderByDescending(c => c.Name)
                .FirstOrDefaultAsync();
        }
    }
}