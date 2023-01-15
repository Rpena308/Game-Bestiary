using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IUserManager
    {
        UserAccount LoginUser(string email, string password);
        string HashSHA256(string source);
        bool AuthenticateUser(string email, string passwordHash); 
        UserAccount GetUserAccountByEmail(string email);
        List<string> GetRolesForUserAccount(int employeeID);
        bool ResetPassword(string email, string oldPassword, string newPassword);
        UserAccount GetUserByUserAccountID(int userAccountID);
        bool EditUser(UserAccount oldUserAccount, UserAccount newUserAccount);
        bool CreateUser(UserAccount userAccount);
        List<UserAccount> GetAllUserAccounts();
    }
}
