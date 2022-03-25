using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class EmployeeDetailDTO
    {
        public int EmployeeID { get; set; } //0
        public int UserNo { get; set; }  //1
        public string Password { get; set; }  //2
        public string Name { get; set; }  //3
        public string Surname { get; set; }  //4
        public string DepartmentName { get; set; }  //5
        public string PositionName { get; set; }  //6
        public int DepartmentID { get; set; }  //7
        public int PositionID { get; set; }  //8
        public int Salary { get; set; }  //9
        public bool? isAdmin { get; set; }  //10
        public string ImagePath { get; set; }  //11
        public string Adress { get; set; } //12
        public DateTime? BirthDay { get; set; }  //13
        public string Email { get; set; }  //14
        public int? PhoneNumber { get; set; }  //15
        public DateTime? AdmissionDate { get; set; }  //16

    }
}
