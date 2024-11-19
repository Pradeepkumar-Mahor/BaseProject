using DataAccess.Context;
using Domain.Core.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Interface
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCoolestCategory();
    }
}