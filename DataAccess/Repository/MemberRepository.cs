using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void AddMember(MemberObject member) => MemberDAO.Instance.AddMember(member);

        public MemberObject CheckUser(string email, string password) =>  MemberDAO.Instance.CheckUser(email, password);


        public MemberObject GetMemberByMemberId(int MemberId) => MemberDAO.Instance.GetMemberByMemberID(MemberId);


        public IEnumerable<MemberObject> GetMembers() => MemberDAO.Instance.GetMemberList;

        public IEnumerable<MemberObject> GetMembersByCityAndCountry(string city, string country) => MemberDAO.Instance.SearchByCityAndCountry(city, country);

        public IEnumerable<MemberObject> GetMembersByNameAndId(string name, int id) => MemberDAO.Instance.SearchByNameAndId(name, id);

        public void RemoveMember(int MemberId) => MemberDAO.Instance.RemoveMember(MemberId);


        public void UpdateMember(MemberObject member) => MemberDAO.Instance.UpdateMember(member);

    }
}
