using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using BLL.Models;

namespace BLL
{
    public class DepartmentBLL
    {
        public static void AddDepartment(DepartmentModel entity)
        {
            Department department = new Department();
            department.ID = entity.ID;
            department.DeparmentName = entity.DeparmentName;
            DepartmentDAO.AddDepartment(department);
        }

        public static List<Department> GetDepartments()
        {
            Department department = new Department();

            return DepartmentDAO.GetDepartments();
        }

        public static void UpdateDepartment(DepartmentModel entity)
        {
            Department department = new Department();
            department.ID = entity.ID;
            department.DeparmentName = entity.DeparmentName;
            DepartmentDAO.UpdateDepartment(department);
        }

        public static void DeleteDepartment(int iD)
        {
            DepartmentDAO.DeleteDepartment(iD);
        }
    }
}
