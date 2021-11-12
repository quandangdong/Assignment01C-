using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {

        //Load admin info from json
        MemberObject adminMember = Program.Configuration.GetSection("AdminAccount").Get<MemberObject>();

        public IMemberRepository MemberRepository = new MemberRepository();
        public frmLogin()
        {
            InitializeComponent();
        }


        public MemberObject Login()
        {
            MemberObject user = null;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            
            if (email == adminMember.Email && password == adminMember.Password)
            {
                return adminMember;
            }
            else if (email != adminMember.Email && password != adminMember.Password)
            {
                user = MemberRepository.CheckUser(email, password);
                return user;
            }
            else return user;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = Login();
            string email = txtEmail.Text;
            string password = txtPassword.Text;


            if (email == "" || password == "")
            {
                DialogResult dialogResult = MessageBox.Show("Please enter all email and password", 
                    "Fstore error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (user!=null)
            {
                //MessageBox.Show("Login successful");
                frmMemberManagement MemberForm = new frmMemberManagement (user) {

                    Text = "Member Management"

                };
                MemberForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Fail to login");
            }
        }
    }
}
