using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserAccessor : IUserAccessor
    {
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_authenticate_user";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int InsertUser(UserAccount userAccount)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_UserAccount";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@GivenName"].Value = userAccount.GivenName;

            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@FamilyName"].Value = userAccount.FamilyName;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters["@Email"].Value = userAccount.Email;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public List<UserAccount> SelectAllUserAccounts()
        {
            List<UserAccount> userAccounts = new List<UserAccount>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_UserAccounts";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userAccounts.Add(new UserAccount()
                        {
                            UserAccountID = reader.GetInt32(0),
                            GivenName = reader.GetString(1),
                            FamilyName = reader.GetString(2),
                            Email = reader.GetString(3),
                            Active = reader.GetBoolean(4)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userAccounts;
        }

        public List<string> SelectUserAccountRolesByUserAccountID(int UserAccountID)
        {
            var roles = new List<string>();

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_select_UserAccountRoles_by_UserAccountID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserAccountID", SqlDbType.Int);

            cmd.Parameters["@UserAccountID"].Value = UserAccountID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return roles;
        }

        public UserAccount SelectUserByEmail(string email)
        {
            UserAccount userAccount = null;

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_select_user_by_email";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    userAccount = new UserAccount()
                    {
                        UserAccountID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Active = reader.GetBoolean(4)
                    };
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userAccount;
        }

        public UserAccount SelectUserByUserAccountID(int userAccountID)
        {
            UserAccount userAccount = null;

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_select_user_by_userAccountID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserAccountID", SqlDbType.Int);

            cmd.Parameters["@UserAccountID"].Value = userAccountID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    userAccount = new UserAccount()
                    {
                        UserAccountID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        FamilyName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Active = reader.GetBoolean(4)
                    };
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userAccount;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();

            string cmdText = "sp_update_passwordHash";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public int UpdateUser(UserAccount oldUserAccount, UserAccount newUserAccount)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_UserAccount";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserAccountID", oldUserAccount.UserAccountID);
            cmd.Parameters.AddWithValue("@OldGivenName", oldUserAccount.GivenName);
            cmd.Parameters.AddWithValue("@OldFamilyName", oldUserAccount.FamilyName);
            cmd.Parameters.AddWithValue("@OldEmail", oldUserAccount.Email);
            cmd.Parameters.AddWithValue("@OldActive", oldUserAccount.Active);

            cmd.Parameters.Add("@NewGivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewGivenName"].Value = newUserAccount.GivenName;

            cmd.Parameters.Add("@NewFamilyName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewFamilyName"].Value = newUserAccount.FamilyName;

            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters["@NewEmail"].Value = newUserAccount.Email;

            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newUserAccount.Active;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }
    }
}
