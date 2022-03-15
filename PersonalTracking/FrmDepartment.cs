using System;
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


namespace PersonalTracking
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartment.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the name field");
            }
            else
            {
                Department department = new Department();
                if (!isUpdate)
                {
                    department.DeparmentName = txtDepartment.Text;
                    DepartmentBLL.AddDepartment(department);
                    MessageBox.Show("Department was added");
                    txtDepartment.Clear();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warning!!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        department.ID = detail.ID;
                        department.DeparmentName = txtDepartment.Text;
                        DepartmentBLL.UpdateDepartment(department);
                        MessageBox.Show("Department was updated");
                        this.Close();

                    }
                }
            }
        }
        public bool isUpdate = false;
        public Department detail = new Department();
        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (isUpdate)
                txtDepartment.Text = detail.DeparmentName;
        }
    }
}
