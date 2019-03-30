using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomRservation
{
    static class Validation
    {
        public static bool validateDiscountText(String discount)
        {
            double d;
            if (!Double.TryParse(discount,out d))
            {
                return false;
            }

            if(d > 100 || d < 0)
            {
                return false;
            }

            return true;
        }
        public static bool validateName(String name)
        {
            string firstNamePattern = "^[a-zA-Z][a-zA-Z\\s]+$";
            return Regex.IsMatch(name, firstNamePattern);
        }
        public static bool validatePhoneNo(String phoneNo)
        {
            string phonePattern = "[0-9]{10}";
            return Regex.IsMatch(phoneNo, phonePattern);
        }
      

    }


}
