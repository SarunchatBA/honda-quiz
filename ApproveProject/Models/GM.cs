using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApproveProject.Models
{
    class GM : ManagementPerson
    {
        public override bool Approve(int amount)
        {
            bool isRoleGM = (Name.IndexOf("GM") != -1);
            CreditLine = isRoleGM && CreditLine <= 0 ? int.MaxValue : CreditLine;
            if (amount <= CreditLine && isRoleGM)
            {
                return true;
            }
            else if (successor != null)
            {
                successor.Approve(amount);
            }
            return false;
        }
    }
}