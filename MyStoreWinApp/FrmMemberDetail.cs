using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class FrmMemberDetail : Form
    {
        public FrmMemberDetail()
        {
            InitializeComponent();
        }

        //----------------------------------------------------
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public MemberObject MemberInfo { get; set; }

        //----------------------------------------------------

        private void FrmMemberDetail_Load(object sender, EventArgs e)
        {
            txtMemberID.Enabled = !InsertOrUpdate;
            if(InsertOrUpdate == true)
            {
                txtMemberID.Text = MemberInfo.MemberID.ToString();
                txtName.Text = MemberInfo.MemberName;
                txtEmail.Text = MemberInfo.Email;
                txtMemberPassword.Text = MemberInfo.Password;
                txtCity.Text = MemberInfo.City;
                txtCountry.Text = MemberInfo.Country;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var Member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtName.Text,
                    Password = txtMemberPassword.Text,
                    Email = txtEmail.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
                Console.WriteLine("Form member detail: " + Member);
                if(InsertOrUpdate == false)
                {
                    MemberRepository.AddMember(Member);
                }
                else
                {
                    MemberRepository.UpdateMember(Member);
                }
                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add Member" : "Update a Member");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}
