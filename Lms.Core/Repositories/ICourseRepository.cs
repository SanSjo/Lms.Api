using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourse(int? id);
        Task<bool> SaveAsync();
        Task AddAsync<T>(T added);
    }
}
