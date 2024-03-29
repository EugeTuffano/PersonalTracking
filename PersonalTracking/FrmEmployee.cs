﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;
using System.IO;
using System.Text.RegularExpressions;

namespace PersonalTracking
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool isUpdate = false;
        string imagepath = "";

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DeparmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = true;
            if (isUpdate)
            {
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtUserNo.Text = detail.UserNo.ToString();
                txtPassword.Text = General.descifrar(detail.Password);
                chAdmin.Checked = Convert.ToBoolean(detail.isAdmin);
                txtAdress.Text = detail.Adress;
                dateTimePicker1.Value = Convert.ToDateTime(detail.BirthDay);
                cmbDepartment.SelectedValue = detail.DepartmentID;
                cmbPosition.SelectedValue = detail.PositionID;
                txtSalary.Text = detail.Salary.ToString();
                txtPhoneNumber.Text = detail.PhoneNumber.ToString();
                txtEmail.Text = detail.Email;
                imagepath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                txtImagePath.Text = imagepath;
                pictureBox1.ImageLocation = imagepath;
               

                if (!UserStatic.isAdmin)
                {
                    chAdmin.Enabled = false;
                    txtUserNo.Enabled = false;
                    txtSalary.Enabled = false;
                    cmbDepartment.Enabled = false;
                    cmbPosition.Enabled = false;
                }
            }
        }

        bool combofull = false;

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == departmentID).ToList();
            }
        }

        string fileName = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string Unique = Guid.NewGuid().ToString();
                fileName += Unique + openFileDialog1.SafeFileName;
            }
        }
        private bool form = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            ValidateForm();
            /*
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User number is empty");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Password is empty");
            else if (txtName.Text.Trim() == "")
                MessageBox.Show("Name is empty");
            else if (txtSurname.Text.Trim() == "")
                MessageBox.Show("Surname is empty");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("Salary is empty");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Select a department");
            else if (cmbPosition.SelectedIndex == -1)
                MessageBox.Show("Select a position");
            else*/

            if(form == true)
            {
                if (!isUpdate)
                {
                    if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                        MessageBox.Show("This user number is used by another employee, please change");
                    else
                    {
                        Employee employee = new Employee();
                        employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = General.cifrar(txtPassword.Text);
                        employee.isAdmin = chAdmin.Checked;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.BirthDay = dateTimePicker1.Value;
                        employee.Adress = txtAdress.Text;
                        employee.ImagePath = fileName;
                        employee.PhoneNumber = Convert.ToInt32(txtPhoneNumber.Text);
                        employee.Email = txtEmail.Text;
                        employee.AdmissionDate = DateTime.Today;

                        EmployeeBLL.AddEmployee(employee);
                        File.Copy(txtImagePath.Text, @"images\\" + fileName);
                        MessageBox.Show("Employee was added");

                        txtUserNo.Clear();
                        txtPassword.Clear();
                        chAdmin.Checked = false;
                        txtName.Clear();
                        txtSurname.Clear();
                        txtSalary.Clear();
                        cmbDepartment.SelectedIndex = -1;
                        cmbPosition.DataSource = dto.Positions;
                        cmbPosition.SelectedIndex = -1;
                        combofull = true;
                        dateTimePicker1.Value = DateTime.Today;
                        txtAdress.Clear();
                        txtImagePath.Clear();
                        pictureBox1.Image = null;
                        txtPhoneNumber.Clear();
                        txtEmail.Clear();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Employee employee = new Employee();
                        if (txtImagePath.Text != imagepath)
                        {
                            if (File.Exists(@"images\\" + detail.ImagePath))
                                File.Delete(@"images\\" + detail.ImagePath);
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                            employee.ImagePath = fileName;
                        }
                        else
                        {
                            employee.ImagePath = detail.ImagePath;
                            employee.ID = detail.EmployeeID;
                            employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                            employee.Name = txtName.Text;
                            employee.Surname = txtSurname.Text;
                            employee.isAdmin = chAdmin.Checked;
                            employee.Password = General.cifrar(txtPassword.Text);
                            employee.Adress = txtAdress.Text;
                            employee.BirthDay = dateTimePicker1.Value;
                            employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                            employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                            employee.Salary = Convert.ToInt32(txtSalary.Text);
                            employee.Email = txtEmail.Text;
                            employee.PhoneNumber = Convert.ToInt32(txtPhoneNumber.Text);
                            //employee.AdmissionDate = DateTime.Today;
                            employee.AdmissionDate = detail.AdmissionDate;
                            EmployeeBLL.UpdateEmployee(employee);
                            
                            MessageBox.Show("Employee was updated");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void ValidateForm()
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User number is empty");
            else if (!ValidatePassword(txtPassword.Text))
                MessageBox.Show("Password is not correct");
            else if (txtName.Text.Trim() == "")
                MessageBox.Show("Name is empty");
            else if (txtSurname.Text.Trim() == "")
                MessageBox.Show("Surname is empty");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("Salary is empty");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Select a department");
            else if (cmbPosition.SelectedIndex == -1)
                MessageBox.Show("Select a position");
            else if (!ValidateEmail(txtEmail.Text))
                MessageBox.Show("The mail is not correct");
            else if (!ValidatePhoneNumber(txtPhoneNumber.Text))
                MessageBox.Show("The phone number is not correct");
            else
            {
                form = true;
            }
        }

        private bool ValidatePassword(string password)
        {
            var expresion = @"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{3,10}$";
            if (Regex.IsMatch(password, expresion))
                return true;
            else
                return false;
        }
 
        private bool ValidatePhoneNumber(string phonenumber)
        {
            string expresion = @"\A(11)[0-9]{4}[0-9]{4}\Z";
            if (Regex.IsMatch(phonenumber, expresion))
            {
                if (Regex.Replace(phonenumber, expresion, String.Empty).Length == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
            
        }

        private bool ValidateEmail(string email)
        {
            string expresion = @"\A(\w+\.?\w*\@\w+\.)(com)\Z";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        bool isUnique = false;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User number is empty");
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUnique)
                    MessageBox.Show("This user number is used by another employee, please change");
                else
                    MessageBox.Show("This user number is usable");
            }
        }
    }
}
