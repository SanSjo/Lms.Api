using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface IUnitOfWork
    {
        public ICourseRepository CourseRepository { get; }
        public IModuleRepository ModuleRepository { get; }

        Task CompleteAsync();
    }
}
