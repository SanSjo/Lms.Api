using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAllModules();
        Task<Module> GetModule(string title);
        Task<bool> SaveAsync();
        Task AddAsync<T>(T added);
        void Remove<T>(T removed);
    }
}
