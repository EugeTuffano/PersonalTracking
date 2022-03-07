using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PositionDAO : EmployeeContext
    {
        public static void AddPosition(Position position)
        {
            try
            {
                db.Position.InsertOnSubmit(position);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PositionDTO> GetPositions()
        {
            try
            {
                var list = (from p in db.Position
                            join d in db.Department on p.DepartmentID equals d.ID
                            select new
                            {
                                positionID = p.ID,
                                PositionName = p.PositionName,
                                deparmentName = d.DeparmentName,
                                deparmentID = p.DepartmentID
                            }).OrderBy(x => x.positionID).ToList();
                List<PositionDTO> positionList = new List<PositionDTO>();
                foreach (var item in list)
                {
                    PositionDTO dto = new PositionDTO();
                    dto.ID = item.positionID;
                    dto.PositionName = item.PositionName;
                    dto.DepartmentName = item.deparmentName;
                    dto.DepartmentID = item.deparmentID;
                    positionList.Add(dto);
                }
                return positionList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
