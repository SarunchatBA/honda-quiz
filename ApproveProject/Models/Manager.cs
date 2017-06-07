using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApproveProject.Models
{
    class Manager : ManagementPerson
    {
        public override bool Approve(int amount)
        {
            if (amount <= CreditLine && (Name.IndexOf("Manager") != -1))
            {
                return true;
            }
            else if (successor != null)
            {
                return successor.Approve(amount);
            }
            return false;
        }
    }
}