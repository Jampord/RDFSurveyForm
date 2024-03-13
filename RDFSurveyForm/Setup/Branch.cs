namespace RDFSurveyForm.Setup
{
    public class Branch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchDescription { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

    }
}
