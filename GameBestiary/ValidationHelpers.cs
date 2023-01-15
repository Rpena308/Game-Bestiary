using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPresentation
{
    // what do you need to do to create extensions methods?
    // those are methods that extend a sealed (or final) class?
    // 1. the class must be public
    // 2. the class must be static
    public static class ValidationHelpers
    {
        // 3. any extension method must be public and static also
        // 4. the return type can be anything,
        // 5. but the first parameter is the type being extended
        // 6. and that parameter has the word 'this' in front of it
        // 7. remember, you can't access private data, only pulic
        //      with an extension method (encapsulation is not broken).
        public static bool isValidPrice(this decimal price, string time)
        {
            bool result = false;

            switch (time)
            {
                case "hour":
                    if (price < 50.0M)
                    {
                        result = true;
                    }
                    break;
                case "day":
                    if (price < 200.0M)
                    {
                        result = true;
                    }
                    break;
            }
            if (price <= 0)
            {
                result = false;
            }

            return result;
        }

        public static bool isValidEmail(this string email)
        {
            bool isValid = false;

            if (email.EndsWith("@beast.com") &&
                email.Length > 12 && email.Length <= 100)
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
