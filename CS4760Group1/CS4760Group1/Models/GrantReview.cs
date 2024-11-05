namespace CS4760Group1.Models
{
    public class GrantReview
    {
        public int ID { get; set; }
        public int GrantId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public float ReviewScore { get; set; }
    }
}