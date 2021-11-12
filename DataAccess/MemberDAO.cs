using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class MemberDAO
    {
        private static List<MemberObject> MemberList = new List<MemberObject>(){
            new MemberObject { MemberID = 1, MemberName = "Dong Quan", Email = "dongquan@fstore.com",
                Password = "1234", City = "VungTau", Country = "VietNam" },

            new MemberObject { MemberID = 2, MemberName = "Quang Ky", Email = "tackedev@fstore.com",
                Password = "1234", City = "HoChiMinh", Country = "VietNam" },

            new MemberObject { MemberID = 3, MemberName = "Phuoc Thanh", Email = "megastart@fstore.com",
                Password = "1234", City = "TayNinh", Country = "VietNam" },

            new MemberObject { MemberID = 4, MemberName = "Xuan Dat", Email = "xuandat@fstore.com",
                Password = "1234", City = "HoChiMinh", Country = "VietNam" },

            new MemberObject { MemberID = 5, MemberName = "Tam Pham", Email = "thanhtam@fstore.com",
                Password = "1234", City = "SanJose", Country = "America" },

            };

        //------------------------------
        //Using singleton pattern
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object(); // process a problem that
                                                                    // access object many time at one time
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        //View all member
        public List<MemberObject> GetMemberList => MemberList;

        //View member by MemberID
       public MemberObject GetMemberByMemberID(int memberId)
        {
            MemberObject member = MemberList.SingleOrDefault(mem => mem.MemberID == memberId);
            return member;
        }
        //--------------------------------------------------------
        //Add new member
        public void AddMember(MemberObject member)
        {
            MemberObject mem = GetMemberByMemberID(member.MemberID);
            if (mem == null)
            {
                MemberList.Add(member);
            }
            else
            {
                throw new Exception("Member ID is already exists");
            }

        }

        //----------------------------------------------------------
        //Modifying-update a member
        public void UpdateMember(MemberObject member)
        {
            MemberObject mem = GetMemberByMemberID(member.MemberID);
            if (mem != null)
            {
                var index = MemberList.IndexOf(mem);
                MemberList[index] = mem;
            }
            else
            {
                throw new Exception("Member does not exist");
            }
        }

        //-----------------------------------------------------------
        //Remove Member
        public void RemoveMember(int MemberId)
        {
            MemberObject mem = GetMemberByMemberID(MemberId);
            if (mem != null)
            {
                MemberList.Remove(mem);
            }
            else
            {
                throw new Exception("Member does not exists");
            }
        }
        //--------------------------------------------------------------


        public MemberObject CheckUser(string email, string password)
        {
            foreach (MemberObject mem in MemberList)
            {
                if (mem.Email == email && mem.Password == password)
                {
                    return mem;

                }

            }
            return null;

        }

        //--------------------------------------------------------------
        public List<MemberObject> SearchByNameAndId(string name, int id)
        {
            List<MemberObject> mems = new List<MemberObject>();
            MemberObject member = GetMemberByMemberID(id);
            if (member.MemberName == name)
            {
                mems.Add(member);
            }
            return mems;
        }

        public MemberObject GetMemberByMemberCityAndCountry(string city, string country)
        {
            MemberObject member = MemberList.SingleOrDefault(mem => mem.City == city || mem.Country == country);
            return member;
        }

        public List<MemberObject> SearchByCityAndCountry(string city, string country)
        {
            List<MemberObject> mems = new List<MemberObject>();
            MemberObject member = GetMemberByMemberCityAndCountry(city, country);
            if (member.City == city.Trim() || member.Country == country.Trim()) 
            {
                mems.Add(member);
            }
            return mems;
        }
    }
}
