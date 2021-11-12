using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        IEnumerable<MemberObject> GetMembersByNameAndId(String name, int id);
        IEnumerable<MemberObject> GetMembersByCityAndCountry(String city, string country);
        MemberObject GetMemberByMemberId(int MemberId);
        void AddMember(MemberObject member);
        void UpdateMember(MemberObject member);
        void RemoveMember(int MemberId);
        MemberObject CheckUser(string email, string password);
    }
}
