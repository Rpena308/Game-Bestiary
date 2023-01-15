using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessLayer;
using DataObjects;
using DataAccess;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        IUserAccessor _userAccessor;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public bool AuthenticateUser(string email, string passwordHash)
        {
            bool result = false;

            try
            {
                result =
                    (1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(
                        email, passwordHash));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool CreateUser(UserAccount userAccount)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.InsertUser(userAccount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<string> GetRolesForUserAccount(int userAccountID)
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectUserAccountRolesByUserAccountID(userAccountID);
            }
            catch (Exception)
            {
                throw;
            }

            return roles;
        }

        public UserAccount GetUserAccountByEmail(string email)
        {
            UserAccount requestedUserAccount = null;

            try
            {
                requestedUserAccount = _userAccessor.SelectUserByEmail(email);
            }
            catch (Exception)
            {
                throw;
            }
            return requestedUserAccount;
        }

        public UserAccount GetUserByUserAccountID(int userAccountID)
        {
            UserAccount requestedUserAccount = null;

            try
            {
                requestedUserAccount = _userAccessor.SelectUserByUserAccountID(userAccountID);
            }
            catch (Exception)
            {
                throw;
            }
            return requestedUserAccount;
        }

        public string HashSHA256(string source)
        {
            string result = null;

            // create a byte array
            byte[] data;

            using (SHA256 sha256hasher = SHA256.Create())
            {
                // hash the input
                data = sha256hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(source));
            }

            // create a string builder object
            var s = new StringBuilder();

            // loop through the hashed bytes build a string
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            // convert the string builder to a string
            result = s.ToString().ToUpper();

            return result;
        }

        public UserAccount LoginUser(string email, string password)
        {
            UserAccount loggedInUserAccount = null;

            try
            {
                if (email == "" || email == null)
                {
                    throw new ArgumentException("Missing email.");
                }
                if (password == "" || password == null)
                {
                    throw new ArgumentException("Missing password.");
                }
                password = HashSHA256(password);
                if (AuthenticateUser(email, password))
                {
                    loggedInUserAccount = GetUserAccountByEmail(email);
                    loggedInUserAccount.Roles = GetRolesForUserAccount(
                        loggedInUserAccount.UserAccountID);
                }
                else
                {
                    throw new ApplicationException(
                        "Login failed. Please check your credentials.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return loggedInUserAccount;
        }

        public bool ResetPassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            try
            {
                result = (1 == _userAccessor.UpdatePasswordHash(
                    email,
                    HashSHA256(oldPassword),
                    HashSHA256(newPassword)
                    ));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool EditUser(UserAccount oldUserAccount, UserAccount newUserAccount)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.UpdateUser(oldUserAccount, newUserAccount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<UserAccount> GetAllUserAccounts()
        {
            List<UserAccount> userAccounts = null;

            try
            {
                userAccounts = _userAccessor.SelectAllUserAccounts();
            }
            catch (Exception)
            {
                throw;
            }

            return userAccounts;
        }
    }
}
