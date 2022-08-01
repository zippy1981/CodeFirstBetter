using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zippysoft.CodeFirst.DAL.Models
{
    public class Aduser
    {
        [Key]
        [MaxLength(36)]
        [Column(TypeName = "CHAR")]
        public string Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string DisplayName { get; set; }
        
        public DateTimeOffset? EmployeeHireDate { get; set; }

        [MaxLength(21)]
        [Column(TypeName = "CHAR")]
        public string PhoneNumber { get; set; }

        [MaxLength(256)]
        public string AlternateEmail { get; set; }

        public string PostalCode { get; set; }
    }
}
