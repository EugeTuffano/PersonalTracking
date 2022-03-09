using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class SalaryBLL
    {
        public static SalaryDTO GetAll()
        {
            SalaryDTO dto = new SalaryDTO();
            dto.Employees = EmployeeDAO.GetEmployees();
            dto.Departments = DepartmentBLL.GetDepartments();
            dto.Positions = PositionDAO.GetPositions();
            dto.Months = SalaryDAO.GetMonths();
            dto.Salaries = SalaryDAO.GetSalaries();
            return dto;
        }

        public static void AddSalary(Salary salary, bool control)
        {
            SalaryDAO.AddSalary(salary);
            if (control)
                EmployeeDAO.UpdateEmployee(salary.EmployeeId, salary.Amount);
        }

        public static void UpdateSalary(Salary salary, bool control)
        {
            SalaryDAO.UpdateSalary(salary);
            if (control)
                EmployeeDAO.UpdateEmployee(salary.EmployeeId, salary.Amount);
        }
    }
}
