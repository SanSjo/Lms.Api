using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    class ModuleRepository : IModuleRepository
    {

        private readonly LmsApiContext db;

        public ModuleRepository(LmsApiContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await db.Module.ToListAsync();
        }

        public async Task<Module> GetModule(string title)
        {
            var module = await db.Module.FirstOrDefaultAsync(m => m.Title == title);

            return module;
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync() >= 0);
        }

        public async Task AddAsync<T>(T added)
        {
            await db.AddAsync(added);
        }

        public void Remove<T>(T removed)
        {
            db.Remove(removed);
        }

    }
}
