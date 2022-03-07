using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;

namespace PersonalTracking
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        TimeSpan PermissionDay;
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserStatic.UserNo.ToString();
        }

        private void dpStart_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dpFinish.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void dpFinish_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dpFinish.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDayAmount.Text.Trim() == "")
                MessageBox.Show("Please change end or start date");
            else if (Convert.ToInt32(txtDayAmount.Text) <= 0)
                MessageBox.Show("Permission day must be bigger than 0");
            else if (txtExplanation.Text.Trim() == "")
                MessageBox.Show("Explanation is empty");
            else
            {
                Permission permission = new Permission();
                permission.EmployeeID = UserStatic.EmployeeID;
                permission.PermissionState = 1;
                permission.PermissionStartDate = dpFinish.Value.Date;
                permission.PermissionEndDate = dpFinish.Value.Date;
                permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                permission.PermissionExplanation = txtExplanation.Text;
                PermissionBLL.AddPermision(permission);
                MessageBox.Show("Permission was added");
                permission = new Permission();
                dpStart.Value = DateTime.Today;
                dpFinish.Value = DateTime.Today;
                txtDayAmount.Clear();
                txtExplanation.Clear();



            }
        }
    }
}
