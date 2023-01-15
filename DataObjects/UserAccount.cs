using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class UserAccount
    {
        public int UserAccountID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
        public List<string> Roles { get; set; }
    }
}
