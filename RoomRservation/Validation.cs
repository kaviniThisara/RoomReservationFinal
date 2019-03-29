using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
