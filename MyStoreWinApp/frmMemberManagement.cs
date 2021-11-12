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
    public partial class frmMemberManagement : Form
    {
        public frmMemberManagement(MemberObject user)
        {
            InitializeComponent();
            loggedInUser = user;

        }
        //----------------------------------
        IMemberRepository memberRepository = new MemberRepository();
        private MemberObject loggedInUser { get; set; }
        //create data source
        BindingSource source;

        //----------------------------------


        private MemberObject GetMemberObject()
        {
            MemberObject member = null;
            try
            {
                if (loggedInUser.isAdmin == false)
                {
                    int txtMemberIDValue = int.Parse(txtMemberID.Text);
                    if (loggedInUser.MemberID == txtMemberIDValue)
                    {
                        member = new MemberObject
                        {
                            MemberID = int.Parse(txtMemberID.Text),
                            MemberName = txtMemberName.Text,
                            City = txtMemberCity.Text,
                            Country = txtMemberCountry.Text,
                            Email = txtMemberEmail.Text,
                            Password = txtPassword.Text,
                        };
                    }
                    else
                    {
                        MessageBox.Show("You can only update your own account!");
                    }

                }
                else
                {
                    member = new MemberObject
                    {
                        MemberID = int.Parse(txtMemberID.Text),
                        MemberName = txtMemberName.Text,
                        City = txtMemberCity.Text,
                        Country = txtMemberCountry.Text,
                        Email = txtMemberEmail.Text,
                        Password = txtPassword.Text,
                    };
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member");
            }
            return member;
        }


        private void ClearText()
        {
            txtMemberID.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtMemberEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtMemberCity.Text = string.Empty;
            txtMemberCountry.Text = string.Empty;
        }

        public void LoadMemberList(string action)
        { 
            IEnumerable<MemberObject> mems = null;
            string name = txtSearchName.Text;
            string id = txtSearchID.Text;
            string city = txtFilterCity.Text;
            string country = txtFilterCountry.Text;
            if (action == "loadAllMember")
            {
                mems = memberRepository.GetMembers();
            }
            if (action == "SearchByIDandName")
            {
                
                if (name == "" || id == "")
                {
                    MessageBox.Show("Please input name to search by ID and Name!");
                }
                else
                {
                    int searchId = int.Parse(id);
                    mems = memberRepository.GetMembersByNameAndId(name, searchId);
                }
            }
            if (action == "FilterCountryAndCity")
            {

                if (city == "" && country == "")
                {
                    MessageBox.Show("Please input to filter by city and country!");
                }
                else
                {
                    mems = memberRepository.GetMembersByCityAndCountry(city, country);
                }
            }

            try
            {
                source = new BindingSource();
                source.DataSource = mems;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtMemberEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtMemberCity.DataBindings.Clear();
                txtMemberCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtMemberEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtMemberCity.DataBindings.Add("Text", source, "City");
                txtMemberCountry.DataBindings.Add("Text", source, "Country");

                MemberDataTable.DataSource = null;
                MemberDataTable.DataSource = source;
                if (mems.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else if(loggedInUser.isAdmin)
                {
                    btnDelete.Enabled = true;
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Member List");
            }
        }




        //-----------------------------------------------------
        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            MemberDataTable.CellDoubleClick += MemberDataTable_CellContentClick;
        }

        private void MemberDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmMemberDetail frmMemberDetail = new FrmMemberDetail
            {
                Text = "Update Member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if(frmMemberDetail.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList("loadAllMember");
                source.Position = source.Count - 1;
                MemberDataTable.AllowUserToAddRows = false;
            }
        }

        

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList("loadAllMember");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (loggedInUser.isAdmin) { 
                FrmMemberDetail frmMemberDetail = new FrmMemberDetail
                {
                    Text = "Add Member",
                    InsertOrUpdate = false,
                    MemberRepository = memberRepository,
                };
                if (frmMemberDetail.ShowDialog() == DialogResult.OK)
                {
                    LoadMemberList("loadAllMember");
                    source.Position = source.Count - 1;
                }
            }
            else
            {
                MessageBox.Show("You must be admin to create new Nember");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var member = GetMemberObject();
                memberRepository.RemoveMember(member.MemberID);
                LoadMemberList("loadAllMember");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMemberList("SearchByIDandName");
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadMemberList("FilterCountryAndCity");
        }
    }
}
