using System.ComponentModel.DataAnnotations.Schema;

namespace CS4760Group1.Models
{

    public class UserAffiliation
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int CollegeId { get; set; }

        public int DepartmentId { get; set; }
    }


}
