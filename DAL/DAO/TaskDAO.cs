﻿using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static List<TaskState> GetTaskStates()
        {
            return db.TaskState.ToList();
        }

        public static void AddTask(Task task)
        {
            try
            {
                db.Task.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<TaskDetailDTO> GetTasks()
        {
            List<TaskDetailDTO> tasklist = new List<TaskDetailDTO>();
            var list = (from t in db.Task
                        join s in db.TaskState on t.TaskState equals s.ID
                        join e in db.Employee on t.EmployeeID equals e.ID
                        join d in db.Department on e.DepartmentID equals d.ID
                        join p in db.Position on e.PositionID equals p.ID
                        select new
                        {
                            taskID = t.ID,
                            title = t.TaskTitle,
                            content = t.TaskContent,
                            startDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryDate,
                            taskStateName = s.StateName,
                            taskStateID = t.TaskState,
                            UserNo = e.UserNo,
                            Name = e.Name,
                            EmployeeID = t.EmployeeID,
                            Surname = e.Surname,
                            positionName = p.PositionName,
                            departmentName = d.DeparmentName,
                            positionID = e.PositionID,
                            departmentID = e.DepartmentID,

                        }).OrderBy(x => x.startDate).ToList();

            foreach (var item in list)
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.TaskID = item.taskID;
                dto.Title = item.title;
                dto.Content = item.content;
                dto.TaskStartDate = item.startDate;
                dto.TaskDeliveryDate = item.deliveryDate;
                dto.TaskStateName = item.taskStateName;
                dto.TaskStateID = item.taskStateID;
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.DepartmentName = item.departmentName;
                dto.PositionID = item.positionID;
                dto.PositionName = item.positionName;
                dto.EmployeeID = item.EmployeeID;
                tasklist.Add(dto);
            }

            return tasklist;
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            try
            {
                Task tsk = db.Task.First(x => x.ID == taskID);
                if (isAdmin)
                    tsk.TaskState = TaskStates.Delivered;
                tsk.TaskDeliveryDate = DateTime.Today;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteTask(int taskID)
        {
            try
            {
                Task ts = db.Task.First(x => x.ID == taskID);
                db.Task.DeleteOnSubmit(ts);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateTask(Task task)
        {
            try
            {
                Task ts = db.Task.First(x => x.ID == task.ID);
                ts.TaskTitle = task.TaskTitle;
                ts.TaskContent = task.TaskContent;
                ts.TaskState = task.TaskState;
                ts.EmployeeID = task.EmployeeID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
