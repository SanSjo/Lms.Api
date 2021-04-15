using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Core.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get { return StartDate.AddMonths(3); } }

        public ICollection<ModuleDto> Modules { get; set; }
    }
}
