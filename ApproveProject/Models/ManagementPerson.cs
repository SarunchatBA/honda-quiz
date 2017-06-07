using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApproveProject.Models
{
    abstract class ManagementPerson
    {
        protected ManagementPerson successor;

        public string Name;
        public decimal CreditLine;
 
        public void SetSuccessor(ManagementPerson successor)
        {
            this.successor = successor;
        }

        public abstract bool Approve(int amount);
    }
}