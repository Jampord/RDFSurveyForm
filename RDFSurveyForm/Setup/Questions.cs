namespace RDFSurveyForm.Setup
{
    public class Questions
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
