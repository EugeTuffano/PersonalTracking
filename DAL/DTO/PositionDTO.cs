using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PositionDTO:Position
    {
        public String DepartmentName { get; set; }
        public int OldDepartment { get; set; }
    }
}
