using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PermissionDAO : EmployeeContext
    {
        public static void AddPermision(Permission permission)
        {
            try
            {
                db.Permission.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PermissionState> GetStates()
        {
            return db.PermissionState.ToList();
        }

        public static List<PermissionDetailDTO> GetPermissions()
        {
            List<PermissionDetailDTO> permission = new List<PermissionDetailDTO>();
            var list = (from p in db.Permission
                        join s in db.PermissionState on p.PermissionState equals s.Id
                        join e in db.Employee on p.EmployeeID equals e.ID
                        select new
                        {
                            userNo = e.UserNo, 
                            name = e.Name, 
                            surname = e.Surname,
                            stateName = s.StateName,
                            stateID = p.PermissionState,
                            startDate = p.PermissionStartDate,
                            endDate = p.PermissionEndDate,
                            employeeID = p.EmployeeID,
                            permissionID = p.ID,
                            explanation = p.PermissionExplanation,
                            dayAmount = p.PermissionDay,
                            departmentID = e.DepartmentID,
                            positionID = e.PositionID
                        }).OrderBy(x => x.startDate).ToList();

            foreach (var item in list)
            {
                PermissionDetailDTO dto = new PermissionDetailDTO();
                dto.UserNo = item.userNo;
                dto.Name = item.name;
                dto.Surname = item.surname;
                dto.EmployeeID = item.employeeID;
                dto.PermissionDayAmount = item.dayAmount;
                dto.StartDate = item.startDate;
                dto.EndDate = item.endDate;
                dto.DepartmentID = item.departmentID;
                dto.PositionID = item.positionID;
                dto.StateName = item.stateName;
                dto.State = item.stateID;
                dto.Explanation = item.explanation;
                permission.Add(dto);
            }
            return permission;
        }
    }
}
