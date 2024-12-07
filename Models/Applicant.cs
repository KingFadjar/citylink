namespace RecruitmentApp.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string PositionApplied { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
