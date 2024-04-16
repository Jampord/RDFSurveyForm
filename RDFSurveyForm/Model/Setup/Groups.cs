namespace RDFSurveyForm.Model.Setup
{
    public class Groups
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public string UpdatedBy { get; set; }
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public ICollection<GroupSurvey> GroupSurvey { get; set; }
        public ICollection<User> User { get; set; }
    }
}
