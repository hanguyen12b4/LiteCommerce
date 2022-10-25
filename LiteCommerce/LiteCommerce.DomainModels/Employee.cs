using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public String Photo { get; set; }
        public String Notes { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

}
}
