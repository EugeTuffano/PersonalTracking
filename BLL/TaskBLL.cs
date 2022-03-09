using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;

namespace BLL
{
    public class TaskBLL
    {
        public static TaskDTO GetAll()
        {
            TaskDTO taskdto = new TaskDTO();
            taskdto.Employees = EmployeeDAO.GetEmployees();
            taskdto.Departments = DepartmentDAO.GetDepartments();
            taskdto.Positions = PositionDAO.GetPositions();
            taskdto.TaskStates = TaskDAO.GetTaskStates();
            taskdto.Tasks = TaskDAO.GetTasks();
            return taskdto;

        }

        public static void AddTask(DAL.Task task)
        {
            TaskDAO.AddTask(task);
        }

        public static void UpdateTask(DAL.Task task)
        {
            TaskDAO.UpdateTask(task);
        }
    }
}
