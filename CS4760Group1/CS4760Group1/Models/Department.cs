using System.ComponentModel.DataAnnotations;
namespace CS4760Group1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Dean { get; set; }
        public string? Location { get; set; }
        public string? College { get; set; }
    }
}
