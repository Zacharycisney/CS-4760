using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CS4760Group1.Models
{
    public class GrantFile
    {
        [Key]
        public int FileID { get; set; } //Change this in the db to auto increment

        public int GrantID;

        public string? FileName;

        public string? Location;
    }
}
