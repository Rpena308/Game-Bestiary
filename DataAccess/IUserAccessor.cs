using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    public interface IUserAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        UserAccount SelectUserByEmail(string email);
        UserAccount SelectUserByUserAccountID(int userAccountID);
        List<string> SelectUserAccountRolesByUserAccountID(int userAccountID);
        List<UserAccount> SelectAllUserAccounts();
        int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);
        int UpdateUser(UserAccount oldUserAccount, UserAccount newUserAccount);
        int InsertUser(UserAccount userAccount);
    }
}
