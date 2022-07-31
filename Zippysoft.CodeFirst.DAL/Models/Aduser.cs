using System.ComponentModel.DataAnnotations;

namespace Zippysoft.CodeFirst.DAL.Models
{
    public class Aduser
    {
        [Key]
        public string Id { get; set; }

        public string DisplayName { get; set; }
            
        public DateTimeOffset? EmployeeHireDate { get; set; }

        public string PhoneNumber { get; set; }

        public string AlternateEmail { get; set; }

        public string PostalCode { get; set; }
    }
}
