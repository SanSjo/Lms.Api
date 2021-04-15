using System;
using System.Collections.Generic;
using System.Text;

namespace Lms.Core.Dto
{
    public class ModuleDto
    {

        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get { return StartDate.AddMonths(1); } }
    }
}
