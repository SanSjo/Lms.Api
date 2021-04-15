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
    public class CourseRepository : ICourseRepository
    {
        private readonly LmsApiContext db;

        public CourseRepository(LmsApiContext db)
        {
            this.db = db;
        }

   

        public async Task<IEnumerable<Course>> GetAllCourses(bool includeModules)
        {
            return includeModules ? await db.Course.ToListAsync() : await db.Course.Include(c => c.Modules).ToListAsync();
        }

        public async Task<Course> GetCourse(int? id)
        {
            var course = await db.Course.FindAsync(id);
            return course;
            //var query = db.Course.AsQueryable();

            //if (includeModules)
            //{
            //    query = query.Include(e => e.Modules);
            //}

            //return await query.FirstOrDefaultAsync(c => c.Id == id);


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
